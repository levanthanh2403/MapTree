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
  dataProjects : project_dto[] = [];
  lstLocationStatus: any[] = [
    { value: "", text: "Tất cả"},
    { value: "0", text: "Không trồng cây"},
    { value: "1", text: "Đã trồng cây"}
  ];
  lstRecordStat: any[] = [
    { value: "", text: "Tất cả"},
    { value: "O", text: "Mở"},
    { value: "C", text: "Đóng"}
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
    this.locationService.getDetailLocation(this.locationId).subscribe(
      data => {
        console.log(new Date(), data);
        
        if(this.locationId != null && this.locationId != '' && data != null) {
          this.dto = data.data;
          this.dto.location_lon = data.location.coordinates[0];
          this.dto.location_lat = data.location.coordinates[1];
        }
        this.dataProjects = data.lstProject;
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
    if (this.locationId != null && this.locationId != '') this.updateProject();
    else this.createProject();
  }

  createProject() {
    if (this.dto.location_lon == null || this.dto.location_lon == 0
      || this.dto.location_lat == null || this.dto.location_lat == 0)
      this.notification.alertError("Chưa nhập đủ thông tin vị trí!")
    else {
      let point = {
        type: "Point",
        coordinates: [this.dto.location_lon, this.dto.location_lat]
      };

      let req = {
        locationid: this.dto.locationid,
        projectid: this.dto.projectid,
        locationname: this.dto.locationname,
        locationinfo: this.dto.locationinfo,
        address: this.dto.address,
        location: JSON.stringify(point),
        locationstatus: this.dto.locationstatus,
        treecode: this.dto.treecode,
        treename: this.dto.treename,
        treeinfor: this.dto.treeinfor,
        treetype: this.dto.treetype,
        treestatus: this.dto.treestatus,
        record_stat: this.dto.record_stat
      }
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
  }

  updateProject() {
    if (this.dto.location_lon == null || this.dto.location_lon == 0
      || this.dto.location_lat == null || this.dto.location_lat == 0)
      this.notification.alertError("Chưa nhập đủ thông tin vị trí!")
    else {
      let point = {
        type: "Point",
        coordinates: [this.dto.location_lon, this.dto.location_lat]
      };

      let req = {
        locationid: this.dto.locationid,
        projectid: this.dto.projectid,
        locationname: this.dto.locationname,
        locationinfo: this.dto.locationinfo,
        address: this.dto.address,
        location: JSON.stringify(point),
        locationstatus: this.dto.locationstatus,
        treecode: this.dto.treecode,
        treename: this.dto.treename,
        treeinfor: this.dto.treeinfor,
        treetype: this.dto.treetype,
        treestatus: this.dto.treestatus,
        record_stat: this.dto.record_stat
      }
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
  }

  closedModal() {
    this.modal.close('Close click');
    this.parrentEvent.emit();
  }
}
