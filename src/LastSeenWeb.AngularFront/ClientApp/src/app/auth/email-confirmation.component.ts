import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ErrorType } from '../shared/error-type';
import { AuthApiWrapperService } from './auth-api-wrapper.service';

@Component({
  templateUrl: './email-confirmation.component.html'
})
export class EmailConfirmationComponent implements OnInit {
  checkEmailResponse: Object | ErrorType;

  constructor(private activatedRoute: ActivatedRoute, private authApiWrapperService: AuthApiWrapperService) { }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      this.authApiWrapperService.checkEmail(params['userId'], params['code']).subscribe(e =>
        this.checkEmailResponse = e || {});
    });
  }
}
