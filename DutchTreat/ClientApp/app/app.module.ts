import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { Cart } from './shop/cart.component';
import { Checkout } from './checkout/checkout.component';
import { DataService } from './shared/dataService';
import { ProductList } from './shop/productList.component';
import { Shop } from './shop/shop.component';

let routes = [
  { path: '', component: Shop },
  { path: 'checkout', component: Checkout },
];

@NgModule({
  declarations: [
    AppComponent,
    Cart,
    Checkout,
    ProductList,
    Shop
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(routes,
      {
        useHash: true,
        enableTracing: false, // for debugging routes
      })
  ],
  providers: [
    DataService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
