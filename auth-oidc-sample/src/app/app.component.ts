import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import {
  OidcSecurityService,
  AuthorizationResult
} from 'angular-auth-oidc-client';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'app';

  constructor(
    public oidcSecurityService: OidcSecurityService,
    private router: Router
  ) {
    if (this.oidcSecurityService.moduleSetup) {
      this.onOidcModuleSetup();
    } else {
      this.oidcSecurityService.onModuleSetup.subscribe(() => {
        this.onOidcModuleSetup();
      });
    }

    this.oidcSecurityService.onAuthorizationResult.subscribe(
      (authorizationResult: AuthorizationResult) => {
        this.onAuthorizationResultComplete(authorizationResult);
      }
    );
  }

  ngOnInit() {}

  ngOnDestroy() {
    this.oidcSecurityService.onModuleSetup.unsubscribe();
  }

  login() {
    console.log('start login');
    this.oidcSecurityService.authorize();
  }

  logout() {
    console.log('start logout');
    this.oidcSecurityService.logoff();
  }

  refreshSession() {
    console.log('start refreshSession');
    this.oidcSecurityService.authorize();
  }

  private onAuthorizationResultComplete(
    authorizationResult: AuthorizationResult
  ) {
    console.log('AppComponent:onAuthorizationResultComplete');
    const path = this.read('redirect');
    if (authorizationResult === AuthorizationResult.authorized) {
      this.router.navigate([path]);
    } else {
      this.router.navigate(['/Unauthorized']);
    }
  }

  private onOidcModuleSetup() {
    if (window.location.hash) {
      this.oidcSecurityService.authorizedCallback();
    } else {
      if ('/autologin' !== window.location.pathname) {
        this.write('redirect', window.location.pathname);
      }
      console.log('AppComponent:onModuleSetup');
      this.oidcSecurityService
        .getIsAuthorized()
        .subscribe((authorized: boolean) => {
          if (!authorized) {
            //this.router.navigate(['/autologin']);
          }
        });
    }
  }

  private read(key: string): any {
    const data = localStorage.getItem(key);
    if (data != null) {
      return JSON.parse(data);
    }
    return;
  }

  private write(key: string, value: any): void {
    localStorage.setItem(key, JSON.stringify(value));
  }
}
