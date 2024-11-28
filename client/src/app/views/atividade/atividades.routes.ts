import { Routes } from "@angular/router";
import { ListarAtividadesComponent } from "./listar/listar-atividade.component";

export const AtividadesRoutes: Routes = [
  { path: '', redirectTo: 'listar', pathMatch: 'full' },
  { path: 'listar', component: ListarAtividadesComponent },
]
