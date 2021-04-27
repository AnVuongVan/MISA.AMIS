import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";

@Injectable({
	providedIn: 'root'
})

export class UserService {

	constructor(private http: HttpClient) { }

	private readonly BaseURI = 'https://localhost:44317/api/v1/users';

	login(formData) {
		return this.http.post(this.BaseURI + '/login', formData);
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