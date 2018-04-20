import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-forbidden',
  templateUrl: './forbidden.component.html',
  styleUrls: ['./forbidden.component.scss']
})
export class ForbiddenComponent implements OnInit {
  public message: string;
  public values: any[];

  constructor() {
    this.message = 'ForbiddenComponent constructor';
  }

  ngOnInit() {}
}
