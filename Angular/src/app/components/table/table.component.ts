import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IEmployee } from 'src/app/interfaces/IEmployee.interface';
import { EmployeesService } from 'src/app/services/employees.service';
import { EmployeeComponent } from '../employee/employee.component';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})
export class TableComponent implements OnInit {

  constructor(private employeesService: EmployeesService, private route: ActivatedRoute, private modalService: NgbModal) { }
  isManager: boolean = false;
  data?: IEmployee[];

  ngOnInit(): void {
    this.isManager = (this.route.snapshot.data["isManager"] as boolean) === true;
    this.employeesService.getAll(this.isManager).subscribe(x => { 
      this.data = x;
    });

  }

  add() {
    const modalRef = this.modalService.open(EmployeeComponent);
		modalRef.componentInstance.isNew = true;
  }

  edit(idNumber: string) {
		const modalRef = this.modalService.open(EmployeeComponent);
		modalRef.componentInstance.isNew = false;
		modalRef.componentInstance.employeeId = idNumber;
	}

  delete(idNumber: string) {
    this.employeesService.delete(idNumber).subscribe(x => { 
      alert(!x ? "Error!" : "Success"); 
      window.location.reload();  
    });
  }
}
