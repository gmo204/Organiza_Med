import { Routes } from "@angular/router";
import { ListagemMedicosResolver } from "../medico/services/listagem-medico.resolver";
import { EditarAtividadeComponent } from "./editar/editar-atividade.component";
import { ExcluirAtividadeComponent } from "./excluir/excluir-atividade.component";
import { ListarAtividadesComponent } from "./listar/listar-atividade.component";
import { ListagemAtividadeResolver } from "./services/listagem-atividade.resolver";
import { VisualizarAtividadeResolver } from "./services/visualizar-atividade.resolver";
import { CadastrarAtividadeComponent } from "./cadastrar/cadastrar-cirurgia.component";


export const AtividadesRoutes: Routes = [
  {
    path: '',
    redirectTo: 'listar',
    pathMatch: 'full'
  },
  {
    path: 'listar',
    component: ListarAtividadesComponent,
    resolve: {
      atividades: ListagemAtividadeResolver,
      medicos: ListagemMedicosResolver
    }
  },
  {
    path: 'cadastrar',
    component: CadastrarAtividadeComponent,
    resolve: { medicos: ListagemMedicosResolver }
  },
  {
    path: 'editar/:id',
    component: EditarAtividadeComponent,
    resolve: {
      atividade: VisualizarAtividadeResolver,
      medicos: ListagemMedicosResolver }
  },
  {
    path: 'excluir/:id',
    component: ExcluirAtividadeComponent,
    resolve: {
      atividade: VisualizarAtividadeResolver}
  }
]
