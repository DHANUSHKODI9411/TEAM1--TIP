import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TicketType } from '../../models/TicketType';

@Injectable({
  providedIn: 'root'
})
export class TickettypeService {
  private http: HttpClient = inject(HttpClient);

  private apiUrl: string = "http://localhost:5106/api/TicketType/";
  private getHeaders(): HttpHeaders {
    const token = sessionStorage.getItem("token");
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }
  getAllTicketTypes(): Observable<TicketType[]> {
    return this.http.get<TicketType[]>(this.apiUrl, { headers: this.getHeaders() });
  }
  getTicketType(id: string): Observable<TicketType> {
    return this.http.get<TicketType>(`${this.apiUrl}/${id}`, { headers: this.getHeaders() });
  }
  addTicketType(ticketType: TicketType): Observable<any> {
    return this.http.post<any>(this.apiUrl, ticketType, { headers: this.getHeaders() });
  }
  updateTicketType(id: string, ticketType: TicketType): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, ticketType, { headers: this.getHeaders() });
  }
  deleteTicketType(id: string): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`, { headers: this.getHeaders() });
  }
}
