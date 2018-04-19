import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Headers, Response, RequestOptions, Http } from '@angular/http';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-call-api',
  templateUrl: './call-api.component.html',
  styleUrls: ['./call-api.component.css']
})
export class CallApiComponent implements OnInit {
  response: any;
  constructor(private http: HttpClient, private authService: AuthService) {}

  ngOnInit() {
    const header = new HttpHeaders({
      Authorization: this.authService.getAuthorizationHeaderValue()
    });
    // const options = new RequestOptions({ headers: header });
    this.http.get('http://localhost:44350/api', { headers: header }).subscribe(
      response => {
        console.log(response);
        this.response = response;
      },
      err => {
        console.error(err);
      }
    );
  }
}
