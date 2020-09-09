import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from './layout/layout.component';
import { ListingComponent } from './listing/listing.component';
import { BusinessRoutingModule } from "./business-routing.module";


@NgModule({
  declarations: [LayoutComponent, ListingComponent],
  imports: [
    CommonModule,
    BusinessRoutingModule
  ]
})
export class BusinessModule { }
