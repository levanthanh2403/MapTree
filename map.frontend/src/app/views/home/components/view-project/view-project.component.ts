import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { LocationService } from 'src/app/services/location.service';
import { NotificationService } from 'src/app/services/notification.service';
import { location_dto } from 'src/app/shared/dto/location_dto';
import { project_dto } from 'src/app/shared/dto/project_dto';

@Component({
  selector: 'app-view-project',
  templateUrl: './view-project.component.html',
  styleUrls: ['./view-project.component.scss']
})
export class ViewProjectComponent implements OnInit {
  @Input() public item: project_dto;
  @Input() public modal: any;
  locations: location_dto[] = [];
  dataLocations: location_dto[] = [];
  itemLocation: location_dto = new location_dto();
  searchBox: string = '';

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

  constructor(
    private locationService: LocationService,
    private notification: NotificationService,
    private modalService: NgbModal) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    var query = '?locationid=' + this.locationid + '&projectid=' + this.item.projectid
      + '&projectname=' + this.projectname + '&locationname=' + this.locationname
      + '&address=' + this.address
      + '&locationstatus=' + this.locationstatus + '&treecode=' + this.treecode
      + '&treename=' + this.treename + '&treetype=' + this.treetype
      + '&treestatus=' + this.treestatus + '&record_stat=' + this.record_stat
      + '&fromCreatedDate=' + this.fromCreatedDate + '&toCreatedDate=' + this.toCreatedDate;
    this.locationService.getListLocation(query).subscribe(
      data => {
        console.log(new Date(), data);
        this.locations = data.lstlocations;
        this.dataLocations = data.lstlocations;
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

  viewLocation(item: location_dto, modal: any) {
    this.itemLocation = item;
    this.modalService.open(modal, { size: 'xl' });
  }

  search(event: any) {
    if (this.searchBox == null) {
      this.locations = this.dataLocations;
    } else {
      this.locations = this.dataLocations.filter(o =>
        o.locationname.toUpperCase().includes(this.searchBox.toUpperCase())
        || o.address.toUpperCase().includes(this.searchBox.toUpperCase())
        || o.locationstatus.toUpperCase().includes(this.searchBox.toUpperCase()));
    }
  }
}
