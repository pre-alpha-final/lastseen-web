import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from './auth.service';
import { ErrorType } from '../shared/errortype';

@Component({
  templateUrl: './emailconfirmation.component.html'
})
export class EmailConfirmationComponent implements OnInit {
  checkEmailResponse: Object | ErrorType;

  constructor(private activatedRoute: ActivatedRoute, private authService: AuthService) { }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      this.authService.checkEmail(params['userId'], params['code']).subscribe(e =>
        this.checkEmailResponse = e || {});
    });
  }
}
