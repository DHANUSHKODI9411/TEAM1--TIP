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

        // Store ALL session data FIRST
        sessionStorage.setItem('empName', this.Employee.employeeName);
        sessionStorage.setItem('userRole', this.Employee.role || 'User');

        // Update auth service LAST
        this.authSvc.setLogin(this.Employee.employeeName, this.Employee.role);

        this.errMsg="";

        // Navigate with {skipLocationChange: true} to prevent duplicate history
        this.router.navigate(['/'], { skipLocationChange: true }).then(() => {
          // Force navbar refresh by calling update on auth service
          this.authSvc.empNameSignal.set(this.Employee.employeeName);
          window.dispatchEvent(new Event('storage'));
        });
      },
      error:(err)=>{
        this.errMsg = err.error?.message || err.error || 'Login failed';
        console.error('Login error:', err);
      }
    });
  }
}
