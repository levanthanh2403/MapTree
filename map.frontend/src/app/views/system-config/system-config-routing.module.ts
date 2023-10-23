import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProjectsComponent } from './projects/projects.component';
import { LocationsComponent } from './locations/locations.component';
import { UsersComponent } from './users/users.component';

const routes: Routes = [
    {
      path: '',
      children: [
        {
          path: 'projects',
          component: ProjectsComponent
        },
        // {
        //   path: 'projects/:projectId',
        //   component: CreateProjectComponent
        // },
        {
          path: 'locations',
          component: LocationsComponent
        },
        {
          path: 'users',
          component: UsersComponent
        }
      ],
    },
  ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SystemConfigRoutingModule {}
