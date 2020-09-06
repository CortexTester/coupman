import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '@app/services/account.service';
import { AlertService } from '@app/services/alert.service';
import { MustMatch } from '@app/helpers/must-match.validator';
import { first } from 'rxjs/operators';

enum TokenStatus {
  Validating,
  Valid,
  Invalid
}

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {
  token = null;
  email = null
  form: FormGroup;
  loading = false;
  submitted = false;
  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private alertService: AlertService,
  ) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      password: ['', [Validators.required, Validators.minLength(4)]],
      confirmPassword: ['', Validators.required],
    }, {
      validator: MustMatch('password', 'confirmPassword')
    });
    this.token = this.route.snapshot.queryParams['token'];
    this.email = this.route.snapshot.queryParams['email'];
    console.log('token:' + this.token)

    // remove token from url to prevent http referer leakage
    // this.router.navigate([], { relativeTo: this.route, replaceUrl: true });
  }

  get f() { return this.form.controls; }

  onSubmit() {
    this.submitted = true;

    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    this.accountService.resetPassword(this.token, this.email, this.f.password.value, this.f.confirmPassword.value)
      .pipe(first())
      .subscribe({
        next: () => {
          this.alertService.success('Password reset successful, you can now login', { keepAfterRouteChange: true });
          this.router.navigate(['../login'], { relativeTo: this.route });
        },
        error: error => {
          this.alertService.error(error);
          this.loading = false;
        }
      });
  }

}
