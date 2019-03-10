import { Component } from '@angular/core';
import { FormBuilder, Validators, FormGroup, ValidationErrors } from '@angular/forms';
import { ErrorType } from '../shared/errortype';
import { AuthApiCallsService } from './auth-apicalls.service';

@Component({
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  registerResponse: Object | ErrorType;
  form = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [
      Validators.required,
      Validators.minLength(8),
      Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[ A-Za-z\\d@$!%*?&]{1,}$')
    ]],
    password2: ['']
  }, { validator: this.passwordMatchValidator });

  constructor(private formBuilder: FormBuilder, private authApiCallsService: AuthApiCallsService) { }

  passwordMatchValidator(group: FormGroup): ValidationErrors {
    const password = group.controls.password.value;
    const password2 = group.controls.password2.value;

    return password === password2
      ? null
      : { passwordsMatch: false };
  }

  onSubmit() {
    if (this.form.valid === false) {
      return;
    }
    this.authApiCallsService.register(
      this.form.controls.email.value,
      this.form.controls.password.value,
      this.form.controls.password2.value
    ).subscribe(e => this.registerResponse = e || {});
  }
}
