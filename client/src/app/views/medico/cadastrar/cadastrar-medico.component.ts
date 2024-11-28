import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { Router, RouterLink } from '@angular/router';
import { MedicoService } from '../services/medico.service';
import { InserirMedicoViewModel } from '../models/medico.model';
import { NotificacaoService } from '../../../core/notificacao/notificacao.service';

@Component({
  selector: 'app-cadastrar-medico',
  standalone: true,
  imports: [
    NgIf,
    RouterLink,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    MatSelectModule,
    MatCardModule,
  ],  templateUrl: './cadastrar-medico.component.html',
})
export class CadastrarMedicoComponent {
  medicoForm: FormGroup;

  constructor(
    private router: Router,
    private medicoService: MedicoService,
    private notificacao: NotificacaoService
  ) {
    this.medicoForm = new FormGroup({
      nome: new FormControl<string>('', [Validators.required]),
      crm: new FormControl<string>('', [Validators.required]),
      ocupado: new FormControl<boolean>(false)
    });
  }

  get nome() {
    return this.medicoForm.get('nome');
  }

  get crm() {
    return this.medicoForm.get('crm');
  }

  cadastrar(): void {
    const novoMedico: InserirMedicoViewModel = this.medicoForm.value;

    this.medicoService.cadastrar(novoMedico).subscribe((res) => {
      this.notificacao.sucesso(
        `O medico [${res.nome}] foi cadastrado com sucesso!`
      );

      this.router.navigate(['/medicos']);
    });
  }
}
