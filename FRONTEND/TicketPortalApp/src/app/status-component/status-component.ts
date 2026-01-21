import { Component, inject } from '@angular/core';
import { StatusService } from '../services/status-service';
import { Status } from '../../models/Status';
import { CommonModule} from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-status-component',
  imports: [CommonModule, FormsModule],
  templateUrl: './status-component.html',
  styleUrl: './status-component.css',
})
export class StatusComponent {
  statusSvc: StatusService = inject(StatusService);
  statuss: Status[];
  status: Status;
  errMsg: string;
  constructor() {
    this.statuss = [];
    this.status = { statusId: '', statusName: '', description: '', isActive: true };
    this.errMsg = "";
    this.showAllStatuses();
  }
  showAllStatuses() {
    this.statusSvc.getAllStatuses().subscribe({
      next: (response: any) => {
        this.statuss = response;
        console.log(response);
        this.errMsg = "";
      },
      error: (err) => { this.errMsg = err.message; console.log(err); }
    });
  }
  saveStatus() {
    this.statusSvc.addStatus(this.status).subscribe({
      next: (response: any) => {
        alert("New status added");
        this.errMsg = "";
        this.showAllStatuses();
      },
      error: (err) => this.errMsg = err.error
    });
  }
  newStatus() {
    this.status = { statusId: '', statusName: '', description: '', isActive: true };
  }
  showStatus() {
    this.statusSvc.getStatus(this.status.statusId).subscribe({
      next: (response: any) => {
        this.status = response;
        this.errMsg = "";
        this.showAllStatuses();
      },
      error: (err) => {
        this.errMsg = err.message;
        console.log(err);
      }
    });
  }
  updateStatus() {
    this.statusSvc.updateStatus(this.status.statusId, this.status).subscribe({
      next: (response: any) => {
        alert("Status details updated");
        this.errMsg = "";
        this.showAllStatuses();
      },
      error: (err) => this.errMsg = err.error
    });
  }
  deleteStatus() {
    this.statusSvc.deleteStatus(this.status.statusId).subscribe({
      next: (response: any) => {
        alert("Status deleted");
        this.errMsg = "";
        this.showAllStatuses();
      },
      error: (err) => this.errMsg = err.error
    });
  }
}
