import { NgIf, AsyncPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { VisualizarAtividadeViewModel } from '../models/atividade.models';
import { NotificacaoService } from '../../../core/notificacao/notificacao.service';
import { AtividadeService } from '../services/atividade.service';

@Component({
  selector: 'app-excluir-atividade',
  standalone: true,
  imports: [NgIf, RouterLink, AsyncPipe, MatButtonModule, MatIconModule],
  templateUrl: './excluir-atividade.component.html',
})
export class ExcluirAtividadeComponent implements OnInit {
  id?: string;
  atividade$?: Observable<VisualizarAtividadeViewModel>;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private atividadeService: AtividadeService,
    private notificacao: NotificacaoService
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];

    if (!this.id) {
      this.notificacao.erro('Não foi possível recuperar o id requisitado!');
      return;
    }
  }
  excluir() {
    if (!this.id) {
      this.notificacao.erro('Não foi possível recuperar o id requisitado!');
      return;
    }

    this.atividadeService.excluir(this.id).subscribe((res) => {
      this.notificacao.sucesso(
        `O registro ID [${this.id}] foi excluído com sucesso!`
      );

      this.router.navigate(['/atividades']);
    });
  }
}
