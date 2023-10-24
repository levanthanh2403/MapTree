import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationService } from 'src/app/services/notification.service';
import { ProjectService } from 'src/app/services/project.service';
import { ImageBase64 } from 'src/app/shared/ImageBase64';
import { location_dto } from 'src/app/shared/dto/location_dto';
import { project_dto } from 'src/app/shared/dto/project_dto';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.scss']
})
export class NewsComponent implements OnInit {
  imgSrc: string = '';
  param1: string = '';
  param2: string = '';

  top10: project_dto[] = [];
  badLocation: location_dto[] = [];

  itemProject: project_dto = new project_dto();
  itemLocation: location_dto = new location_dto();
  constructor(
    private projectService: ProjectService,
    private notification: NotificationService,
    private modalService: NgbModal) { }

  ngOnInit(): void {
    this.imgSrc = ImageBase64.defaultImgNews;
    this.loadData();
  }

  loadData() {
    var query = '?param1=' + this.param1 + '&param2=' + this.param2;
    this.projectService.getListNews(query).subscribe(
      data => {
        console.log(new Date(), data);
        this.top10 = data.top10;
        this.badLocation = data.badLocation;
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
  viewProject(item: project_dto, modal: any) {
    this.itemProject = item;
    this.modalService.open(modal, { size: 'xl' });
  }
  viewLocation(item: location_dto, modal: any) {
    this.itemLocation = item;
    this.modalService.open(modal, { size: 'xl' });
  }
}
