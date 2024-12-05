import { NgForOf, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { NotificacaoService } from '../../../core/notificacao/notificacao.service';
import { ListarMedicoViewModel } from '../../medico/models/medico.model';
import { AtividadeService } from '../services/atividade.service';
import { EditarAtividadeViewModel, TipoAtividadeEnum } from '../models/atividade.models';

@Component({
  selector: 'app-editar-atividade',
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
  ],  templateUrl: './editar-atividade.component.html',
})
export class EditarAtividadeComponent {
  atividadeForm: FormGroup

  medicos?: ListarMedicoViewModel[] = [];

  public tipoAtividades = Object.values(TipoAtividadeEnum).filter(
    (v) => !Number.isFinite(v)
  );

  constructor(
    private router: Router,
    private route: ActivatedRoute,
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
    return this.atividadeForm.get('horaInicio')
  }
  get horaFim(){
    return this.atividadeForm.get('horaFim')
  }
  get medicoId(){
    return this.atividadeForm.get('medicoId')
  }

  ngOnInit(): void {
    this.medicos = this.route.snapshot.data['medicos']

    const atividade = this.route.snapshot.data['atividade'];
    if (atividade) {

      const { horaInicio, horaFim, ...resto } = atividade;

      const data = new Date(horaInicio).toISOString().split('T')[0]; 
      const horaInicioStr = new Date(horaInicio).toLocaleTimeString('en-US', {
        hour12: false,
        hour: '2-digit',
        minute: '2-digit',
      });
      const horaFimStr = new Date(horaFim).toLocaleTimeString('en-US', {
        hour12: false,
        hour: '2-digit',
        minute: '2-digit',
      });

      this.atividadeForm.patchValue({
        ...resto,
        data,
        horaInicio: horaInicioStr,
        horaFim: horaFimStr,
      });
    }
  }

  editar(): void {
    if (this.atividadeForm.invalid) {
      this.notificacao.aviso(
        'Por favor, preencha o formulário corretamente.'
      );

      return;
    }
    const id = this.route.snapshot.params['id'];

    const { data, horaInicio, horaFim, medicoId, tipo } = this.atividadeForm.value;

    const horaInicioCompleta = new Date(`${data}T${horaInicio}`);
    const horaFimCompleta = new Date(`${data}T${horaFim}`);

    const atividadeEditada:EditarAtividadeViewModel = {
      tipo,
      horaInicio: horaInicioCompleta,
      horaFim: horaFimCompleta,
      medicoId,
    };

    this.atividadeService.editar(id, atividadeEditada).subscribe((res) => {
      this.notificacao.sucesso(
         `O atividade foi editada com sucesso!`
      );

      this.router.navigate(['/atividades']);
    })
  }
}
