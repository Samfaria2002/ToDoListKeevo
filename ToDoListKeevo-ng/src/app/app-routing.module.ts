import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TarefasComponent } from './components/tarefas/tarefas.component';

const routes: Routes = [
  { path: 'home', component: TarefasComponent},
  { path: 'tarefas', component: TarefasComponent },
  // Redireciona para a lista de tarefas se não houver nada na URL
  { path: '', redirectTo: '/tarefas', pathMatch: 'full'},
  // Redireciona para a lista de tarefas se a URL for inválida
  { path: '**', redirectTo: '/tarefas', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
