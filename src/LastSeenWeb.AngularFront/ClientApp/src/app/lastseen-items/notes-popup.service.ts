import { Injectable, EventEmitter } from '@angular/core';
import { LastSeenItem } from '../shared/lastseen-item';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class NotesPopupService {
  contentLoaded: EventEmitter<LastSeenItem> = new EventEmitter<LastSeenItem>();

  constructor(private httpClient: HttpClient, private authService: AuthService, private router: Router) {
    window.onclick = event => {
      if (event.target === this.getPopupOverlay()) {
        this.close();
      }
    };
  }

  loadContent(id?: string): void {
    if (!id) {
      this.contentLoaded.emit(this.generateInitialContent());
      return;
    }

    this.authService.isAuthenticated(60)
      .then(authenticated => {
        if (authenticated !== true) {
          this.router.navigate(['auth/login']);
        } else {
          this.handleItem(id);
        }
      });
  }

  open(): void {
    this.getPopupOverlay().style.display = 'flex';
  }

  close(): void {
    this.getPopupOverlay().style.display = 'none';
  }

  private handleItem(id?: string) {
    this.httpClient.get('/api/lastseenitems/' + id)
      .pipe(catchError(e => of(this.generateInitialContent())))
      .subscribe(e => this.contentLoaded.emit(e));
  }

  private getPopupOverlay(): any {
    return document.getElementById('notes-popupOverlay');
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
