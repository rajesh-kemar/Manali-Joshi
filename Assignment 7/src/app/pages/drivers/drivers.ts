import { Component, OnInit } from '@angular/core';
import { Driver } from '../../Models/driver.model';
import { DriverService } from '../../services/driver.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-drivers',
  imports: [CommonModule, FormsModule],
  standalone: true,
  templateUrl: './drivers.html',
  styleUrls: ['./drivers.css'],
})
export class Drivers implements OnInit {
  drivers: Driver[] = [];
  newDriver: Driver = { name: '', licenseNumber: '', phoneNumber: ''};

  constructor(private driverService: DriverService) {}

  ngOnInit(): void {
    this.loadDrivers();
  }

  // Load all drivers from backend
  loadDrivers(): void {
    this.driverService.getAll().subscribe({
      next: (data) => {
        this.drivers = data || []; // Spread operator ensures Angular detects changes
      },
      error: (err) => {
        console.error('Error loading drivers:', err);
      },
    });
  }

  // Add a new driver
  addDriver(): void {
    if (!this.newDriver.name || !this.newDriver.licenseNumber || !this.newDriver.phoneNumber) {
      alert('Please fill all fields!');
      return;
    }

    this.driverService.create(this.newDriver).subscribe({
      next: () => {
        this.newDriver = { name: '', licenseNumber: '', phoneNumber: '' };
        this.loadDrivers();
      },
      error: (err) => {
        console.error('Error adding driver:', err);
      },
    });
  }
}
