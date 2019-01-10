import { Component, OnInit } from '@angular/core';
import { ILastSeenItem } from './lastseen-item.component';

@Component({
  templateUrl: './masonry-list.component.html',
  styleUrls: ['./masonry-list.component.css']
})
export class MasonryListComponent implements OnInit {
  masonryItems: ILastSeenItem[] = [
    // tslint:disable-next-line:max-line-length
    { name: 'item 1', season: 1, unfinished: true, hours: 2, minutes: 3, notes: 'some notes', visitUrl: 'http://google.com', imageUrl: 'https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/SNice.svg/1200px-SNice.svg.png' },
    // tslint:disable-next-line:max-line-length
    { name: 'item 2', season: 1, episode: 1, imageUrl: 'https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/SNice.svg/1200px-SNice.svg.png' },
    // tslint:disable-next-line:max-line-length
    { name: 'item', season: 1, episode: 1, imageUrl: 'https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/SNice.svg/1200px-SNice.svg.png' },
    // tslint:disable-next-line:max-line-length
    { name: 'item 3', episode: 1, notes: 'some notes', visitUrl: 'http://google.com', imageUrl: 'https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/SNice.svg/1200px-SNice.svg.png' },
    // tslint:disable-next-line:max-line-length
    { name: 'item 4', unfinished: true, hours: 1, imageUrl: 'https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/SNice.svg/1200px-SNice.svg.png' },
    // tslint:disable-next-line:max-line-length
    { name: 'item 5', unfinished: true, minutes: 1, visitUrl: '', imageUrl: 'https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/SNice.svg/1200px-SNice.svg.png' },
    // tslint:disable-next-line:max-line-length
    { name: 'item 6', unfinished: true, hours: 1, minutes: 2, imageUrl: 'https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/SNice.svg/1200px-SNice.svg.png' }
  ];

  constructor() { }

  ngOnInit() {
  }

}
