import { Injectable,inject} from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from '../../models/Employee';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  http:HttpClient=inject(HttpClient);
  token;
  baseUrl:string="http://localhost:5106/api/Employee/";
  httpOptions;
  constructor(){this.token=sessionStorage.getItem("token");
    this.httpOptions={
      headers:new HttpHeaders({
        'Authorization':'Bearer'+this.token
      })
    }
  };
  getallemployees():Observable<Employee[]>{
    return this.http.get<Employee[]>(this.baseUrl,this.httpOptions)
  }
  getOneEmployee(employeeId:string):Observable<Employee>{
    return this.http.get<Employee>(this.baseUrl+employeeId,this.httpOptions)
  }
  addEmployee(employee:Employee):Observable<Employee>{
    return this.http.post<Employee>(this.baseUrl,employee,this.httpOptions)
  }
  updateEmployee(employeeId:string,employee:Employee):Observable<Employee>{
    return this.http.put<Employee>(this.baseUrl+employeeId,employee,this.httpOptions)
  }
  deleteEmployee(employeeId:string):Observable<any>{
    return this.http.delete(this.baseUrl+employeeId,this.httpOptions)
  }
  login(employeeId: string, password: string): Observable<any> {
    return this.http.get<any>(this.baseUrl + "login/" + employeeId + "/" + password);
  }
}
