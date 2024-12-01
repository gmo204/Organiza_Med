import { NgIf, NgForOf, AsyncPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterLink } from '@angular/router';
import { finalize, Observable, of, switchMap } from 'rxjs';
import { ListarMedicoViewModel } from '../models/medico.model';
import { MedicoService } from '../services/medico.service';
import { NotificacaoService } from '../../../core/notificacao/notificacao.service';

@Component({
  selector: 'app-listagem-notas',
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
  ],
  templateUrl: './listar-medico.component.html',
  styleUrl: './listar-medico.component.scss',
})
export class ListagemMedicosComponent implements OnInit {
  medicos$?: Observable<ListarMedicoViewModel[]>;

  notasEmCache: ListarMedicoViewModel[];

  constructor(
    private medicoService: MedicoService,
    private notificacao: NotificacaoService
  ) {
    this.notasEmCache = [];
  }

   ngOnInit(): void {
    this.medicos$ = this.medicoService.selecionarTodos();

   }

  filtrar(categoriaId?: string) {
    const medicosDisponiveis = this.obterMedicosDescupados(
      this.notasEmCache);

    this.medicos$ = of(medicosDisponiveis);
  }

  private obterMedicosDescupados(medicos: ListarMedicoViewModel[])
  {
      return medicos.filter((m) => m.ocupado == false);
  }
}
