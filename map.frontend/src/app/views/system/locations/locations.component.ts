import { AfterViewInit, Component, OnInit } from '@angular/core';
import * as L from 'leaflet';
import * as geojson from 'geojson';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProjectService } from 'src/app/services/project.service';
import { NotificationService } from 'src/app/services/notification.service';
import { LocationService } from 'src/app/services/location.service';
import { project_dto } from 'src/app/shared/dto/project_dto';

@Component({
  selector: 'app-locations',
  templateUrl: './locations.component.html',
  styleUrls: ['./locations.component.scss']
})
export class LocationsComponent implements OnInit {
  ColumnMode = ColumnMode;
  rows: any = [];
  totalCount: number = 0;
  page: number = 0;
  pagesize: number = 10;

  locationIdDetail: string = '';

  locationid: string = '';
  projectid: string = '';
  projectname: string = '';
  locationname: string = '';
  address: string = '';
  locationstatus: string = '';
  treecode: string = '';
  treename: string = '';
  treetype: string = '';
  treestatus: string = '';
  record_stat: string = '';
  fromCreatedDate: string = '';
  toCreatedDate: string = '';

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
  lstTreeStatus: any[] = [
    { value: "", text: "Tất cả" },
    { value: "0", text: "Ổn định" },
    { value: "1", text: "Khô héo" },
    { value: "2", text: "Không phát triển" },
    { value: "3", text: "Đổ" }
  ];

  constructor(
    private router: Router,
    private modalService: NgbModal,
    private locationService: LocationService,
    private notification: NotificationService) {

  }

  ngOnInit(): void {
    this.loadData();
  }
  loadData() {
    var query = '?locationid=' + this.locationid + '&projectid=' + this.projectid
      + '&projectname=' + this.projectname + '&locationname=' + this.locationname
      + '&address=' + this.address
      + '&locationstatus=' + this.locationstatus + '&treecode=' + this.treecode
      + '&treename=' + this.treename + '&treetype=' + this.treetype
      + '&treestatus=' + this.treestatus + '&record_stat=' + this.record_stat
      + '&fromCreatedDate=' + this.fromCreatedDate + '&toCreatedDate=' + this.toCreatedDate;
    this.locationService.getListLocation(query).subscribe(
      data => {
        console.log(new Date(), data);
        this.rows = data.lstlocations;
        this.totalCount = (data == null || data.lstlocations == null) ? 0 : data.lstlocations.length;
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

  openModal(modal: any) {
    this.locationIdDetail = "";
    this.modalService.open(modal, { size: 'xl' });
  }

  loadDataDetail(row: any, modal: any) {
    this.locationIdDetail = row.locationid;
    this.modalService.open(modal, { size: 'xl' });
  }
}
