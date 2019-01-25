import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Data } from '@angular/router';
import { LastSeenItem } from '../shared/lastseenitem';

@Component({
  templateUrl: './masonry-list.component.html',
  styleUrls: ['./masonry-list.component.css']
})
export class MasonryListComponent implements OnInit {
  updateMasonryLayout: boolean;
  masonryItems: LastSeenItem[];

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe((data: Data) => {
      this.masonryItems = data['lastseenitems'];
    });
  }

  public onLoaded(): void {
    this.updateMasonryLayout = !this.updateMasonryLayout;
  }
}
