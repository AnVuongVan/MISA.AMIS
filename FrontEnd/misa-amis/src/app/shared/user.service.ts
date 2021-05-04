import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { TreeviewItem } from 'ngx-treeview';
import { User } from './user';
import { Position } from './position';

@Injectable({
	providedIn: 'root'
})

export class UserService {

	constructor(private http: HttpClient) { }

	private readonly UserURI = 'https://localhost:44317/api/v1/users';

	private readonly PositionURI = 'https://localhost:44317/api/v1/positions';

	formData: User = new User();

	listPositions: Position[];

	login(formData) {
		return this.http.post(this.UserURI + '/login', formData);
	}

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

	fetchPositions() {
		this.http.get(this.PositionURI)
			.toPromise()
			.then(res => this.listPositions = res as Position[]);
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