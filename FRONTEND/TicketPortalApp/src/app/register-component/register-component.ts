import { Component, inject } from '@angular/core';
import { RegisterService } from '../register-service';  // ✅ Fix service name
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Employee } from '../../models/Employee';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-component',
  imports: [FormsModule, CommonModule],
  templateUrl: './register-component.html',
  styleUrl: './register-component.css',
})
export class RegisterComponent {
  registerSvc: RegisterService = inject(RegisterService);  // ✅ Fixed typo
  user: Employee;
  errMsg: string = '';
  router: Router = inject(Router);

  constructor() {
    // ✅ Proper initialization with empty strings
    this.user = {
      employeeId: '',
      employeeName: '',
      email: '',
      password: '',
      role: 'User'
    };
    this.errMsg = '';
  }

  register() {
    // ✅ Validate before API call
    if (!this.user.employeeId || this.user.employeeId.length !== 5) {
      this.errMsg = 'Employee ID must be exactly 5 characters (e.g., EMP01)';
      return;
    }

    this.registerSvc.register(this.user).subscribe({
      next: (response: any) => {
        alert("New user registered successfully!");
        this.errMsg = '';
        this.router.navigate(['/']);  // Navigate to home
        this.user = { employeeId: '', employeeName: '', email: '', password: '', role: 'User' };
      },
      error: (err) => {
        console.log('Register error:', err);
        // ✅ Handle validation errors properly
        if (err.status === 400 && err.error?.errors) {
          // Model validation errors
          const errors = err.error.errors;
          this.errMsg = Object.values(errors).flat().join(', ');
        } else {
          this.errMsg = err.error?.Message || err.message || 'Registration failed';
        }
      }
    });
  }
}
