import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { TokenResponse } from './models/token-response';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { User } from './models/user';
import { TokenService } from './token.service';
import { environment } from '../../environments/environment';

@Injectable()
export class AccountService {

  baseUrl = environment.baseUrl + '/users';

  constructor(private http: HttpClient, private tokenService: TokenService) {
  }

  public getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl);
  }

  public deleteUser(userId: number) {
    return this.http.delete(`${this.baseUrl}/${userId}`);
  }

  public registerUser(name: string, email: string, showEmail: boolean, imageLink: string, password: string): Observable<User[]> {
    return this.http.post<User[]>(`${this.baseUrl}/register`, { name, email, showEmail, imageLink, password });
  }

  public login(email: string, password: string): Observable<TokenResponse> {
    const tokenResponse = this.http.post<TokenResponse>(`${this.baseUrl}/login`, { email, password });
    tokenResponse.subscribe(res => {
      this.tokenService.setTokenResponse(res);
    });
    return tokenResponse;
  }

  public logout() {
    this.tokenService.logout();
  }

  private getRandomNumber(min: number, max: number): number // min and max included
  {
    return Math.floor(Math.random() * (max - min + 1) + min);
  }

  // create login request to google with state
  public createLoginViaGoogleUrl() : string {
    let state = String(this.getRandomNumber(1, 999999));
    localStorage.setItem("LoginViaGoogleState", state);
    return 'https://accounts.google.com/o/oauth2/auth?scope=email%20profile&state=' + state + '&redirect_uri=https%3A%2F%2Fsubtitleedit.io%2Flogin%2Foauth2callback&response_type=token&client_id=118671712464-ds9a5skbkqarpj1smebu45bimae83fdk.apps.googleusercontent.com';
  }

  // verifies state created in 'createLoginViaGoogleUrl'
  public isLoginViaGoogleStateValid(state: string) : boolean {
    let savedState = localStorage.getItem('LoginViaGoogleState');
    return savedState == state;
  }
  
  public loginViaGoogleToken(token: string) : Observable<TokenResponse> {
    console.log('loginViaGoogleToken called with token: ' + token);
    console.log('loginViaGoogleToken url: ' + `${this.baseUrl}/google-tokensignin`);
    const tokenResponse = this.http.post<TokenResponse>(`${this.baseUrl}/google-tokensignin`, { token });
    tokenResponse.subscribe(res => {
      this.tokenService.setTokenResponse(res);
    });
    return tokenResponse;    
  }

}
