// see https://medium.com/@ryanchenkie_40935/angular-authentication-using-the-http-client-and-http-interceptors-2f9d1540eb8
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
  } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import Accountservice = require("./account.service");
import AccountService = Accountservice.AccountService;

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(public accountService: AccountService) { }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const accessToken = this.accountService.getTokenResponse().access_token;
    if (accessToken) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${accessToken}`
        }
      });
    }
    return next.handle(request);
  }
}
