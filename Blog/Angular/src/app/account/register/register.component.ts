import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from '../account.service';

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

  constructor(private accountService: AccountService) { 
  }

  ngOnInit() {
  }

  public registerUser() {
    console.log('Calling register user');
    this.accountService.registerUser(this.name, this.email, this.showEmail, this.imageLink, this.password)
    .subscribe(res => {
        console.log(res);
    });
  }

}
