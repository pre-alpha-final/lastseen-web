import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ForgotPasswordResponse, AuthService } from './auth.service';

@Component({
  templateUrl: './forgotpassword.component.html'
})
export class ForgotpasswordComponent {
  forgotPasswordResponse: ForgotPasswordResponse;
  form = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]]
  });

  constructor(private formBuilder: FormBuilder, private authService: AuthService) { }

  onSubmit() {
    this.authService.forgotPassword(this.form.controls.email.value).subscribe(
      e => this.forgotPasswordResponse = e);
  }
}
