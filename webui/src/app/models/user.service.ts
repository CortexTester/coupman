import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Repository } from "./repository";
import { Observable } from 'rxjs/internal/Observable';
import { User } from './user.model';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class UserService {
  baseUrl = 'http://localhost:5000/'
  // baseUrl = 'http://klickon.canadacentral.cloudapp.azure.com:5051/'
  constructor(private fb: FormBuilder, private repository: Repository, private http: HttpClient) { }
  formModel = this.fb.group({
    UserName: ['', Validators.required],
    Email: ['', Validators.email],
    FirstName: [''],
    LastName: [''],
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(4)]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.comparePasswords }),
    AcceptTerms: [false, Validators.requiredTrue]
  });

  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get('ConfirmPassword');
    //passwordMismatch
    //confirmPswrdCtrl.errors={passwordMismatch:true}
    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (fb.get('Password').value != confirmPswrdCtrl.value)
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      else
        confirmPswrdCtrl.setErrors(null);
    }
  }

  login(formData): Observable<User> {
    return this.repository.login(formData);
  }

  // register() {
  //  return this.repository.register(this.formModel)
  // }

  register() {
    var body = {
      UserName: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      FirstName: this.formModel.value.FirstName,
      LastName: this.formModel.value.LastName,
      Password: this.formModel.value.Passwords.Password
    };
    return this.http.post(this.baseUrl + 'user/register', body);
  }

}