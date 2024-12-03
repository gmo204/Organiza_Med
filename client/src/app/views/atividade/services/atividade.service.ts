import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import {
  AtividadeEditadaViewModel,
  AtividadeExcluidaViewModel,
  AtividadeInseridaViewModel,
  EditarAtividadeViewModel,
  InserirAtividadeViewModel,
  ListarAtividadeViewModel,
  VisualizarAtividadeViewModel 
} from '../models/atividade.models';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AtividadeService {

  private readonly url = `${environment.API_URL}/atividades`;

  constructor(private http: HttpClient) {}

  cadastrar(novaAtividade: InserirAtividadeViewModel): Observable<AtividadeInseridaViewModel> {
    return this.http.post<AtividadeInseridaViewModel>(this.url, novaAtividade);
  }

  editar(
    id: string,
    atividadeEditada: EditarAtividadeViewModel
  ): Observable<AtividadeEditadaViewModel> {
    const urlCompleto = `${this.url}/${id}`;

    return this.http.put<AtividadeEditadaViewModel>(urlCompleto, atividadeEditada);
  }

  excluir(id: string): Observable<AtividadeExcluidaViewModel> {
    const urlCompleto = `${this.url}/${id}`;

    return this.http.delete<AtividadeExcluidaViewModel>(urlCompleto);
  }

  selecionarTodos(): Observable<ListarAtividadeViewModel[]> {
    const urlCompleto = `${this.url}`;

    return this.http.get<ListarAtividadeViewModel[]>(urlCompleto);
  }

  selecionarPorId(id: string): Observable<VisualizarAtividadeViewModel> {
    const urlCompleto = `${this.url}/${id}`;

    return this.http.get<VisualizarAtividadeViewModel>(urlCompleto);
  }

}
