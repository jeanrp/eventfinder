import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";


@Component({
  selector: 'app-menu-superior',
  templateUrl: './menu-superior.component.html',
  styleUrls: ['./menu-superior.component.css']
})
export class MenuSuperiorComponent implements OnInit {
  public token;
  public user;
  public razaoSocial: string = "";
  public isCollapsed: boolean = true;
  public mostrarMenu: boolean = true;

  constructor(
    private router: Router,
    private toastr: ToastrService) {
    this.token = localStorage.getItem('eio.token');
    this.user = JSON.parse(localStorage.getItem('eio.user'));
  }

  ngOnInit() {
    if (this.user) {
      this.razaoSocial = this.user.nome;
    }
  }

  usuarioLogado(): boolean {
    return this.token !== null;
  }

  logout() {
    localStorage.removeItem('eio.token');
    localStorage.removeItem('eio.user');
    this.toastr.info('Sua sessão foi finalizada');
    this.router.navigate(['/home']);
  }
}
