import { Component, OnInit, OnDestroy } from '@angular/core';
import { UpdatePopupService } from './update-popup.service';
import { LastSeenItem } from './lastseenitem';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-update-popup',
  templateUrl: './update-popup.component.html',
  styleUrls: ['./update-popup.component.css']
})
export class UpdatePopupComponent implements OnInit, OnDestroy {
  content: LastSeenItem;
  contentLoaded$: Subscription;

  constructor(private updatePopupService: UpdatePopupService) { }

  ngOnInit() {
    this.contentLoaded$ = this.updatePopupService.contentLoaded.subscribe((e: LastSeenItem) => this.contentLoaded(this, e));
  }

  ngOnDestroy(): void {
    this.contentLoaded$.unsubscribe();
  }

  contentLoaded(me: UpdatePopupComponent, content: LastSeenItem) {
    me.content = content;
    me.updatePopupService.open();
  }
}
