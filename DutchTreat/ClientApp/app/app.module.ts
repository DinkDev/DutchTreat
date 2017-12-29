import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { Cart } from './shop/cart.component';
import { DataService } from './shared/dataService';
import { ProductList } from './shop/productList.component';

@NgModule({
  declarations: [
    AppComponent,
    Cart,
    ProductList
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [
    DataService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
