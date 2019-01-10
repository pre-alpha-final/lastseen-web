import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Data } from '@angular/router';
import { ILastSeenItem } from './lastseen-item.component';

@Component({
  templateUrl: './masonry-list.component.html',
  styleUrls: ['./masonry-list.component.css']
})
export class MasonryListComponent implements OnInit {
  updateMasonryLayout: boolean;
  masonryItems: ILastSeenItem[];

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe((value: Data) => {
      this.masonryItems = value['lastseenitems'];
    });
  }

  public onLoaded(): void {
    this.updateMasonryLayout = !this.updateMasonryLayout;
  }
}
