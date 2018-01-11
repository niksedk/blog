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

  baseUrl : string = 'http://localhost:54882/api'; // TODO: move to json setting

  constructor(private http: HttpClient, private tokenService: TokenService) {
  }

  public getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseUrl}/users`);
  }

  public deleteUser(userId: number) {
    return this.http.delete(`${this.baseUrl}/users/${userId}`);
  }

  public registerUser(name: string, email: string, showEmail: boolean, imageLink: string, password: string): Observable<User[]> {
    return this.http.post<User[]>(`${this.baseUrl}/users/register`, { name, email, showEmail, imageLink, password });
  }

  public login(email: string, password: string): Observable<TokenResponse> {
    var tokenResponse = this.http.post<TokenResponse>(`${this.baseUrl}/users/login`, { email, password });
    tokenResponse.subscribe(res => {
      this.tokenService.setTokenResponse(res);
    });
    return tokenResponse;
  }

  public logout() {
    this.tokenService.logout();
  }
}