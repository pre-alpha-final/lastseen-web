import { Injectable } from '@angular/core';
import { ILastSeenItem } from './lastseen-item.component';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MasonryListResolver implements Resolve<any> {
  constructor(private http: HttpClient) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    // https://github.com/DeborahK/Angular-Routing/blob/master/APM-Final/src/app/products/product-resolver.service.ts
    // return this.http.get('');

    return of<ILastSeenItem[]>([
      // tslint:disable-next-line:max-line-length
      { name: 'item 1', season: 1, unfinished: true, hours: 2, minutes: 3, notes: 'some notes', visitUrl: 'http://google.com', imageUrl: 'https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/SNice.svg/1200px-SNice.svg.png' },
      // tslint:disable-next-line:max-line-length
      { name: 'item 2', season: 1, episode: 1, imageUrl: 'https://images-na.ssl-images-amazon.com/images/I/51zLZbEVSTL._SX425_.jpg' },
      // tslint:disable-next-line:max-line-length
      { name: 'item', season: 1, episode: 1, imageUrl: 'https://i.guim.co.uk/img/static/sys-images/Arts/Arts_/Pictures/2009/2/20/1235150177056/Smiley-001.jpg?width=300&quality=85&auto=format&fit=max&s=5cf88208cce59af2cf03b827227efdff' },
      // tslint:disable-next-line:max-line-length
      { name: 'item 3', episode: 1, notes: 'some notes', visitUrl: 'http://google.com', imageUrl: 'https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/SNice.svg/220px-SNice.svg.png' },
      // tslint:disable-next-line:max-line-length
      { name: 'item 4', unfinished: true, hours: 1, imageUrl: 'https://spectator.imgix.net/content/uploads/2015/06/Emoji.jpg?auto=compress,enhance,format&crop=faces,entropy,edges&fit=crop&w=620&h=413' },
      // tslint:disable-next-line:max-line-length
      { name: 'item 5', unfinished: true, minutes: 1, visitUrl: '', imageUrl: 'https://cdn.shopify.com/s/files/1/1061/1924/products/Smiling_Face_Emoji_with_Blushed_Cheeks_large.png?v=1480481056' },
      // tslint:disable-next-line:max-line-length
      { name: 'item 6', unfinished: true, hours: 1, minutes: 2, imageUrl: 'https://images-na.ssl-images-amazon.com/images/I/41q0g8iq5iL.jpg' }
    ]);
  }
}
