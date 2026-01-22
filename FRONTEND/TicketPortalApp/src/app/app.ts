import { Component, inject, signal } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { TicketTypeComponent } from './tickettype-component/tickettype-component';
import { StatusComponent } from './status-component/status-component';
import { EmployeeComponent } from './employee-component/employee-component';
import { LoginComponent } from './login-component/login-component';
import { LogoutComponent } from './logout-component/logout-component';
import { HomeComponent } from './home-component/home-component';
import { AuthService } from './auth-service';
import { SLAComponent } from './sla-component/sla-component';
import { TicketComponent } from './ticket-component/ticket-component';
import { TicketrepliesComponent } from './ticketreplies-component/ticketreplies-component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink, RouterLinkActive,
    TicketTypeComponent,
    StatusComponent,
    EmployeeComponent,
    LoginComponent, LogoutComponent,
    HomeComponent,
    SLAComponent,
    TicketComponent,
    TicketrepliesComponent,
  ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('TicketPortalApp');
  auth=inject(AuthService);
  empName=signal(sessionStorage.getItem('employeeName'));
}
