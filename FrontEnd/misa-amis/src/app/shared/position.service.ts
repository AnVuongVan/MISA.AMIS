import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Position } from './position';
import { Observable, Subject } from 'rxjs';
import { TreeviewItem } from 'ngx-treeview';
import {tap} from 'rxjs/operators';

@Injectable({
	providedIn: 'root'
})

export class PositionService {

	constructor(private http: HttpClient) { }

	private readonly PositionURI = 'https://localhost:44317/api/v1/positions';

	// Trạng thái form thêm, sửa vị trí, chức vụ
	editDialog: boolean = false;

	// Trạng thái form xóa vị trí, chức vụ
	removeDialog: boolean = false;

	// Biến lưu cây phân cấp
	items: TreeviewItem[];

	formData: Position = new Position();

	private _refreshTree = new Subject<void>();

	get refreshTrees() {
		return this._refreshTree;
	}

	/** Lấy lên cây phân cấp theo người dùng hiện tại */
	getTreePositions(): TreeviewItem[] {
		var childrenCategory = new TreeviewItem({text: '', value: 0,  
		collapsed: true, children: [{ text: '', value: 0 }] });
		
		this.http.get(this.PositionURI + '/child') 
		    .toPromise()
			.then(response => {
				childrenCategory.text = response['result'].text;
				childrenCategory.value = response['result'].value;
				childrenCategory.children = response['result'].children;
			})
			.catch(err => {
				console.log(err);
			});	
		
		return [childrenCategory];
	}

	/** Lấy tất cả vị trí, chức vụ */
	fetchPositions(): Position[] {
		var positions: Position[] = []; 
		this.http.get<any[]>(this.PositionURI)
			.toPromise()
			.then(res => {
				res.forEach(position => positions.push(position));
			});
		return positions;
	}

	/** Lấy chi tiết vị trí, chức vụ */
	getPositionById(id: string): Observable<Position> {
		return this.http.get<Position>(`${this.PositionURI}/${id}`);
	}

	/** Thêm vị trí, chức vụ */
	postPosition(): Observable<Position> {
		return this.http.post<Position>(this.PositionURI, this.formData)
		.pipe(
			tap(() =>  {
			  this._refreshTree.next();
			})
		);
	}

	/** Chỉnh sửa vị trí, chức vụ */
	putPosition(): Observable<Position> {
		return this.http.put<Position>(`${this.PositionURI}/${this.formData.positionId}`, this.formData)
		.pipe(
			tap(() =>  {
			  this._refreshTree.next();
			})
		);
	}

	/** Xóa vị trí, chức vụ */
	deletePosition(id: string) {
		return this.http.delete(`${this.PositionURI}/${id}`);
	}
}
