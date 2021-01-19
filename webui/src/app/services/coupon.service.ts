import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { AccountService } from './account.service';
import { Coupon } from "./../models/coupon";
import { environment } from '@environments/environment';
import { first, map } from 'rxjs/operators';
import { AlertService } from './alert.service';
import { Category } from '@app/models/category';
import { Filter, Pagination } from "./../models/filterPaging";
import { City } from '@app/models/city';
const baseUrl = `${environment.apiUrl}`
const baseCouponUrl = `${environment.apiUrl}/coupon/`;

@Injectable({ providedIn: 'root' })
export class CouponService {
  private couponsSubject: BehaviorSubject<Coupon[]>;
  private couponSubject: BehaviorSubject<Coupon>;
  public coupon: Observable<Coupon>;
  public coupons: Observable<Coupon[]>;
  filter: Filter = new Filter();
  paginationObject = new Pagination();

  constructor(
    private accountService: AccountService,
    private http: HttpClient,
    private alertService: AlertService) {

    this.couponSubject = new BehaviorSubject<Coupon>(null)
    this.coupon = this.couponSubject.asObservable()
    this.couponsSubject = new BehaviorSubject<Coupon[]>(null)
    this.coupons = this.couponsSubject.asObservable()
  }

  getAllCouponsByAccount() {
    return this.http.get<Coupon[]>(baseCouponUrl + 'getCouponsByAccountId')
      .pipe(first())
      .subscribe(coupons => {
        this.couponsSubject.next(coupons)
      })
  }

  getAllCouponsByCityIdCategoryId() {
    return this.http.get<Coupon[]>(baseUrl + "/coupon?cityId=" + this.filter.cityId + '&categoryid=' + this.filter.categoryId)

  }

  deleteCoupon(couponId: number) {
    this.http.delete(`${baseCouponUrl}${couponId}`)
      .subscribe(() => this.getAllCouponsByAccount())
  }

  getCoupon(couponId: number) {
    this.http.get<Coupon>(`${baseCouponUrl}GetCoupon/${couponId}`)
      .subscribe({
        next: coupon => {
          this.couponSubject.next(coupon)
        },
        error: error => this.alertService.error(error)
      })
  }

  newCoupon(coupon: Coupon) {
    return this.http.post<Coupon>(`${baseCouponUrl}createCoupon`, coupon)
  }

  updateCoupon(coupon: Coupon) {
    return this.http.put(`${baseCouponUrl}${coupon.couponId}`, coupon)
  }

  getCategories() {
   return  this.http.get<Category[]>(`${baseUrl}/setting/getCategories`)
  }

  getCities() {
    return  this.http.get<City[]>(`${baseUrl}/setting/getCities`)
   }

}
