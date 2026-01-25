import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TicketService } from '../services/ticket-service';
import { EmployeeService } from '../services/employee-service';
import { TickettypeService } from '../services/tickettype-services';
import { StatusService } from '../services/status-service';
import { Ticket } from '../../models/Ticket';
import { Employee } from '../../models/Employee';
import { TicketType } from '../../models/TicketType';
import { Status } from '../../models/Status';

@Component({
  selector: 'app-ticket-component',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './ticket-component.html',
  styleUrls: ['./ticket-component.css'], // fixed typo
})
export class TicketComponent implements OnInit {
  private tckSrc = inject(TicketService);
  private empSrc = inject(EmployeeService);
  private typeSrc = inject(TickettypeService);
  private statusSrc = inject(StatusService);

  readonly DEFAULT_USER_CREATE_STATUS = 'S0001';
  readonly UNASSIGNED_EMP = 'UNASN';

  ticket: Ticket;
  tickets: Ticket[] = [];
  employees: Employee[] = [];
  ticketTypes: TicketType[] = [];
  statuses: Status[] = [];
  errMsg: string = '';

  selectedCreatedEmployeeId: string = '';
  selectedAssignedEmployeeId: string = '';
  selectedStatusId: string = '';

  constructor() {
    this.ticket = this.createEmptyTicket();
  }

  ngOnInit() {
    this.loadDropdownData();
    this.showAllTickets();
  }

  
  private handleError(err: any, fallbackMsg: string = 'An unexpected error occurred') {
    console.error(err); 

    if (err.error) {
      if (typeof err.error === 'string') {
        this.errMsg = err.error;
      } else if (err.error.message) {
        this.errMsg = err.error.message;
      } else if (err.error.Message) {
        this.errMsg = err.error.Message;
      } else {
        this.errMsg = JSON.stringify(err.error);
      }
    } else if (err.message) {
      this.errMsg = err.message;
    } else {
      this.errMsg = fallbackMsg;
    }
  }

 
  private createEmptyTicket(): Ticket {
    return {
      ticketId: '',
      title: '',
      description: '',
      createdEmployeeId: '',
      ticketTypeId: '',
      statusId: '',
      assignedEmployeeId: ''
    };
  }

  getCurrentEmployeeId(): string | null {
    return sessionStorage.getItem('employeeId');
  }

  isAdmin(): boolean {
    return (sessionStorage.getItem('userRole') || 'User').toLowerCase() === 'admin';
  }

  isUser(): boolean {
    return !this.isAdmin();
  }

  assignableEmployees(): Employee[] {
    if (!this.ticket?.createdEmployeeId) return this.employees;
    return this.employees.filter(e => e.employeeId !== this.ticket.createdEmployeeId);
  }

  getStatusName(statusId: string | null | undefined): string {
    if (!statusId) return 'N/A';
    const s = this.statuses.find(x => x.statusId === statusId);
    return s?.statusName ?? statusId;
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
    if (this.ticket.assignedEmployeeId === this.ticket.createdEmployeeId) {
      this.errMsg = 'Assigned employee cannot be the same as creator';
      return false;
    }
    this.errMsg = '';
    return true;
  }

  private createTicketForUser() {
    this.ticket.statusId = this.DEFAULT_USER_CREATE_STATUS;
    this.ticket.assignedEmployeeId = this.UNASSIGNED_EMP;

    const myId = this.getCurrentEmployeeId();
    if (!myId) {
      this.errMsg = 'Your Employee ID was not found in session. Please log in again.';
      return false;
    }
    this.ticket.createdEmployeeId = myId;
    return true;
  }

 
  loadDropdownData() {
    this.empSrc.getallemployees().subscribe({
      next: res => this.employees = res || [],
      error: err => this.handleError(err, 'Failed to load employees')
    });

    this.typeSrc.getAllTicketTypes().subscribe({
      next: res => this.ticketTypes = res || [],
      error: err => this.handleError(err, 'Failed to load ticket types')
    });

    this.statusSrc.getAllStatuses().subscribe({
      next: res => this.statuses = res || [],
      error: err => this.handleError(err, 'Failed to load statuses')
    });
  }

  showAllTickets() {
    this.tckSrc.getalltickets().subscribe({
      next: res => { this.tickets = res || []; this.errMsg = ''; },
      error: err => this.handleError(err, 'Failed to load tickets')
    });
  }

 
  newticket() {
    this.ticket = this.createEmptyTicket();
    this.errMsg = '';
  }

