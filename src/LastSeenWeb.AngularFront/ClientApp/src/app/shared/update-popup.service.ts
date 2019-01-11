import { Injectable, EventEmitter } from '@angular/core';
import { ILastSeenItem } from '../lastseen-items/lastseen-item.component';

@Injectable({
  providedIn: 'root'
})
export class UpdatePopupService {
  contentLoaded: EventEmitter<ILastSeenItem> = new EventEmitter<ILastSeenItem>();

  constructor() {
    window.onclick = event => {
      if (event.target === this.getPopup()) {
        this.close();
      }
    };
  }

  loadContent(): void {
    let content: ILastSeenItem;
    // tslint:disable-next-line:max-line-length
    content = { name: 'item 1', season: 1, unfinished: true, hours: 2, minutes: 3, notes: 'some notes', visitUrl: 'http://google.com', imageUrl: 'https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/SNice.svg/1200px-SNice.svg.png' };
    this.contentLoaded.emit(content);
  }

  open(): void {
    this.getPopup().style.display = 'flex';
  }

  close(): void {
    this.getPopup().style.display = 'none';
  }

  private getPopup(): any {
    return document.getElementById('popup');
  }
}
