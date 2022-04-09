import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class ListaClientesService {
  constructor(private http: HttpClient) { }
}