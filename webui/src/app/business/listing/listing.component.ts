import { Component, OnInit, TemplateRef, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Coupon } from '@app/models/coupon';
// import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { FormlyFieldConfig, FormlyFormOptions } from '@ngx-formly/core';
import { CouponService } from "./../../services/coupon.service";
import { FormConfig } from "./../../shared/form/formConfig";
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AlertService } from '@app/services/alert.service';
import { first } from 'rxjs/operators';
import { textChangeRangeIsUnchanged } from 'typescript';
declare var $: any;

@Component({
  selector: 'app-listing',
  templateUrl: './listing.component.html',
  encapsulation: ViewEncapsulation.None,
  styleUrls: ['./listing.component.scss']
})
export class ListingComponent implements OnInit {

  coupons: Coupon[]
  coupon: Coupon
  closeResult = ''
  form = new FormGroup({})
  options: FormlyFormOptions = {}
  formTitle = ''
  isLoading = false
  isAddMode: boolean;
  modalRef: BsModalRef
  // model:any

  constructor(
    private couponService: CouponService,
    private formConfig: FormConfig,
    private modalService: BsModalService,
    private alertService: AlertService) { }

  ngOnInit(): void {
    this.couponService.coupons.subscribe(coupons => {
      this.coupons = coupons
      this.isLoading = false
    })
    this.couponService.coupon.subscribe(coupon => {
      this.coupon = coupon
      this.isLoading = false
    })

    this.couponService.getAllCouponsByAccount()
  }


  updateList(id: number, property: string, event: any) {
    const editField = event.target.textContent;
    this.coupons[id][property] = editField;
  }

  delete(couponId: number) {
    if (confirm('Are you sure?')) {
      const deletingCoupon: any = this.coupons.find(x => x.couponId === couponId)
      this.couponService.deleteCoupon(couponId)
    }
  }

  add(template: TemplateRef<any>) {
    this.isAddMode = true
    this.setTitle()
    this.coupon = new Coupon()
    this.openPopup(template)
  }

  edit(template: TemplateRef<any>, couponId: number) {
    this.isAddMode = false
    this.isLoading = true
    this.setTitle()
    this.couponService.getCoupon(couponId)

    this.openPopup(template)
  }

  changeValue(id: number, property: string, event: any) {
  }


  //model 

  openPopup(template: TemplateRef<any>) {
    // $("#myModal").modal("show", );
    // modalService.
    this.modalRef = this.modalService.show(template, { class: 'modal-xl' });
  }

  fields: FormlyFieldConfig[] = this.formConfig.getCouponFormConfig()


  onSubmit() {
    console.log(JSON.stringify(this.coupon));
    this.alertService.clear();
    this.isLoading = true
    if (this.isAddMode) {
      this.couponService.newCoupon(this.coupon)
        .pipe(first())
        .subscribe({
          next: coupon => {
            this.alertService.success('the coupon was created')
            this.couponService.getAllCouponsByAccount()
            this.modalRef.hide()
          },
          error: error => this.alertService.error(error)
        })
    } else {
      this.couponService.updateCoupon(this.coupon)
        .subscribe({
          next: coupon => {
            this.alertService.success('the coupon was updated')
            this.couponService.getAllCouponsByAccount()
            this.modalRef.hide()
          },
          error: error => this.alertService.error(error)
        })
    }

    // if (this.form.valid) {
    //   this.http.post('url', this.model, null).subscribe((response) => {
    //     console.log('response:', response)
    //   }, (error) => {
    //     console.error('error:', error)
    //   })
    // }
  }

  setTitle() {
    if (this.isAddMode)
      this.formTitle = 'create a coupon'
    else
      this.formTitle = 'edit a coupon'
  }
}
