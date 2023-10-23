import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { ImagePickerConf } from 'ngp-image-picker';
import { AuthService } from 'src/app/services/auth.service';
import { NotificationService } from 'src/app/services/notification.service';
import { ImageBase64 } from 'src/app/shared/ImageBase64';
import { RegisterRequest } from 'src/app/shared/request/register_request';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  config: ImagePickerConf = {
    borderRadius: '50%',
    language: 'en',
    width: '110px',
    objectFit: 'cover',
    aspectRatio: 4 / 3,
    compressInitial: null,
  };

  ColumnMode = ColumnMode;
  rows: any = [];
  totalCount: number = 0;
  page: number = 0;
  pagesize: number = 10;
  userId: string = '';
  username: string = '';
  phone: string = '';
  email: string = '';
  status: string = '';
  rolecode: string = '';
  req: RegisterRequest = new RegisterRequest();
  isEdit: boolean = false;
  lstRoles: any[] = [];
  lstStatus: any[] = [
    { value: "O", text: "Mở"},
    { value: "C", text: "Đóng"}
  ];
  constructor(private authService: AuthService, private notification: NotificationService, private _sanitizer: DomSanitizer) {

  }
  ngOnInit(): void {
    this.req.img = ImageBase64.defaultImg;
    this.loadData();
  }

  loadData() {
    var query = '?page=' + this.page + '&pagesize=' + this.pagesize
      + '&userId=' + this.userId + '&username=' + this.username
      + '&phone=' + this.phone + '&email=' + this.email 
      + '&status=' + this.status + '&rolecode=' + this.rolecode;
    this.authService.getListUser(query).subscribe(
      data => {
        console.log(new Date(), data);
        this.rows = data.users;
        this.totalCount = (data == null || data.users == null) ? 0 : data.users.length;
        this.lstRoles = data.roles;
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

  loadDataDetail(row: any) {
    console.log(row);
    this.authService.getDetailUser(row.userid).subscribe(
      data => {
        console.log(new Date(), data);
        this.req.userId = data.userid;
        this.req.email = data.email;
        this.req.phone = data.phone;
        this.req.userName = data.username;
        this.req.status = data.status;
        this.req.img = ((data.img == null || data.img == '') ? ImageBase64.defaultImg : data.img);
        this.req.roleCode = data.rolecode;
        this.isEdit = true;
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

  registerUser() {
    let req = {
      userId: this.req.userId,
      userName: this.req.userName,
      password: this.req.password,
      rePassword: this.req.rePassword,
      email: this.req.email,
      phone: this.req.phone,
      img: this.req.img,
      status: this.req.status,
      rolecode: this.req.roleCode
    }
    this.authService.registerUser(req).subscribe(
      data => {
        console.log(new Date(), data);
        this.notification.alertSussess(data.resDesc);
        this.loadData();
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

  updateUser() {
    let req = {
      userId: this.req.userId,
      userName: this.req.userName,
      password: this.req.password,
      rePassword: this.req.rePassword,
      email: this.req.email,
      phone: this.req.phone,
      img: this.req.img,
      status: this.req.status,
      rolecode: this.req.roleCode
    }
    this.authService.updateUser(req).subscribe(
      data => {
        console.log(new Date(), data);
        this.notification.alertSussess(data.resDesc);
        this.loadData();
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

  createNew() {
    this.isEdit = false;
    this.req = new RegisterRequest();
    this.req.img = ImageBase64.defaultImg;
  }

  onImageChanged(dataUri) {
    this.req.img = ((dataUri == null || dataUri == '') ? ImageBase64.defaultImg : dataUri);
  }
}