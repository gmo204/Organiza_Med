import { Routes } from "@angular/router";
import { ListarAtividadesComponent } from "./listar/listar-atividade.component";
import { CadastrarAtividadeComponent } from "./cadastrar/cadastrar-atividade.component";
import { EditarAtividadeComponent } from "./editar/editar-atividade.component";
import { ExcluirAtividadeComponent } from "./excluir/excluir-atividade.component";

export const AtividadesRoutes: Routes = [
  {
    path: '',
    redirectTo: 'listar',
    pathMatch: 'full'
  },
  { path: 'listar', component: ListarAtividadesComponent },
  { path: 'cadastrar', component: CadastrarAtividadeComponent },
  { path: 'editar', component: EditarAtividadeComponent },
  { path: 'excluir', component: ExcluirAtividadeComponent }
]
