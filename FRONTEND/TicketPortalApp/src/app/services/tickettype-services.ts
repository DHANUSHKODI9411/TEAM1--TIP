import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TicketType } from '../../models/TicketType';

@Injectable({
  providedIn: 'root'
})
export class TickettypeService {
  private http: HttpClient = inject(HttpClient);
  
  // URL pointing to your backend on port 5106
  private apiUrl: string = "http://localhost:5106/api/TicketType/";

  /**
   * Generates headers including the Bearer token from Session Storage.
   * This ensures your API requests are authorized.
   */
  private getHeaders(): HttpHeaders {
    const token = sessionStorage.getItem("token");
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }

  // GET: Retrieve all records
  getAllTicketTypes(): Observable<TicketType[]> {
    return this.http.get<TicketType[]>(this.apiUrl, { headers: this.getHeaders() });
  }

  // GET: Retrieve a single record by its ID
  getTicketType(id: string): Observable<TicketType> {
    return this.http.get<TicketType>(`${this.apiUrl}/${id}`, { headers: this.getHeaders() });
  }

  // POST: Create a new record
  addTicketType(ticketType: TicketType): Observable<any> {
    return this.http.post<any>(this.apiUrl, ticketType, { headers: this.getHeaders() });
  }

  // PUT: Update an existing record
  updateTicketType(id: string, ticketType: TicketType): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, ticketType, { headers: this.getHeaders() });
  }

  // DELETE: Remove a record
  deleteTicketType(id: string): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`, { headers: this.getHeaders() });
  }
}import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TicketType } from '../../models/TicketType';

@Injectable({
  providedIn: 'root'
})
export class TickettypeService {
  private http: HttpClient = inject(HttpClient);
  
  // URL pointing to your backend on port 5106
  private apiUrl: string = "http://localhost:5106/api/TicketType/";

  /**
   * Generates headers including the Bearer token from Session Storage.
   * This ensures your API requests are authorized.
   */
  private getHeaders(): HttpHeaders {
    const token = sessionStorage.getItem("token");
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }

  // GET: Retrieve all records
  getAllTicketTypes(): Observable<TicketType[]> {
    return this.http.get<TicketType[]>(this.apiUrl, { headers: this.getHeaders() });
  }

  // GET: Retrieve a single record by its ID
  getTicketType(id: string): Observable<TicketType> {
    return this.http.get<TicketType>(`${this.apiUrl}/${id}`, { headers: this.getHeaders() });
  }

  // POST: Create a new record
  addTicketType(ticketType: TicketType): Observable<any> {
    return this.http.post<any>(this.apiUrl, ticketType, { headers: this.getHeaders() });
  }

  // PUT: Update an existing record
  updateTicketType(id: string, ticketType: TicketType): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, ticketType, { headers: this.getHeaders() });
  }

  // DELETE: Remove a record
  deleteTicketType(id: string): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`, { headers: this.getHeaders() });
  }
}