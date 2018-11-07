import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Referrer } from '../models/Referrer';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-referrer-manage',
  templateUrl: './referrer-manage.component.html',
  styleUrls: ['./referrer-manage.component.scss']
})
export class ReferrerManageComponent implements OnInit {

  baseUrl = '/api/Logs/Referrers'; // TODO: move to json setting

  public referrers: Referrer[];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.get<Referrer[]>(`${this.baseUrl}`)
      .subscribe(res => this.referrers = res);
  }

}
