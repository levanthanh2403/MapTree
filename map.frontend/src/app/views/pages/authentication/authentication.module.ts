import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthenticationRoutingModule } from './authentication-routing.module';
import { FormsModule } from '@angular/forms';
import { AuthSigninComponent } from './auth-signin/auth-signin.component';
import { HttpClientModule } from '@angular/common/http';
import { SharedModule } from 'src/app/theme/shared/shared.module';

@NgModule({
  declarations: [
    AuthSigninComponent
  ],
  imports: [
    CommonModule,
    AuthenticationRoutingModule,
    FormsModule,
    HttpClientModule
  ],
})
export class AuthenticationModule { }
