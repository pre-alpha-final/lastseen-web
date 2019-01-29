import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';

@Component({
  templateUrl: './login.component.html'
})
export class LoginComponent {
  loginError: string;
  form = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [
      Validators.required,
      // Validators.minLength(8),
      // Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{1,}$')
    ]]
  });

  constructor(private formBuilder: FormBuilder, private authService: AuthService, private router: Router) { }

  onSubmit() {
    this.authService.logIn(
      this.form.controls.email.value,
      this.form.controls.password.value
    ).subscribe(
      e => this.router.navigateByUrl('/'),
      e => {
        this.loginError = e;
        console.log(e);
      },
    );
  }
}
