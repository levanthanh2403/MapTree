import { Component, Input } from '@angular/core';
import { location_dto } from 'src/app/shared/dto/location_dto';

@Component({
  selector: 'app-view-location',
  templateUrl: './view-location.component.html',
  styleUrls: ['./view-location.component.scss']
})
export class ViewLocationComponent {
  @Input() public item: location_dto;
}
