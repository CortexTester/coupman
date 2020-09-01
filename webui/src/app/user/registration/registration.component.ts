import { Component, OnInit } from '@angular/core';
import { UserService } from './../../models/user.service'
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

constructor(public service: UserService, private toastr: ToastrService) { }
ngOnInit(): void {
}
onSubmit() {
  this.service.register().subscribe(
    (res: any) => {
      if (res.succeeded) {
        // this.service.formModel.reset();
        alert('You have successfully registered on our website, Please check your email and click on the link we sent you to verify your email address.')
        this.toastr.success('You have successfully registered on our website, Please check your email and click on the link we sent you to verify your email address.');
      } else {
        res.errors.forEach(element => {
          switch (element.code) {
            case 'DuplicateUserName':
              alert('Username is already taken')
              this.toastr.error('Username is already taken', 'Registration failed.');
              break;

            default:
              alert(element.description)
              this.toastr.error(element.description, 'Registration failed.');
              break;
          }
        });
      }
    },
    err => {
      alert(err)
      console.log(err);
    }
  );
}
// onSubmit(){
//   this.toastr.success('You have successfully registered on our website, Please check your email and click on the link we sent you to verify your email address.');
// }
}
