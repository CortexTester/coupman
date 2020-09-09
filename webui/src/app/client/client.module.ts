import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from './layout/layout.component';
import { SearchComponent } from './search/search.component';
import { ClientRoutingModule } from "./client-routing.module";


@NgModule({
  declarations: [LayoutComponent, SearchComponent],
  imports: [
    CommonModule,
    ClientRoutingModule
  ]
})
export class ClientModule { }
