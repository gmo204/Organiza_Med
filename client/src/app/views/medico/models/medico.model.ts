export interface InserirMedicoViewModel {
  nome: string;
  crm: string;
}

export interface MedicoInseridoViewModel{
  nome: string;
  crm: string;
  ocupado: boolean;
}

export interface EditarMedicoViewModel {
  nome: string;
  crm: string;
}

export interface MedicoEditadoViewModel {
  nome: string;
  crm: string;
}

export interface ListarMedicoViewModel {
  id: string;
  nome: string;
  crm: string;
  ocupado: boolean;
}

export interface VisualizarMedicoViewModel {
  id: string;
  nome: string;
  crm: string;
  ocupado: boolean;
}

export interface MedicoExcluidoViewModel {}
