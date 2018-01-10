import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { TokenResponse } from './models/token-response';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { User } from './models/user';
import { TokenService } from './token.service';

@Injectable()
export class AccountService {

  baseUrl = 'http://localhost:54882/api'; // TODO: move to json setting

  constructor(private http: HttpClient, private tokenService: TokenService) {
  }

  public getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + '/users');
//    return this.http.get<User[]>(this.baseUrl + '/users', { headers: this.createAuthorizationHeader() });
  }

  public deleteUser(userId: number) {
    return this.http.delete(this.baseUrl + '/users/' + userId);
//    return this.http.delete(this.baseUrl + '/users/' + userId, { headers: this.createAuthorizationHeader() });
  }

  // private setTokenResponse(tokenResponse: TokenResponse) {
  //   localStorage.setItem('tokenResponse', JSON.stringify(tokenResponse));
  // }

  // public getTokenResponse(): TokenResponse {
  //   const s = localStorage.getItem('tokenResponse');
  //   const tokenResponse: TokenResponse = JSON.parse(s);
  //   return tokenResponse;
  // }  

  public logout() {
      localStorage.removeItem("tokenResponse")
  }

  // private createAuthorizationHeader() : HttpHeaders {   
  //   return new HttpHeaders().set('Authorization', 'Bearer ' + this.getTokenResponse().access_token);
  // }

  public login(email: string, password: string): Observable<TokenResponse> {
    var tokenResponse = this.http.post<TokenResponse>(this.baseUrl + '/users/login', { email, password });    
    tokenResponse.subscribe(res => {
      this.tokenService.setTokenResponse(res);
    });
    return tokenResponse;
  }

}