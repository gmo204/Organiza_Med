import { NgIf, NgForOf, AsyncPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterLink } from '@angular/router';
import { AtividadeService } from '../services/atividade.service';
import { Observable } from 'rxjs';
import { ListarAtividadeViewModel } from '../models/atividade.models';

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

  constructor(private atividadeService: AtividadeService) {}

  ngOnInit(): void {
    this.atividades$ = this.atividadeService.selecionarTodos();
  }

  getDataFormatada(data: Date | string): string {
    const d = new Date(data);
    return d.toLocaleString('pt-BR', { dateStyle: 'short', timeStyle: 'short' });
  }
}
