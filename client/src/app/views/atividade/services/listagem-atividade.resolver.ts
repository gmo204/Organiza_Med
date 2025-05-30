import { inject } from "@angular/core";
import { ResolveFn } from "@angular/router";
import { ListarAtividadeViewModel } from "../models/atividade.models";
import { AtividadeService } from "./atividade.service";

export const ListagemAtividadeResolver: ResolveFn<
  ListarAtividadeViewModel[]
> = () => {
  return inject(AtividadeService).selecionarTodos();
};
