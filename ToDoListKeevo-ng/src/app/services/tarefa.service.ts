import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environment/environment.prod';
import { Tarefa } from '../models/Tarefa';

@Injectable({
  providedIn: 'root'
})
export class TarefaService {

  baseURL = `${environment.mainUrlAPI}tarefa`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Tarefa[]> {
    return this.http.get<Tarefa[]>(this.baseURL);
  }

  /*
  getById(id: number): Observable<Tarefa> {
    return this.http.get<Tarefa>(`${this.baseURL}/${id}`);
  }
  */

  post(tarefa: Tarefa) {
    return this.http.post(this.baseURL, tarefa);
  }

  put(tarefa: Tarefa) {
    return this.http.put(`${this.baseURL}/${tarefa.id}`, tarefa);
  }

  delete(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }

}
