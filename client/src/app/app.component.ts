import { Component, OnInit } from '@angular/core';
import { ShellComponent } from './core/components/shell/shell.component';
import { Router, RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';
import { UsuarioTokenViewModel } from './core/auth/models/auth.models';
import { AuthService } from './core/auth/services/auth.service';
import { LocalStorageService } from './core/auth/services/local-storage.service';
import { UsuarioService } from './core/auth/services/usuario.service';
import { NotificacaoService } from './core/notificacao/notificacao.service';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, AsyncPipe, ShellComponent],
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  usuarioAutenticado$?: Observable<UsuarioTokenViewModel | undefined>;

  constructor(
    private router: Router,
    private authService: AuthService,
    private usuarioService: UsuarioService,
    private notificacaoService: NotificacaoService,
    private localStorageService: LocalStorageService
  ) {}

  ngOnInit(): void {
    this.usuarioAutenticado$ = this.usuarioService.usuarioAutenticado;

    const token = this.localStorageService.obterTokenAutenticacao();

    if (!token) return;

    const usuarioPersistido = token.usuario;
    const dataExpiracaoToken = new Date(token.dataExpiracao);

    const tokenValido: boolean =
      this.authService.validarExpiracaoToken(dataExpiracaoToken);

    if (usuarioPersistido && tokenValido) {
      this.usuarioService.logarUsuario(usuarioPersistido);
    } else {
      this.efetuarLogout();
    }
  }

  efetuarLogout() {
    this.usuarioService.logout();
    this.authService.logout();
    this.localStorageService.limparDadosLocais();

    this.notificacaoService.sucesso('Logout realizado com sucesso!');

    this.router.navigate(['/login']);
  }
}
