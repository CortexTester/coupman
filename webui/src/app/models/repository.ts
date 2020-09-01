import { User } from "./user.model";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';

@Injectable()
export class Repository {
    baseUrl = 'http://localhost:5000/' //call local
    //baseUrl = 'https://coupmanapi.azurewebsites.net/' //call web app
    // baseUrl = 'http://klickon.canadacentral.cloudapp.azure.com:5051/'
    party$: Observable<User>;

    constructor(private http: HttpClient) {
        // this.party$ = this.http.get<User>(`${this.baseUrl}Party/1`)
    }

    register(formModel) {
        var body = {
            UserName: formModel.value.UserName,
            Email: formModel.value.Email,
            FirstName: formModel.value.FirstName,
            LastName: formModel.value.LastName,
            Password: formModel.value.Passwords.Password
        };
        return this.http.post(this.baseUrl + 'user/register', body);
    }

    login(formData): Observable<User> {
        return this.http.post(this.baseUrl + 'user/login', formData);
      }
}