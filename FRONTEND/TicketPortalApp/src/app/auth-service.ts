import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  http:HttpClient=inject(HttpClient);
  baseUrl:string="http://localhost:5106/api/Auth/";
  userName:string="ramesh@ey.com";
  role:string="Admin";
  empNameSignal = signal<string | null>(sessionStorage.getItem('empName'));
  secretKey:string="Ticket Portal App Created by Team 1.";
  getToken():Observable<string>{
    return this.http.get(this.baseUrl+this.userName+"/"+this.role+"/"+this.secretKey,{responseType:'text'});
  }
  setLogin(empName: string, role?: string) {
  sessionStorage.setItem('empName', empName);
  if (role) {
    sessionStorage.setItem('userRole', role);
  }
  this.empNameSignal.set(empName);
}
  logout() {
    sessionStorage.clear();
    this.empNameSignal.set(null);
  }
}
