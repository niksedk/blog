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
  loginErrorMessage: string;

  constructor(private accountService: AccountService,
              private router: Router) { 
  }

  ngOnInit() {
  }

  public login() {
    this.loginErrorMessage = null;
    this.accountService.login(this.email, this.password)
      .subscribe(res => {
        this.router.navigate(['']);
      },
      (err: HttpErrorResponse) => {
        if (err.error instanceof Error) {
          // A client-side or network error occurred. Handle it accordingly.
          if (err.error.message)
            this.loginErrorMessage = err.error.message;
          else
            this.loginErrorMessage = 'Unknown error occured :(';
        } else {
          // The backend returned an unsuccessful response code.
          // The response body may contain clues as to what went wrong,
          this.loginErrorMessage = err.error.message;
        }
      });
  }

  public logout() {
    this.loginErrorMessage = null;
    this.accountService.logout();
  }  

}
