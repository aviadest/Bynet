import { Component, Input, OnInit } from "@angular/core";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { NumberOnlyDirective } from "src/app/directives/number-only.directive";
import { IEmployee, IEmployeeDto } from "src/app/interfaces/IEmployee.interface";
import { IManager } from "src/app/interfaces/IManager.interface";
import { EmployeesService } from "src/app/services/employees.service";

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.scss']
})
export class EmployeeComponent implements OnInit {
	@Input() employeeId = "";
  @Input() isNew?: boolean;

  employee?: IEmployee;
  managersList: IManager[] = [];

  idNumberInput = new FormControl(null,[Validators.required, Validators.minLength(9), Validators.maxLength(9)]);
  firstNameInput = new FormControl(null,[Validators.required, Validators.maxLength(20)]);
  lastNameInput = new FormControl(null,[Validators.required, Validators.maxLength(20)]);
  roleInput = new FormControl(null,[Validators.required, Validators.maxLength(20)]);
  managerInput = new FormControl<string | null>(null);

  form: FormGroup = this.formBuilder.group(
    { idNumber: this.idNumberInput, 
      firstName: this.firstNameInput,
      lastName: this.lastNameInput,
      role: this.roleInput,
      manager: this.managerInput,
    });
  
  isSubmitFailed: boolean = false;


	constructor(private employeesService: EmployeesService, private formBuilder: FormBuilder, public activeModal: NgbActiveModal) {}

  ngOnInit(): void {
    if (!this.isNew) {
      this.employeesService.get(this.employeeId).subscribe(e =>  { 
        this.employee = e; 
        this.initForm();
      });
    }
    else{

      this.employeesService.getManagers().subscribe(m => this.managersList = m);
    }

  }

  initForm() {
    this.employeesService.getManagers().subscribe(m => {
      this.managersList = m;

      var mngr = this.managersList.find(x => x.id === this.employee?.managerId);

      this.form.patchValue({
        idNumber: this.employee?.idNumber,
        firstName: this.employee?.firstName,
        lastName: this.employee?.lastName,
        role: this.employee?.role,
        manager: mngr?.id
      });
      this.idNumberInput.disable();
    });
    
    
  }

  
  submit() {
    this.isSubmitFailed = false;
    
    this.form.markAllAsTouched();
    this.form.updateValueAndValidity();
    
    if (!this.form.valid) {
      this.isSubmitFailed = true;
      return;
    }
    const form: IEmployeeDto = {
      idNumber: this.idNumberInput.value,
      firstName: this.firstNameInput.value,
      lastName: this.lastNameInput.value,
      role: this.roleInput.value,
      managerId: this.managerInput.value === '' ? null : this.managerInput.value
    }

    if (this.isNew) {
      this.employeesService.add(form).subscribe(x => { 
        alert(!x ? "Error!" : "Success"); 
        window.location.reload();
      });
    }
    else {
      this.employeesService.edit(this.employeeId, form).subscribe(x => { 
        alert(!x ? "Error!" : "Success"); 
        window.location.reload();
      });
    }
  }

  change(value: string | null) {
    this.managerInput.setValue(value);
  }
}









// import { Component, Input } from '@angular/core';
// import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';

// @Component({
// 	selector: 'ngbd-modal-content',
// 	standalone: true,
// 	template: `
		
// 	`,
// })
// export class NgbdModalContent {
// 	@Input() name = "";

// 	constructor(public activeModal: NgbActiveModal) {}
// }

// @Component({ selector: 'ngbd-modal-component', templateUrl: './employee.component.html' })
// export class NgbdModalComponent {
// 	constructor() {}

	
// }