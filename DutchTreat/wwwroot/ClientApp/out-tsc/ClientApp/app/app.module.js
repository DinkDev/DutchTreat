"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var platform_browser_1 = require("@angular/platform-browser");
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var http_1 = require("@angular/common/http");
var router_1 = require("@angular/router");
var app_component_1 = require("./app.component");
var cart_component_1 = require("./shop/cart.component");
var checkout_component_1 = require("./checkout/checkout.component");
var dataService_1 = require("./shared/dataService");
var login_component_1 = require("./login/login.component");
var productList_component_1 = require("./shop/productList.component");
var shop_component_1 = require("./shop/shop.component");
var routes = [
    { path: '', component: shop_component_1.Shop },
    { path: 'checkout', component: checkout_component_1.Checkout },
    { path: 'login', component: login_component_1.Login }
];
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            declarations: [
                app_component_1.AppComponent,
                cart_component_1.Cart,
                checkout_component_1.Checkout,
                login_component_1.Login,
                productList_component_1.ProductList,
                shop_component_1.Shop
            ],
            imports: [
                platform_browser_1.BrowserModule,
                forms_1.FormsModule,
                http_1.HttpClientModule,
                router_1.RouterModule.forRoot(routes, {
                    useHash: true,
                    enableTracing: false,
                })
            ],
            providers: [
                dataService_1.DataService
            ],
            bootstrap: [app_component_1.AppComponent]
        })
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map