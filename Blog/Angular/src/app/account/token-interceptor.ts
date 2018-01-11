// see https://medium.com/@ryanchenkie_40935/angular-authentication-using-the-http-client-and-http-interceptors-2f9d1540eb8
import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { TokenService } from './token.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(public tokenService: TokenService) { }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let tokenResponse = this.tokenService.getTokenResponse();
    if (tokenResponse && tokenResponse.access_token) {
      request = request.clone({
        setHeaders: { Authorization: `Bearer ${tokenResponse.access_token}` }
      });
    }
    return next.handle(request);
  }
}
