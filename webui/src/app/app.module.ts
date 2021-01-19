import { BrowserModule } from '@angular/platform-browser';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { YouTubePlayerModule } from "@angular/youtube-player";


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { FooterComponent } from './shared/footer/footer.component';
import { SpinnerComponent } from './shared/spinner/spinner.component';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AlertComponent } from './shared/alert/alert.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ErrorInterceptor } from "@app/helpers/error.interceptor";
import { JwtInterceptor } from "@app/helpers/jwt.interceptor";
import { AdminModule } from './admin/admin.module';
import { BusinessModule } from './business/business.module';
import { ClientModule } from './client/client.module';
import { HomeComponent } from './home/home.component';

import { FormlyModule } from '@ngx-formly/core';
// import { FormlyBootstrapModule } from "@ngx-formly/bootstrap";
import { FormlyMaterialModule } from '@ngx-formly/material';
import { FieldQuillType } from './shared/form/quill.type'

import { TooltipModule } from "ngx-bootstrap/tooltip";
import { TimepickerModule } from "ngx-bootstrap/timepicker";
import { ModalModule, BsModalRef } from 'ngx-bootstrap/modal';
import { QuillModule } from 'ngx-quill';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormlyFieldNgSelect } from "./shared/form/ngSelect.type";
import { CouponsComponent } from './coupons/coupons.component';
import { CouponComponent } from './shared/coupon/coupon.component';
import { SafeHtmlPipe } from "./shared/safe-html.pipe";
import { TopNavComponent } from './shared/top-nav/top-nav.component';
import { MaterialModule } from "./material/material.module";
import { EditTableComponent } from './shared/edit-table/edit-table.component';
import { EditModeDirective } from './shared/edit-table/edit-mode.directive';
import { EditableOnEnterDirective } from "./shared/edit-table/edit-on-enter.directive";
import { FocusableDirective } from "./shared/edit-table/focusable.directive";


@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    NavbarComponent,
    FooterComponent,
    SpinnerComponent,
    AlertComponent,
    HomeComponent,
    FieldQuillType,
    FormlyFieldNgSelect,
    CouponsComponent,
    CouponComponent,
    SafeHtmlPipe,
    TopNavComponent,
    EditTableComponent,
    EditModeDirective,
    FocusableDirective,
    EditableOnEnterDirective
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ToastrModule.forRoot({
      progressBar: true
    }),
    YouTubePlayerModule,
    AdminModule,
    BusinessModule,
    ClientModule,
    // NgbModule,
    FormlyModule.forRoot({
      extras: { lazyRender: true },
      types: [
        { name: 'quill', component: FieldQuillType, wrappers: ['form-field'] },
        { name: 'ng-select', component: FormlyFieldNgSelect }
      ],
    }),
    FormlyMaterialModule,
    // FormlyBootstrapModule,

    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    QuillModule.forRoot(),
    NgSelectModule,
    MaterialModule,
    TimepickerModule
  ],
  providers: [{ provide: LocationStrategy, useClass: HashLocationStrategy },
  { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    BsModalRef],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule { }
