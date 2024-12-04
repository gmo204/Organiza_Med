import { Routes } from "@angular/router";
import { ListagemMedicosResolver } from "../medico/services/listagem-medico.resolver";
import { CadastrarCirurgiaComponent } from "./cadastrar-cirurgia/cadastrar-cirurgia.component";
import { CadastrarConsultaComponent } from "./cadastrar-consulta/cadastrar-atividade.component";
import { EditarAtividadeComponent } from "./editar/editar-atividade.component";
import { ExcluirAtividadeComponent } from "./excluir/excluir-atividade.component";
import { ListarAtividadesComponent } from "./listar/listar-atividade.component";
import { listagemAtividadeResolver } from "./services/listagem-atividade.resolver";
import { visualizarAtividadeResolver } from "./services/visualizar-atividade.resolver";


export const AtividadesRoutes: Routes = [
  {
    path: '',
    redirectTo: 'listar',
    pathMatch: 'full'
  },
  {
    path: 'listar',
    component: ListarAtividadesComponent,
    resolve: { atividade: listagemAtividadeResolver }
  },
  {
    path: 'cadastrar-consulta',
    component: CadastrarConsultaComponent,
    resolve: { medicos: ListagemMedicosResolver }
  },
  {
    path: 'cadastrar-cirurgia',
    component: CadastrarCirurgiaComponent,
    resolve: { medicos: ListagemMedicosResolver }
  },
  {
    path: 'editar/:id',
    component: EditarAtividadeComponent,
    resolve: {
      atividade: visualizarAtividadeResolver,
      medicos: ListagemMedicosResolver }
  },
  {
    path: 'excluir/:id',
    component: ExcluirAtividadeComponent,
    resolve: {
      atividade: visualizarAtividadeResolver}
  }
]
