import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Position } from './position';

@Injectable({
	providedIn: 'root'
})

export class PositionService {

	constructor(private http: HttpClient) { }

	private readonly PositionURI = 'https://localhost:44317/api/v1/positions';

	formData: Position = new Position();

	fetchPositions(): Position[] {
		var positions: Position[] = []; 
		this.http.get<any[]>(this.PositionURI)
			.toPromise()
			.then(res => {
				res.forEach(position => positions.push(position));
			});
		return positions;
	}

	getPositionById(id: string) {
		return this.http.get(`${this.PositionURI}/${id}`);
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
