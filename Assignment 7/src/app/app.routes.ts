import { Routes } from '@angular/router';
import { Dashboard } from './pages/dashboard/dashboard';
import { Drivers } from './pages/drivers/drivers';
import { Vehicles } from './pages/vehicles/vehicles';
 import { Trips } from './pages/trips/trips';

export const routes: Routes = [
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    { path: 'dashboard', component: Dashboard },
    { path: 'drivers', component: Drivers },
    { path: 'vehicles', component: Vehicles },
    { path: 'trips', component: Trips },
];
