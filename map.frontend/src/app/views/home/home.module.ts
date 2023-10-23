import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MapComponent } from './map/map.component';
import { RouterModule, Routes } from '@angular/router';
import { NewsComponent } from './news/news.component';
import { SharedModule } from 'src/app/theme/shared/shared.module';

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
    NewsComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule
  ]
})
export class HomeModule { }
