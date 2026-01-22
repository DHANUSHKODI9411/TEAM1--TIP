import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { Employee } from '../../models/Employee';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { EmployeeService } from '../services/employee-service';
import { AuthService } from '../auth-service';

@Component({
  selector: 'app-login-component',
  imports: [FormsModule, CommonModule],
  templateUrl: './login-component.html',
  styleUrl: './login-component.css',
})
/* export class LoginComponent {
  loginSvc: LoginService = inject(LoginService);
  router: Router = inject(Router);
  employeeId: string;
  password: string;
  user!: Employee;
  errMsg: string;
  constructor() {
    this.employeeId = "";
    this.password = "";
    this.errMsg = "";
  }
  login() {
    this.loginSvc.login(this.employeeId, this.password).subscribe({
      next: (response: Employee) => {
        this.user = response;
        sessionStorage.setItem("employeeId", this.user.employeeId);
        sessionStorage.setItem("role", this.user.role);
        alert("Login succesfully")
        this.errMsg = "";
        this.router.navigate(['/']);
      },
      error: (err) => {this.errMsg = err.error;console.log("Login error:", err);
      }
    });
  }
} */
export class LoginComponent {
  EmpSvc:EmployeeService=inject(EmployeeService);
  authSvc:AuthService=inject(AuthService);
  Employee:Employee;
  router:Router=inject(Router);
  EmployeeId:string;
  Password:string;
  errMsg:string;
  constructor(){
    this.EmployeeId="";
    this.Password="";
    this.errMsg="";
    this.Employee = new Employee();
  }
  login(){
    this.EmpSvc.login(this.EmployeeId,this.Password).subscribe({
      next:(response:any)=>{
        this.Employee=response;
        this.authSvc.setLogin(this.Employee.employeeName);
        this.errMsg="";
        this.router.navigate(['']);
      },
      error:(err)=>this.errMsg=err.error
    })
  }
}
