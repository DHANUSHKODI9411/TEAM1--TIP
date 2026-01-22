import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TicketService } from '../services/ticket-service';
import { EmployeeService } from '../services/employee-service';  // ✅ Added
import { TickettypeService } from '../services/tickettype-services';  // ✅ Added
import { StatusService } from '../services/status-service';  // ✅ Added
import { Ticket } from '../../models/Ticket';
import { Employee } from '../../models/Employee';
import { TicketType } from '../../models/TicketType';
import { Status } from '../../models/Status';

@Component({
  selector: 'app-ticket-component',
  imports: [FormsModule, CommonModule],
  templateUrl: './ticket-component.html',
  styleUrl: './ticket-component.css',
})
export class TicketComponent implements OnInit {
  private tckSrc = inject(TicketService);
  private empSrc = inject(EmployeeService);
  private typeSrc = inject(TickettypeService);
  private statusSrc = inject(StatusService);

  ticket: Ticket;
  tickets: Ticket[];
  employees: Employee[] = [];
  ticketTypes: TicketType[] = [];
  statuses: Status[] = [];
  errMsg: string = '';

  constructor() {
    this.tickets = [];
    this.ticket = {
      ticketId: '',
      title: '',
      description: '',
      createdEmployeeId: '',
      ticketTypeId: '',
      statusId: '',
      assignedEmployeeId: ''
    };
  }

  ngOnInit() {
    this.loadDropdownData();
    this.showalltickets();
  }

  loadDropdownData() {
    // Load Employees
    this.empSrc.getallemployees().subscribe({
      next: (response: any) => {
        this.employees = response;
      },
      error: (err) => console.error('Employees load error:', err)
    });

    // Load Ticket Types
    this.typeSrc.getAllTicketTypes().subscribe({
      next: (response: any) => {
        this.ticketTypes = response;
      },
      error: (err) => console.error('Ticket types load error:', err)
    });

    // Load Statuses
    this.statusSrc.getAllStatuses().subscribe({
      next: (response: any) => {
        this.statuses = response;
      },
      error: (err) => console.error('Statuses load error:', err)
    });
  }

  newticket() {
    this.ticket = {
      ticketId: '',
      title: '',
      description: '',
      createdEmployeeId: '',
      ticketTypeId: '',
      statusId: '',
      assignedEmployeeId: ''
    };
    this.errMsg = '';
  }

  showalltickets() {
    this.tckSrc.getalltickets().subscribe({
      next: (response: any) => {
        this.tickets = response;
        this.errMsg = '';
        console.log('Tickets loaded:', response);
      },
      error: (err) => {
        this.errMsg = err.error || err.message;
        console.error('Tickets load error:', err);
      }
    });
  }

  showoneticket() {
    if (!this.ticket.ticketId) {
      this.errMsg = 'Please enter Ticket ID';
      return;
    }

    this.tckSrc.getOneTicket(this.ticket.ticketId).subscribe({
      next: (response: any) => {
        this.ticket = response;
        this.errMsg = '';
        console.log('Ticket loaded:', response);
      },
      error: (err) => {
        this.errMsg = err.error?.Message || err.message || 'Ticket not found';
        console.error('Ticket load error:', err);
      }
    });
  }

  addticket() {
    if (!this.validateTicket()) return;

    this.tckSrc.addticket(this.ticket).subscribe({
      next: (response: any) => {
        alert('New ticket created successfully!');
        this.errMsg = '';
        this.newticket();
        this.showalltickets();
      },
      error: (err) => {
        this.errMsg = err.error?.Message || err.error || err.message || 'Failed to create ticket';
        console.error('Add ticket error:', err);
      }
    });
  }

  updateticket() {
    if (!this.ticket.ticketId) {
      this.errMsg = 'Please load a ticket first or enter Ticket ID';
      return;
    }

    if (!this.validateTicket()) return;
    this.tckSrc.updateticket(this.ticket.ticketId, this.ticket).subscribe({
      next: (response: any) => {
        alert('Ticket updated successfully!');
        this.errMsg = '';
        this.showalltickets();
        this.newticket();
      },
      error: (err) => {
        this.errMsg = err.error?.Message || err.error || err.message || 'Failed to update ticket';
        console.error('Update ticket error:', err);
      }
    });
  }
  deleteticket() {
    if (!this.ticket.ticketId) {
      this.errMsg = 'Please enter Ticket ID to delete';
      return;
    }
    if (!confirm(`Are you sure you want to delete ticket ${this.ticket.ticketId}?`)) {
      return;
    }
    this.tckSrc.deleteTicket(this.ticket.ticketId).subscribe({
      next: (response: any) => {
        alert('Ticket deleted successfully!');
        this.errMsg = '';
        this.newticket();
        this.showalltickets();
      },
      error: (err) => {
        this.errMsg = err.error?.Message || err.error || err.message || 'Failed to delete ticket';
        console.error('Delete ticket error:', err);
      }
    });
  }
  private validateTicket(): boolean {
    if (!this.ticket.title?.trim()) {
      this.errMsg = 'Title is required';
      return false;
    }
    if (!this.ticket.createdEmployeeId) {
      this.errMsg = 'Created Employee is required';
      return false;
    }
    if (!this.ticket.ticketTypeId) {
      this.errMsg = 'Ticket Type is required';
      return false;
    }
    if (!this.ticket.statusId) {
      this.errMsg = 'Status is required';
      return false;
    }
    this.errMsg = '';
    return true;
  }
}
