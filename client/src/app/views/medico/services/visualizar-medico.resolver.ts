import { inject } from "@angular/core";
import { ResolveFn, ActivatedRouteSnapshot } from "@angular/router";
import { VisualizarMedicoViewModel } from "../models/medico.model";
import { MedicoService } from "./medico.service";

export const visualizarMedicoResolver: ResolveFn<
  VisualizarMedicoViewModel
> = (route: ActivatedRouteSnapshot) => {
  const id = route.params['id'];

  return inject(MedicoService).selecionarPorId(id);
};
