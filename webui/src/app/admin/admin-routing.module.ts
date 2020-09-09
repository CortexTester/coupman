import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { NgModule } from '@angular/core';
import { OverviewComponent } from './overview/overview.component';

const routes: Routes = [
    {
        path: '', component: LayoutComponent,
        children: [
            { path: '', component: OverviewComponent },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AdminRoutingModule { }