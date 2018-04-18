import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { ProtectedComponent } from './protected/protected.component';
import { AuthGuard } from './auth.guard';

@NgModule({
  declarations: [AppComponent, ProtectedComponent],
  imports: [BrowserModule, AppRoutingModule],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule {}
