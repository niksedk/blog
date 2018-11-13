import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../account.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-google-login',
  templateUrl: './google-login.component.html',
  styleUrls: ['./google-login.component.scss']
})
export class GoogleLoginComponent implements OnInit {

  accessToken: string;
  tokenType: string;
  expiresIn: string;
  error: string;
  infoMessage: string;

  constructor(private accountService: AccountService,
    private route: ActivatedRoute,
    private router: Router) { }

  getFragmentParam(paramName : string) {
    let name = paramName.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"), results = regex.exec(this.route.snapshot.fragment);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " ")); 
  }

  ngOnInit() {
    this.accessToken = this.getFragmentParam('access_token');
    this.tokenType = this.getFragmentParam('token_type');
    this.expiresIn = this.getFragmentParam('expires_in');
    this.error = this.getFragmentParam('error');
    console.log('accessToken: ' + this.accessToken);

    if (this.accessToken) {
      console.log('accessToken found: ' + this.accessToken);
      this.accountService.loginViaGoogleToken(this.accessToken)
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
            this.error = err.error.message;
          else
            this.error = 'Unknown error occured :(';
        } else {
          // The backend returned an unsuccessful response code.
          // The response body may contain clues as to what went wrong
          if (err.error.message)
            this.error = err.error.message;
          else
            this.error = 'Unknown error occured :(';          
        }
      });
    } else {
      console.log('accessToken not found: ' + this.accessToken);
        this.error = "Unable to login - no token from Google :(";
    }
  }
}
