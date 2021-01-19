import { Injectable, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

import { Account } from '@app/models/account';
import { environment } from '@environments/environment';
import { map, finalize } from 'rxjs/operators';
import { Role } from '@app/models/role';
import { TokenStorageService } from "./token-storage.service";

const baseUrl = `${environment.apiUrl}/authentication`;

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private accountSubject: BehaviorSubject<Account>;
  public account: Observable<Account>;
  constructor(
    private router: Router,
    private http: HttpClient,
    private tokenStorage: TokenStorageService
  ) {
    this.accountSubject = new BehaviorSubject<Account>(null);
    this.account = this.accountSubject.asObservable();
  }

  public get accountValue(): Account {
    if (this.tokenStorage.getAccount()) {
      let savedAccount: Account = this.tokenStorage.getAccount()
      this.accountSubject.next(savedAccount);
    }
    return this.accountSubject.value;
  }

  login(userName: string, password: string) {
    return this.http.post<any>(`${baseUrl}/authenticate`, { userName, password }, { withCredentials: true })
      .pipe(map(account => {
        this.accountSubject.next(account);
        this.tokenStorage.saveToken(account.jwtToken);
        this.tokenStorage.saveAccount(account);
        return account;
      }));
  }

  logout() {
    this.http.post<any>(`${baseUrl}/logout`, {}, { withCredentials: true }).subscribe();
    this.accountSubject.next(null);
    this.tokenStorage.signOut()
    this.router.navigate(['/account/login']);
  }

  register(account: Account) {
    return this.http.post(`${baseUrl}/register`, account);
  }

  forgotPassword(email: string) {
    return this.http.post(`${baseUrl}/forgotPassword`, { email });
  }

  resetPassword(token: string, email: string, password: string, confirmPassword: string) {
    return this.http.post(`${baseUrl}/resetPassword`, { token, email, password, confirmPassword });
  }
}
