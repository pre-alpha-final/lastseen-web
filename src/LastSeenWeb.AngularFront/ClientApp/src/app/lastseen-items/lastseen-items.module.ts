import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MasonryListComponent } from './masonry-list.component';
import { NgxMasonryModule } from 'ngx-masonry';
import { LastseenItemComponent } from './lastseen-item.component';

@NgModule({
  declarations: [MasonryListComponent, LastseenItemComponent],
  imports: [
    CommonModule,
    NgxMasonryModule
  ]
})
export class LastseenItemsModule { }
