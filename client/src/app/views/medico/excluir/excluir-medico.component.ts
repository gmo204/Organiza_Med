import { NgIf, AsyncPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { NotificacaoService } from '../../../core/notificacao/notificacao.service';
import { VisualizarMedicoViewModel } from '../models/medico.model';
import { MedicoService } from '../services/medico.service';

@Component({
  selector: 'app-exclusao-nota',
  standalone: true,
  imports: [NgIf, RouterLink, AsyncPipe, MatButtonModule, MatIconModule],
  templateUrl: './excluir-medico.component.html',
})
export class ExcluirMedicoComponent implements OnInit {
  id?: string;
  medico$?: Observable<VisualizarMedicoViewModel>;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private medicoService: MedicoService,
    private notificacao: NotificacaoService
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];

    if (!this.id) {
      this.notificacao.erro('Não foi possível recuperar o id requisitado!');
      return;
    }
    this.medico$ = this.medicoService.selecionarPorId(this.id);
  }

  excluir() {
    if (!this.id) {
      this.notificacao.erro('Não foi possível recuperar o id requisitado!');
      return;
    }

    this.medicoService.excluir(this.id).subscribe((res) => {
      this.notificacao.sucesso(
        `O registro ID [${this.id}] foi excluído com sucesso!`
      );

      this.router.navigate(['/medicos']);
    });
  }
}
