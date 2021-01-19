import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Coupon } from '@app/models/coupon';
import { CouponService } from '@app/services/coupon.service';

@Component({
  selector: 'app-coupons',
  templateUrl: './coupons.component.html',
  styleUrls: ['./coupons.component.scss']
})
export class CouponsComponent implements OnInit {
  coupons:Coupon[]
  constructor(private route: ActivatedRoute, private couponService:CouponService) {
    // this.cityId = this.route.snapshot.queryParamMap.get('cityId')
    // this.categoryId = this.route.snapshot.queryParamMap.get('categoryId')
    
  }

  ngOnInit(): void {
    this.couponService.getAllCouponsByCityIdCategoryId().subscribe(c=>this.coupons = c)
  }

}
