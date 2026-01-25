import { Routes } from '@angular/router';
import { TicketTypeComponent } from './tickettype-component/tickettype-component';
import { StatusComponent } from './status-component/status-component';
import { EmployeeComponent } from './employee-component/employee-component';
import { HomeComponent } from './home-component/home-component';
import { LoginComponent } from './login-component/login-component';
import { LogoutComponent } from './logout-component/logout-component';
import { RegisterComponent } from './register-component/register-component';
import { SLAComponent } from './sla-component/sla-component';
import { TicketComponent } from './ticket-component/ticket-component';
import { TicketrepliesComponent } from './ticketreplies-component/ticketreplies-component';
import { userAccessGuard } from './user-access-guard';

export const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'login', component: LoginComponent },
  {path: 'logout', component: LogoutComponent ,canActivate:[userAccessGuard]},
  { path: 'register', component: RegisterComponent },
  { path: 'tickettype',component: TicketTypeComponent,canActivate:[userAccessGuard]},
  { path: 'status', component: StatusComponent ,canActivate:[userAccessGuard]},
  { path: 'employee', component: EmployeeComponent,canActivate:[userAccessGuard] },
  { path: 'sla', component: SLAComponent,canActivate:[userAccessGuard] },
  { path: 'ticket', component: TicketComponent,canActivate:[userAccessGuard] },
  { path: 'ticketreplies', component: TicketrepliesComponent ,canActivate:[userAccessGuard]},
];
