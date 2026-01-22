import { CommonModule } from '@angular/common';
import { Component ,inject} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Employee } from '../../models/Employee';

import { EmployeeService } from '../services/employee-service';

@Component({
  selector: 'app-employee-component',
  imports: [FormsModule,CommonModule],
  templateUrl: './employee-component.html',
  styleUrl: './employee-component.css',
})
export class EmployeeComponent {
  private EmpSrvc = inject(EmployeeService);

  employee:Employee;
  employees:Employee[];
  errMsg:string;
  constructor(){
    this.employees=[];
    this.employee=new Employee("","","","","User");
    this.errMsg="";
    this.showAllEmp();
  }
  newEmployee(){
    this.employee = new Employee("","","","","User");
  }
  showAllEmp(){
    this.EmpSrvc.getallemployees().subscribe({
      next:(response:any)=>{
        this.employees=response;
        this.errMsg="";
        console.log(response)
      },
      error:(err)=>{this.errMsg=err.error;console.log(err);}
    })
  }
  showOneEmp(){
    this.EmpSrvc.getOneEmployee(this.employee.employeeId).subscribe({
      next:(response:any)=>{
        this.employee=response;
        this.errMsg="";
      },
      error:(err)=>{this.errMsg=err.error;console.log(err);}
    })
  }
  addEmployee(){
    this.EmpSrvc.addEmployee(this.employee).subscribe({
      next:(response:any)=>{
        this.errMsg="",
        this.showAllEmp();
      },
      error:(err)=>{this.errMsg=err.error;console.log(err);}
    })
  }
  updateEmployee(){
    this.EmpSrvc.updateEmployee(this.employee.employeeId,this.employee).subscribe({
      next:(response:any)=>{
        this.errMsg="";
        this.showAllEmp();
        this.newEmployee();
      },
      error:(err)=>{this.errMsg=err.error;console.log(err);}
    })
  }
  deleteEmployee(){
    this.EmpSrvc.deleteEmployee(this.employee.employeeId).subscribe({
      next:(response:any)=>{
        this.errMsg="";
        this.showAllEmp();
        this.newEmployee();
      },
      error:(err)=>{this.errMsg=err.error;console.log(err);}
    })
  }
  loginEmployee() {
    this.EmpSrvc.login(this.employee.employeeId, this.employee.password).subscribe({
      next: (response: any) => {
        this.errMsg = "";
        console.log("Login successful:", response);
        this.employee = response;
      },
      error: (err) => {this.errMsg = err.error;console.log("Login error:", err);
      }
    });
  }
}
