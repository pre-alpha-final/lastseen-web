import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthErrorPipe } from './auth-error.pipe';
import { TokenInterceptor } from './token.interceptor';
import { EmailConfirmationComponent } from './emailconfirmation.component';
import { ForgotPasswordComponent } from './forgotpassword.component';
import { RegisterComponent } from './register.component';

@NgModule({
  declarations: [
    LoginComponent,
    AuthErrorPipe,
    EmailConfirmationComponent,
    ForgotPasswordComponent,
    RegisterComponent
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    ReactiveFormsModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ]
})
export class AuthModule { }
