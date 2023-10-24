import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { LocationService } from 'src/app/services/location.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-hist-location',
  templateUrl: './hist-location.component.html',
  styleUrls: ['./hist-location.component.scss']
})
export class HistLocationComponent implements OnInit {
  @Input() public locationId: string;
  @Input() public modal: any;

  ColumnMode = ColumnMode;
  rows: any = [];
  totalCount: number = 0;
  page: number = 0;
  pagesize: number = 10;

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

  constructor(
    private locationService: LocationService,
    private notification: NotificationService,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    var query = '?locationid=' + this.locationId + '&projectid=' + this.projectid
      + '&projectname=' + this.projectname + '&locationname=' + this.locationname
      + '&address=' + this.address
      + '&locationstatus=' + this.locationstatus + '&treecode=' + this.treecode
      + '&treename=' + this.treename + '&treetype=' + this.treetype
      + '&treestatus=' + this.treestatus + '&record_stat=' + this.record_stat
      + '&fromCreatedDate=' + this.fromCreatedDate + '&toCreatedDate=' + this.toCreatedDate;
    this.locationService.getListLocationHist(query).subscribe(
      data => {
        console.log(new Date(), data);
        this.rows = data.lstlocations;
        this.totalCount = (data == null || data.lstlocations == null) ? 0 : data.lstlocations.length;
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

  loadDataDetail(row: any, modal: any) {
    
  }

  closedModal() {
    this.modal.close('Close click');
  }
}
