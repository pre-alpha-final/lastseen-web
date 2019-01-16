import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { JQUERY_PROVIDER } from './shared/jquery.service';
import { AppComponent } from './app.component';
import { NavBarComponent } from './nav-bar.component';
import { LastseenItemsModule } from './lastseen-items/lastseen-items.module';
import { SharedModule } from './shared/shared.module';
import { AppRoutingModule } from './app-routing.module';
import { AuthModule } from './auth/auth.module';

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
    SharedModule,
    LastseenItemsModule,
    AuthModule,
    AppRoutingModule
  ],
  providers: [JQUERY_PROVIDER],
  bootstrap: [AppComponent]
})
export class AppModule { }
