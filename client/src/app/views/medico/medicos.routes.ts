import { Routes } from "@angular/router";
import { ListagemMedicosComponent } from "./listar/listar-medico.component";
import { CadastrarMedicoComponent } from "./cadastrar/cadastrar-medico.component";
import { EditarMedicoComponent } from "./editar/editar-medico.component";

export const MedicosRoutes : Routes = [
  {
    path: '',
    redirectTo: 'listar',
    pathMatch: 'full',
  },
  { path: 'listar', component: ListagemMedicosComponent },
  { path: 'cadastrar', component: CadastrarMedicoComponent },
  { path: 'editar', component: EditarMedicoComponent },

]
