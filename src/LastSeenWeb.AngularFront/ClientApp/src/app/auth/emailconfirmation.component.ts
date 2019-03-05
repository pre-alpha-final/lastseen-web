import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService, ErrorResponse } from './auth.service';

@Component({
  templateUrl: './emailconfirmation.component.html'
})
export class EmailConfirmationComponent implements OnInit {
  checkEmailResponse: Object | ErrorResponse;

  constructor(private activatedRoute: ActivatedRoute, private authService: AuthService) { }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      this.authService.checkEmail(params['userId'], params['code']).subscribe(e =>
        this.checkEmailResponse = e || {});
    });
  }
}
