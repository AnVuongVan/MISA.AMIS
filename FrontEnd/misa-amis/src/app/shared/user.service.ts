import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { User } from './user';
import { Position } from './position';

@Injectable({
	providedIn: 'root'
})

export class UserService {

	constructor(private http: HttpClient) { }

	private readonly UserURI = 'https://localhost:44317/api/v1/users';

	private readonly PositionURI = 'https://localhost:44317/api/v1/positions';

	viewDialog: boolean = false;

	editDialog: boolean = false;

	removeDialog: boolean = false;

	formData: User = new User();

	login(formData) {
		return this.http.post(this.UserURI + '/login', formData);
	}

	getUsersByPositionId(id: string): User[] {
		var users: User[] = [];  
		let params = new HttpParams().set('positionId', id);
		this.http.get<any[]>(`${this.UserURI}/position`, { params: params })
			.toPromise()
			.then(res => {
				res.forEach(user => users.push(user));
			})
		return users;
	}

	getUserById(id: string) {
		return this.http.get(`${this.UserURI}/${id}`);
	}

	postUser() {
		return this.http.post(this.UserURI, this.formData);
	}

	putUser() {
		return this.http.put(`${this.UserURI}/${this.formData.userId}`, this.formData);
	}

	deleteUser(id: string) {
		return this.http.delete(`${this.UserURI}/${id}`);
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

	getRoleName(): string {
		let payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
		return payLoad.role;
	}

	roleMatch(allowedRoles): boolean {
		var isMatch = false;
		var payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
		var userRole = payLoad.role;

		allowedRoles.forEach(element => {
			if (userRole == element) {
				isMatch = true;
				return;
			}
		});

		return isMatch;
	}
}