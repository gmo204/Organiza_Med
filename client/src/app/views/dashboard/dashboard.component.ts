import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ItemDashboard } from './models/item-dashboard.model';
import { RouterLink } from '@angular/router';
import { NgForOf } from '@angular/common';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    RouterLink,
    NgForOf,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent {
  itens: ItemDashboard[] = [
    {
      titulo: 'Atividade',
      descricao: 'Gerencie o agendamento das atividades medicas.',
      rota: '/atividades',
      icone: 'bookmark',
    },
    {
      titulo: 'Medicos',
      descricao:
        'Gerencie ooos medicos disponiveis para atividades.',
      rota: '/medicos',
      icone: 'people',
    },
  ];
}
