import { Injectable ,inject} from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SLA } from '../../models/SLA';

@Injectable({
  providedIn: 'root',
})
export class SlaService{
  http: HttpClient = inject(HttpClient);
  httpOptions;
  token;
  baseUrl: string = 'http://localhost:5106/api/SLA/';
  constructor() {
    this.token = sessionStorage.getItem("token");
    this.httpOptions = { headers: new HttpHeaders({
      'Authorization': 'Bearer ' + this.token
    })};
  }
  getAllSlas(): Observable<SLA[]> {
  return this.http.get<SLA[]>(this.baseUrl);
  }

  getSla(slAid: string): Observable<SLA> {
    return this.http.get<SLA>(this.baseUrl+slAid);
  }

  addSla(sla: any): Observable<SLA> {
    return this.http.post<SLA>(this.baseUrl, sla);
  }

  updateSla(slAidid: string, sla:SLA): Observable<SLA> {
    return this.http.put<SLA>(this.baseUrl+sla.slAid, sla);
  }

  deleteSla(slAid: string): Observable<any> {
    return this.http.delete(this.baseUrl+slAid);
  }
}
