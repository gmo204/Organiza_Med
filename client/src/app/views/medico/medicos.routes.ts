import { Routes } from "@angular/router";
import { ListagemMedicosComponent } from "./listar/listar-medico.component";
import { CadastrarMedicoComponent } from "./cadastrar/cadastrar-medico.component";
import { EditarMedicoComponent } from "./editar/editar-medico.component";
import { ExcluirMedicoComponent } from "./excluir/excluir-medico.component";
import { ListagemMedicosResolver } from "./services/listagem-medico.resolver";
import { visualizarMedicoResolver } from "./services/visualizar-medico.resolver";

export const MedicosRoutes : Routes = [
  {
    path: '',
    redirectTo: 'listar',
    pathMatch: 'full',
  },
  {
    path: 'listar',
    component: ListagemMedicosComponent,
    resolve: { medico: ListagemMedicosResolver }
  },
  {
    path: 'cadastrar',
    component: CadastrarMedicoComponent,
  },
  {
    path: 'editar/:id',
    component: EditarMedicoComponent,
    resolve: { medico: visualizarMedicoResolver}

  },
  {
    path: 'excluir/:id',
    component: ExcluirMedicoComponent,
    resolve: { medico: visualizarMedicoResolver}
  }
]
