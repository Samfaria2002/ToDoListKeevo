import { Component, OnDestroy, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subject, takeUntil } from 'rxjs';
import { Tarefa } from '../../models/Tarefa'; // Replace 'path/to/tarefa.model' with the actual path to the Tarefa model file
import { TarefaService } from '../../services/tarefa.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-tarefas',
  templateUrl: './tarefas.component.html',
  styleUrls: ['./tarefas.component.css']
})


//Classe responsável por gerenciar as tarefas
//Os métodos consistem em: salvar, deletar, carregar, filtrar e selecionar tarefas
//Uso o toastr para exibir mensagens de sucesso e erro
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
  public mostrarFormulario: boolean = false;
  private unsubscriber = new Subject();
  private modalService: NgbModal;
  filtroStatus: string = '';
  tarefasFiltradas: Tarefa[];
  statusList: string[] = ['Pendente', 'Concluida', 'EmAndamento'];
  selectedStatus: string = '';
  modalAberto = false;

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

  tarefaSelect(tarefaId?: number): void {
    this.modeSave = 'patch';
  
    if (tarefaId) {
      this.tarefaService.getById(tarefaId).subscribe(
        (tarefaReturn) => {
          this.tarefaSelecionado = tarefaReturn;
          this.tarefaForm.patchValue(this.tarefaSelecionado);
        },
        (error) => {
          this.toastr.error('Erro ao carregar tarefa!');
          console.error(error);
          this.spinner.hide();
        },
        () => this.spinner.hide()
      );
    } else {
      this.tarefaSelecionado = null;
      this.tarefaForm.reset();
    }
  }

  cadastrarTarefa(): void {
    console.log('Dados a serem enviados:', this.tarefaForm.value);
    if (this.tarefaForm.valid) {
      // Criando um objeto com os campos necessários para a requisição
      const { nome, status, tipo, prazo, prioridade } = this.tarefaForm.value;
      const novaTarefa: Tarefa = { id: 0, nome, status, tipo, prazo, prioridade };
  
      this.tarefaService.post(novaTarefa).subscribe(
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
      

      this.tarefaService.post(this.tarefa)
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

  salvarTarefa(): void {
    const tarefa: Tarefa = {
      id: this.tarefaForm.value.id,
      nome: this.tarefaForm.value.nome,
      status: this.tarefaForm.value.status,
      tipo: this.tarefaForm.value.tipo,
      prazo: this.tarefaForm.value.prazo,
      prioridade: this.tarefaForm.value.prioridade
    };
    this.tarefaService.post(tarefa);
  }
  
  filterTarefas(): void {
    if (this.selectedStatus) {
      this.tarefasFiltradas = this.tarefas.filter(tarefa => tarefa.status === this.selectedStatus);
    } else {
      this.tarefasFiltradas = [...this.tarefas]; // Se nenhum status for selecionado, exiba todas as tarefas
    }
  }

  newTarefaForm(): void {
    this.modeSave = 'post';
    this.tarefaSelecionado = null;
    this.tarefaForm.reset();
    this.mostrarFormulario = true;
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

  voltar(): void {
    this.tarefaSelecionado = null;
    this.mostrarFormulario = false;
  }

  ngOnDestroy(): void {
    this.unsubscriber.next({});
    this.unsubscriber.complete();
  }
}
