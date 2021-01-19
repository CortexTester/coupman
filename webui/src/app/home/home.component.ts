import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Category } from '@app/models/category';
import { City } from '@app/models/city';
import { CouponService } from '@app/services/coupon.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  categories: Category[]
  cities: City[]
  selectedCategoryId: number = 0
  selectedCityId: number = 0

  constructor(private router: Router, private couponServie: CouponService) { }

  ngOnInit(): void {
    this.couponServie.getCities().subscribe(c => this.cities = [{ cityId: 0, name: 'select location' }, ...c])
    this.couponServie.getCategories().subscribe(c => this.categories = [{ categoryId: 0, name: 'select category' }, ...c])
  }
  setCategoryId(id: number) {
    this.selectedCategoryId = id
    this.couponServie.filter.categoryId = this.selectedCategoryId
  }
  setCityId(id: number) {
    this.selectedCityId = id
    this.couponServie.filter.cityId = this.selectedCityId
  }
  search() {

    this.router.navigate(['/coupons'])//, { queryParams: { categoryId: this.selectedCategoryId, cityId: this.selectedCityId } })
  }
}
