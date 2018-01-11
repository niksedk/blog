import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  email: string;
  password: string;

  constructor(private accountService: AccountService,
              private router: Router) { }

  ngOnInit() {
  }

  login() {
    this.accountService.login(this.email, this.password).subscribe(res => {
      this.router.navigate(['']);
      },
      err => {
       // console.log(`Error message: ${err.error.error}`);
      }
    );
  }

}
