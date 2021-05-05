import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/user.service';
import { Router } from '@angular/router';
import { TreeviewItem, TreeviewConfig } from 'ngx-treeview';
import { PositionService } from '../shared/position.service';
import { Position } from '../shared/position';

@Component({
	selector: 'app-home',
	templateUrl: './home.component.html',
	styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
	positionId: string;
	item: TreeviewItem;

	config = TreeviewConfig.create({		
		decoupleChildFromParent: false,
		maxHeight: 400
	});
  
	constructor(public userService: UserService, public positionService: PositionService, 
		private router: Router) { }
		
	ngOnInit(): void {
		this.positionService.refreshTrees
		.subscribe(() => {
			this.getTreePositions();
		});
		this.getTreePositions();
	}

	private getTreePositions() {
		this.positionService.items = this.positionService.getTreePositions();
	}

	/** Thêm vị trí, chức vụ */
	addItem() {
		this.positionService.formData = new Position();
		this.positionService.editDialog = !this.positionService.editDialog;
	}

	/** Xem danh sách người dùng */
	viewUser(id: string): void {
		this.positionId = id;	
		this.userService.viewDialog = !this.userService.viewDialog;
	}

	/** Chỉnh sửa vị trí, chức vụ */
	editItem(item: TreeviewItem): void {
		this.positionService.editDialog = !this.positionService.editDialog;
		this.positionService.getPositionById(item.value).subscribe(
			res => this.positionService.formData = Object.assign({}, res)
		)
	}
	
	/** Xóa vị trí, chức vụ */
	removeItem(item: TreeviewItem): void {
		this.positionService.removeDialog = !this.positionService.removeDialog;
		this.item = item;
	}

	onLogout() {
		localStorage.removeItem('token');
		this.router.navigate(['/login']);
	}

	ngAfterViewInit() {
		(document.querySelector('.form-check-inline') as HTMLElement).style.display = 'none';
		(document.querySelector('.dropdown-divider') as HTMLElement).style.display = 'none';
	}
}