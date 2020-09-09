import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AuthGuard } from "./helpers/auth.guard";
import { Role } from './models/role';

const accountModule = () => import('./account/account.module').then(x => x.AccountModule);
const adminModule = () => import('./admin/admin.module').then(x => x.AdminModule);
const businessModule = () => import('./business/business.module').then(x => x.BusinessModule);
const clientModule = () => import('./client/client.module').then(x => x.ClientModule);

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'account', loadChildren: accountModule },
  { path: 'admin', loadChildren: adminModule ,canActivate: [AuthGuard], data: { roles: [Role.Administrator] } },
  { path: 'business', loadChildren: businessModule, canActivate: [AuthGuard], data: { roles: [Role.Business] } },
  { path: 'client', loadChildren: clientModule, canActivate: [AuthGuard], data: { roles: [Role.Client] }},
  { path: 'dashboard', component: DashboardComponent }

  // { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
