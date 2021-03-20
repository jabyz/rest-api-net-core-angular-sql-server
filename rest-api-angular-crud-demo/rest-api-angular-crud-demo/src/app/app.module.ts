import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddEditEmpComponent } from './employee/add-edit-emp/add-edit-emp.component';
import { ShowEmpComponent } from './employee/show-emp/show-emp.component';
import { DepartmentComponent } from './department/department.component';
import { EmployeeComponent } from './employee/employee.component';
import { SharedService } from './shared.service';
import { HttpClientModule } from '@angular/common/http'
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { AddEditDepComponent } from './department/add-edit-dep/add-edit-dep.component';
import { ShowDepComponent } from './department/show-dep/show-dep.component'

@NgModule({
  declarations: [
    AppComponent,
    AddEditEmpComponent,
    ShowEmpComponent,
    DepartmentComponent,
    EmployeeComponent,
    AddEditDepComponent,
    ShowDepComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [SharedService],
  bootstrap: [AppComponent]
})
export class AppModule { }
