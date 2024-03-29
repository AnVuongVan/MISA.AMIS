import { Component, OnInit, Output, EventEmitter, ElementRef, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { PositionService } from '../../shared/position.service';
import { ToastrService } from 'ngx-toastr';
import { Position } from 'src/app/shared/position';

@Component({
	selector: 'app-edit-position',
	templateUrl: './edit.component.html',
	styleUrls: ['./edit.component.css']
})

export class EditPositionComponent implements OnInit {

	@Output() closedDialog = new EventEmitter<boolean>();

	@ViewChild('autoFocus', {static: false}) inputEl: ElementRef;

	positions: Position[];

	constructor(public service: PositionService, private toastr: ToastrService) { }

	ngOnInit(): void {
		this.positions = this.service.fetchPositions();
	}

	/** Xử lý thêm, sửa vị trí, chức vụ */
	onSubmit(form: NgForm): void {
		if (this.service.formData.positionId) {
			this.updatePosition(form);
			this.toastr.success('Cập nhật chức vụ thành công', 'Thông báo');
		} else {
			this.insertPosition(form);
			this.toastr.success('Thêm chức vụ thành công', 'Thông báo');
		}
		this.closeDialog();
	}

	/** Thêm chức vụ, vị trí */
	insertPosition(form: NgForm) {
		this.service.postPosition().subscribe(
			res => {
				this.resetForm(form);
				console.log(res);
			},
			err => {
				console.log(err);
			}
		);
	}

	/** Chỉnh sửa chức vụ, vị trí */
	updatePosition(form: NgForm) {
		this.service.putPosition().subscribe(
			res => {
				this.resetForm(form);
				console.log(res);
			},
			err => {
				console.log(err);
			}
		);
	}

	/** Đóng form thêm, sửa */
	closeDialog(): void {
		this.service.editDialog = !this.service.editDialog;
	}

	/** Reset form sau khi thao tác */
	resetForm(form: NgForm) {
		form.reset();
		this.service.formData = new Position();
	}

	ngAfterViewInit() {
		setTimeout(() => this.inputEl.nativeElement.focus());
	}

}
