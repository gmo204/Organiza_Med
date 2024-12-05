import { NgIf, NgForOf, AsyncPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { AtividadeService } from '../services/atividade.service';
import { Observable } from 'rxjs';
import { ListarAtividadeViewModel } from '../models/atividade.models';
import { ListarMedicoViewModel } from '../../medico/models/medico.model';

@Component({
  selector: 'app-listar-atividade',
  standalone: true,
  imports: [
    NgIf,
    NgForOf,
    RouterLink,
    AsyncPipe,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
  ],  templateUrl: './listar-atividade.component.html',
})
export class ListarAtividadesComponent implements OnInit {
  atividades$?: Observable<ListarAtividadeViewModel[]>;
  medicos$?: Observable<ListarMedicoViewModel[]>;

  constructor(
    private atividadeService: AtividadeService,
    private medicoService: AtividadeService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.atividades$ = this.atividadeService.selecionarTodos();
    this.medicos$ = this.route.snapshot.data['medicos']
  }

  getDataFormatada(data: Date | string): string {
    const d = new Date(data);
    return d.toLocaleDateString('pt-BR', { dateStyle: 'short' });
  }

  getHoraFormatada(data: Date | string): string {
    const d = new Date(data);
    return d.toLocaleTimeString('pt-BR', { timeStyle: 'short' });
  }
}
