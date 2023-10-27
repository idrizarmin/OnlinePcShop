import { Component, OnInit } from '@angular/core';
import {mojConfig} from "../moj-config";
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute} from "@angular/router";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {NarudzbaService} from "../_helpers/narudzba.service";

@Component({
  selector: 'app-proizvodi-korisnik',
  templateUrl: './proizvodi-korisnik.component.html',
  styleUrls: ['./proizvodi-korisnik.component.css']
})
export class ProizvodiKorisnikComponent implements OnInit {

  sub:any;
  proizvodiPodatci:any=null;
  private id:number;
  kategorijaGet:any=null;


  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute,private narudzbaService:NarudzbaService) {}


  ngOnInit(): void {

    this.sub = this.route.params.subscribe(params => {
      this.id = +params['id']; // (+) converts string 'id' to a number
      console.log(this.proizvodiPodatci);
    });
    this.preuzmiPodakte();
    this.preuzmiKategoriju();
  }
  preuzmiPodakte() {
    this.httpKlijent.get(mojConfig.adresa_servera+ `/Proizvod/GetAll`, mojConfig.http_opcije()).subscribe(x=>{
      this.proizvodiPodatci = x


    });
  }
  getProizvod(){
    if (this.proizvodiPodatci==null)
      return [];
    return this.proizvodiPodatci.filter((x:any)=>x.kategorijaID==this.id);
    console.log(this.proizvodiPodatci);
  }
  preuzmiKategoriju() {
    this.httpKlijent.get(mojConfig.adresa_servera+ `/Kategorija/Get/${this.id}`, mojConfig.http_opcije()).subscribe(x=>{
      this.kategorijaGet = x

    });
  }
  getKategoriju() {
    if (this.kategorijaGet == null)
      return [];
    return this.kategorijaGet;
  }
  loginInfo(): LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  dodajUKorpu(p:any) {
    this.narudzbaService.addtoCart(p);
  }
}

