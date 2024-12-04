import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  EditarMedicoViewModel,
  InserirMedicoViewModel,
  ListarMedicoViewModel,
  MedicoEditadoViewModel,
  MedicoExcluidoViewModel,
  MedicoInseridoViewModel,
  VisualizarMedicoViewModel } from '../models/medico.model';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MedicoService {
  private readonly url = `${environment.API_URL}/medicos`;

  constructor(private http: HttpClient) { }

  cadastrar(novaNota: InserirMedicoViewModel): Observable<MedicoInseridoViewModel> {
    return this.http.post<MedicoInseridoViewModel>(this.url, novaNota);
  }

  editar(
    id: string,
    medicoEditado: EditarMedicoViewModel
  ): Observable<MedicoEditadoViewModel> {
    const urlCompleto = `${this.url}/${id}`;

    return this.http.put<MedicoEditadoViewModel>(urlCompleto, medicoEditado);
  }

  excluir(id: string): Observable<MedicoExcluidoViewModel> {
    const urlCompleto = `${this.url}/${id}`;

    return this.http.delete<MedicoExcluidoViewModel>(urlCompleto);
  }

  selecionarTodos(): Observable<ListarMedicoViewModel[]> {
    const urlCompleto = `${this.url}`;

    return this.http.get<ListarMedicoViewModel[]>(urlCompleto);
  }

  selecionarDisponiveis(): Observable<ListarMedicoViewModel[]> {
    const urlCompleto = `${this.url}?ocupado=false`;

    return this.http.get<ListarMedicoViewModel[]>(urlCompleto);
  }

  selecionarPorId(id: string): Observable<VisualizarMedicoViewModel> {
    const urlCompleto = `${this.url}/${id}`;

    return this.http.get<VisualizarMedicoViewModel>(urlCompleto);
  }

  // alterarStatus(id: string): Observable<VisualizarMedicoViewModel> {
  //   const urlCompleto = `${this.url}/${id}/alterar-status`;

  //   return this.http.put<VisualizarMedicoViewModel>(urlCompleto, {});
  // }
}
