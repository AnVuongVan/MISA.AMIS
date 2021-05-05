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

	// Biến lưu danh sách người dùng theo vị trí
	listUsers: User [];

	userId: string;

	constructor(public service: UserService) { }

	ngOnInit(): void {
		this.listUsers = this.service.getUsersByPositionId(this.positionId);
	}

	/** Thêm người dùng */
	addUser(id: string): void {
		this.service.formData = new User();
		this.service.formData.positionId = id;
		this.service.editDialog = !this.service.editDialog;
	}

	/** Chỉnh sửa người dùng */
	editUser(user: User): void {
		this.service.formData = Object.assign({}, user);
		this.service.editDialog = !this.service.editDialog;
	}

	/** Xóa người dùng */
	removeUser(id: string): void {
		this.service.removeDialog = !this.service.removeDialog;
		this.userId = id;
	}

	/** Đóng form xem danh sách người dùng */
	closeView(): void {
		this.service.viewDialog = !this.service.viewDialog;		
	}
}
