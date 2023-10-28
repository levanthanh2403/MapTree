import { Component, OnInit } from '@angular/core';
import { NotificationService } from 'src/app/services/notification.service';
import { ReportService } from 'src/app/services/report.service';
import { project_dto } from 'src/app/shared/dto/project_dto';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent implements OnInit {
  project_code: string = '';
  ward_code: string = '';
  district_code: string = '';
  province_code: string = '';

  lstProjects: any[] = [];

  dataWard: any[] = [];
  dataDistrict: any[] = [];

  lstWard: any[] = [];
  lstDistrict: any[] = [];
  lstProvince: any[] = [];

  constructor(
    private reportService: ReportService,
    private notification: NotificationService
  ) { }
  ngOnInit(): void {
    this.loadParam();
  }
  loadParam() {
    this.reportService.getParams().subscribe(
      data => {
        console.log(new Date(), data);
        this.lstProjects = data.lstProject;
        this.lstProvince = data.lstProvince;
        this.lstDistrict = data.lstDistrict;
        this.lstWard = data.lstWard;

        this.dataDistrict = data.lstDistrict;
        this.dataWard = data.lstWard;
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
  provinceChange(event: any) {
    console.log(event);
    if (event != null)
      this.lstDistrict = this.dataDistrict.filter(o => o.province_code === event);
    else
      this.lstDistrict = this.dataDistrict;
    this.district_code = '';
    this.ward_code = '';
  }

  districtChange(event: any) {
    console.log(event);
    if (event != null)
      this.lstWard = this.dataWard.filter(o => o.province_code === this.province_code && o.district_code === event);
    else
      this.lstWard = this.dataWard;
    this.ward_code = '';
  }
  export() {
    var query = '?projectId=' + this.project_code + '&wardCode=' + this.ward_code
      + '&districtCode=' + this.district_code + '&provinceCode=' + this.project_code;
    this.reportService.exportReportLocation(query).subscribe(
      data => {
        console.log(new Date(), data);
        if (data == null) {
          this.notification.alertError("Không có dữ liệu export!");
        } else {
          var mediaType = `data:application/vnd.ms-excel;base64,`;
          var a = document.createElement('a');
          a.href = mediaType + data.fileBase64;
          a.download = 'location_report.xlsx';
          a.textContent = 'Download file!';
          a.click();
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
}
