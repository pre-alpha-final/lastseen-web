import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { JQUERY_PROVIDER } from './jquery.service';
import { AppComponent } from './app.component';
import { NavBarComponent } from './nav-bar.component';
import { MasonryListComponent } from './lastseen-items/masonry-list.component';
import { LastseenItemsModule } from './lastseen-items/lastseen-items.module';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    LastseenItemsModule,
    RouterModule.forRoot([
      { path: '', component: MasonryListComponent, pathMatch: 'full' },
    ])
  ],
  providers: [JQUERY_PROVIDER],
  bootstrap: [AppComponent]
})
export class AppModule { }
