import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Editor, Toolbar } from 'ngx-editor';
import { LocationService } from 'src/app/services/location.service';
import { NotificationService } from 'src/app/services/notification.service';
import { location_dto } from 'src/app/shared/dto/location_dto';
import { project_dto } from 'src/app/shared/dto/project_dto';

@Component({
  selector: 'app-create-location',
  templateUrl: './create-location.component.html',
  styleUrls: ['./create-location.component.scss']
})
export class CreateLocationComponent implements OnInit {
  @Input() public locationId: string;
  @Input() public type: string;
  @Input() public id: string;
  @Input() public modal: any;
  @Output() parrentEvent = new EventEmitter<any>();

  editor: Editor;
  toolbar: Toolbar = [
    ['bold', 'italic'],
    ['underline', 'strike'],
    ['code', 'blockquote'],
    ['ordered_list', 'bullet_list'],
    [{ heading: ['h1', 'h2', 'h3', 'h4', 'h5', 'h6'] }],
    ['link', 'image'],
    ['text_color', 'background_color'],
    ['align_left', 'align_center', 'align_right', 'align_justify'],
  ];
  dto: location_dto = new location_dto();
  dataUser: any[] = [];
  dataProjects: project_dto[] = [];

  dataWard: any[] = [];
  dataDistrict: any[] = [];

  lstWard: any[] = [];
  lstDistrict: any[] = [];
  lstProvince: any[] = [];

  lstLocationStatus: any[] = [
    { value: "", text: "Tất cả" },
    { value: "0", text: "Không trồng cây" },
    { value: "1", text: "Đã trồng cây" }
  ];
  lstRecordStat: any[] = [
    { value: "", text: "Tất cả" },
    { value: "O", text: "Mở" },
    { value: "C", text: "Đóng" }
  ];
  lstTreeStatus: any[] = [
    { value: "", text: "Tất cả" },
    { value: "0", text: "Ổn định" },
    { value: "1", text: "Khô héo" },
    { value: "2", text: "Không phát triển" },
    { value: "3", text: "Đổ" }
  ];

  constructor(
    private router: ActivatedRoute,
    private locationService: LocationService,
    private notification: NotificationService) { }

  ngOnInit(): void {
    console.log("locationId: ", this.locationId);
    this.editor = new Editor();
    this.loadData();
  }
  loadData() {
    var responseAPI;
    if (this.type == 'HIST')
      responseAPI = this.locationService.getDetailLocationHist(this.locationId, this.id);
    else
      responseAPI = this.locationService.getDetailLocation(this.locationId);
    responseAPI.subscribe(
      data => {
        console.log(new Date(), data);
        this.dataProjects = data.lstProject;
        this.lstProvince = data.lstProvince;
        this.dataDistrict = data.lstDistrict;
        this.dataWard = data.lstWard;
        this.dataUser = data.lstUser;

        if (this.locationId != null && this.locationId != '' && data != null) {
          this.dto = data.data;
          this.dto.location_lat = data.data.location.coordinates[1];
          this.dto.location_lon = data.data.location.coordinates[0];
          this.lstDistrict = this.dataDistrict.filter(o => o.province_code === data.data.province_code);
          this.lstWard = this.dataWard.filter(o => o.province_code === data.data.province_code && o.district_code === data.data.district_code);
          this.dto.lstUserid = data.data.lstUser;
        }
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
    var _wardName = this.dataWard.filter(o => o.ward_code == this.dto.ward_code)[0].ward_name_value;
    var _districtName = this.dataDistrict.filter(o => o.district_code == this.dto.district_code)[0].district_name_value;
    var _provinceName = this.lstProvince.filter(o => o.province_code == this.dto.province_code)[0].province_name_value;

    this.dto.address = ((this.dto.address_detail == null || this.dto.address_detail == '') ? '' : (this.dto.address_detail + ", "))
      + _wardName + ", "
      + _districtName + ", "
      + _provinceName;

    if (this.locationId != null && this.locationId != '') this.updateProject();
    else this.createProject();
  }

  createProject() {
    let req = {
      locationid: this.dto.locationid,
      projectid: this.dto.projectid,
      locationname: this.dto.locationname,
      locationinfo: this.dto.locationinfo,
      ward_code: this.dto.ward_code,
      district_code: this.dto.district_code,
      province_code: this.dto.province_code,
      address_detail: this.dto.address_detail,
      address: this.dto.address,
      location_lon: this.dto.location_lon,
      location_lat: this.dto.location_lat,
      locationstatus: this.dto.locationstatus,
      treecode: this.dto.treecode,
      treename: this.dto.treename,
      treeinfor: this.dto.treeinfor,
      treetype: this.dto.treetype,
      treestatus: this.dto.treestatus,
      record_stat: this.dto.record_stat,
      lstUserid: this.dto.lstUserid,
    }
    console.log("req: ", req);
    this.locationService.createLocation(req).subscribe(
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

  updateProject() {
    let req = {
      locationid: this.dto.locationid,
      projectid: this.dto.projectid,
      locationname: this.dto.locationname,
      locationinfo: this.dto.locationinfo,
      ward_code: this.dto.ward_code,
      district_code: this.dto.district_code,
      province_code: this.dto.province_code,
      address_detail: this.dto.address_detail,
      address: this.dto.address,
      location_lon: this.dto.location_lon,
      location_lat: this.dto.location_lat,
      locationstatus: this.dto.locationstatus,
      treecode: this.dto.treecode,
      treename: this.dto.treename,
      treeinfor: this.dto.treeinfor,
      treetype: this.dto.treetype,
      treestatus: this.dto.treestatus,
      record_stat: this.dto.record_stat,
      lstUserid: this.dto.lstUserid,
    }
    console.log("req: ", req);
    this.locationService.updateLocation(req).subscribe(
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
    this.parrentEvent.emit();
  }

  provinceChange(event: any) {
    console.log(event);
    this.lstDistrict = this.dataDistrict.filter(o => o.province_code === event);
    this.dto.district_code = '';
    this.dto.ward_code = '';
  }

  districtChange(event: any) {
    console.log(event);
    this.lstWard = this.dataWard.filter(o => o.province_code === this.dto.province_code && o.district_code === event);
    this.dto.ward_code = '';
  }
}
