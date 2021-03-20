import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-show-emp',
  templateUrl: './show-emp.component.html',
  styles: [],
})
export class ShowEmpComponent implements OnInit {
  constructor(private service: SharedService) {}

  EmployeeList: any = [];
  ModalTitle: string = '';
  ActivateAddEditEmpComp: boolean = false;
  emp: any;

  ngOnInit(): void {
    this.getEmpList();
  }

  addClick() {
    this.emp = {
      EmployeeId: 0,
      EmployeeName: '',
      Department: '',
      DateOfJoining: '',
      PhotoFileName: 'anonymous.png',
      eMail: ''

    };
    this.ModalTitle = 'Add Employee';
    this.ActivateAddEditEmpComp = true;
  }

  closeClick() {
    this.ActivateAddEditEmpComp = false;
    this.getEmpList();
  }

  editClick(item) {
    this.emp = item;
    this.ModalTitle = 'Edit Employee';
    this.ActivateAddEditEmpComp = true;
  }

  deleteClick(item) {
    if (confirm('Are you sure??')) {
      this.service.deleteEmployee(item.EmployeeId).subscribe((data) => {
        alert(data.toString());
        this.getEmpList();
      });
    }
  }

  getEmpList() {
    this.service.getEmpList().subscribe((data) => {
      this.EmployeeList = data;
    });
  }
}
