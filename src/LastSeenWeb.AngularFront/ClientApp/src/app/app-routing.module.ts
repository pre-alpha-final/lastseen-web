import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MasonryListComponent } from './lastseen-items/masonry-list.component';
import { MasonryListResolver } from './lastseen-items/masonry-list-resolver.service';
import { AuthGuard } from './auth/auth-guard.service';

const routes: Routes = [
  {
    path: '',
    component: MasonryListComponent,
    pathMatch: 'full',
    resolve: { lastseenitems: MasonryListResolver },
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
