import { Routes } from "@angular/router";
import { ListagemMedicosComponent } from "./listar/listar-medico.component";
import { CadastrarMedicoComponent } from "./cadastrar/cadastrar-medico.component";
import { EditarMedicoComponent } from "./editar/editar-medico.component";
import { ExcluirMedicoComponent } from "./excluir/excluir-medico.component";

export const MedicosRoutes : Routes = [
  {
    path: '',
    redirectTo: 'listar',
    pathMatch: 'full',
  },
  { path: 'listar', component: ListagemMedicosComponent },
  { path: 'cadastrar', component: CadastrarMedicoComponent },
  { path: 'editar/:id', component: EditarMedicoComponent },
  { path: 'excluir/:id', component: ExcluirMedicoComponent }

]
