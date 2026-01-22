import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TicketReplies } from '../../models/TicketReplies';

@Injectable({
  providedIn: 'root',
})
export class TicketrepliesServices {

  http: HttpClient = inject(HttpClient);
  baseUrl: string = 'http://localhost:5106/api/TicketReplies/';
  token;
  httpOptions;
  constructor(){
    this.token = sessionStorage.getItem("token")
    this.httpOptions = {headers: new HttpHeaders ({
      'Authorization': 'Bearer ' + this.token
    })};
  }
  addTicketReply(reply: TicketReplies): Observable<TicketReplies>{
    return this.http.post<TicketReplies>(this.baseUrl, reply, this.httpOptions);
  }

  UpdateTicketReply(replyId: string, reply: TicketReplies): Observable<TicketReplies>{
    return this.http.put<TicketReplies>(this.baseUrl + replyId, reply, this.httpOptions);
  }

  deleteTicketReply(replyId: string): Observable<any>{
    return this.http.delete(this.baseUrl + replyId , this.httpOptions);
  }

  GetAllTicketReplies(): Observable<TicketReplies[]>{
    return this.http.get<TicketReplies[]>(this.baseUrl, this.httpOptions);
  }

  GetTicketReply(replyId: string): Observable<TicketReplies>{
    return this.http.get<TicketReplies>(this.baseUrl + replyId, this.httpOptions);
  }

  getRepliesByTicket(ticketId: string): Observable<TicketReplies[]>{
  return this.http.get<TicketReplies[]>(this.baseUrl + 'byticket/' + ticketId, this.httpOptions);
}

getRepliesByCreatedEmployee(employeeId: string): Observable<TicketReplies[]>{
  return this.http.get<TicketReplies[]>(this.baseUrl + 'bycreated/' + employeeId, this.httpOptions);
}

getRepliesByAssignedEmployee(assignedEmployeeId: string): Observable<TicketReplies[]>{
  return this.http.get<TicketReplies[]>(this.baseUrl + 'byassigned/' + assignedEmployeeId, this.httpOptions);
}

}
