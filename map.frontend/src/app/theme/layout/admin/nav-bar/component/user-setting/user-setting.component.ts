import { Component, Input, OnInit } from '@angular/core';
import { ImagePickerConf } from 'ngp-image-picker';
import { AuthService } from 'src/app/services/auth.service';
import { NotificationService } from 'src/app/services/notification.service';
import { ImageBase64 } from 'src/app/shared/ImageBase64';
import { RegisterRequest } from 'src/app/shared/request/register_request';

@Component({
  selector: 'app-user-setting',
  templateUrl: './user-setting.component.html',
  styleUrls: ['./user-setting.component.scss']
})
export class UserSettingComponent implements OnInit {
  @Input() public modal: any;
  config: ImagePickerConf = {
    borderRadius: '50%',
    language: 'en',
    width: '110px',
    objectFit: 'cover',
    aspectRatio: 4 / 3,
    compressInitial: null,
  };
  userId: string = '';
  req: RegisterRequest = new RegisterRequest();
  constructor(
    private authService: AuthService,
    private notification: NotificationService) {
    var _tokenInfo = authService.getAuthFromLocalStorage();
    this.userId = _tokenInfo.userid;
  }
  ngOnInit(): void {
    this.req.img = ImageBase64.defaultImg;
    this.loadDataDetail();
  }
  loadDataDetail() {
    this.authService.getDetailUser(this.userId).subscribe(
      data => {
        console.log(new Date(), data);
        this.req.userId = data.userid;
        this.req.email = data.email;
        this.req.phone = data.phone;
        this.req.userName = data.username;
        this.req.status = 'O';
        this.req.img = ((data.img == null || data.img == '') ? ImageBase64.defaultImg : data.img);
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
  updateData() {
    let req = {
      userId: this.req.userId,
      userName: this.req.userName,
      password: this.req.password,
      rePassword: this.req.rePassword,
      email: this.req.email,
      phone: this.req.phone,
      img: this.req.img
    }
    this.authService.updateUser(req).subscribe(
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
  closedModal() {
    this.modal.close('Close click');
  }
  onImageChanged(dataUri) {
    this.req.img = ((dataUri == null || dataUri == '') ? ImageBase64.defaultImg : dataUri);
  }
}
