import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from './auth.service';

@Component({
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  form = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [
      Validators.required,
      // Validators.minLength(8),
      // Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{1,}$')
    ]]
  });

  constructor(private formBuilder: FormBuilder, private authService: AuthService) { }

  ngOnInit() {
  }

  onSubmit() {
    this.authService.logIn(
      this.form.controls.email.value,
      this.form.controls.password.value);
  }
}
