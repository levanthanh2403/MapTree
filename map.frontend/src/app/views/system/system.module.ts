import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LocationsComponent } from './locations/locations.component';
import { ProjectsComponent } from './projects/projects.component';
import { UsersComponent } from './users/users.component';
import { SharedModule } from 'src/app/theme/shared/shared.module';
import { CreateProjectComponent } from './components/create-project/create-project.component';
import { CreateLocationComponent } from './components/create-location/create-location.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'locations',
        component: LocationsComponent
      },
      {
        path: 'projects',
        component: ProjectsComponent
      },
      {
        path: 'users',
        component: UsersComponent
      }
    ],
  },
];


@NgModule({
  declarations: [
    LocationsComponent,
    ProjectsComponent,
    UsersComponent,
    CreateProjectComponent,
    CreateLocationComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule
  ]
})
export class SystemModule { }
