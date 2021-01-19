import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AccountService } from './account.service';
import { AlertService } from './alert.service';

@Injectable({
  providedIn: 'root'
})
export class BusinessService {

  constructor(private accountService: AccountService,
    private http: HttpClient,
    private alertService: AlertService) { }
}
