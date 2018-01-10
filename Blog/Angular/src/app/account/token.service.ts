import { Injectable } from '@angular/core';
import { TokenResponse } from './models/token-response';

@Injectable()
export class TokenService {

  constructor() { }
  
  public setTokenResponse(tokenResponse: TokenResponse) {
    localStorage.setItem('tokenResponse', JSON.stringify(tokenResponse));
  }

  public getTokenResponse(): TokenResponse {
    const s = localStorage.getItem('tokenResponse');
    const tokenResponse: TokenResponse = JSON.parse(s);
    return tokenResponse;
  }  
}
