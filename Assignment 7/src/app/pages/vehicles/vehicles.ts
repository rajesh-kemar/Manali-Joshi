import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../../services/vehicle.service';
import { Vehicle } from '../../Models/vehicle.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-vehicles',
  imports: [CommonModule, FormsModule],
  standalone: true,
  templateUrl: './vehicles.html',
  styleUrls: ['./vehicles.css'],
})
export class Vehicles implements OnInit {
  vehicles: Vehicle[] = [];
  newVehicle: Vehicle = { plateNumber: '', model: '', isAvailable: true };
  editMode: boolean = false;
  selectedVehicle: Vehicle | null = null;

  constructor(private vehicleService: VehicleService) {}

  ngOnInit(): void {
    this.loadVehicles();
  }

  // Load all vehicles
  loadVehicles() {
    this.vehicleService.getAll().subscribe((data) => (this.vehicles = data));
  }

  // Add or Update Vehicle
  addVehicle() {
    if (this.editMode && this.selectedVehicle) {
      // Ensure id is included for update
      this.newVehicle.id = this.selectedVehicle.id;

      this.vehicleService
        .update(this.selectedVehicle.id!, this.newVehicle)
        .subscribe(() => {
          this.cancelEdit();
          this.loadVehicles();
        });
    } else {
      this.vehicleService.create(this.newVehicle).subscribe(() => {
        this.newVehicle = { plateNumber: '', model: '', isAvailable: true };
        this.loadVehicles();
      });
    }
  }

  // Edit vehicle
  editVehicle(vehicle: Vehicle) {
    this.editMode = true;
    this.selectedVehicle = vehicle;
    // Clone the vehicle so changes don't immediately affect table
    this.newVehicle = { ...vehicle };
  }

  // Cancel edit mode
  cancelEdit() {
    this.editMode = false;
    this.selectedVehicle = null;
    this.newVehicle = { plateNumber: '', model: '', isAvailable: true };
  }

  // Delete vehicle
  deleteVehicle(id: number) {
    if (confirm('Are you sure you want to delete this vehicle?')) {
      this.vehicleService.delete(id).subscribe(() => this.loadVehicles());
    }
  }
}
