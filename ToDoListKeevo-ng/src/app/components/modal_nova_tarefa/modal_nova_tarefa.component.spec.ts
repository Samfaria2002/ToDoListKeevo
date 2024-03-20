/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { Modal_nova_tarefaComponent } from './modal_nova_tarefa.component';

describe('Modal_nova_tarefaComponent', () => {
  let component: Modal_nova_tarefaComponent;
  let fixture: ComponentFixture<Modal_nova_tarefaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Modal_nova_tarefaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Modal_nova_tarefaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
