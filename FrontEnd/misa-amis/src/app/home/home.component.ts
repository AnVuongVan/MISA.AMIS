import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../shared/user.service';
import { Router } from '@angular/router';
import { TreeviewItem, TreeviewComponent, TreeviewConfig } from 'ngx-treeview';
import { PositionService } from '../shared/position.service';
import { Position } from '../shared/position';

@Component({
	selector: 'app-home',
	templateUrl: './home.component.html',
	styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
	/*items: TreeviewItem[];
	@ViewChild(TreeviewComponent, { static: false }) treeviewComponent: TreeviewComponent;*/
	positionId: string;
	item: TreeviewItem;

	config = TreeviewConfig.create({		
		decoupleChildFromParent: false,
		maxHeight: 400
	});
  
	constructor(public userService: UserService, public positionService: PositionService, 
		private router: Router) { }
		
	ngOnInit(): void {
		this.positionService.items = this.positionService.getTreePositions();
	}

	addItem() {
		this.positionService.formData = new Position();
		this.positionService.editDialog = !this.positionService.editDialog;
	}

	viewUser(id: string): void {
		this.positionId = id;	
		this.userService.viewDialog = !this.userService.viewDialog;
	}

	editItem(item: TreeviewItem): void {
		this.positionService.editDialog = !this.positionService.editDialog;
		this.positionService.getPositionById(item.value).subscribe(
			res => this.positionService.formData = Object.assign({}, res)
		)
	}
	
	removeItem(item: TreeviewItem): void {
		this.positionService.removeDialog = !this.positionService.removeDialog;
		//this.positionService.formData.positionId = item.value;
		this.item = item;
		
		/*if (this.positionService.isRemoved) {
			for (const tmpItem of this.items) {
				if (tmpItem === item) {
					remove(this.items, item);
				} else {
					if (TreeviewHelper.removeItem(tmpItem, item)) {
						break;
					}
				}
			}
			this.treeviewComponent.raiseSelectedChange();
		}*/
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