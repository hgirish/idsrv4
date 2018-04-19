import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { ProtectedComponent } from './protected/protected.component';
import { AuthGuard } from './auth.guard';
import { AuthCallbackComponent } from './auth-callback/auth-callback.component';
import { AuthService } from './services/auth.service';
import { CallApiComponent } from './call-api/call-api.component';

@NgModule({
  declarations: [
    AppComponent,
    ProtectedComponent,
    AuthCallbackComponent,
    CallApiComponent
  ],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule],
  providers: [AuthGuard, AuthService],
  bootstrap: [AppComponent]
})
export class AppModule {}
