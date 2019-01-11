import { Component, OnInit } from '@angular/core';
import { ILastSeenItem } from '../lastseen-items/lastseen-item.component';
import { UpdatePopupService } from './update-popup.service';

@Component({
  selector: 'app-update-popup',
  templateUrl: './update-popup.component.html',
  styleUrls: ['./update-popup.component.css']
})
export class UpdatePopupComponent implements OnInit {
  content: ILastSeenItem;

  constructor(private updatePopupService: UpdatePopupService) { }

  ngOnInit() {
    this.updatePopupService.contentLoaded.subscribe((e: ILastSeenItem) => this.contentLoaded(this, e));
  }

  contentLoaded(me: UpdatePopupComponent, content: ILastSeenItem) {
    me.content = content;
    me.updatePopupService.open();
  }
}
