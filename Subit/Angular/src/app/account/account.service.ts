import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers, HttpModule } from '@angular/http';
import { TokenResponse } from './models/token-response';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { User } from './models/user';

@Injectable()
export class AccountService {

  baseUrl = 'http://localhost:54882/api'; // TODO: move to json setting

  constructor(private http: Http) {
  }

  public getUsers(): Observable<User[]> {
    return this.http.get(this.baseUrl + '/users', { headers: this.createAuthorizationHeader() }).map(res => <User[]>res.json());
  }

  public deleteUser(userId: number) {
    return this.http.delete(this.baseUrl + '/users/' + userId, { headers: this.createAuthorizationHeader() });
  }

  private setTokenResponse(tokenResponse: TokenResponse) {
    localStorage.setItem('tokenResponse', JSON.stringify(tokenResponse));
  }

  public getTokenResponse(): TokenResponse {
    const s = localStorage.getItem('tokenResponse');
    const tokenResponse: TokenResponse = JSON.parse(s);
    return tokenResponse;
  }  

  private createAuthorizationHeader() : Headers {
    let headers = new Headers();
    headers.append('Authorization', 'Bearer ' + this.getTokenResponse().accessToken); 
    return headers;
  }

  public login(email: string, password: string): Observable<TokenResponse> {
    return this.http.post(this.baseUrl + '/users/login', { email, password })
      .map(res => {
        const json = res.json();
        const tokenResponse = {
          accessToken: json.access_token,
          refreshToken: json.refresh_token,
          tokenType: json.token_type,
          expiresIn: json.expires_in
        };
        this.setTokenResponse(tokenResponse);
        return tokenResponse;
      });
  }

}