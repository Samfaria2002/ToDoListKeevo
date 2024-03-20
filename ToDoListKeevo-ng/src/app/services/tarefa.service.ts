import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environment/environment.prod';
import { Tarefa } from '../models/Tarefa';

@Injectable({
  providedIn: 'root'
})
export class TarefaService {

  baseURL = `${environment.mainUrlAPI}`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Tarefa[]> {
    return this.http.get<Tarefa[]>(this.baseURL)
  }

  getById(id: number): Observable<Tarefa> {
    return this.http.get<Tarefa>(`${this.baseURL}/${id}`);
  }

  getByStatus(status: string): Observable<Tarefa[]> {
    const params = new HttpParams().set('status', status);
    return this.http.get<Tarefa[]>(`${this.baseURL}`, { params });
  }  

  post(tarefa: Tarefa) {
    return this.http.post(this.baseURL, tarefa);
  }

  put(tarefa: Tarefa) {
    return this.http.put(`${this.baseURL}/${tarefa.id}`, tarefa);
  }

  patch(tarefa: Tarefa) {
    return this.http.patch(`${this.baseURL}/${tarefa.id}`, tarefa);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseURL}/${id}`);
  }  
  
}
