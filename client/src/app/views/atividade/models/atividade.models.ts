import { ListarMedicoViewModel } from "../../medico/models/medico.model";

export enum TipoAtividadeEnum {
  Consulta,
  Cirurgia
}

export interface InserirAtividadeViewModel {
  tipo : number;
  horaInicio: Date;
  horaFim: Date;

  medicoId: string[];
}

export interface AtividadeInseridaViewModel {
  tipo : number;
  horaInicio: Date;
  horaFim: Date;

  medicoId: string[];
}

export interface EditarAtividadeViewModel {
  tipo : number;
  horaInicio: Date;
  horaFim: Date;

  medicoId: string[];
}

export interface AtividadeEditadaViewModel {
  tipo : number;
  horaInicio: Date;
  horaFim: Date;

  medicoId: string[];
}

export interface ListarAtividadeViewModel {
  id: string;
  tipo : number;
  horaInicio: Date;
  horaFim: Date;
}

export interface VisualizarAtividadeViewModel {
  id: string;
  tipo : number;
  horaInicio: Date;
  horaFim: Date;

  medico: ListarMedicoViewModel[];
}

export interface AtividadeExcluidaViewModel {}
