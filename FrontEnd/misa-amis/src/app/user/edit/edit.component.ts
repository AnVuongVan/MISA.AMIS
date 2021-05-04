import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { User } from '../../shared/user';
import { UserService } from '../../shared/user.service';
import { ToastrService } from 'ngx-toastr';
import { Position } from '../../shared/position';

@Component({
	selector: 'app-edit-user',
	templateUrl: './edit.component.html',
	styleUrls: ['./edit.component.css']
})

export class EditUserComponent implements OnInit {

	@ViewChild('autoFocus', {static: false}) inputEl: ElementRef;

	positions: Position[];

	constructor(public service: UserService, private toastr: ToastrService) { }

	ngOnInit(): void { 
		this.positions = this.service.fetchPositions();
	}

	onSubmit(form: NgForm): void {
		if (this.service.formData.userId) {
			this.updateUser(form);
			this.toastr.success('Cập nhật thông tin thành công', 'Thông báo');
		} else {
			this.insertUser(form);
			this.toastr.success('Thêm người dùng thành công', 'Thông báo');
		}
		this.closeDialog();
		this.service.viewDialog = !this.service.viewDialog;
	}

	insertUser(form: NgForm) {
		this.service.postUser().subscribe(
			res => {
				this.resetForm(form);
				console.log(res);
			},
			err => {
				console.log(err);
			}
		);
	}

	updateUser(form: NgForm) {
		this.service.putUser().subscribe(
			res => {
				this.resetForm(form);
				console.log(res);
			},
			err => {
				console.log(err);
			}
		);
	}

	resetForm(form: NgForm) {
		form.reset();
		this.service.formData = new User();
	}

	closeDialog(): void {
		this.service.editDialog = !this.service.editDialog;		
	}

	ngAfterViewInit() {
		setTimeout(() => this.inputEl.nativeElement.focus());
	}
}
