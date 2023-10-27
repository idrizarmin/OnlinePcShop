import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {mojConfig} from "../moj-config";
declare function porukaSuccess(x:string):any;

@Component({
  selector: 'app-administrator',
  templateUrl: './administrator.component.html',
  styleUrls: ['./administrator.component.css']
})
export class AdministratorComponent implements OnInit {

  noviProizvodjac:any=null;
  novaKategorija:any=null;
  noviPost:any=null;

  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
  }

  btnDodajProizvodjaca() {
    this.noviProizvodjac = {
      prikazi: true,
      id: 0,
      nazivProizvodjaca:"",
      sjedisteID:1,
    }
  }

  btnDodajKategoriju() {
    this.novaKategorija={
      id:0,
      prikazi:true,
      nazivKategorije:"",
    };
  }
}
