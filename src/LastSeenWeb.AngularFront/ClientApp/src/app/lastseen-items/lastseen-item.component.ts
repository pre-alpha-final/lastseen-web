import { Component, Input, Output, EventEmitter } from '@angular/core';
import { UpdatePopupService } from '../shared/update-popup.service';
import { LastSeenItem } from '../shared/lastseenitem';

@Component({
  selector: 'app-lastseen-item',
  templateUrl: './lastseen-item.component.html',
  styleUrls: ['./lastseen-item.component.css']
})
export class LastseenItemComponent {
  @Input() item: LastSeenItem;
  @Output() loaded: EventEmitter<any> = new EventEmitter<any>();

  constructor(private updatePopupService: UpdatePopupService) { }

  onLoad(): void {
    this.loaded.emit();
  }

  onClick(): void {
    this.updatePopupService.loadContent();
  }
}
