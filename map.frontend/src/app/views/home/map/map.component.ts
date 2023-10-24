import { AfterViewInit, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as L from 'leaflet';
import { LocationService } from 'src/app/services/location.service';
import { NotificationService } from 'src/app/services/notification.service';
import { project_dto } from 'src/app/shared/dto/project_dto';

const iconRetinaUrl = 'assets/marker-icon-2x.png';
const iconUrl = 'assets/marker-icon.png';
const shadowUrl = 'assets/marker-shadow.png';
const iconDefault = L.icon({
  iconRetinaUrl,
  iconUrl,
  shadowUrl,
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34],
  tooltipAnchor: [16, -28],
  shadowSize: [41, 41]
});
L.Marker.prototype.options.icon = iconDefault;

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements AfterViewInit {
  private map;

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
    private locationService: LocationService, 
    private notification: NotificationService,
    private changeDetectorRef: ChangeDetectorRef,
    private modalService: NgbModal,
    ) {
  }
  
  ngAfterViewInit(): void {
    this.initMap();
    this.loadData(this.map);
  }
  private initMap(): void {
    this.map = L.map('map', {
      center: [21.027763, 105.834160],
      zoom: 12
    });

    const tiles = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 18,
      minZoom: 3,
      attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    });

    tiles.addTo(this.map);
  }
  loadData(map: any) {
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
        this.dataProjects = data.lstProject;;
        if(data != null && data.lstlocations != null && data.lstlocations.length > 0) {
          for (const c of data.lstlocations) {
            console.log(new Date(), c.location);
            console.log(new Date(), c.location.coordinates[0]);
            console.log(new Date(), c.location.coordinates[1]);
            const lon = c.location.coordinates[0];
            const lat = c.location.coordinates[1];
            const marker = L.marker([lat, lon]);
            console.log(new Date(), marker);
            marker.bindPopup(`<b>Mã vị trí: </b> ${c.locationid} <br/>
                              <b>Tên vị trí: </b> ${c.locationname} <br/>
                              <b>Dự án: </b> ${c.projectname} <br/>
                              <b>Latitude: </b> ${lat} <br/>
                              <b>Longitude: </b> ${lon} <br/>
                              <b>Mã cây: </b> ${c.treecode} <br/>
                              <b>Tên cây: </b> ${c.treename} <br/>
                              <b>Loại cây: </b> ${c.treetype} <br/>
                              <b>Trang thái cây: </b> ${c.treestatus} <br/>`);
            marker.addTo(map);
          }
          this.changeDetectorRef.detectChanges();
        } else {
          this.notification.alertInfor("Chưa tạo thông tin vị trí!");
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

  openModal(modal: any) {
    this.modalService.open(modal, { size: 'xl' });
  }

  search(modal: any) {
    this.initMap();
    this.loadData(this.map);
    modal.close('Close click');
  }
}
