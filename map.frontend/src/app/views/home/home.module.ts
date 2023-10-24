import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MapComponent } from './map/map.component';
import { RouterModule, Routes } from '@angular/router';
import { NewsComponent } from './news/news.component';
import { SharedModule } from 'src/app/theme/shared/shared.module';
import { ViewProjectComponent } from './components/view-project/view-project.component';
import { ViewLocationComponent } from './components/view-location/view-location.component';
import { NewsItemComponent } from './components/news-item/news-item.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'news',
        component: NewsComponent
      },
      {
        path: 'map',
        component: MapComponent
      }
    ],
  },
];

@NgModule({
  declarations: [
    MapComponent,
    NewsComponent,
    ViewProjectComponent,
    ViewLocationComponent,
    NewsItemComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule
  ]
})
export class HomeModule { }
