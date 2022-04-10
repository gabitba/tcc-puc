import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-clientes',
  templateUrl: './clientes.component.html',
  styleUrls: ['./clientes.component.css']
})
export class ClientesComponent implements OnInit {
  moduloClientesUrl: SafeResourceUrl | undefined;
  constructor(public sanitizer: DomSanitizer) {
    this.moduloClientesUrl = this.sanitizer.bypassSecurityTrustResourceUrl(environment.moduloClienteUrl)
   }
  ngOnInit(): void {
  }
}
