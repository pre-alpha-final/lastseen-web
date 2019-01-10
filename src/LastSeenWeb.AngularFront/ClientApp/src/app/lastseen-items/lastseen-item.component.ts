import { Component, Input, Output, EventEmitter } from '@angular/core';

export interface ILastSeenItem {
  // Data section
  id?: string;
  modified?: Date;

  // Status section
  season?: number;
  episode?: number;
  visitUrl?: string;
  unfinished?: boolean;
  hours?: number;
  minutes?: number;
  seconds?: number;
  notes?: string;
  moveToTop?: boolean;

  // Config section
  name?: string;
  imageUrl?: string;

  // Tracking section
  trackingUrl?: string;
  episodesBehind?: number;

  // Removal
  remove?: boolean;
}

@Component({
  selector: 'app-lastseen-item',
  templateUrl: './lastseen-item.component.html',
  styleUrls: ['./lastseen-item.component.css']
})
export class LastseenItemComponent {
  @Input() item: ILastSeenItem;
  @Output() loaded: EventEmitter<any> = new EventEmitter<any>();

  public onLoad(): void  {
    this.loaded.emit();
  }
}
