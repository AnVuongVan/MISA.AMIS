import { Component, OnInit, Input } from '@angular/core';
import { PositionService } from '../../shared/position.service';
import { remove } from 'lodash';
import { TreeviewHelper } from 'ngx-treeview';
import { ToastrService } from 'ngx-toastr';

@Component({
	selector: 'app-remove-position',
	templateUrl: './remove.component.html',
	styleUrls: ['./remove.component.css']
})
export class RemovePositionComponent implements OnInit {

	@Input() item;

	constructor(private service: PositionService, private toastr: ToastrService) { }

	ngOnInit(): void {
	}

	removePosition() {
		this.service.deletePosition(this.item.value).subscribe(
			res => console.log(res),
			err => console.log(err)
		)
		this.closeDialog();
		this.toastr.success('Xóa chức vụ thành công', 'Thông báo');
		for (const tmpItem of this.service.items) {
			if (tmpItem === this.item) {
				remove(this.service.items, this.item);
			} else {
				if (TreeviewHelper.removeItem(tmpItem, this.item)) {
					break;
				}
			}
		}
	}

	closeDialog(): void {
		this.service.removeDialog = !this.service.removeDialog;
	}

}
