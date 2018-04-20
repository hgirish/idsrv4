import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, OnDestroy {
  message: string;
  name: 'none';
  email: 'none';
  isAuthorized: boolean;
  userDataSubscription: Subscription;
  userData: boolean;
  isAuthorizedSubscription: Subscription;

  constructor(public oidcSecurityService: OidcSecurityService) {
    this.message = 'HomeComponent constructor';
  }

  ngOnInit() {
    this.isAuthorizedSubscription = this.oidcSecurityService
      .getIsAuthorized()
      .subscribe((isAuthorized: boolean) => {
        this.isAuthorized = isAuthorized;
      });

    this.userDataSubscription = this.oidcSecurityService
      .getUserData()
      .subscribe((userData: any) => {
        if (userData && userData !== '') {
          this.name = userData.name;
          this.email = userData.email;
        }
        console.log('userData getting data');
      });
  }

  ngOnDestroy() {
    this.userDataSubscription.unsubscribe();
    this.isAuthorizedSubscription.unsubscribe();
  }
}
