import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  email: string;

  constructor(private route: ActivatedRoute) {
    this.route.params.subscribe(res =>  this.email = res.email);
   }

  ngOnInit() {
  }

}
