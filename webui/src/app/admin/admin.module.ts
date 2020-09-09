import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from './layout/layout.component';
import { OverviewComponent } from './overview/overview.component';
import { AdminRoutingModule } from './admin-routing.module';


@NgModule({
  declarations: [LayoutComponent, OverviewComponent],
  imports: [
    CommonModule,
    AdminRoutingModule
  ]
})
export class AdminModule { }
