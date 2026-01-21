import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Status } from '../../models/Status';

@Injectable({
  providedIn: 'root',
})
export class StatusService {
  http: HttpClient = inject(HttpClient);
  httpOptions;
  token;
  baseUrl: string = "http://localhost:5106/api/Status/";
  constructor() {
    this.token = sessionStorage.getItem("token");
    this.httpOptions = { headers: new HttpHeaders({
      'Authorization': 'Bearer ' + this.token
    })};
  }
  getAllStatuses(): Observable<Status[]> {
    return this.http.get<Status[]>(this.baseUrl, this.httpOptions);
  }
  addStatus(status: Status): Observable<Status> {
    return this.http.post<Status>(this.baseUrl, status, this.httpOptions);
  }
  getStatus(statusId: string): Observable<Status> {
    return this.http.get<Status>(`${this.baseUrl}${statusId}`, this.httpOptions);
  }
  updateStatus(statusId: string, status: Status): Observable<Status> {
    return this.http.put<Status>(`${this.baseUrl}${statusId}`, status, this.httpOptions);
  }
  deleteStatus(statusId: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}${statusId}`, this.httpOptions);
  }
}
