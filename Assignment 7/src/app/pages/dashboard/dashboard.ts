import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TripService } from '../../services/trip.service';
import { VehicleService } from '../../services/vehicle.service';
import { Trip } from '../../Models/trip.model';
import { Vehicle } from '../../Models/vehicle.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.css']
})
export class Dashboard implements OnInit {

  activeTrips = 0;
  completedTrips = 0;
  availableVehicles = 0;
  longTrips: Trip[] = [];

  loading = true;
  errorMessage = '';

  constructor(
    private tripService: TripService,
    private vehicleService: VehicleService
  ) {}

  ngOnInit(): void {
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this.loading = true;

    this.tripService.getAll().subscribe({
      next: (trips: Trip[]) => {
        // Normalize status case (in case backend uses 'Completed' or 'completed')
        this.activeTrips = trips.filter(t => t.status?.toLowerCase() === 'active').length;
        this.completedTrips = trips.filter(t => t.status?.toLowerCase() === 'completed').length;
      },
      error: () => {
        this.errorMessage = 'Failed to load trip data.';
      }
    });

    this.vehicleService.getAll().subscribe({
      next: (vehicles: Vehicle[]) => {
        this.availableVehicles = vehicles.filter(v => v.isAvailable).length;
        this.loading = false;
      },
      error: () => {
        this.errorMessage = 'Failed to load vehicle data.';
        this.loading = false;
      }
    });
  }

}
