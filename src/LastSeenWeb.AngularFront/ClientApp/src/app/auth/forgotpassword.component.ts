import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from './auth.service';
import { ErrorType } from '../shared/errortype';

@Component({
  templateUrl: './forgotpassword.component.html'
})
export class ForgotPasswordComponent {
  forgotPasswordResponse: Object | ErrorType;
  form = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]]
  });

  constructor(private formBuilder: FormBuilder, private authService: AuthService) { }

  onSubmit() {
    if (this.form.valid === false) {
      return;
    }
    this.authService.forgotPassword(this.form.controls.email.value).subscribe(
      e => this.forgotPasswordResponse = e || {});
  }
}
