import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService, TokenResponse } from './auth.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  templateUrl: './login.component.html'
})
export class LoginComponent {
  tokenResponse: TokenResponse | HttpErrorResponse;
  form = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required]
  });

  constructor(private formBuilder: FormBuilder, private authService: AuthService, private router: Router) { }

  onSubmit() {
    if (this.form.valid === false) {
      return;
    }
    this.authService.logIn(
      this.form.controls.email.value,
      this.form.controls.password.value
    ).subscribe(e => {
      this.tokenResponse = e;
      if ((<HttpErrorResponse>e).error == null) {
        this.router.navigateByUrl('/');
      }
    });
  }
}
