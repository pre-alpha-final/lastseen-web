import { Component, OnInit } from '@angular/core';

@Component({
  templateUrl: './masonry-list.component.html',
  styleUrls: ['./masonry-list.component.css']
})
export class MasonryListComponent implements OnInit {

  masonryItems = [
    { title: 'item 1' },
    { title: 'item 2' },
    { title: 'item 3' },
    { title: 'item 4' },
    { title: 'item 5' },
    { title: 'item 6' }
  ];

  constructor() { }

  ngOnInit() {
  }

}
