import { Component, OnInit, OnDestroy, Inject, AfterViewChecked } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { NotesPopupService } from './notes-popup.service';
import { Subscription, of } from 'rxjs';
import { LastSeenItem } from '../shared/lastseen-item';
import { JQ_TOKEN } from '../shared/jquery.service';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthService } from '../auth/auth.service';
import { MdEditorOption } from 'ngx-markdown-editor';

@Component({
  selector: 'app-notes-popup',
  templateUrl: './notes-popup.component.html',
  styleUrls: ['./notes-popup.component.css']
})
export class NotesPopupComponent implements OnInit, OnDestroy, AfterViewChecked {
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

  public options: MdEditorOption = {
    showPreviewPanel: false,
    enablePreviewContentClick: false,
    resizable: true,
    customRender: {
      image: function (href: string, title: string, text: string) {
        let out = `<img style="max-width: 100%; border: 20px solid red;" src="${href}" alt="${text}"`;
        if (title) {
          out += ` title="${title}"`;
        }
        out += (<any>this.options).xhtml ? "/>" : ">";
        return out;
      }
    },
    markedjsOpt: {
      sanitize: true
    }
  };
  public content = 'foo';
  public mode = 'preview';

  constructor(private formBuilder: FormBuilder, private httpClient: HttpClient, private authService: AuthService,
    private notesPopupService: NotesPopupService, private router: Router, @Inject(JQ_TOKEN) private $: any) { }

  ngOnInit() {
    this.$('[data-toggle="tooltip"]').tooltip();
    this.contentLoadedSubscription = this.notesPopupService.contentLoaded.subscribe(
      (e: LastSeenItem) => this.contentLoaded(this, e));
  }

  ngOnDestroy(): void {
    this.contentLoadedSubscription.unsubscribe();
  }

  ngAfterViewChecked(): void {
    if (this.$('#popupOverlay').css('display') === 'none' && this.$('#collapse1').hasClass('show') === false) {
      this.$('#collapse1').collapse('toggle');
    }
  }

  contentLoaded(_: NotesPopupComponent, content: LastSeenItem) {
    this.form.controls.id.setValue(content.id);
    this.form.controls.season.setValue(content.season);
    this.form.controls.episode.setValue(content.episode);
    this.form.controls.visitUrl.setValue(content.visitUrl);
    this.form.controls.notes.setValue(content.notes);
    this.form.controls.unfinished.setValue(content.unfinished);
    this.form.controls.hours.setValue(content.hours);
    this.form.controls.minutes.setValue(content.minutes);
    this.form.controls.seconds.setValue(content.seconds);
    this.form.controls.moveToTop.setValue(true);
    this.form.controls.name.setValue(content.name);
    this.form.controls.imageUrl.setValue(content.imageUrl);

    for (let i = 2; i <= 4; i++) {
      if (this.$('#collapse' + i).hasClass('show')) {
        this.$('#collapse' + i).collapse('toggle');
      }
    }
    this.notesPopupService.open();
  }

  remove() {
    if (!this.form.controls.id.value) {
      return;
    }

    this.authService.isAuthenticated()
      .then(authenticated => {
        if (authenticated !== true) {
          this.router.navigate(['auth/login']);
        } else {
          this.removeItem();
        }
      });
  }

  onSubmit() {
    if (this.form.valid === false) {
      return;
    }

    this.authService.isAuthenticated()
      .then(authenticated => {
        if (authenticated !== true) {
          this.router.navigate(['auth/login']);
        } else {
          this.upsertItem();
        }
      });
  }

  private removeItem() {
    this.httpClient.delete('/api/lastseenitems/' + this.form.controls.id.value)
      .pipe(catchError(e => of(e)))
      .subscribe(e => {
        this.notesPopupService.close();
        this.router.navigated = false;
        this.router.navigate(['/']);
      });
  }

  private upsertItem() {
    this.httpClient.put('/api/lastseenitems/', this.form.value)
      .pipe(catchError(e => of(e)))
      .subscribe(e => {
        this.notesPopupService.close();
        this.router.navigated = false;
        this.router.navigate(['/']);
      });
  }
}
