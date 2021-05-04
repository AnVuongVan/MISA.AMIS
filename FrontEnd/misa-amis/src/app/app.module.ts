import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { FormsModule } from '@angular/forms';
import { TreeviewModule } from 'ngx-treeview';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { UserService } from './shared/user.service';
import { AuthInterceptor } from './auth/auth.interceptor';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { ViewUserComponent } from './user/view/view.component';
import { RemoveUserComponent } from './user/remove/remove.component';
import { EditUserComponent } from './user/edit/edit.component';
import { RemovePositionComponent } from './position/remove/remove.component';
import { EditPositionComponent } from './position/edit/edit.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { NgxLoadingModule } from 'ngx-loading';

@NgModule({
	declarations: [
		AppComponent,
		LoginComponent,
		HomeComponent,
        ForbiddenComponent,
        ViewUserComponent,
        RemoveUserComponent,
        EditUserComponent,
		RemovePositionComponent,
        EditPositionComponent
	],
	imports: [
		BrowserModule,
		AppRoutingModule,
		FormsModule,
		HttpClientModule,
		TreeviewModule.forRoot(),
		BrowserAnimationsModule, 
        ToastrModule.forRoot({
			timeOut: 3000,
			positionClass: 'toast-bottom-right',
			preventDuplicates: true,
		}),
		NgxLoadingModule.forRoot({})
	],
	providers: [UserService, {
		provide: HTTP_INTERCEPTORS,
		useClass: AuthInterceptor,
		multi: true
	}],
	bootstrap: [AppComponent]
})
export class AppModule { }
