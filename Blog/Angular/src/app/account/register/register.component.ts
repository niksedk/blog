import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../account.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  name: string;
  email: string;
  showEmail: boolean;
  imageLink: string;
  password: string;
  errorMessage: string;
  infoMessage: string;

  constructor(private accountService: AccountService,
    private router: Router) {
  }

  ngOnInit() {
  }

  public registerUser() {
    this.errorMessage = null;
    this.infoMessage = null;
    this.accountService.registerUser(this.name, this.email, this.showEmail, this.imageLink, this.password)
      .subscribe(res => {
        this.infoMessage = 'Thank you for registering :)';
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
  
}
