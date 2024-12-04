import { inject } from "@angular/core";
import { ResolveFn, ActivatedRouteSnapshot } from "@angular/router";
import { VisualizarAtividadeViewModel } from "../models/atividade.models";
import { AtividadeService } from "./atividade.service";

export const VisualizarAtividadeResolver: ResolveFn<
  VisualizarAtividadeViewModel
> = (route: ActivatedRouteSnapshot) => {
  const id = route.params['id'];

  return inject(AtividadeService).selecionarPorId(id);
};
