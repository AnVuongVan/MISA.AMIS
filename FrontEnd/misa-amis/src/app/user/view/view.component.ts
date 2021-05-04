import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { User } from 'src/app/shared/user';
import { UserService } from '../../shared/user.service';

@Component({
	selector: 'app-view-user',
	templateUrl: './view.component.html',
	styleUrls: ['./view.component.css']
})

export class ViewUserComponent implements OnInit {

	@Input() positionId;

	listUsers: User [];

	userId: string;

	removeDialog: boolean = false;

	@Output() closedView = new EventEmitter<boolean>();

	@Output() closedDialog = new EventEmitter<boolean>();

	constructor(public service: UserService) { }

	ngOnInit(): void {
		this.listUsers = this.service.getUsersByPositionId(this.positionId);
	}

	editUser(user: User): void {
		this.service.formData = Object.assign({}, user);
		this.closeView(false);
		this.closeDialog(true);
	}

	removeUser(id: string): void {
		this.removeDialog = !this.removeDialog;
		this.userId = id;
		this.listUsers = this.listUsers.filter(user => user.userId !== id);
	}

	closeView(isClosed: boolean): void {
		this.closedView.emit(isClosed);		
	}

	closeDialog(isClosed: boolean): void {
		this.closedDialog.emit(isClosed);		
	}

	onCloseRemoveDialog(isClosed: boolean): void {
		this.removeDialog = isClosed;
	}
}
