import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AtividadeService } from '../services/atividade.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { NotificacaoService } from '../../../core/notificacao/notificacao.service';
import { AtividadeInseridaViewModel, InserirAtividadeViewModel, TipoAtividadeEnum,  } from '../models/atividade.models';
import { ListarMedicoViewModel } from '../../medico/models/medico.model';
import { NgForOf, NgIf } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';

@Component({
  selector: 'app-cadastrar-atividade',
  standalone: true,
  imports: [
    NgIf,
    NgForOf,
    RouterLink,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    MatSelectModule,
    MatCardModule,
    MatDatepickerModule
  ],
  templateUrl: './cadastrar-cirurgia.component.html',
})
export class CadastrarAtividadeComponent implements OnInit{
  atividadeForm: FormGroup;

  medicos: ListarMedicoViewModel[] = [];

  public tipoAtividade = Object.values(TipoAtividadeEnum).filter(
    (v) => !Number.isFinite(v)
  );
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private atividadeService:AtividadeService,
    private notificacao: NotificacaoService
  ) {
    this.atividadeForm = new FormGroup({
      tipo: new FormControl<number | null>(0, [Validators.required]),
      data: new FormControl<string | null>(null, [Validators.required]),
      horaInicio: new FormControl<string | null>(null, [Validators.required]),
      horaFim: new FormControl<string | null>(null, [Validators.required]),
      medicoId: new FormControl<string[]>([])
    });
  }

  get tipo(){
    return this.atividadeForm.get('tipo')
  }
  get horaInicio(){
    return this.atividadeForm.get('horaInicio');
  }
  get horaFim(){
    return this.atividadeForm.get('horaFim');
  }
  get medicoId(){
    return this.atividadeForm.get('medicoId');
  }

  ngOnInit(): void {
    this.medicos = this.route.snapshot.data['medicos']
  }

  cadastrar(): void {
    if (this.atividadeForm.invalid) {
      this.notificacao.erro('Preencha todos os campos corretamente!');
      return;
    }

    const { data, horaInicio, horaFim, medicoId, tipo } = this.atividadeForm.value;

    const horaInicioCompleta = new Date(`${data}T${horaInicio}Z`);
    const horaFimCompleta = new Date(`${data}T${horaFim}Z`);

    const novaAtividade: InserirAtividadeViewModel = {
      tipo,
      horaInicio: horaInicioCompleta,
      horaFim: horaFimCompleta,
      medicoId,
    };

    this.atividadeService.cadastrar(novaAtividade).subscribe({
      next: (contatoInserido) => this.processarSucesso(contatoInserido),
      error: (erro) => this.processarFalha(erro),
    });
  }

  private processarSucesso(atividade: AtividadeInseridaViewModel): void {
    this.notificacao.sucesso(
     `Atividade cadastrada com sucesso!`
    );

    this.router.navigate(['/atividades', 'listar']);
  }

  private processarFalha(erro: any): any {
    this.notificacao.erro(erro.error[0]);
  }
}
