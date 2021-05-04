import { Component, OnInit, Output, EventEmitter, ElementRef, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { PositionService } from '../../shared/position.service';
import { ToastrService } from 'ngx-toastr';
import { Position } from 'src/app/shared/position';

@Component({
	selector: 'app-edit-position',
	templateUrl: './edit.component.html',
	styleUrls: ['./edit.component.css']
})

export class EditPositionComponent implements OnInit {

	@Output() closedDialog = new EventEmitter<boolean>();

	@ViewChild('autoFocus', {static: false}) inputEl: ElementRef;

	constructor(public service: PositionService, private toastr: ToastrService) { }

	ngOnInit(): void {
	}

	onSubmit(form: NgForm): void {
		if (this.service.formData.positionId) {
			this.updatePosition(form);
			this.toastr.success('Cập nhật chức vụ thành công', 'Thông báo');
		} else {
			this.insertPosition(form);
			this.toastr.success('Thêm chức vụ thành công', 'Thông báo');
		}
		this.closeDialog(false);
	}

	insertPosition(form: NgForm) {
		this.service.postPosition().subscribe(
			res => {
				this.resetForm(form);
				console.log(res);
			},
			err => {
				console.log(err);
			}
		);
	}

	updatePosition(form: NgForm) {
		this.service.putPosition().subscribe(
			res => {
				this.resetForm(form);
				console.log(res);
			},
			err => {
				console.log(err);
			}
		);
	}

	closeDialog(isClosed: boolean): void {
		this.closedDialog.emit(isClosed);
	}

	resetForm(form: NgForm) {
		form.reset();
		this.service.formData = new Position();
	}

	ngAfterViewInit() {
		setTimeout(() => this.inputEl.nativeElement.focus());
	}

}
