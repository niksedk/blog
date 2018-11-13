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
  window.location.href = this.accountService.createLoginViaGoogleUrl();
}

  public logout() {
    this.errorMessage = null;
    this.accountService.logout();
    this.infoMessage = 'You are now logged out';
  }  

}
