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

	// Trạng thái form xem danh sách người dùng
	viewDialog: boolean = false;

	// Trạng thái form thêm, sửa người dùng
	editDialog: boolean = false;

	// Trạng thái form xóa người dùng
	removeDialog: boolean = false;

	formData: User = new User();

	/** Gửi yêu cầu đăng nhập */
	login(formData) {
		return this.http.post(this.UserURI + '/login', formData);
	}

	/** Lấy danh sách người dùng theo vị trí, chức vụ */
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

	/** Lấy thông tin chi tiết người dùng */
	getUserById(id: string) {
		return this.http.get(`${this.UserURI}/${id}`);
	}

	/** Thêm người dùng */
	postUser() {
		return this.http.post(this.UserURI, this.formData);
	}

	/** Chỉnh sửa người dùng */
	putUser() {
		return this.http.put(`${this.UserURI}/${this.formData.userId}`, this.formData);
	}

	/** Xóa người dùng */
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

	/** Lấy ra quyền của người dùng */
	getRoleName(): string {
		let payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
		return payLoad.role;
	}

	/** Kiểm tra người dùng có quyền vào trang nào đó hay không */
	roleMatch(allowedRoles): boolean {
		var isMatch = false;

		// Lấy ra quyền của người dùng
		var userRole = this.getRoleName();

		// Kiểm tra quyền đó với các quyền được khai báo trong route
		allowedRoles.forEach(element => {
			if (userRole == element) {
				isMatch = true;
				return;
			}
		});

		return isMatch;
	}
}