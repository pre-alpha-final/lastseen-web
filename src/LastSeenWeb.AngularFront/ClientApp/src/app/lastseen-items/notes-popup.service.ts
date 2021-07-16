import { Injectable, EventEmitter } from '@angular/core';
import { LastSeenItem } from '../shared/lastseen-item';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';
import { Notes } from '../shared/notes';

@Injectable({
  providedIn: 'root'
})
export class NotesPopupService {
  contentLoaded: EventEmitter<string> = new EventEmitter<string>();

  constructor(private httpClient: HttpClient, private authService: AuthService, private router: Router) {
    window.onclick = event => {
      if ((<HTMLElement>event.target).classList.contains('popup-overlay')) {
        this.close();
      }
    };
  }

  loadContent(): void {
    this.authService.isAuthenticated(60)
      .then(authenticated => {
        if (authenticated !== true) {
          this.router.navigate(['auth/login']);
        } else {
          this.httpClient.get('/api/notes/')
          .subscribe(e => {
            this.contentLoaded.emit((<Notes>e).notes);
            console.log(e);
          });
        }
      });
  }

  open(): void {
    this.getPopupOverlay().style.display = 'flex';
  }

  close(): void {
    Array.from(document.getElementsByClassName('popup-overlay')).forEach(element => {
      (<HTMLElement>element).style.display = 'none';
    });
  }

  private getPopupOverlay(): any {
    return document.getElementById('notes-popupOverlay');
  }
}
