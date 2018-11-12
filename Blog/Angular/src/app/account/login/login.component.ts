import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { TokenService } from '../token.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  email: string;
  password: string;
  errorMessage: string;
  infoMessage: string;

  constructor(private accountService: AccountService,
              private router: Router) { 
  }

  ngOnInit() {
  }

  public login() {
    this.errorMessage = null;
    this.infoMessage = null;
    this.accountService.login(this.email, this.password)
      .subscribe(res => {
        this.infoMessage = 'You are now logged in :)';
        setTimeout(() => {
          this.router.navigate(['']);
        }, 2000);
      },
      (err: HttpErrorResponse) => {
        if (err.error instanceof Error) {
          // A client-side or network error occurred. Handle it accordingly.
          if (err.error.message)
            this.errorMessage = err.error.message;
          else
            this.errorMessage = 'Unknown error occured :(';
        } else {
          // The backend returned an unsuccessful response code.
          // The response body may contain clues as to what went wrong
          if (err.error.message)
            this.errorMessage = err.error.message;
          else
            this.errorMessage = 'Unknown error occured :(';          
        }
      });
  }

public loginViaGoogle() {
  window.location.href = 'https://accounts.google.com/o/oauth2/auth?scope=email%20profile&state=1847377&redirect_uri=https%3A%2F%2Fsubtitleedit.io%2Flogin%2Foauth2callback&response_type=token&client_id=118671712464-ds9a5skbkqarpj1smebu45bimae83fdk.apps.googleusercontent.com';
}

  public logout() {
    this.errorMessage = null;
    this.accountService.logout();
    this.infoMessage = 'You are now logged out';
  }  

}
