import { Component, OnDestroy, OnInit } from '@angular/core';
import { MediaObserver, MediaChange } from '@angular/flex-layout';
import { AccountService } from '@app/services/account.service';
import { CouponService } from '@app/services/coupon.service';
import { Subscription } from 'rxjs';
import { Account } from '@app/models/account';
import { Router } from '@angular/router';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit, OnDestroy {
  account:Account
  profileOnly:boolean = true
  // sideNavOpened = true;
  // sideNavMode: 'side' | 'over' = 'side';
  // toolBarHeight = 64;
  // private readonly mediaWatcher: Subscription;
  // constructor(media: MediaObserver) {
  //   this.mediaWatcher = media.media$.subscribe((change: MediaChange) => {
  //     if (change.mqAlias === 'sm' || change.mqAlias === 'xs') {
  //       if (this.sideNavOpened) {
  //         this.sideNavOpened = false;
  //       }
  //       this.sideNavMode = 'over';
  //     } else {
  //       this.sideNavOpened = true;
  //       this.sideNavMode = 'side';
  //     }
  //     if (change.mqAlias === 'xs') {
  //       this.toolBarHeight = 56;
  //     } else {
  //       this.toolBarHeight = 64;
  //     }
  //   });
  //  }
  ngOnDestroy(): void {
    // this.mediaWatcher.unsubscribe();
  }

  constructor(private accountService: AccountService, private router: Router){
    this.account = accountService.accountValue
  }

  ngOnInit(): void {
    if(this.account.status == "Empty"){
      this.profileOnly = true
      this.router.navigate(['/business/profile'])
    }else{
      this.profileOnly = false
      this.router.navigate(['/business/coupons'])
    }

  }

}
