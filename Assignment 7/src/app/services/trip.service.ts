import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Trip } from '../Models/trip.model';

@Injectable({ providedIn: 'root' })
export class TripService {
  private baseUrl = 'http://localhost:5230/api/Trips';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Trip[]> {
    return this.http.get<Trip[]>(this.baseUrl);
  }

  createTrip(trip: Trip): Observable<Trip> {
    return this.http.post<Trip>(this.baseUrl, trip);
  }

  updateTripStatus(id: number, status: string): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, { id, status });
  }

  completeTrip(id: number): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/${id}/complete`, {});
  }

  deleteTrip(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  //   getLongTrips(): Observable<Trip[]> {
//     return this.http.get<Trip[]>(`${this.baseUrl}/long-trips`);
//   }

}
