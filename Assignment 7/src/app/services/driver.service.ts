import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Driver } from '../Models/driver.model';

@Injectable({
  providedIn: 'root',
})
export class DriverService {
  
  private baseUrl = 'http://localhost:5230/api/Drivers';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Driver[]> {
    return this.http.get<Driver[]>(this.baseUrl);
  }
  create(driver: Driver):Observable<Driver> {
    return this.http.post<Driver>(this.baseUrl,driver);
  }
  
}
