import { Component, Input } from '@angular/core';
import { project_dto } from 'src/app/shared/dto/project_dto';

@Component({
  selector: 'app-view-project',
  templateUrl: './view-project.component.html',
  styleUrls: ['./view-project.component.scss']
})
export class ViewProjectComponent {
  @Input() public item: project_dto;
}
