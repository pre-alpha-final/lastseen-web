import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { VoidResponse, AuthService } from './auth.service';

@Component({
  templateUrl: './forgotpassword.component.html'
})
export class ForgotPasswordComponent {
  forgotPasswordResponse: VoidResponse;
  form = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]]
  });

  constructor(private formBuilder: FormBuilder, private authService: AuthService) { }

  onSubmit() {
    if (this.form.valid === false) {
      return;
    }
    this.authService.forgotPassword(this.form.controls.email.value).subscribe(
      e => this.forgotPasswordResponse = e);
  }
}
