import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Data } from '@angular/router';
import { LastSeenItem } from '../shared/lastseenitem';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  templateUrl: './masonry-list.component.html',
  styleUrls: ['./masonry-list.component.css']
})
export class MasonryListComponent implements OnInit {
  updateMasonryLayout: boolean;
  masonryItems: LastSeenItem[];
  error: string;

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe((e: Data) => {
      const data = e['lastseenitems'];
      if (data as Array<LastSeenItem>) {
        this.masonryItems = e['lastseenitems'];
      }
      if (data as HttpErrorResponse && data.status === 403) {
        this.error = 'You are not authorized to use this resource';
      }
    });
  }

  public onLoaded(): void {
    this.updateMasonryLayout = !this.updateMasonryLayout;
  }
}
