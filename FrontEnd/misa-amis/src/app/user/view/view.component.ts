import { Component, OnInit, Input } from '@angular/core';
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

	constructor(public service: UserService) { }

	ngOnInit(): void {
		this.listUsers = this.service.getUsersByPositionId(this.positionId);
	}

	addUser(id: string): void {
		this.service.formData = new User();
		this.service.formData.positionId = id;
		this.service.editDialog = !this.service.editDialog;
	}

	editUser(user: User): void {
		this.service.formData = Object.assign({}, user);
		this.service.editDialog = !this.service.editDialog;
	}

	removeUser(id: string): void {
		this.service.removeDialog = !this.service.removeDialog;
		this.userId = id;
		//this.listUsers = this.listUsers.filter(user => user.userId !== id);
	}

	closeView(): void {
		this.service.viewDialog = !this.service.viewDialog;		
	}
}
