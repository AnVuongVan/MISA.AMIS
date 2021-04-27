import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { UserService } from '../shared/user.service';

@Injectable({
	providedIn: 'root'
})

export class AuthGuard implements CanActivate {
	
	constructor(private router: Router, private service: UserService) { }

	canActivate(
		next: ActivatedRouteSnapshot,
		state: RouterStateSnapshot
	): boolean {
		if (localStorage.getItem('token')) {
			let roles = next.data['permittedRoles'] as Array<string>;
			if (roles) {
				if (this.service.roleMatch(roles)) return true;
				
				this.router.navigate(['/forbidden']);
				return false;
			}
		} else {
			this.router.navigate(['/login']);
			return false;
		}
	}

}
