import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { ProtectedComponent } from './protected/protected.component';
import { AuthGuard } from './auth.guard';
import { AuthCallbackComponent } from './auth-callback/auth-callback.component';
import { AuthService } from './services/auth.service';

@NgModule({
  declarations: [AppComponent, ProtectedComponent, AuthCallbackComponent],
  imports: [BrowserModule, AppRoutingModule],
  providers: [AuthGuard, AuthService],
  bootstrap: [AppComponent]
})
export class AppModule {}
