import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '@app/services/account.service';
import { AlertService } from '@app/services/alert.service';
import { Account } from '@app/models/account';
import { Profile } from "./../../models/profile";

export interface HoursOperation {
  day: string
  from?: string,
  to?: string,
  text?: string
}

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  nameForm: FormGroup;
  imagesForm: FormGroup
  videosArray: FormArray
  
  hoursForm: FormGroup[]
  hoursArray: FormArray

  isAddMode: boolean;
  submitted = false;
  loading = false;
  account: Account
  isLinear = true
  profile: Profile
  images = [];

  hoursDisplayedColumns: string[] = ['day', 'from', 'to', 'text'];
  hoursDataSource: HoursOperation[] = [
    { day: 'Monday', from: new Date().toTimeString(), to: '11:00', text: '' },
    { day: 'Tuesday', from: new Date().toTimeString(), to: '', text: '' },
     { day: 'Wenesday', from: new Date().toTimeString(), to: '', text: '' },
    { day: 'Thursday', from: new Date().toTimeString(), to: '', text: '' },
    { day: 'Friday', from: new Date().toTimeString(), to: '', text: '' },
    { day: 'Saturday', from: new Date().toTimeString(), to: '', text: '' },
    { day: 'Sunday', from: new Date().toTimeString(), to: '', text: '' },
  ]
  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private alertService: AlertService
  ) {
    this.account = accountService.accountValue
    this.profile = new Profile()
  }

  ngOnInit(): void {
    this.isAddMode = this.account.status == "Empty" ? true : false
    this.nameForm = this.fb.group({
      name: ['', Validators.required],
      description: ['']
    })
    this.imagesForm = this.fb.group({
      file: [''],
      fileSource: ['']
    });
    this.videosArray = this.fb.array([]);

    this.hoursForm = this.hoursDataSource.map(d => {
      return new FormGroup({
        day:  new FormControl(d.day),
        from: new FormControl(d.from),
        to:  new FormControl(d.to),
        text:  new FormControl(d.text)
      }, { updateOn: "blur" })
    })
    this.hoursArray = this.fb.array(this.hoursForm)
  }

  //videos
  addVideoUrl() {
    this.videosArray.push(new FormControl(''))
  }
  removeVideoUrl(index: number) {
    this.videosArray.removeAt(index)
  }
  //end videos

  //hours
  updateField(index, field) {
    const control = this.getControl(index, field);
    if (control.valid) {
      // this.core.update(index,field,control.value);
    }

  }

  getControl(index, fieldName) {
    const a = this.hoursArray.at(index).get(fieldName) as FormControl;
    return this.hoursArray.at(index).get(fieldName) as FormControl;
  }

  updateList(id: number, property: string, event: any) {
    const editField = event.target.textContent;
    // this.coupons[id][property] = editField;
  }
  changeValue(id: number, property: string, event: any) {
  }
  //end hours

  // get f() { return this.form.controls; }
  onSubmit() {
    this.submitted = true;

    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    // if (this.form.invalid) {
    //   return;
    // }
    this.loading = true;

    if (this.isAddMode) {
      this.createAccount();
    } else {
      this.updateAccount();
    }


  }
  private createAccount() {
    // this.accountService.create(this.form.value)
    //     .pipe(first())
    //     .subscribe({
    //         next: () => {
    //             this.alertService.success('Account created successfully', { keepAfterRouteChange: true });
    //             this.router.navigate(['../'], { relativeTo: this.route });
    //         },
    //         error: error => {
    //             this.alertService.error(error);
    //             this.loading = false;
    //         }
    //     });
  }

  private updateAccount() {
    // this.accountService.update(this.id, this.form.value)
    // .pipe(first())
    // .subscribe({
    //     next: () => {
    //         this.alertService.success('Update successful', { keepAfterRouteChange: true });
    //         this.router.navigate(['../../'], { relativeTo: this.route });
    //     },
    //     error: error => {
    //         this.alertService.error(error);
    //         this.loading = false;
    //     }
    // });
  }

  // submit() {

  //   console.log(this.firstFormGroup.value);

  //   console.log(this.secondFormGroup.value);

  // }

  get f() {
    return this.imagesForm.controls;
  }
  onFileChange(event) {
    if (event.target.files && event.target.files[0]) {
      var filesAmount = event.target.files.length;
      for (let i = 0; i < filesAmount; i++) {
        var reader = new FileReader();

        reader.onload = (event: any) => {
          console.log(event.target.result);
          this.images.push(event.target.result);

          this.imagesForm.patchValue({
            fileSource: this.images
          });
        }

        reader.readAsDataURL(event.target.files[i]);
      }
    }
  }

}
