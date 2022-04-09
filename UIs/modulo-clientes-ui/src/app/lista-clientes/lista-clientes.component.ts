import { Component, OnInit } from '@angular/core';
import { Injectable } from '@angular/core';
import { Cliente, ClientesService } from './clientes.service';

@Component({
  selector: 'app-lista-clientes',
  templateUrl: './lista-clientes.component.html',
  styleUrls: ['./lista-clientes.component.css']
})
@Injectable()
export class ListaClientesComponent implements OnInit {
  constructor(private clientesService: ClientesService) { }
  
  clientes: Cliente[] = [] 

  ngOnInit(): void {
    this.clientesService.getListaClientes().subscribe(
      (clientes: Cliente[]) => this.clientes.push(...clientes)
    )
  }

  displayedColumns = ['id', 'nome', 'endereco'];
  //clientes = ELEMENT_DATA;
}

const CLIENTE_DATA: Cliente[] = [
  {id: 1, nome: 'Hydrogen', endereco: "1.0079"},
  {id: 2, nome: 'Helium', endereco: "4.0026"},
  {id: 3, nome: 'Lithium', endereco: "6.941"},
  {id: 4, nome: 'Beryllium', endereco: "9.0122"},
  {id: 5, nome: 'Boron', endereco: "10.811"},
  {id: 6, nome: 'Carbon', endereco: "12.0107"},
  {id: 7, nome: 'Nitrogen', endereco: "14.0067"},
  {id: 8, nome: 'Oxygen', endereco: "15.9994"},
  {id: 9, nome: 'Fluorine', endereco: "18.9984"},
  {id: 10, nome: 'Neon', endereco: "20.1797"},
  {id: 11, nome: 'Sodium', endereco: "22.9897"},
  {id: 12, nome: 'Magnesium', endereco: "24.305"},
  {id: 13, nome: 'Aluminum', endereco: "26.9815"},
  {id: 14, nome: 'Silicon', endereco: "28.0855"},
  {id: 15, nome: 'Phosphorus', endereco: "30.9738"},
  {id: 16, nome: 'Sulfur', endereco: "32.065"},
  {id: 17, nome: 'Chlorine', endereco: "35.453"},
  {id: 18, nome: 'Argon', endereco: "39.948"},
  {id: 19, nome: 'Potassium', endereco: "39.0983"},
  {id: 20, nome: 'Calcium', endereco: "40.078"},
];