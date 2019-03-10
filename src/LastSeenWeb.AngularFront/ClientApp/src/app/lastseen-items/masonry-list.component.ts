import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Data } from '@angular/router';
import { LastSeenItem } from '../shared/lastseen-item';
import { HttpErrorResponse } from '@angular/common/http';
import { ErrorType } from '../shared/error-type';

@Component({
  templateUrl: './masonry-list.component.html',
  styleUrls: ['./masonry-list.component.css']
})
export class MasonryListComponent implements OnInit {
  updateMasonryLayout: boolean;
  masonryItems: LastSeenItem[] | ErrorType;

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe((e: Data) => {
      const data = e['lastseenitems'] as LastSeenItem[] | HttpErrorResponse;
      if ((<HttpErrorResponse>data).status === 403) {
        this.masonryItems = { 'error': 'You are not authorized to use this resource' } as ErrorType;
      } else {
        this.masonryItems = data as LastSeenItem[];
      }
    });
  }

  public onLoaded(): void {
    this.updateMasonryLayout = !this.updateMasonryLayout;
  }
}
