import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { IEmployee, IEmployeeDto } from '../interfaces/IEmployee.interface';
import { IManager } from '../interfaces/IManager.interface';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {

  constructor(private http: HttpClient) { }

  url = "http://localhost:5200/api"

  get(idNumber: string): Observable<IEmployee> {
    return this.http.get<IEmployee>(`${this.url}/Get/${idNumber}`);
  }
 //
  getAll(isFilter: boolean): Observable<IEmployee[]> {
    return this.http.get<IEmployee[]>(`${this.url}/GetAll?isFilter=${ isFilter }`);
  }

  getManagers(): Observable<IManager[]> {
    return this.http.get<IManager[]>(`${this.url}/GetManagers`);
  }

  add(data: IEmployeeDto): Observable<any> {
    return this.http.post(this.url, data);
  }

  edit(idNumber: string, data: IEmployeeDto): Observable<any> {
    let API_URL = `${this.url}/${idNumber}`;
    return this.http.put(API_URL, data);
  }

  delete(idNumber: string): Observable<any> {
    var API_URL = `${this.url}/${idNumber}`;
    return this.http.delete(API_URL);
  }

}
