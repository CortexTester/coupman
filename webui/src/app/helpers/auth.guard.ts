import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AccountService } from '@app/services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private accountService: AccountService
) { }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      const account = this.accountService.accountValue;
      if (account) {
        // check if route is restricted by role
        if (next.data.roles && !next.data.roles.includes(account.role)) {
            // role not authorized so redirect to home page
            this.router.navigate(['/']);
            return false;
        }

        // authorized so return true
        return true;
    }

    // not logged in so redirect to login page with the return url 
    this.router.navigate(['/account/login'], { queryParams: { returnUrl: state.url }});
    return false;
  }
  
}
