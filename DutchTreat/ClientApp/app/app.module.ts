import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { Cart } from './shop/cart.component';
import { Checkout } from './checkout/checkout.component';
import { DataService } from './shared/dataService';
import { Login } from './login/login.component';
import { ProductList } from './shop/productList.component';
import { Shop } from './shop/shop.component';

let routes = [
  { path: '', component: Shop },
  { path: 'checkout', component: Checkout },
  { path: 'login', component: Login }
];

@NgModule({
  declarations: [
    AppComponent,
    Cart,
    Checkout,
    Login,
    ProductList,
    Shop
  ],
  imports: [
    BrowserModule,
    FormsModule,
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
