import { Component, inject } from '@angular/core';
import { SlaService } from '../services/sla-service';
import { FormsModule } from '@angular/forms';
import { SLA } from '../../models/SLA';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-sla-component',
  imports: [FormsModule, CommonModule],
  templateUrl: './sla-component.html',
  styleUrl: './sla-component.css',
})
export class SLAComponent {
  slaSvc: SlaService = inject(SlaService);
  sla: SLA;
  slas: SLA[];
  errMsg: string;
  constructor() {
    this.sla = new SLA('', '', '','', 1, 1);
    this.slas = [];
    this.errMsg = '';
    this.showAllSlas();
  }
  newSla() {
     this.sla = new SLA('', '', '','', 1, 1);
  }
  showAllSlas() {
    this.slaSvc.getAllSlas().subscribe({
      next: (response: any) => {
        console.log(response);
        this.slas = response;
        this.errMsg = '';
      },
      error: (err) => (this.errMsg = err.error),
    });
  }
  getSla() {
    this.slaSvc.getSla(this.sla.slAid).subscribe({
      next: (response: any) => {
        this.sla = response;
        this.errMsg = '';
        this.showAllSlas();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }
  addSla() {
    this.slaSvc.addSla(this.sla).subscribe({
      next: () => {
        alert('New SLA Added!');
        this.errMsg = '';
        this.showAllSlas();
        this.newSla();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }
  updateSla() {
    this.slaSvc.updateSla(this.sla.slAid, this.sla).subscribe({
      next: () => {
        alert('SLA Updated Successfully!');
        this.errMsg = '';
        this.showAllSlas();
        this.newSla();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }
  deleteSla() {
    this.slaSvc.deleteSla(this.sla.slAid).subscribe({
      next: () => {
        alert('SLA Deleted Successfully!');
        this.errMsg = '';
        this.showAllSlas();
        this.newSla();
      },
      error: (err) => (this.errMsg = err.error),
    });
  }
}
