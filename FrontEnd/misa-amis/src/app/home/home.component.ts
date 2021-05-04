import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../shared/user.service';
import { Router } from '@angular/router';
import { isNil, remove, reverse } from 'lodash';
import { TreeviewItem, TreeviewComponent, 
	TreeviewConfig, TreeviewHelper, DownlineTreeviewItem 
} from 'ngx-treeview';
import { User } from '../shared/user';

@Component({
	selector: 'app-home',
	templateUrl: './home.component.html',
	styleUrls: ['./home.component.css'],
	providers: [
		UserService
	]
})
export class HomeComponent implements OnInit {
	//dropdownEnabled = true;
	items: TreeviewItem[];
	rows: string[];
	@ViewChild(TreeviewComponent, { static: false }) treeviewComponent: TreeviewComponent;
	//values: number[];
	positionId: string;
	showDialog: boolean = false;
	viewDialog: boolean = false;

	config = TreeviewConfig.create({		
		decoupleChildFromParent: false,
		maxHeight: 400
	});
  
	constructor(public service: UserService, private router: Router) { }
  
	ngOnInit(): void {
		this.items = this.service.getTreePositions();
	}

	addUser() {
		this.service.formData = new User();
		this.showDialog = !this.showDialog;
	}

	onCloseDialog(isClosed: boolean) {
		this.showDialog = isClosed;
	}

	viewUser(id: string): void {
		this.positionId = id;
		this.viewDialog = !this.viewDialog;
	}

	onCloseView(isClosed: boolean) {
		this.viewDialog = isClosed;
	}
	
	removeItem(item: TreeviewItem): void {
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
	}

	onSelectedChange(downlineItems: DownlineTreeviewItem[]): void {
		this.rows = [];
		downlineItems.forEach(downlineItem => {
			const item = downlineItem.item;
			const value = item.value;
			const texts = [item.text];
			let parent = downlineItem.parent;
			while (!isNil(parent)) {
				texts.push(parent.item.text);
				parent = parent.parent;
			}
			const reverseTexts = reverse(texts);
			const row = `${reverseTexts.join(' -> ')} : ${value}`;
			this.rows.push(row);
		});
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