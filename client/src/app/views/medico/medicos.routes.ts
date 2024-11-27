import { Routes } from "@angular/router";
import { ListagemMedicosComponent } from "./listar/listar-medico.component";

export const MedicosRoutes : Routes = [
  {
    path: '',
    redirectTo: 'listar',
    pathMatch: 'full',
  },
  { path: 'listar', component: ListagemMedicosComponent },
]
