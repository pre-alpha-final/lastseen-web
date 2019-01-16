import { Component, OnInit, OnDestroy, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { UpdatePopupService } from './update-popup.service';
import { LastSeenItem } from './lastseenitem';
import { Subscription } from 'rxjs';
import { JQ_TOKEN } from './jquery.service';

@Component({
  selector: 'app-update-popup',
  templateUrl: './update-popup.component.html',
  styleUrls: ['./update-popup.component.css']
})
export class UpdatePopupComponent implements OnInit, OnDestroy {
  contentLoaded$: Subscription;
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
    imageUrl: [''],
    remove: [false]
  });

  constructor(private updatePopupService: UpdatePopupService,
    private formBuilder: FormBuilder, @Inject(JQ_TOKEN) private $: any) { }

  ngOnInit() {
    this.$('.custom-tooltip').tooltip();
    this.contentLoaded$ = this.updatePopupService.contentLoaded.subscribe(
      (e: LastSeenItem) => this.contentLoaded(this, e));
  }

  ngOnDestroy(): void {
    this.contentLoaded$.unsubscribe();
  }

  contentLoaded(me: UpdatePopupComponent, content: LastSeenItem) {
    me.updatePopupService.open();
  }
}
