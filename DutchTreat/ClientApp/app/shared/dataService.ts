import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from './product';
import 'rxjs/add/operator/map';

@Injectable()
export class DataService {

  constructor(private http: HttpClient) { }

  public products: Product[] = [];

  loadProducts(): Observable<boolean> {
    return this.http.get('/api/products')
      .map((data: any[]) => {
        this.products = data;
        return true;
      });
  }

}