import { Component, OnInit } from '@angular/core';
import { AccountService } from './account.service';
import 'rxjs/add/operator/catch';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss']
})
export class AccountComponent implements OnInit {

  constructor(private accountService: AccountService) { }

  ngOnInit() {
  }

}
