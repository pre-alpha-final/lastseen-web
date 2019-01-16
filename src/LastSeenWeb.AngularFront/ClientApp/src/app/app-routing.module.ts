import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MasonryListComponent } from './lastseen-items/masonry-list.component';
import { MasonryListResolver } from './lastseen-items/masonry-list-resolver.service';

const routes: Routes = [
  {
    path: '',
    component: MasonryListComponent,
    pathMatch: 'full',
    resolve: { lastseenitems: MasonryListResolver }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
