import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  {
    path: 'dashboard',
    loadComponent: () =>
      import('./views/dashboard/dashboard.component').then(
        (m) => m.DashboardComponent
      ),
  },
  {
    path: 'medicos',
    loadChildren: () =>
      import('./views/medico/medicos.routes').then(
        (m) => m.MedicosRoutes
      ),
  },
];
