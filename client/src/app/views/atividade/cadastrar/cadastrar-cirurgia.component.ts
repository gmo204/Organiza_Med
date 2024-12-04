import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AtividadeService } from '../services/atividade.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { NotificacaoService } from '../../../core/notificacao/notificacao.service';
import { InserirAtividadeViewModel, TipoAtividadeEnum,  } from '../models/atividade.models';
import { ListarMedicoViewModel } from '../../medico/models/medico.model';
import { NgIf } from '@angular/common';
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

  constructor(
    private fb: FormBuilder,
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
      medicoId: this.fb.array([]),
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
    return this.atividadeForm.get('medicoId') as FormArray;
  }

  ngOnInit(): void {
    this.medicos = this.route.snapshot.data['medicoId']
  }

  cadastrar(): void {
    if (this.atividadeForm.invalid) {
      this.notificacao.erro('Preencha todos os campos corretamente!');
      return;
    }

    const { data, horaInicio, horaFim, medicoId } = this.atividadeForm.value;

    const horaInicioCompleta = new Date(`${data}T${horaInicio}`);
    const horaFimCompleta = new Date(`${data}T${horaFim}`);

    const novaAtividade: InserirAtividadeViewModel = {
      tipo: TipoAtividadeEnum.Cirurgia,
      horaInicio: horaInicioCompleta,
      horaFim: horaFimCompleta,
      medicoId,
    };

    this.atividadeService.cadastrar(novaAtividade).subscribe((res) => {
      this.notificacao.sucesso(
        `A cirurgia foi cadastrada com sucesso!`
      );

      this.router.navigate(['/atividades']);
    });
  }
}
