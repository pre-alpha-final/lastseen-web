import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MasonryListComponent } from './masonry-list.component';
import { NgxMasonryModule } from 'ngx-masonry';

@NgModule({
  declarations: [MasonryListComponent],
  imports: [
    CommonModule,
    NgxMasonryModule
  ]
})
export class LastseenItemsModule { }
