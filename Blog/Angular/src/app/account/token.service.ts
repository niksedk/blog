import { Injectable } from '@angular/core';
import { TokenResponse } from './models/token-response';

@Injectable()
export class TokenService {

  constructor() { }

  public setTokenResponse(tokenResponse: TokenResponse) {
    localStorage.setItem('tokenResponse', JSON.stringify(tokenResponse));
  }

  public logout() {
    localStorage.removeItem('tokenResponse');
  }

  public getTokenResponse(): TokenResponse {
    const s = localStorage.getItem('tokenResponse');
    if (s === null)
      return null;
    const tokenResponse: TokenResponse = JSON.parse(s);
    return tokenResponse;
  }

  private base64Decode(str): string {
    return decodeURIComponent(Array.prototype.map.call(atob(str), function (c) {
      return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
    }).join(''))
  }

  private getTokePayload() {
    let tokenResponse = this.getTokenResponse();
    if (!tokenResponse || !tokenResponse.access_token)
      return null;
    var rawPayload = tokenResponse.access_token.split('.')[1];
    var payloadString = this.base64Decode(rawPayload);
    return JSON.parse(payloadString);
  }

  // check if user has a valid jwt token (exp)
  public hasValidToken(): boolean {
    let payload = this.getTokePayload();
    if (!payload)
      return false;
    if (!payload.exp)
      return false;

    let expirationDate = new Date(payload.exp * 1000).getTime();
    let remaining = expirationDate - new Date().getTime();
    return remaining > 0;
  }

  public getTokenUserId(): number {
    let payload = this.getTokePayload();
    if (!payload)
      return null;
    if (!payload.nameid)
      return null;

    return parseInt(payload.nameid);
  }

  public getTokenName(): string {
    let payload = this.getTokePayload();
    if (!payload)
      return null;
    if (!payload.name)
      return null;

    return payload.name;
  }

  public isAdministrator(): boolean {
    let payload = this.getTokePayload();
    if (!payload)
      return false;
    if (!payload.role)
      return false;

    if (payload.role.constructor === Array)
      return payload.role.includes('admin');

    return payload.role === 'admin';
  }

}
