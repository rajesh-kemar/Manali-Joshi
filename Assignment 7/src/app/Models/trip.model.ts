import { Driver } from "./driver.model";
import { Vehicle } from "./vehicle.model";

export interface Trip {
  id?: number;
  vehicleId: number;
  driverId: number;
  source: string;
  destination: string;
  startTime?: string;
  endTime?: string;
  status?: string;
  driver?: Driver;
  vehicle?: Vehicle;
}
