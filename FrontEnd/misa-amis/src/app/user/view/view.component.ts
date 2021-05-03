import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { User } from 'src/app/shared/user';
import { UserService } from '../../shared/user.service';

@Component({
	selector: 'app-view',
	templateUrl: './view.component.html',
	styleUrls: ['./view.component.css']
})

export class ViewComponent implements OnInit {

	@Input() users;

	listUsers: User [];

	@Output() closedView = new EventEmitter<boolean>();

	@Output() closedDialog = new EventEmitter<boolean>();

	constructor(public service: UserService) { }

	ngOnInit(): void {
		this.listUsers = this.users;
	}

	editUser(user: User): void {
		this.service.formData = Object.assign({}, user);
		this.closeView(false);
		this.closeDialog(true);
	}

	removeUser(id: number): void {
		this.service.deleteUser(id);
	}

	closeView(isClosed: boolean): void {
		this.closedView.emit(isClosed);		
	}

	closeDialog(isClosed: boolean): void {
		this.closedDialog.emit(isClosed);		
	}

}
