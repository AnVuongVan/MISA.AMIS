import { Component, OnInit, Output, EventEmitter, ElementRef, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { User } from '../shared/user';
import { UserService } from '../shared/user.service';

@Component({
	selector: 'app-user',
	templateUrl: './user.component.html',
	styleUrls: ['./user.component.css']
})

export class UserComponent implements OnInit {

	@Output() closedDialog = new EventEmitter<boolean>();

	@ViewChild('autoFocus', {static: false}) inputEl: ElementRef;

	constructor(public service: UserService) { }

	ngOnInit(): void { 
		this.service.getOffices();
		this.service.getPositions();
	}

	onSubmit(form: NgForm): void {
		if (this.service.formData.userId) {
			this.updateUser(form);
		} else {
			this.insertUser(form);
		}
		this.closeDialog(false);
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

	closeDialog(isClosed: boolean): void {
		this.closedDialog.emit(isClosed);		
	}

	ngAfterViewInit() {
		setTimeout(() => this.inputEl.nativeElement.focus());
	}
}