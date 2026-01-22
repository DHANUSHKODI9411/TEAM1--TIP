import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TicketrepliesServices } from '../services/ticketreplies-services';
import { TicketService } from '../services/ticket-service';
import { EmployeeService } from '../services/employee-service';
import { TicketReplies } from '../../models/TicketReplies';
import { Ticket } from '../../models/Ticket';
import { Employee } from '../../models/Employee';

@Component({
  selector: 'app-ticketreplies-component',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './ticketreplies-component.html',
  styleUrls: ['./ticketreplies-component.css'],
})
export class TicketrepliesComponent implements OnInit {
  ticketrepliesSvc: TicketrepliesServices = inject(TicketrepliesServices);
  ticketSvc: TicketService = inject(TicketService);
  empSvc: EmployeeService = inject(EmployeeService);

  ticketreplies: TicketReplies[] = [];
  errMsg: string = '';
  reply: TicketReplies;

  // ✅ DROPDOWN DATA
  tickets: Ticket[] = [];
  employees: Employee[] = [];

  constructor() {
    this.reply = {
      replyId: '',
      ticketId: '',
      createdEmployeeId: '',
      assignedEmployeeId: '',
      replyText: '',
      repliedDate: new Date()
    };
  }

  ngOnInit(): void {
    this.loadDropdownData();
  }

  // ✅ LOAD DROPDOWNS
  loadDropdownData() {
    // Load Tickets
    this.ticketSvc.getalltickets().subscribe({
      next: (tickets) => this.tickets = tickets,
      error: (err) => console.error('Tickets load error:', err)
    });

    // Load Employees
    this.empSvc.getallemployees().subscribe({
      next: (employees) => this.employees = employees,
      error: (err) => console.error('Employees load error:', err)
    });
  }

  showAllTicketReplies(): void {
    this.ticketrepliesSvc.GetAllTicketReplies().subscribe({
      next: (response) => {
        this.ticketreplies = response;
        this.errMsg = "";
      },
      error: (err) => {
        this.errMsg = err.message || 'Failed to load replies';
      }
    });
  }

  addTicketReply(): void {
    if (!this.validateReply()) return;

    this.ticketrepliesSvc.addTicketReply(this.reply).subscribe({
      next: (response: TicketReplies) => {
        this.ticketreplies.push(response);
        this.newTicketReply();
        alert("Reply added successfully");
        this.errMsg = "";
      },
      error: (err) => {
        this.errMsg = err.error?.Message || err.message || 'Failed to add reply';
      }
    });
  }

  showTicketReply(): void {
    if (!this.reply.replyId.trim()) {
      this.errMsg = 'Please enter Reply ID';
      return;
    }

    this.ticketrepliesSvc.GetTicketReply(this.reply.replyId).subscribe({
      next: (response: TicketReplies) => {
        this.reply = response;
        this.errMsg = "";
      },
      error: (err) => {
        this.errMsg = err.message || 'Reply not found';
      }
    });
  }

  updateTicketReply(): void {
    if (!this.reply.replyId.trim()) {
      this.errMsg = 'Please load a reply first';
      return;
    }

    if (!this.validateReply()) return;

    this.ticketrepliesSvc.UpdateTicketReply(this.reply.replyId, this.reply).subscribe({
      next: () => {
        this.showAllTicketReplies();
        alert("Reply updated successfully");
        this.errMsg = "";
      },
      error: (err) => {
        this.errMsg = err.message || 'Failed to update reply';
      }
    });
  }

  deleteTicketReply(): void {
    if (!this.reply.replyId.trim()) {
      this.errMsg = 'Please enter Reply ID to delete';
      return;
    }

    if (!confirm(`Delete reply ${this.reply.replyId}?`)) return;

    this.ticketrepliesSvc.deleteTicketReply(this.reply.replyId).subscribe({
      next: () => {
        this.showAllTicketReplies();
        alert("Reply deleted successfully");
        this.errMsg = "";
      },
      error: (err) => {
        this.errMsg = err.message || 'Failed to delete reply';
      }
    });
  }

  newTicketReply() {
    this.reply = {
      replyId: '',
      ticketId: '',
      createdEmployeeId: '',
      assignedEmployeeId: '',
      replyText: '',
      repliedDate: new Date()
    };
    this.errMsg = '';
  }

  trackByReplyId(index: number, item: TicketReplies): string {
    return item.replyId || `${index}`;
  }

  private validateReply(): boolean {
    if (!this.reply.ticketId?.trim()) {
      this.errMsg = 'Ticket ID is required';
      return false;
    }
    if (!this.reply.createdEmployeeId?.trim()) {
      this.errMsg = 'Created Employee is required';
      return false;
    }
    if (!this.reply.replyText?.trim()) {
      this.errMsg = 'Reply Text is required';
      return false;
    }
    this.errMsg = '';
    return true;
  }
  formatDateToString(date: Date): string {
    return date.toISOString().slice(0, 16);
  }
  onDateChange(dateStr: string) {
    if (dateStr) {
      this.reply.repliedDate = new Date(dateStr);
    }
  }
}
