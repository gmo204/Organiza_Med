import { NgIf, AsyncPipe, NgForOf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { VisualizarAtividadeViewModel } from '../models/atividade.models';
import { NotificacaoService } from '../../../core/notificacao/notificacao.service';
import { AtividadeService } from '../services/atividade.service';
import { Observable } from 'rxjs';
import { MatCardModule } from '@angular/material/card';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
  selector: 'app-excluir-atividade',
  standalone: true,
  imports: [
    NgIf,
    NgForOf,
    AsyncPipe,
    RouterLink,
    MatCardModule,
    MatIconModule,
    MatButtonModule,
    MatTooltipModule,
  ],  templateUrl: './excluir-atividade.component.html',
})
export class ExcluirAtividadeComponent implements OnInit {
  detalhesAtividade?: VisualizarAtividadeViewModel;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private atividadeService: AtividadeService,
    private notificacao: NotificacaoService
  ) { }

  ngOnInit(): void {
    this.detalhesAtividade = this.route.snapshot.data['atividade']
    console.log(this.detalhesAtividade)
  }

  public excluir() {
    if (this.detalhesAtividade?.id == null)
      return this.notificacao.erro("Id da atividade não foi encontrado")

    console.log(this.detalhesAtividade?.id)
    this.atividadeService.excluir(this.detalhesAtividade?.id).subscribe({
      next: () => this.processarSucesso(),
      error: (erro) => this.processarFalha(erro),
    });
  }

  private processarSucesso(): void {
    this.notificacao.sucesso('Atividade excluída com sucesso!');

    this.router.navigate(['/atividades', 'listar']);
  }

  private processarFalha(erro: Error) {
    this.notificacao.erro(erro.message);
  }

  getDataFormatada(data: Date | void): string {
    if (data == null)
      return "Erro ao mepear data"
    const d = new Date(data);
    return d.toLocaleString('pt-BR', { dateStyle: 'short', timeStyle: 'short' });
  }
}
