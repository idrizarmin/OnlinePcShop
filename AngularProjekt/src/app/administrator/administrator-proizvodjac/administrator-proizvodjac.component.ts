import {Component, Input, OnInit} from '@angular/core';
import {mojConfig} from "../../moj-config";
import {HttpClient} from "@angular/common/http";
declare function porukaSuccess(x:string):any;

@Component({
  selector: 'app-administrator-proizvodjac',
  templateUrl: './administrator-proizvodjac.component.html',
  styleUrls: ['./administrator-proizvodjac.component.css']
})
export class AdministratorProizvodjacComponent implements OnInit {
  @Input()
  urediProizvodjaca: any;
  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
  }

  snimiProizvodjaca() {
    this.httpKlijent.post(mojConfig.adresa_servera+"/Proizvodjac/Add/",this.urediProizvodjaca).subscribe((x:any)=>{
      porukaSuccess("Uspjesno dodan proizvodjac");
      this.urediProizvodjaca.prikazi=false;
    });
  }
}
