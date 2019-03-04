import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login.component';
import { EmailConfirmationComponent } from './emailconfirmation.component';
import { ForgotpasswordComponent } from './forgotpassword.component';

const routes: Routes = [
  { path: 'auth/login', component: LoginComponent },
  { path: 'auth/emailconfirmation', component: EmailConfirmationComponent },
  { path: 'auth/forgotpassword', component: ForgotpasswordComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
