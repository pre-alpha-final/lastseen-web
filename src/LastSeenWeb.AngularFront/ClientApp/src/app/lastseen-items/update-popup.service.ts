import { Injectable, EventEmitter } from '@angular/core';
import { LastSeenItem } from '../shared/lastseen-item';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UpdatePopupService {
  contentLoaded: EventEmitter<LastSeenItem> = new EventEmitter<LastSeenItem>();

  constructor(private httpClient: HttpClient) {
    window.onclick = event => {
      if (event.target === this.getPopup()) {
        this.close();
      }
    };
  }

  loadContent(id?: string): void {
    if (!id) {
      this.contentLoaded.emit(this.generateInitialContent());
      return;
    }

    this.httpClient.get('/api/lastseenitems/' + id)
      .pipe(catchError(e => of(this.generateInitialContent())))
      .subscribe(e => this.contentLoaded.emit(e));
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

  private generateInitialContent(): LastSeenItem {
    return {
      id: '',
      season: 0,
      episode: 0,
      visitUrl: '',
      notes: '',
      unfinished: false,
      hours: 0,
      minutes: 0,
      seconds: 0,
      moveToTop: true,
      name: '',
      imageUrl: '',
    };
  }
}
