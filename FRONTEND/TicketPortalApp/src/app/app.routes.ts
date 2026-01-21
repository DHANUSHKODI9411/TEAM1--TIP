import { Routes } from '@angular/router';
import { TicketTypeComponent } from './tickettype-component/tickettype-component';
import { StatusComponent } from './status-component/status-component';

export const routes: Routes = [
  { path: 'tickettype',component:TicketTypeComponent},
  { path: 'status', component: StatusComponent }
];
