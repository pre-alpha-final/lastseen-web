import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ErrorType } from '../shared/errortype';
import { AuthApiCallsService } from './auth-apicalls.service';

@Component({
  templateUrl: './forgotpassword.component.html'
})
export class ForgotPasswordComponent {
  forgotPasswordResponse: Object | ErrorType;
  form = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]]
  });

  constructor(private formBuilder: FormBuilder, private authApiCallsService: AuthApiCallsService) { }

  onSubmit() {
    if (this.form.valid === false) {
      return;
    }
    this.authApiCallsService.forgotPassword(this.form.controls.email.value).subscribe(
      e => this.forgotPasswordResponse = e || {});
  }
}
