import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { UserService } from '../../shared/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
	selector: 'app-remove-user',
	templateUrl: './remove.component.html',
	styleUrls: ['./remove.component.css']
})

export class RemoveUserComponent implements OnInit {

	@Input() userId;

	userName: string;

	@Output() closedRemoveDialog = new EventEmitter<boolean>();

	constructor(private service: UserService, private toastr: ToastrService) { }

	ngOnInit(): void {
		this.service.getUserById(this.userId).subscribe(
			res => this.userName = res['userName']
		);
	}

	removeUser() {
		this.service.deleteUser(this.userId).subscribe(
			res => console.log(res),
			err => console.log(err)
		)
		this.closeRemoveDialog(false);
		this.toastr.success('Xóa người dùng thành công', 'Thông báo');
	}

	closeRemoveDialog(isClosed: boolean): void {
		this.closedRemoveDialog.emit(isClosed);
	}
}
