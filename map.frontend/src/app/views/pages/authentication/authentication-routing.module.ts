import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { AuthSigninComponent } from './auth-signin/auth-signin.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'signin',
        component: AuthSigninComponent,
      },
      {
        path: 'signup',
        loadComponent: () => import('./auth-signup/auth-signup.component'),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes),FormsModule],
  exports: [RouterModule],
})
export class AuthenticationRoutingModule {}
