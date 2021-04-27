import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { TreeviewItem } from 'ngx-treeview';

@Injectable({
	providedIn: 'root'
})

export class UserService {

	constructor(private http: HttpClient) { }

	private readonly BaseURI = 'https://localhost:44317/api/v1/users';

	login(formData) {
		return this.http.post(this.BaseURI + '/login', formData);
	}

	getUsers(): TreeviewItem[] {
		const childrenCategory = new TreeviewItem({
			text: 'Children', value: 1, collapsed: true, children: [
			{ text: 'Baby 3-5', value: 11 },
			{ text: 'Baby 6-8', value: 12 },
			{ text: 'Baby 9-12', value: 13 }
			]
		});

		const itCategory = new TreeviewItem({
			text: 'IT', value: 9, collapsed: true, children: [
			{
				text: 'Programming', value: 91, collapsed: true, children: [
					{
					text: 'Frontend', value: 911, collapsed: true, children: [
						{ text: 'Angular 1', value: 9111 },
						{ text: 'Angular 2', value: 9112 },
						{ text: 'ReactJS', value: 9113 }
				    ]}, 
					{
					text: 'Backend', value: 912, collapsed: true, children: [
						{ text: 'C#', value: 9121 },
						{ text: 'Java', value: 9122 },
						{ text: 'Python', value: 9123 }
					]}
				]
			},
			{
				text: 'Networking', value: 92, collapsed: true, children: [
					{ text: 'Internet', value: 921 },
					{ text: 'Security', value: 922 }
				]
			}
			]
		});

		const teenCategory = new TreeviewItem({
			text: 'Teen', value: 2, collapsed: true, children: [
				{ text: 'Adventure', value: 21 },
				{ text: 'Science', value: 22 }
			]
		});

		return [childrenCategory, itCategory, teenCategory];
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