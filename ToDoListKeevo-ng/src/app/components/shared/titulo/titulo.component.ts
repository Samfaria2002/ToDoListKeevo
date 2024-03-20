import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.css']
})

//Componente responsável por exibir o título da página
export class TituloComponent implements OnInit {

  @Input()titulo!: string;

  constructor() { }

  ngOnInit() {
  }

}
