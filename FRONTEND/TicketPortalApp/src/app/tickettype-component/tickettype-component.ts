import { Component, inject } from '@angular/core';
import { TickettypeService } from '../services/tickettype-services';
import { TicketType } from '../../models/TicketType';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-tickettype',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './tickettype-component.html',
  styleUrl: './tickettype-component.css',
})
export class TicketTypeComponent {
  ticketTypeSvc: TickettypeService = inject(TickettypeService);
  ticketTypes: TicketType[];
  ticketType: TicketType;
  errMsg: string;

  constructor() {
    this.ticketTypes = [];
    this.ticketType = new TicketType();
    this.errMsg = "";
    this.showAllTicketTypes();
  }

  showAllTicketTypes() {
    this.ticketTypeSvc.getAllTicketTypes().subscribe({
      next: (response: any) => {
        this.ticketTypes = response;
        this.errMsg = "";
      },
      error: (err) => { this.errMsg = err.message; }
    });
  }

  saveTicketType() {
    this.ticketTypeSvc.addTicketType(this.ticketType).subscribe({
      next: (response: any) => {
        alert("New Ticket Type added");
        this.newTicketType();
        this.showAllTicketTypes();
      },
      error: (err) => this.errMsg = err.error
    });
  }

  newTicketType() {
    this.ticketType = new TicketType();
    this.errMsg = "";
  }

  showTicketType() {
    this.ticketTypeSvc.getTicketType(this.ticketType.ticketTypeId).subscribe({
      next: (response: any) => {
        this.ticketType = response;
        this.errMsg = "";
      },
      error: (err) => { this.errMsg = err.message; }
    });
  }

  updateTicketType() {
    this.ticketTypeSvc.updateTicketType(this.ticketType.ticketTypeId, this.ticketType).subscribe({
      next: (response: any) => {
        alert("Ticket Type details updated");
        this.showAllTicketTypes();
      },
      error: (err) => this.errMsg = err.error
    });
  }

  deleteTicketType() {
    if (confirm("Are you sure?")) {
      this.ticketTypeSvc.deleteTicketType(this.ticketType.ticketTypeId).subscribe({
        next: (response: any) => {
          alert("Ticket Type deleted");
          this.newTicketType();
          this.showAllTicketTypes();
        },
        error: (err) => this.errMsg = err.error
      });
    }
  }

  selectTicketType(selected: TicketType) {
    this.ticketType = { ...selected };
  }
}
