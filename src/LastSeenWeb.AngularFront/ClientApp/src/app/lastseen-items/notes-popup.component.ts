import { Component, OnInit, OnDestroy, Inject } from '@angular/core';
import { NotesPopupService } from './notes-popup.service';
import { Subscription, of } from 'rxjs';
import { JQ_TOKEN } from '../shared/jquery.service';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { MdEditorOption } from 'ngx-markdown-editor';

@Component({
  selector: 'app-notes-popup',
  templateUrl: './notes-popup.component.html',
  styleUrls: ['./notes-popup.component.css']
})
export class NotesPopupComponent implements OnInit, OnDestroy {
  contentLoadedSubscription: Subscription;
  public content = '';
  public mode = 'preview';
  public options: MdEditorOption = {
    showPreviewPanel: false,
    enablePreviewContentClick: true,
    resizable: true,
    customRender: {
      image: function (href: string, title: string, text: string) {
        let out = `<img style="max-width: 100%; border: 20px solid red;" src="${href}" alt="${text}"`;
        if (title) {
          out += ` title="${title}"`;
        }
        out += (<any>this.options).xhtml ? '/>' : '>';
        return out;
      }
    },
    markedjsOpt: {
      sanitize: true
    }
  };

  constructor(private httpClient: HttpClient, private notesPopupService: NotesPopupService,
    @Inject(JQ_TOKEN) private $: any) {
  }

  ngOnInit() {
    this.contentLoadedSubscription = this.notesPopupService.contentLoaded.subscribe(
      (e: string) => this.contentLoaded(this, e));
  }

  ngOnDestroy(): void {
    this.contentLoadedSubscription.unsubscribe();
  }

  contentLoaded(_: NotesPopupComponent, content: string) {
    this.content = content;
    this.notesPopupService.open();
  }

  save(): void {
    this.httpClient.put('/api/notes/', {
      notes: this.content
    })
    .pipe(catchError(e => of(e)))
    .subscribe(e => { });
  }

  toogleMode(): void {
    if (this.mode === 'edit') {
      this.mode = 'preview';
    } else {
      this.mode = 'edit';
    }
  }
}
