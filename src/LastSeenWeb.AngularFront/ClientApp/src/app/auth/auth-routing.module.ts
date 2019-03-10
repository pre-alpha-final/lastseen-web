import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login.component';
import { EmailConfirmationComponent } from './email-confirmation.component';
import { ForgotPasswordComponent } from './forgot-password.component';
import { RegisterComponent } from './register.component';

const routes: Routes = [
  { path: 'auth/register', component: RegisterComponent },
  { path: 'auth/login', component: LoginComponent },
  { path: 'auth/emailconfirmation', component: EmailConfirmationComponent },
  { path: 'auth/forgotpassword', component: ForgotPasswordComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
