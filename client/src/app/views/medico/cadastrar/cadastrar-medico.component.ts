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
import { InserirMedicoViewModel, MedicoInseridoViewModel } from '../models/medico.model';
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
      nome: new FormControl<string>('', [Validators.required, Validators.maxLength(30),Validators.minLength(3)]),
      crm: new FormControl<string>('', [Validators.required, Validators.maxLength(8), Validators.minLength(8)]),
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
    if (this.medicoForm.invalid) {
      this.notificacao.aviso(
        'Por favor, preencha o formulário corretamente!'
      );

      return;
    }
    const novoMedico: InserirMedicoViewModel = this.medicoForm.value;

    this.medicoService.cadastrar(novoMedico).subscribe({
      next: (contatoInserido) => this.processarSucesso(contatoInserido),
      error: (erro) => this.processarFalha(erro),
    });
  }

  private processarSucesso(medico: MedicoInseridoViewModel): void {
    this.notificacao.sucesso(
     `Medico "${medico.nome}" cadastrado com sucesso!`
    );

    this.router.navigate(['/contatos', 'listar']);
  }

  private processarFalha(erro: Error): any {
    this.notificacao.erro(erro.message);
  }
}
