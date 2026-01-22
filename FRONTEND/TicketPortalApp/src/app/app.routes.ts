import { Routes } from '@angular/router';
import { TicketTypeComponent } from './tickettype-component/tickettype-component';
import { StatusComponent } from './status-component/status-component';
import { EmployeeComponent } from './employee-component/employee-component';
import { HomeComponent } from './home-component/home-component';
import { LoginComponent } from './login-component/login-component';
import { LogoutComponent } from './logout-component/logout-component';
import { NavbarComponent } from './navbar-component/navbar-component';
import { RegisterComponent } from './register-component/register-component';

export const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'login', component: LoginComponent },
  {path: 'logout', component: LogoutComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'tickettype',component: TicketTypeComponent},
  { path: 'status', component: StatusComponent },
  { path: 'employee', component: EmployeeComponent },
  { path: 'navbar', component: NavbarComponent }
];
