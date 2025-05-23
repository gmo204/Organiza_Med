import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { catchError, map, Observable, throwError } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import {
  AutenticarUsuarioViewModel,
  RegistrarUsuarioViewModel,
  TokenViewModel,
} from '../models/auth.models';

@Injectable()
export class AuthService {
  private apiUrl: string = environment.API_URL;

  constructor(private http: HttpClient) {}

  public registrar(
    registro: RegistrarUsuarioViewModel
  ): Observable<TokenViewModel> {
    const urlCompleto = `${this.apiUrl}/auth/registrar`;

    return this.http
      .post<TokenViewModel>(urlCompleto, registro)
      .pipe(map(this.processarDados));
  }

  public login(loginUsuario: AutenticarUsuarioViewModel) {
    const urlCompleto = `${this.apiUrl}/auth/autenticar`;

    return this.http
      .post<TokenViewModel>(urlCompleto, loginUsuario)
      .pipe(map(this.processarDados), catchError(this.processarFalha));
  }

  public logout() {
    const urlCompleto = `${this.apiUrl}/auth/sair`;

    return this.http.post(urlCompleto, {});
  }

  public validarExpiracaoToken(dataExpiracaoToken: Date): boolean {
    return dataExpiracaoToken > new Date(); // obtém a data de agora
  }

  private processarDados(resposta: any): TokenViewModel {
    if (resposta) return resposta as TokenViewModel;

    throw new Error('Erro ao mapear token do usuário.');
  }

  private processarFalha(resposta: any) {
    return throwError(() => new Error(resposta.error.erros[0]));
  }
}
