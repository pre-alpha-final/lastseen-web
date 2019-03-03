import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MasonryListComponent } from './masonry-list.component';
import { NgxMasonryModule } from 'ngx-masonry';
import { LastseenItemComponent } from './lastseen-item.component';
import { UpdatePopupComponent } from './update-popup.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    MasonryListComponent,
    LastseenItemComponent,
    UpdatePopupComponent
  ],
  imports: [
    CommonModule,
    NgxMasonryModule,
    ReactiveFormsModule
  ],
  exports: [
    UpdatePopupComponent
  ]
})
export class LastseenItemsModule { }