  showoneticket() {
    if (!this.ticket.ticketId) {
      this.errMsg = 'Please enter Ticket ID';
      return;
    }

    this.tckSrc.getOneTicket(this.ticket.ticketId).subscribe({
      next: res => { this.ticket = res; this.errMsg = ''; },
      error: err => this.handleError(err, 'Ticket not found')
    });
  }

  addticket() {
    if (this.isUser() && !this.createTicketForUser()) return;
    if (!this.validateTicket()) return;

    this.tckSrc.addticket(this.ticket).subscribe({
      next: () => { 
        alert('Ticket created successfully!'); 
        this.errMsg = '';
        this.selectedCreatedEmployeeId = '';
        this.selectedAssignedEmployeeId = '';
        this.selectedStatusId = '';
        
        // Load all tickets, then reset form
        this.tckSrc.getalltickets().subscribe({
          next: res => { 
            this.tickets = res || []; 
            this.newticket(); 
          },
          error: err => {
            this.handleError(err, 'Failed to refresh tickets');
            this.newticket(); // Still reset form even if refresh fails
          }
        });
      },
      error: err => this.handleError(err, 'Failed to create ticket')
    });
  }

  updateticket() {
    if (!this.ticket.ticketId) { this.errMsg = 'Please load a ticket first'; return; }
    if (!this.validateTicket()) return;

    this.tckSrc.updateticket(this.ticket.ticketId, this.ticket).subscribe({
      next: () => { 
        alert('Ticket updated successfully!'); 
        this.errMsg = '';
        this.selectedCreatedEmployeeId = '';
        this.selectedAssignedEmployeeId = '';
        this.selectedStatusId = '';
        
        // Load all tickets, then reset form
        this.tckSrc.getalltickets().subscribe({
          next: res => { 
            this.tickets = res || []; 
            this.newticket(); 
          },
          error: err => {
            this.handleError(err, 'Failed to refresh tickets');
            this.newticket(); // Still reset form even if refresh fails
          }
        });
      },
      error: err => this.handleError(err, 'Failed to update ticket')
    });
  }

  deleteticket() {
    if (!this.ticket.ticketId) { this.errMsg = 'Please enter Ticket ID to delete'; return; }
    if (!confirm(`Are you sure you want to delete ticket ${this.ticket.ticketId}?`)) return;

    this.tckSrc.deleteTicket(this.ticket.ticketId).subscribe({
      next: () => { 
        alert('Ticket deleted successfully!'); 
        this.errMsg = '';
        this.selectedCreatedEmployeeId = '';
        this.selectedAssignedEmployeeId = '';
        this.selectedStatusId = '';
        
        // Load all tickets, then reset form
        this.tckSrc.getalltickets().subscribe({
          next: res => { 
            this.tickets = res || []; 
            this.newticket(); 
          },
          error: err => {
            this.handleError(err, 'Failed to refresh tickets');
            this.newticket(); // Still reset form even if refresh fails
          }
        });
      },
      error: err => this.handleError(err, 'Failed to delete ticket')
    });
  }

 
  showByCreatedEmployee() {
    let employeeId = this.isUser() ? this.getCurrentEmployeeId() : this.selectedCreatedEmployeeId?.trim();
    if (!employeeId) { this.errMsg = 'No employee selected for Created filter'; return; }
    this.errMsg = '';

    this.tckSrc.getTicketsByCreatedEmployee(employeeId).subscribe({
      next: res => { this.tickets = res || []; },
      error: err => this.handleError(err, 'Failed to filter tickets by creator')
    });
  }

  showByAssignedEmployee() {
    let employeeId = this.isUser() ? this.getCurrentEmployeeId() : this.selectedAssignedEmployeeId?.trim();
    if (!employeeId) { this.errMsg = 'No employee selected for Assigned filter'; return; }
    this.errMsg = '';

    this.tckSrc.getTicketsByAssignedEmployee(employeeId).subscribe({
      next: res => { this.tickets = res || []; },
      error: err => this.handleError(err, 'Failed to filter tickets by assigned employee')
    });
  }

  showByStatus() {
    if (!this.selectedStatusId) { this.errMsg = 'Please select a status'; return; }
    this.errMsg = '';

    this.tckSrc.getTicketsByStatus(this.selectedStatusId).subscribe({
      next: res => { this.tickets = res || []; },
      error: err => this.handleError(err, 'Failed to filter tickets by status')
    });
  }

  showAll() {
    this.selectedCreatedEmployeeId = '';
    this.selectedAssignedEmployeeId = '';
    this.selectedStatusId = '';
    this.errMsg = '';
    this.showAllTickets();
  }
}