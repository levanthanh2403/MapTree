import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LocationsComponent } from './locations/locations.component';
import { UsersComponent } from './users/users.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { SharedModule } from 'src/app/theme/shared/shared.module';
import { ProjectsComponent } from './projects/projects.component';
import { CreateProjectComponent } from './components/create-project/create-project.component';
import { SystemConfigRoutingModule } from './system-config-routing.module';

@NgModule({
  declarations: [
    LocationsComponent,
    UsersComponent,
    ProjectsComponent,
    CreateProjectComponent
  ],
  imports: [
    CommonModule,
    SystemConfigRoutingModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule
  ]
})
export class SystemConfigModule { }
