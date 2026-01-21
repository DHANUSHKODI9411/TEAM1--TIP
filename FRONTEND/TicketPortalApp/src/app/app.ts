import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TicketTypeComponent } from './tickettype-component/tickettype-component';
import { StatusComponent } from './status-component/status-component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TicketTypeComponent, StatusComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('TicketPortalApp');
}
