import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TicketrepliesServices } from '../services/ticketreplies-services';
import { TicketReplies } from '../../models/TicketReplies';

@Component({
  selector: 'app-ticketreplies-component',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './ticketreplies-component.html',
  styleUrls: ['./ticketreplies-component.css'],
})
export class TicketrepliesComponent implements OnInit  {
  ticketrepliesSvc: TicketrepliesServices = inject(TicketrepliesServices);
  ticketreplies: TicketReplies[];
  errMsg: string;
  reply: TicketReplies;
  constructor() {
    this.ticketreplies = [];
    this.errMsg = "";
    this.reply = new TicketReplies();
  }
  ngOnInit(): void {
    this.showAllTicketReplies();
  }
  showAllTicketReplies(): void {
    this.ticketrepliesSvc.GetAllTicketReplies().subscribe({
      next: (response) => {
        this.ticketreplies = response;
        this.errMsg = "";
      },
      error: (err) => {
        this.errMsg = err.message;
      }
    });
  }
  addTicketReply(): void {
    this.ticketrepliesSvc.addTicketReply(this.reply).subscribe({
      next: (response: TicketReplies) => {
        this.ticketreplies.push(response);
        this.reply = new TicketReplies("", "", "", "", "", new Date());
        alert("Reply added successfully");
        this.errMsg = "";
      },
      error: (err) => {
        this.errMsg = err.message;
      }
    });
  }
  showTicketReply(): void {
    this.ticketrepliesSvc.GetTicketReply(this.reply.replyId).subscribe({
      next: (response: TicketReplies) => {
        this.reply = response;
        this.errMsg = "";
      },
      error: (err) => {
        this.errMsg = err.message;
      }
    });
  }
  updateTicketReply(): void {
    this.ticketrepliesSvc.UpdateTicketReply(this.reply.replyId, this.reply).subscribe({
      next: () => {
        this.showAllTicketReplies();
        alert("Reply updated successfully");
        this.errMsg = "";
      },
      error: (err) => {
        this.errMsg = err.message;
      }
    });
  }
  deleteTicketReply(): void {
    this.ticketrepliesSvc.deleteTicketReply(this.reply.replyId).subscribe({
      next: () => {
        this.showAllTicketReplies();
        alert("Reply deleted successfully");
        this.errMsg = "";
      },
      error: (err) => {
        this.errMsg = err.message;
      }
    });
  }
}
