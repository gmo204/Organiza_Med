import { Routes } from "@angular/router";
import { ListarAtividadesComponent } from "./listar/listar-atividade.component";
import { CadastrarAtividadeComponent } from "./cadastrar/cadastrar-atividade.component";

export const AtividadesRoutes: Routes = [
  { path: '', redirectTo: 'listar', pathMatch: 'full' },
  { path: 'listar', component: ListarAtividadesComponent },
  { path: 'cadastrar', component: CadastrarAtividadeComponent },
]
