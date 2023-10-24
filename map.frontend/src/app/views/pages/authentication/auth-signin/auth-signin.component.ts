import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-auth-signin',
  templateUrl: './auth-signin.component.html',
  styleUrls: ['./auth-signin.component.scss'],
})
export class AuthSigninComponent implements OnInit {
  userid: string = "";
  password: string = "";
  constructor(
    private authService: AuthService,
    private notification: NotificationService,
    private router: Router
  ) {
  }
  ngOnInit(): void {
    if (this.authService.getAuthFromLocalStorage) {
      this.router.navigate(['/']);
    }
  }
  Login() {
    let req = {
      userid: this.userid,
      password: this.password
    }
    this.authService.login(req).subscribe(
      data => {
        console.log(new Date(), data);
        this.authService.setAuthFromLocalStorage(data.token);
        this.authService.setUserImage(data.img);
        this.router.navigate(['/']);
      },
      err => {
        console.log(new Date(), err);
        if (err.error != null) {
          this.notification.alertError(err.error.resDesc);
        } else {
          this.notification.alertError(err);
        }
      }
    );
  }
}
