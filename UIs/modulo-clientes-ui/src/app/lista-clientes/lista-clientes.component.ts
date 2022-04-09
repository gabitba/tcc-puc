import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTable } from '@angular/material/table';
import { Cliente, ClientesService } from './../clientes.service';

@Component({
  selector: 'app-lista-clientes',
  templateUrl: './lista-clientes.component.html',
  styleUrls: ['./lista-clientes.component.css']
})
export class ListaClientesComponent implements OnInit {
  constructor(private clientesService: ClientesService) { }
  
  clientes: Cliente[] = [] 
  displayedColumns = ['id', 'nome', 'endereco'];

  @ViewChild(MatTable) myTable!: MatTable<any>;

  ngOnInit(): void {
    this.clientesService.getListaClientes().subscribe(
      (clientes: Cliente[]) => {
        this.clientes.push(...clientes);
        this.myTable.renderRows();
      }
    )
  }
}