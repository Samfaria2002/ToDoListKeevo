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

  //Método para inicializar o componente
  ngOnInit(): void {
    this.carregarTarefas();
    this.criarForm();
  }

  // Método para carregar as tarefas na tabela
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

  // Método para selecionar uma tarefa
  // Se o id da tarefa for passado, ele busca a tarefa pelo id e preenche o formulário para edição
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

  //Método responável por cadastrar uma tarefa
  cadastrarTarefa(): void {
    console.log('Dados a serem enviados:', this.tarefaForm.value);
    if (this.tarefaForm.valid) {
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

  //Método responsável por deletar uma tarefa
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
  
  //Método responsável por salvar uma tarefa
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
      

      (this.tarefaService as any)[this.modeSave](this.tarefa)
      .pipe(takeUntil(this.unsubscriber))
      .subscribe(
        () => {
          this.carregarTarefas();
          this.toastr.success('Aluno salvo com sucesso!');
        },
        (error: any) => {
          this.toastr.error(`Erro: Aluno não pode ser salvo!`);
          console.error(error);
          this.spinner.hide();
        },
        () => this.spinner.hide()
      );
    }
  }

  //Método também responsável por salvar uma tarefa, porém, passando diretamente os valores do formulário
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
  
  //Método responsável por filtrar as tarefas por status
  filterTarefas(): void {
    if (this.selectedStatus) {
      this.tarefasFiltradas = this.tarefas.filter(tarefa => tarefa.status === this.selectedStatus);
    } else {
      this.tarefasFiltradas = [...this.tarefas];
    }
  }

  //Método responsável por abrir o formulário de cadastro de tarefas
  newTarefaForm(): void {
    this.modeSave = 'post';
    this.tarefaSelecionado = null;
    this.tarefaForm.reset();
    this.mostrarFormulario = true;
  }

  //Método responsável por iniciar o spinner
  showSpinner(): void {
    this.spinner.show();
    setTimeout(() => {
      this.spinner.hide();
    }, 2000);
  }

  //Método responsável por inicializar o formulário
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

  //Método responsável por fechar o formulário
  voltar(): void {
    this.tarefaSelecionado = null;
    this.mostrarFormulario = false;
  }

  //Método responsável por destruir o componente
  ngOnDestroy(): void {
    this.unsubscriber.next({});
    this.unsubscriber.complete();
  }
}
