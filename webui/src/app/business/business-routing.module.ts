import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { NgModule } from '@angular/core';
import { ListingComponent } from './listing/listing.component';

const routes: Routes = [
    {
        path: '', component: LayoutComponent,
        children: [
            { path: '', component: ListingComponent },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class BusinessRoutingModule { }