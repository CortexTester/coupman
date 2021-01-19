import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { NgModule } from '@angular/core';
import { ListingComponent } from './listing/listing.component';
import { ProfileComponent } from './profile/profile.component';

const routes: Routes = [
    {
        path: '', component: LayoutComponent,
        children: [
            { path: 'profile', component: ProfileComponent },
            { path: 'coupons', component: ListingComponent },
            { path: '', component: ListingComponent },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class BusinessRoutingModule { }