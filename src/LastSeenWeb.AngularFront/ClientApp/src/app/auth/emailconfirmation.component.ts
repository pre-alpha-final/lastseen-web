import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ErrorType } from '../shared/errortype';
import { AuthApiCallsService } from './auth-apicalls.service';

@Component({
  templateUrl: './emailconfirmation.component.html'
})
export class EmailConfirmationComponent implements OnInit {
  checkEmailResponse: Object | ErrorType;

  constructor(private activatedRoute: ActivatedRoute, private authApiCallsService: AuthApiCallsService) { }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      this.authApiCallsService.checkEmail(params['userId'], params['code']).subscribe(e =>
        this.checkEmailResponse = e || {});
    });
  }
}
