import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { NgModule } from '@angular/core';
import { SearchComponent } from "./search/search.component";

const routes: Routes = [
    {
        path: '', component: LayoutComponent,
        children: [
            { path: '', component: SearchComponent },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ClientRoutingModule { }