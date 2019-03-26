import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) { }

  canActivate(): Promise<boolean> {
    return new Promise(async resolve => {
      const isAuthenticated = await this.authService.isAuthenticated();
      if (isAuthenticated !== true) {
        this.router.navigate(['auth/login']);
      }
      resolve(isAuthenticated);
    });
  }
}
