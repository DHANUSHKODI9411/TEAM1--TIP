import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { TicketrepliesServices } from '../services/ticketreplies-services';
import { TicketService } from '../services/ticket-service';
import { Ticket } from '../../models/Ticket';
import { TicketReplies } from '../../models/TicketReplies';

@Component({
  selector: 'app-ticketreplies-component',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './ticketreplies-component.html',
  styleUrls: ['./ticketreplies-component.css'],
})
export class TicketrepliesComponent {
  replySvc: TicketrepliesServices = inject(TicketrepliesServices);
  ticketSvc: TicketService = inject(TicketService);

  tickets: Ticket[] = [];
  replies: TicketReplies[] = [];

  // ✅ TicketId Filter
  filterTicketId: string = '';
  filteredReplies: TicketReplies[] = [];

  // Form models
  ticket: Ticket = new Ticket('', '', '', '', '', '', '');
  reply: TicketReplies = new TicketReplies();

  errMsg: string = '';
  replier: string = '';

  constructor() {
    this.newReply();
    this.showAllTickets();
    this.showAllReplies();
  }

  private todayDateOnly(): Date {
    const d = new Date();
    return new Date(d.getFullYear(), d.getMonth(), d.getDate());
  }

  // ✅ Common Error Handler (Fix [object Object])
  private handleError(err: any, fallbackMsg: string) {
    console.log(err);

    // If backend sends string error
    if (typeof err?.error === 'string') {
      this.errMsg = err.error;
      return;
    }

    // If backend sends ASP.NET validation object
    if (err?.error?.errors) {
      const firstKey = Object.keys(err.error.errors)[0];
      this.errMsg = err.error.errors[firstKey][0];
      return;
    }

    this.errMsg = fallbackMsg;
  }

  newReply() {
    this.reply = new TicketReplies();
    this.reply.repliedDate = this.todayDateOnly();
    this.replier = '';
    this.errMsg = '';
  }

  showAllTickets(): void {
    this.ticketSvc.getalltickets().subscribe({
      next: (response: Ticket[]) => {
        this.tickets = response;
        this.errMsg = '';
      },
      error: (err) => this.handleError(err, 'Failed to load tickets'),
    });
  }

  onTicketIdChange(ticketId: string) {
    if (!ticketId) return;

    this.ticketSvc.getOneTicket(ticketId).subscribe({
      next: (t: Ticket) => {
        this.ticket = t;

        // ✅ Auto fill ids from ticket
        this.reply.ticketId = ticketId;
        this.reply.createdEmployeeId = this.ticket.createdEmployeeId;
        this.reply.assignedEmployeeId = this.ticket.assignedEmployeeId;

        if (!this.reply.repliedDate) {
          this.reply.repliedDate = this.todayDateOnly();
        }

        this.errMsg = '';
      },
      error: (err) => this.handleError(err, 'Invalid Ticket ID'),
    });
  }

  showAllReplies() {
    this.replySvc.GetAllTicketReplies().subscribe({
      next: (res: TicketReplies[]) => {
        this.replies = res;
        this.filteredReplies = res; // ✅ important
        this.errMsg = '';

        // keep filter applied if user already typed
        this.filterByTicketId();
      },
      error: (err) => this.handleError(err, 'Failed to load replies'),
    });
  }

  // ✅ Show by ReplyId
  getReply() {
    this.errMsg = '';

    const id = (this.reply.replyId || '').trim();
    if (id === '') {
      this.errMsg = 'Enter Reply ID';
      return;
    }

    this.replySvc.GetTicketReply(id).subscribe({
      next: (res: TicketReplies) => {
        this.reply = res;
        this.errMsg = '';
      },
      error: (err) => this.handleError(err, 'No such Reply ID'),
    });
  }

  setDateFromInput(event: any) {
    this.reply.repliedDate = event?.target?.valueAsDate || this.todayDateOnly();
  }

  // ✅ Frontend validation
  private validateBeforeSave(): boolean {
    this.errMsg = '';

    if (!this.reply.replyId || this.reply.replyId.trim() === '') {
      this.errMsg = 'Reply ID is required';
      return false;
    }

    // ✅ Your rule: exactly 5 characters
    if (this.reply.replyId.trim().length !== 5) {
      this.errMsg = 'Reply ID must be exactly 5 characters';
      return false;
    }

    if (!this.reply.ticketId || this.reply.ticketId.trim() === '') {
      this.errMsg = 'Ticket ID is required';
      return false;
    }

    if (!this.reply.createdEmployeeId || this.reply.createdEmployeeId.trim() === '') {
      this.errMsg = 'Created Employee is required';
      return false;
    }

    if (!this.reply.assignedEmployeeId || this.reply.assignedEmployeeId.trim() === '') {
      this.errMsg = 'Assigned Employee is required (select Ticket first)';
      return false;
    }

    if (!this.reply.replyText || this.reply.replyText.trim() === '') {
      this.errMsg = 'Reply Text is required';
      return false;
    }

    if (!this.reply.repliedDate) {
      this.reply.repliedDate = this.todayDateOnly();
    }

    return true;
  }

  addReply() {
    if (!this.validateBeforeSave()) return;

    this.replySvc.addTicketReply(this.reply).subscribe({
      next: () => {
        alert('Reply Added Successfully!');
        this.showAllReplies();
        this.newReply();
      },
      error: (err) => this.handleError(err, 'Add failed'),
    });
  }

  updateReply() {
    if (!this.validateBeforeSave()) return;

    this.replySvc.UpdateTicketReply(this.reply.replyId.trim(), this.reply).subscribe({
      next: () => {
        alert('Reply Updated Successfully!');
        this.showAllReplies();
        this.newReply();
      },
      error: (err) => this.handleError(err, 'Update failed'),
    });
  }

  deleteReply() {
    this.errMsg = '';
    const id = (this.reply.replyId || '').trim();

    if (id === '') {
      this.errMsg = 'Enter Reply ID';
      return;
    }

    this.replySvc.deleteTicketReply(id).subscribe({
      next: () => {
        alert('Reply Deleted Successfully!');
        this.showAllReplies();
        this.newReply();
      },
      error: (err) => this.handleError(err, 'Delete failed'),
    });
  }

  // ✅ TicketId Filter
  filterByTicketId() {
    const tid = (this.filterTicketId || '').trim().toLowerCase();

    if (!tid) {
      this.filteredReplies = this.replies;
      return;
    }

    this.filteredReplies = this.replies.filter(r =>
      (r.ticketId || '').toLowerCase().includes(tid)
    );

    // sort by date
    this.filteredReplies.sort(
      (a, b) => new Date(a.repliedDate).getTime() - new Date(b.repliedDate).getTime()
    );
  }

  clearTicketFilter() {
    this.filterTicketId = '';
    this.filteredReplies = this.replies;
    this.errMsg = '';
  }
}
