import { Routes } from "@angular/router";
import { ListagemMedicosComponent } from "./listar/listar-medico.component";
import { CadastrarMedicoComponent } from "./cadastrar/cadastrar-medico.component";
import { EditarMedicoComponent } from "./editar/editar-medico.component";
import { ExcluirMedicoComponent } from "./excluir/excluir-medico.component";
import { ListagemMedicosResolver } from "./services/listagem-medico.resolver";
import { VisualizarMedicoResolver } from "./services/visualizar-medico.resolver";

export const MedicosRoutes : Routes = [
  {
    path: '',
    redirectTo: 'listar',
    pathMatch: 'full',
  },
  {
    path: 'listar',
    component: ListagemMedicosComponent,
    resolve: { medicos: ListagemMedicosResolver }
  },
  {
    path: 'cadastrar',
    component: CadastrarMedicoComponent,
  },
  {
    path: 'editar/:id',
    component: EditarMedicoComponent,
    resolve: { medico: VisualizarMedicoResolver}

  },
  {
    path: 'excluir/:id',
    component: ExcluirMedicoComponent,
    resolve: { medico: VisualizarMedicoResolver}
  }
]
