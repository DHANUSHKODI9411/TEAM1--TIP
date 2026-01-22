import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TicketTypeComponent } from './tickettype-component/tickettype-component';
import { StatusComponent } from './status-component/status-component';
import { EmployeeComponent } from './employee-component/employee-component';
import { LoginComponent } from './login-component/login-component';
import { LogoutComponent } from './logout-component/logout-component';
import { HomeComponent } from './home-component/home-component';
import { NavbarComponent } from './navbar-component/navbar-component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TicketTypeComponent, StatusComponent, EmployeeComponent, LoginComponent, LogoutComponent, HomeComponent, NavbarComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('TicketPortalApp');
}
