import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UpdatePopupComponent } from './update-popup.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [UpdatePopupComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  exports: [
    UpdatePopupComponent
  ]
})
export class SharedModule { }
