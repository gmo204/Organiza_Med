import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { NotificacaoService } from '../../../core/notificacao/notificacao.service';
import { ListarMedicoViewModel } from '../../medico/models/medico.model';
import { MedicoService } from '../../medico/services/medico.service';
import { AtividadeService } from '../services/atividade.service';
import { InserirAtividadeViewModel, TipoAtividadeEnum } from '../models/atividade.models';

@Component({
  selector: 'app-editar-atividade',
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
  ],  templateUrl: './editar-atividade.component.html',
})
export class EditarAtividadeComponent {
  atividadeForm: FormGroup
  id?: string;

  medicos$?: Observable<ListarMedicoViewModel[]>;

  public tipoAtividades = Object.values(TipoAtividadeEnum).filter(
    (v) => !Number.isFinite(v)
  );

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private atividadeService:AtividadeService,
    private notificacao: NotificacaoService
  ) {
    this.atividadeForm = new FormGroup({
      tipo: new FormControl<string>('', [Validators.required]),
      horaInicio: new FormControl<Date | null>(null, [Validators.required]),
      horaFim: new FormControl<Date | null>(null, [Validators.required]),
      medicoId:this.fb.array([]),
    });
  }

  get tipo(){
    return this.atividadeForm.get('tipo')
  }
  get horaInicio(){
    return this.atividadeForm.get('horaInicio')
  }
  get horaFim(){
    return this.atividadeForm.get('horaFim')
  }
  get medicoId(){
    return this.atividadeForm.get('medicoId') as FormArray
  }

  ngOnInit(): void {
    this.medicos$ = this.route.snapshot.data['medicoId']
  }

  editar(): void {
    if (!this.id) {
      this.notificacao.erro('Não foi possível recuperar o id requisitado!');
      return;
    }

    const atividadeEditada: InserirAtividadeViewModel = this.atividadeForm.value;

    this.atividadeService.editar(this.id, atividadeEditada).subscribe((res) => {
      this.notificacao.sucesso(
         `O registro ID [${res.tipo}] foi editado com sucesso!`
      );

      this.router.navigate(['/atividades']);
    })
  }

  // mapearNomeMedico(
  //   id: string,
  //   medicos: ListarMedicoViewModel[]
  // ): string {
  //   const medico = medicos.find((medico) => medico.id === id);

  //   return medico ? medico.nome : 'Medico não encontrado';
  // }
}
