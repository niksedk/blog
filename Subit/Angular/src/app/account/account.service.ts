import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers, HttpModule } from '@angular/http';
import { TokenResponse } from './models/token-response';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class AccountService {

  baseUrl = 'http://localhost:54882/api/login'; // TODO: move to json setting

  constructor(private http: Http) {
  }

  login(email: string, password: string): Observable<TokenResponse> {
    console.log('calling POST ' + this.baseUrl + ' with ' + email + '/' + password);
    return this.http.post(this.baseUrl, { email, password })
//      .catch((error:any) => Observable.throw(error.json().error || 'Server error'))
      .map(res => {
        const json = res.json();
        return {
          accessToken: json.access_token,
          refreshToken: json.refresh_token,
          tokenType: json.token_type,
          expiresIn: json.expires_in
        };
      });
  }

}