import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Position } from './position';
import { Observable } from 'rxjs';
import { TreeviewItem } from 'ngx-treeview';

@Injectable({
	providedIn: 'root'
})

export class PositionService {

	constructor(private http: HttpClient) { }

	private readonly PositionURI = 'https://localhost:44317/api/v1/positions';

	editDialog: boolean = false;

	removeDialog: boolean = false;

	items: TreeviewItem[];

	formData: Position = new Position();

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

	fetchPositions(): Position[] {
		var positions: Position[] = []; 
		this.http.get<any[]>(this.PositionURI)
			.toPromise()
			.then(res => {
				res.forEach(position => positions.push(position));
			});
		return positions;
	}

	getPositionById(id: string): Observable<Position> {
		return this.http.get<Position>(`${this.PositionURI}/${id}`);
	}

	postPosition() {
		return this.http.post(this.PositionURI, this.formData);
	}

	putPosition() {
		return this.http.put(`${this.PositionURI}/${this.formData.positionId}`, this.formData);
	}

	deletePosition(id: string) {
		return this.http.delete(`${this.PositionURI}/${id}`);
	}
}
