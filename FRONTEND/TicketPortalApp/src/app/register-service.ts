import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from '../models/Employee';

@Injectable({
  providedIn: 'root',
})
export class RegisterService {
  http: HttpClient = inject(HttpClient);
  baseUrl: string = "http://localhost:5106/api/employee/";
  register(user: Employee): Observable<any> {
    return this.http.post(this.baseUrl, user);
  }
}
