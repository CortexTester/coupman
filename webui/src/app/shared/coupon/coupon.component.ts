import { Component, Input, OnInit } from '@angular/core';
import { Coupon } from '@app/models/coupon';

@Component({
  selector: 'app-coupon',
  templateUrl: './coupon.component.html',
  styleUrls: ['./coupon.component.scss']
})
export class CouponComponent implements OnInit {
  name = "test"
  @Input() coupon: Coupon
  constructor() { }

  ngOnInit(): void {
  }

}
