import { Component, OnInit } from '@angular/core';
import { UpdatePopupService } from './update-popup.service';
import { LastSeenItem } from './lastseenitem';

@Component({
  selector: 'app-update-popup',
  templateUrl: './update-popup.component.html',
  styleUrls: ['./update-popup.component.css']
})
export class UpdatePopupComponent implements OnInit {
  content: LastSeenItem;

  constructor(private updatePopupService: UpdatePopupService) { }

  ngOnInit() {
    this.updatePopupService.contentLoaded.subscribe((e: LastSeenItem) => this.contentLoaded(this, e));
  }

  contentLoaded(me: UpdatePopupComponent, content: LastSeenItem) {
    me.content = content;
    me.updatePopupService.open();
  }
}
