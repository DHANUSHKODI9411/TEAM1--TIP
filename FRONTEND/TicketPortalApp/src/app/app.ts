import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TicketTypeComponent } from './tickettype-component/tickettype-component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,TicketTypeComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('TicketPortalApp');
}
