import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

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

  constructor(private route: ActivatedRoute) { }

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

    // post to server...
  }
}
