import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MasonryListComponent } from './masonry-list.component';
import { NgxMasonryModule } from 'ngx-masonry';
import { LastseenItemComponent } from './lastseen-item.component';
import { UpdatePopupComponent } from './update-popup.component';
import { NotesPopupComponent } from './notes-popup.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LMarkdownEditorModule } from 'ngx-markdown-editor';

@NgModule({
  declarations: [
    MasonryListComponent,
    LastseenItemComponent,
    UpdatePopupComponent,
    NotesPopupComponent,
  ],
  imports: [
    CommonModule,
    NgxMasonryModule,
    ReactiveFormsModule,
    FormsModule,
    LMarkdownEditorModule
  ],
  exports: [
    UpdatePopupComponent,
    NotesPopupComponent
  ]
})
export class LastseenItemsModule { }
