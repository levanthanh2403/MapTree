import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/theme/shared/shared.module';
import { RegisterRequest } from 'src/app/shared/request/register_request';
import { ImageBase64 } from 'src/app/shared/ImageBase64';
import { AuthService } from 'src/app/services/auth.service';
import { NotificationService } from 'src/app/services/notification.service';
import { ImagePickerConf } from 'ngp-image-picker';

@Component({
  selector: 'app-auth-signup',
  standalone: true,
  imports: [CommonModule, RouterModule, SharedModule],
  templateUrl: './auth-signup.component.html',
  styleUrls: ['./auth-signup.component.scss'],
})
export default class AuthSignupComponent implements OnInit {
  config: ImagePickerConf = {
    borderRadius: '50%',
    language: 'en',
    width: '110px',
    objectFit: 'cover',
    aspectRatio: 4 / 3,
    compressInitial: null,
  };
  req: RegisterRequest = new RegisterRequest();
  constructor(private authService: AuthService, private notification: NotificationService) {

  }
  ngOnInit(): void {
    this.req.img = ImageBase64.defaultImg;
  }
  registerUser() {
    let req = {
      userId: this.req.userId,
      userName: this.req.userName,
      password: this.req.password,
      rePassword: this.req.rePassword,
      email: this.req.email,
      phone: this.req.phone,
      img: this.req.img,
      status: 'O',
      rolecode: 'USR'
    }
    this.authService.registerUser(req).subscribe(
      data => {
        console.log(new Date(), data);
        this.notification.alertSussess(data.resDesc);
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
  onImageChanged(dataUri) {
    this.req.img = ((dataUri == null || dataUri == '') ? ImageBase64.defaultImg : dataUri);
  }
}
