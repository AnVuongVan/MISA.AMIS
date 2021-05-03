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
import { ViewComponent } from './user/view/view.component';
import { RemoveComponent } from './user/remove/remove.component';
import { EditComponent } from './user/edit/edit.component';

@NgModule({
	declarations: [
		AppComponent,
		LoginComponent,
		HomeComponent,
        ForbiddenComponent,
        ViewComponent,
        RemoveComponent,
        EditComponent
	],
	imports: [
		BrowserModule,
		AppRoutingModule,
		FormsModule,
		HttpClientModule,
		TreeviewModule.forRoot()
	],
	providers: [UserService, {
		provide: HTTP_INTERCEPTORS,
		useClass: AuthInterceptor,
		multi: true
	}],
	bootstrap: [AppComponent]
})
export class AppModule { }
