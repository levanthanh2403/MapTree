import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { NotificationService } from 'src/app/services/notification.service';
import { ProjectService } from 'src/app/services/project.service';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.scss']
})
export class ProjectsComponent implements OnInit {
  ColumnMode = ColumnMode;
  rows: any = [];
  totalCount: number = 0;
  page: number = 0;
  pagesize: number = 10;

  projectIdDetail: string = '';
  
  projectId: string = '';
  projectName: string = '';
  investor: string = '';
  contractors: string = '';
  total_value: string = '';
  fromOpen: string = '';
  toOpen: string = '';
  fromEnd: string = '';
  toEnd: string = '';
  fromReceipt: string = '';
  toReceipt: string = '';

  constructor(
    private router: Router,
    private modalService: NgbModal, 
    private projectService: ProjectService, 
    private notification: NotificationService) {

  }

  ngOnInit(): void {
    this.loadData();
  }
  loadData() {
    var query = '?projectId=' + this.projectId + '&projectName=' + this.projectName
      + '&investor=' + this.investor + '&contractors=' + this.contractors
      + '&total_value=' + this.total_value 
      + '&fromOpen=' + this.fromOpen + '&toOpen=' + this.toOpen 
      + '&fromEnd=' + this.fromEnd + '&toEnd=' + this.fromEnd
      + '&fromReceipt=' + this.fromEnd + '&toReceipt=' + this.fromEnd;
    this.projectService.getListProjects(query).subscribe(
      data => {
        console.log(new Date(), data);
        this.rows = data.projects;
        this.totalCount = (data == null || data.projects == null) ? 0 : data.projects.length;
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
    this.projectIdDetail = "";
    this.modalService.open(modal, { size: 'xl' });
  }

  loadDataDetail(row: any, modal: any) {
    this.projectIdDetail = row.projectid;
    this.modalService.open(modal, { size: 'xl' });
  }
}
