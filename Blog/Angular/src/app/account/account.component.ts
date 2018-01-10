import { Component, OnInit } from '@angular/core';
import { AccountService } from './account.service';
import 'rxjs/add/operator/catch';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss']
})
export class AccountComponent implements OnInit {

  email: string;
  password: string;
  passwordsUsed = [];

  constructor(private accountService: AccountService) { }

  ngOnInit() {
    this.email = 'no@email.com';
    this.password = 'password';
  }

  login() {
    this.passwordsUsed.push('Logging in...');
    this.accountService.login(this.email, this.password).subscribe(res => {
      this.passwordsUsed.push(`AccessToken: ${res.access_token}`);
    },
      err => {
        const details = err.json().error;
        this.passwordsUsed.push(details);
        console.log(`Error message: ${details}`);
      }
    );
  }

}
