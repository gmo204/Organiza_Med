import { NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { NotificacaoService } from '../../../core/notificacao/notificacao.service';
import { MedicoService } from '../services/medico.service';
import { InserirMedicoViewModel, MedicoEditadoViewModel } from '../models/medico.model';
import { PartialObserver } from 'rxjs';
import { AnyCatcher } from 'rxjs/internal/AnyCatcher';

@Component({
  selector: 'app-editar-medico',
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
    MatCardModule,],
  templateUrl: './editar-medico.component.html',
})
export class EditarMedicoComponent implements OnInit{
  id?: string;
  medicoForm : FormGroup

  constructor(
    private route: ActivatedRoute,
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

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];

    if (!this.id) {
      this.notificacao.erro('Não foi possível recuperar o id requisitado!');
      return;
    }
    const medico = this.route.snapshot.data['medico'];

    this.medicoForm.patchValue(medico);
  }

  editar(): void {
    if (this.medicoForm.invalid) {
      this.notificacao.aviso(
        'Por favor, preencha o formulário corretamente.'
      );

      return;
    }

    const id = this.route.snapshot.params['id'];

    const medicoEditado: InserirMedicoViewModel = this.medicoForm.value;

    const observer: PartialObserver<MedicoEditadoViewModel> = {
      next: (medicoInserido) => this.processarSucesso(medicoInserido),
      error: (erro) => this.processarFalha(erro),
    };

    this.medicoService.editar(id, medicoEditado).subscribe(observer)
  }
  private processarSucesso(medico: MedicoEditadoViewModel): void {
    this.notificacao.sucesso(
     `Medico ${medico.nome} editado com sucesso!`
    );

    this.router.navigate(['/medicos', 'listar']);
  }

  private processarFalha(erro: any): any {
    this.notificacao.erro(erro.error[0]);
  }
}
