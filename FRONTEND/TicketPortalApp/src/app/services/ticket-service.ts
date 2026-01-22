import { Injectable,inject } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Ticket } from '../../models/Ticket';

@Injectable({
  providedIn: 'root',
})
export class TicketService {
  /* http:HttpClient=inject(HttpClient);
  token;
  baseUrl:string="http://localhost:5106/api/Ticket/";
  httpOptions;
  constructor(){this.token=sessionStorage.getItem("token");
    this.httpOptions={
      headers:new HttpHeaders({
        'Authorization':'Bearer'+this.token
      })
    }
  }; */
  http: HttpClient = inject(HttpClient);
  baseUrl: string = "http://localhost:5106/api/Ticket/";
  private get httpOptions() {
    const token = sessionStorage.getItem("token");
    return {
      headers: new HttpHeaders({
        'Authorization': token ? `Bearer ${token}` : ''
      })
    };
  }
  getalltickets():Observable<Ticket[]>{
    return this.http.get<Ticket[]>(this.baseUrl,this.httpOptions)
  }
  getOneTicket(ticketId:string):Observable<Ticket>{
    return this.http.get<Ticket>(this.baseUrl+ticketId,this.httpOptions)
  }
  addticket(ticket:Ticket):Observable<Ticket>{
    return this.http.post<Ticket>(this.baseUrl,ticket,this.httpOptions)
  }
  updateticket(ticketId:string,ticket:Ticket):Observable<Ticket>{
    return this.http.put<Ticket>(this.baseUrl+ticketId,ticket,this.httpOptions)
  }
  deleteTicket(ticketId:string):Observable<any>{
    return this.http.delete(this.baseUrl+ticketId,this.httpOptions)
  }
  getTicketsByCreatedEmployee(createdEmployeeId: string): Observable<Ticket[]> {
  return this.http.get<Ticket[]>(this.baseUrl + 'bycreatedemployee/' + createdEmployeeId,this.httpOptions);
  }
  getTicketsByAssignedEmployee(assignedEmployeeId: string): Observable<Ticket[]> {
  return this.http.get<Ticket[]>(this.baseUrl + 'byassignedemployee/' + assignedEmployeeId,this.httpOptions);
  }
  getTicketsByStatus(statusId: string): Observable<Ticket[]> {
  return this.http.get<Ticket[]>(this.baseUrl + 'bystatus/' + statusId,this.httpOptions);
  }
  getTicketsByTicketType(ticketTypeId: string): Observable<Ticket[]> {
  return this.http.get<Ticket[]>(this.baseUrl + 'bytickettype/' + ticketTypeId,this.httpOptions);
  }

 }
