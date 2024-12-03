import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AtividadeService } from '../services/atividade.service';
import { ActivatedRoute, Route, Router, RouterLink } from '@angular/router';
import { NotificacaoService } from '../../../core/notificacao/notificacao.service';
import { MedicoService } from '../../medico/services/medico.service';
import { Observable } from 'rxjs';
import { InserirAtividadeViewModel, TipoAtividadeEnum } from '../models/atividade.models';
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
  templateUrl: './cadastrar-atividade.component.html',
})
export class CadastrarAtividadeComponent implements OnInit{
  atividadeForm: FormGroup;

  medicos$?: Observable<ListarMedicoViewModel[]>;

  public tipoAtividades = Object.values(TipoAtividadeEnum).filter(
    (v) => !Number.isFinite(v)
  );

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private medicoService: MedicoService,
    private atividadeService:AtividadeService,
    private notificacao: NotificacaoService
  ) {
    this.atividadeForm = new FormGroup({
      tipo: new FormControl<number>(0, [Validators.required]),
      horaInicio: new FormControl<Date | null>(null, [Validators.required]),
      horaFim: new FormControl<Date | null>(null, [Validators.required]),
      medicoId:this.fb.array([]),
    });
  }

  get tipo(){
    return this.atividadeForm.get('tipo');
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
    this.medicos$ = this.route.snapshot.data['medicoId']
  }

  cadastrar(): void {
    const novaAtividade : InserirAtividadeViewModel = this.atividadeForm.value;

    this.atividadeService.cadastrar(novaAtividade).subscribe((res) => {
      this.notificacao.sucesso(
        `O registro de [${res.tipo}] foi cadastrado com sucesso!`
      );

      this.router.navigate(['/atividades']);
    });
  }

  // mapearNomeMedico(
  //   id: string,
  //   medicos: ListarMedicoViewModel[]
  // ): string {
  //   const medico = medicos.find((medico) => medico.id === id);

  //   return medico ? medico.nome : 'Medico não encontrado';
  // }
}
