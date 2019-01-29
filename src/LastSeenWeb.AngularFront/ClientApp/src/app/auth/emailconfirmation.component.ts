import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from './auth.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Component({
  templateUrl: './emailconfirmation.component.html'
})
export class EmailConfirmationComponent implements OnInit {
  emailCheckResponse$: Observable<string>;

  constructor(private activatedRoute: ActivatedRoute, private authService: AuthService) { }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(e => {
      this.emailCheckResponse$ =
        this.authService.checkEmail(e['userId'], e['code']).pipe(
          map(f => f.message)
        );
    });
  }
}
