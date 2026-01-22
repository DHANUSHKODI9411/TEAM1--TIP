
import { Component, inject, signal, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../auth-service';

@Component({
  selector: 'app-navbar-component',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, CommonModule],
  templateUrl: './navbar-component.html',
  styleUrl: './navbar-component.css'
})
export class NavbarComponent implements OnInit {
  authSvc = inject(AuthService);

  isLoggedIn = signal(false);
  currentUserRole = signal<string>('');
  empName = signal<string>('');

  ngOnInit() {
    this.updateNavbarState();
    window.addEventListener('storage', () => {
      this.updateNavbarState();
    });
  }

  updateNavbarState() {
    const empName = sessionStorage.getItem('empName') || '';
    const role = sessionStorage.getItem('userRole') || '';

    this.empName.set(empName);
    this.isLoggedIn.set(!!empName);
    this.currentUserRole.set(role);
  }

  isAdmin(): boolean {
    return this.currentUserRole() === 'Admin';
  }

  isAuthenticated(): boolean {
    return this.isLoggedIn();
  }

  logout() {
    this.authSvc.logout();
    this.updateNavbarState();
  }
}
