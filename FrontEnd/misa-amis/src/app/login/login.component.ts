import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../shared/user.service';

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

	formModel = {
		UserName: '',
		Password: ''
	}

	constructor(private service: UserService, private router: Router) { }

	ngOnInit(): void {
		if (localStorage.getItem('token')) {
			this.router.navigateByUrl('/home');
		}
	}

	onSubmit(formLogin: NgForm) {
		this.service.login(formLogin.value).subscribe(
			(res: any) => {
				localStorage.setItem('token', res.token);
				this.router.navigateByUrl('/home');
			},
			err => {
				if (err.status == 400) {
					//show error message
					console.log(err);
				}
			} 
		)
	}
}
