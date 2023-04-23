import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { NextAction, TavernModel } from './models/tavern';

@Injectable({
  providedIn: 'root'
})
export class TavernService {

  constructor(private http: HttpClient) {}

  enterTavern(): Observable<TavernModel> {
    return this.http.get<TavernModel>('/api/EnterTavern');
  }
  
  action(nextAction: NextAction): Observable<TavernModel> {
    return this.http.post<TavernModel>('/api/Action', nextAction);
  }
}
