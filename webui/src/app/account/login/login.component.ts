import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '@app/services/account.service';
import { AlertService } from '@app/services/alert.service';
import { first } from 'rxjs/operators';
import { Role } from '@app/models/role';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  loading = false;
  submitted = false;
  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private alertService: AlertService
  ) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      userName: ['', [Validators.required]],
      password: ['', Validators.required]
    });
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
    this.accountService.login(this.f.userName.value, this.f.password.value)
      .pipe(first())
      .subscribe({
        next: (account) => {
          // get return url from query parameters or default to home page
          if(this.route.snapshot.queryParams['returnUrl']) this.router.navigateByUrl(this.route.snapshot.queryParams['returnUrl']);
          if(account.role == Role.Administrator) this.router.navigate(['/admin'])
          if(account.role == Role.Business) this.router.navigate(['/business'])
          if(account.role == Role.Client) this.router.navigate(['/client'])
        },
        error: error => {
          this.alertService.error(error);
          this.loading = false;
        }
      });
  }
}
