import { inject } from "@angular/core";
import { ResolveFn } from "@angular/router";
import { ListarMedicoViewModel } from "../models/medico.model";
import { MedicoService } from "./medico.service";

export const ListagemMedicosResolver: ResolveFn<
  ListarMedicoViewModel[]
> = () => {
  return inject(MedicoService).selecionarTodos();
};
