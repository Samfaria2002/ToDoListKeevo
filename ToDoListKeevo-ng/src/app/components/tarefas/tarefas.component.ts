import { Component, OnDestroy, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject, takeUntil } from 'rxjs';
import { Tarefa } from '../../models/Tarefa'; // Replace 'path/to/tarefa.model' with the actual path to the Tarefa model file
import { TarefaService } from '../../services/tarefa.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

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
  public dadosCarregados: boolean = false;
  statusList: string[] = ['Pendente', 'Concluida', 'EmAndamento'];
  tarefasFiltradas: Tarefa[];
  selectedStatus: string = '';
  private unsubscriber = new Subject();
  private modalService: NgbModal

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
    this.criarForm();
  }

  showSpinner(): void {
    this.spinner.show();
    setTimeout(() => {
      this.spinner.hide();
    }, 2000);
  }

  openModal(content: TemplateRef<any>): void {
    this.modalService.open(content, { centered: true });
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
    const tarefaId = this.route.snapshot.paramMap.get('id');
    const id = tarefaId ? +tarefaId : null;
  
    this.tarefaService.getAll()
      .pipe(takeUntil(this.unsubscriber))
      .subscribe((tarefas: Tarefa[]) => {
          this.tarefas = tarefas;
          this.tarefasFiltradas = [...this.tarefas];
  
          if (id && +id > 0) {
            this.tarefaSelect(id);
          }
  
          this.dadosCarregados = true;
          this.toastr.success('Tarefas foram carregadas com sucesso.', 'Sucesso', { timeOut: 3000 });
        },
        (error: any) => {
          this.toastr.error('Tarefas não carregadas!', 'Erro');
          console.error(error);
          this.spinner.hide();
        },
        () => this.spinner.hide()
      );
  }

  tarefaSelect(tarefaId: number): void {
    this.modeSave = 'patch';
    this.tarefaService.getById(tarefaId).subscribe(
      (tarefaReturn) => {
        this.tarefaSelecionado = tarefaReturn;
        this.tarefaForm.patchValue(this.tarefaSelecionado);
      },
      (error) => {
        this.toastr.error('Tarefas não carregadas!');
        console.error(error);
        this.spinner.hide();
      },
      () => this.spinner.hide()
    );
  }

  cadastrarTarefa(): void {
    if (this.tarefaForm.valid) {
      this.tarefaService.post(this.tarefaForm.value).subscribe(
        () => {
          this.toastr.success('Tarefa cadastrada com sucesso!');
          this.tarefaForm.reset();
        },
        error => {
          console.error(error);
          this.toastr.error('Erro ao cadastrar tarefa. Por favor, tente novamente mais tarde.');
        }
      );
    } else {
      this.toastr.error('Por favor, preencha todos os campos corretamente.');
    }
  }

  deleteTarefa(id: number): void {
    if (confirm('Tem certeza de que deseja excluir esta tarefa?')) {
      this.showSpinner();
      this.tarefaService.delete(id)
        .pipe(takeUntil(this.unsubscriber))
        .subscribe(
          () => {
            this.toastr.success('Tarefa excluída com sucesso!');
            window.location.reload();
          },
          (error: any) => {
            console.error(error);
            this.spinner.hide();
            window.location.reload();
          },
          () => this.spinner.hide()
        );
    }
  }
  
  filterTarefas(): void {
    if (this.selectedStatus) {
      this.tarefasFiltradas = this.tarefas.filter(tarefa => tarefa.status === this.selectedStatus);
    } else {
      this.tarefasFiltradas = [...this.tarefas];
    }
  }

  voltar(): void {
    this.tarefaSelecionado = null;
  }

  ngOnDestroy(): void {
    this.unsubscriber.next({});
    this.unsubscriber.complete();
  }
}
