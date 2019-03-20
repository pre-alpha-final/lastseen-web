import { Component, OnInit, OnDestroy, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { UpdatePopupService } from './update-popup.service';
import { Subscription, of } from 'rxjs';
import { LastSeenItem } from '../shared/lastseen-item';
import { JQ_TOKEN } from '../shared/jquery.service';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({
  selector: 'app-update-popup',
  templateUrl: './update-popup.component.html',
  styleUrls: ['./update-popup.component.css']
})
export class UpdatePopupComponent implements OnInit, OnDestroy {
  contentLoadedSubscription: Subscription;
  form = this.formBuilder.group({
    id: [''],
    season: [0],
    episode: [0],
    visitUrl: [''],
    notes: [''],
    unfinished: [false],
    hours: [0],
    minutes: [0],
    seconds: [0],
    moveToTop: [true],
    name: [''],
    imageUrl: ['']
  });

  constructor(private updatePopupService: UpdatePopupService, private httpClient: HttpClient,
    private formBuilder: FormBuilder, private router: Router, @Inject(JQ_TOKEN) private $: any) { }

  ngOnInit() {
    this.$('.custom-tooltip').tooltip();
    this.contentLoadedSubscription = this.updatePopupService.contentLoaded.subscribe(
      (e: LastSeenItem) => this.contentLoaded(this, e));
  }

  ngOnDestroy(): void {
    this.contentLoadedSubscription.unsubscribe();
  }

  contentLoaded(_: UpdatePopupComponent, content: LastSeenItem) {
    this.form.controls.id.setValue(content.id);
    this.form.controls.season.setValue(content.season);
    this.form.controls.episode.setValue(content.episode);
    this.form.controls.visitUrl.setValue(content.visitUrl);
    this.form.controls.notes.setValue(content.notes);
    this.form.controls.unfinished.setValue(content.unfinished);
    this.form.controls.hours.setValue(content.hours);
    this.form.controls.minutes.setValue(content.minutes);
    this.form.controls.seconds.setValue(content.seconds);
    this.form.controls.moveToTop.setValue(content.moveToTop);
    this.form.controls.name.setValue(content.name);
    this.form.controls.imageUrl.setValue(content.imageUrl);

    for (let i = 2; i <= 4; i++) {
      if (this.$('#collapse' + i).hasClass('in')) {
        this.$('#collapse' + i).collapse('toggle');
      }
    }
    this.updatePopupService.open();
    if (this.$('#collapse1').hasClass('in') === false) {
      this.$('#collapse1').collapse('toggle');
    }
  }

  remove() {
    if (!this.form.controls.id.value) {
      return;
    }
    this.httpClient.delete('/api/lastseenitems/' + this.form.controls.id.value)
      .pipe(catchError(e => of(e)))
      .subscribe(e => {
        this.updatePopupService.close();
        this.router.navigated = false;
        this.router.navigate(['/']);
      });
  }

  onSubmit() {
    if (this.form.valid === false) {
      return;
    }
    console.log(this.form.value);
  }
}
