import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Trip } from '../../Models/trip.model';
import { Driver } from '../../Models/driver.model';
import { Vehicle } from '../../Models/vehicle.model';
import { TripService } from '../../services/trip.service';
import { DriverService } from '../../services/driver.service';
import { VehicleService } from '../../services/vehicle.service';

@Component({
  selector: 'app-trips',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './trips.html',
  styleUrls: ['./trips.css']
})
export class Trips implements OnInit {
  trips: Trip[] = [];
  drivers: Driver[] = [];
  vehicles: Vehicle[] = [];

  newTrip: Trip = {
    driverId: 0,
    vehicleId: 0,
    source: '',
    destination: '',
    startTime: '',
    endTime: ''
  };

  constructor(
    private tripService: TripService,
    private driverService: DriverService,
    private vehicleService: VehicleService
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.tripService.getAll().subscribe((data) => (this.trips = data));
    this.driverService.getAll().subscribe((data) => (this.drivers = data));
    this.vehicleService.getAll().subscribe((data) => (this.vehicles = data));
  }

  createTrip(): void {
    if (!this.newTrip.driverId || !this.newTrip.vehicleId) {
      alert('Please select driver and vehicle.');
      return;
    }

    this.tripService.createTrip(this.newTrip).subscribe({
      next: () => {
        this.resetForm();
        this.loadData();
      },
      error: (err) => console.error('Error creating trip', err)
    });
  }

  // Handle status change from dropdown
  onStatusChange(trip: Trip): void {
    if (trip.id && trip.status) {
      this.tripService.updateTripStatus(trip.id, trip.status).subscribe({
        next: () => this.loadData(),
        error: (err) => console.error('Error updating trip status', err)
      });
    }
  }

  completeTrip(id: number): void {
    this.tripService.completeTrip(id).subscribe({
      next: () => this.loadData(),
      error: (err) => console.error('Error completing trip', err)
    });
  }

  deleteTrip(id: number): void {
    if (confirm('Are you sure you want to delete this trip?')) {
      this.tripService.deleteTrip(id).subscribe({
        next: () => this.loadData(),
        error: (err) => console.error('Error deleting trip', err)
      });
    }
  }

  private resetForm(): void {
    this.newTrip = {
      driverId: 0,
      vehicleId: 0,
      source: '',
      destination: '',
      startTime: '',
      endTime: ''
    };
  }
}
