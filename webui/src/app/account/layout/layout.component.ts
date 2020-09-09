import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { AccountService } from '@app/services/account.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {
  isRegisterPro: boolean = false
  constructor(
    private router: Router,
    private accountService: AccountService) {
    router.events.forEach((event) => {
      if (event instanceof NavigationEnd) {
        this.isRegisterPro = this.router.url?.toLocaleLowerCase() == "/account/registerpro"
      }
    });
    if (this.accountService.accountValue) {
      this.router.navigate(['/']);
    }
  }

  ngOnInit(): void {
    this.isRegisterPro = this.router.url?.toLocaleLowerCase() == "/account/registerpro"
  }

}
