import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ImagePickerConf } from 'ngp-image-picker';
import { Editor, Toolbar } from 'ngx-editor';
import { NotificationService } from 'src/app/services/notification.service';
import { ProjectService } from 'src/app/services/project.service';
import { ImageBase64 } from 'src/app/shared/ImageBase64';
import { project_dto } from 'src/app/shared/dto/project_dto';

@Component({
  selector: 'app-create-project',
  templateUrl: './create-project.component.html',
  styleUrls: ['./create-project.component.scss']
})
export class CreateProjectComponent implements OnInit, OnDestroy  {
  @Input() public projectId: string;
  dto: project_dto = new project_dto();
  editor: Editor;
  config: ImagePickerConf = {
    borderRadius: '50%',
    language: 'en',
    width: '110px',
    objectFit: 'cover',
    aspectRatio: 4 / 3,
    compressInitial: null,
  };
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
  constructor(
    private router: ActivatedRoute, 
    private projectService: ProjectService, 
    private notification: NotificationService) {}

  ngOnInit(): void {
    this.router.params.subscribe(({ projectId }) => {
      if (projectId != null) {
        console.log('rpt_code', projectId);
      }
    });

    this.editor = new Editor();
    this.dto.img = ImageBase64.defaultImg;
    if(this.projectId != null && this.projectId != '') {
      this.loadData();
    }
  }
  
  ngOnDestroy(): void {
    // this.editor.destroy();
  }

  loadData() {
    var query = '?projectId=' + this.projectId;
    this.projectService.getListProjects(query).subscribe(
      data => {
        console.log(new Date(), data);
        this.dto = data;
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
    if(this.projectId != null && this.projectId != '') this.updateProject();
    else this.createProject();
  }

  createProject() {
    let req = {
      projectid: this.dto.projectid,
      projectname: this.dto.projectname,
      projectdesc: this.dto.projectdesc,
      projectdetail: this.dto.projectdetail,
      investor: this.dto.investor,
      contractors: this.dto.contractors,
      total_value: this.dto.total_value,
      opendate: this.dto.opendate,
      receiptdate: this.dto.receiptdate,
      enddate: this.dto.enddate,
      img: this.dto.img
    }
    this.projectService.createProject(req).subscribe(
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
      projectid: this.dto.projectid,
      projectname: this.dto.projectname,
      projectdesc: this.dto.projectdesc,
      projectdetail: this.dto.projectdetail,
      investor: this.dto.investor,
      contractors: this.dto.contractors,
      total_value: this.dto.total_value,
      opendate: this.dto.opendate,
      receiptdate: this.dto.receiptdate,
      enddate: this.dto.enddate,
      img: this.dto.img
    }
    this.projectService.updateProject(req).subscribe(
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
