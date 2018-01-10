import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { User } from '../../account/models/user';
import { AccountService } from '../../account/account.service';

@Component({
  selector: 'app-user-manage',
  templateUrl: './user-manage.component.html',
  styleUrls: ['./user-manage.component.scss']
})
export class UserManageComponent implements OnInit {

  public users: Observable<User[]>;

  constructor(private accountService: AccountService) { 
    this.users = accountService.getUsers();
  }

  ngOnInit() {
  }

  public deleteUser(user: User) {
    this.accountService.deleteUser(user.userId)
    .subscribe(res => {
      this.users = this.users.map(u => u.filter(b => b.userId !== user.userId));
    });
  }

}
