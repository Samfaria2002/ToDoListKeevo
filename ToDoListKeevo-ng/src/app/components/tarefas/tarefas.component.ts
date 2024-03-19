import { Component, OnDestroy, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject, takeUntil } from 'rxjs';
import { Tarefa } from '../../models/Tarefa'; // Replace 'path/to/tarefa.model' with the actual path to the Tarefa model file
import { TarefaService } from '../../services/tarefa.service';

@Component({
  selector: 'app-tarefas',
  templateUrl: './tarefas.component.html',
  styleUrls: ['./tarefas.component.css']
})

export class TarefasComponent implements OnInit, OnDestroy {

  public tarefaForm: FormGroup;
  public titulo = 'Tarefas';
  public tarefaSelecionado: Tarefa | null;
  public textSimple: string;
  public tarefas: Tarefa[];
  public tarefa: Tarefa;
  public msnDeleteTarefa: string;
  public modeSave = 'post';
  private unsubscriber = new Subject();

  constructor(
    private tarefaService: TarefaService,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
  ) {
    this.criarForm();
  }

  ngOnInit(): void {
    this.carregarTarefas();
  }

  showSpinner(): void {
    this.spinner.show();
    setTimeout(() => {
      this.spinner.hide();
    }, 2000);
  }

  criarForm(): void {
    this.tarefaForm = this.fb.group({
      id: [0],
      nome: ['', Validators.required],
      status: ['', Validators.required],
      tipo: ['', Validators.required],
      prazo: ['', Validators.required],
      prioridade: ['', Validators.required]
    });
  }

  saveTarefa(): void {
    if (this.tarefaForm.valid) {
      this.showSpinner();

      if (this.modeSave === 'post') {
        this.tarefa = { ...this.tarefaForm.value };
      } else if (this.tarefaSelecionado !== null) {
        this.tarefa = { id: this.tarefaSelecionado.id, ...this.tarefaForm.value };
      } else {
        this.tarefa = { ...this.tarefaForm.value };
      }
      

      const tarefaService = this.tarefaService as any;
      tarefaService[this.modeSave](this.tarefa)
        .pipe(takeUntil(this.unsubscriber))
        .subscribe(
          () => {
            this.carregarTarefas();
            this.toastr.success('Tarefa salva com sucesso!');
          }, (error: any) => {
            this.toastr.error(`Erro: Tarefa não pode ser salva!`);
            console.error(error);
            this.spinner.hide();
          }, () => this.spinner.hide()
        );

    }
  }

  carregarTarefas(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    const id = idParam ? +idParam : null;
  
    this.tarefaService.getAll()
      .pipe(takeUntil(this.unsubscriber))
      .subscribe((tarefas: Tarefa[]) => {
        this.tarefas = tarefas;
  
        if (id !== null && id > 0) {
          const selectedTarefa = this.tarefas.find(tarefa => tarefa.id === id);
          if (selectedTarefa) {
            this.tarefaSelect(selectedTarefa);
          }
        }
  
        this.toastr.success('Tarefas foram carregadas com sucesso!');
      }, (error: any) => {
        this.toastr.error('Tarefas não carregadas!');
        console.error(error);
        this.spinner.hide();
      }, () => this.spinner.hide()
    );
  }

  tarefaSelect(tarefa: Tarefa): void {
    this.modeSave = 'put';
    this.tarefaSelecionado = tarefa;
    this.tarefaForm.patchValue(tarefa);
  }

  voltar(): void {
    this.tarefaSelecionado = null;
  }

  ngOnDestroy(): void {
    this.unsubscriber.next({});
    this.unsubscriber.complete();
  }
}
