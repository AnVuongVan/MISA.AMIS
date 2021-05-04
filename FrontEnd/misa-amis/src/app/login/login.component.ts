import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../shared/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

	public loading: boolean = false;

	formModel = {
		UserName: '',
		Password: ''
	}

	constructor(private service: UserService, private router: Router, private toastr: ToastrService) { }

	ngOnInit(): void {
		if (localStorage.getItem('token')) {
			this.router.navigateByUrl('/home');
		}
	}

	onSubmit(formLogin: NgForm) {
		this.loading = true;
		this.service.login(formLogin.value).subscribe(
			(res: any) => {
				this.loading = false;
				localStorage.setItem('token', res.token);
				this.router.navigateByUrl('/home');
			},
			err => {
				this.loading = false;
				if (err.status == 400) {
					this.toastr.error('Tên đăng nhập hoặc mật khẩu không hợp lệ', 'Thông báo');
					console.log(err);
				}
			} 
		)
	}
}
