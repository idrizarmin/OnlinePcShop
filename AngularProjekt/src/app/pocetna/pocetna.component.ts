import { Component, OnInit } from '@angular/core';
import {mojConfig} from "../moj-config";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {NarudzbaService} from "../_helpers/narudzba.service";

@Component({
  selector: 'app-pocetna',
  templateUrl: './pocetna.component.html',
  styleUrls: ['./pocetna.component.css']
})
export class PocetnaComponent implements OnInit {
  odabraniProizvod:any=null;
  proizvodi:any;
  total:number = 1;
  page:number = 1;
  limit:number = 8;
  loading:boolean = false;

  constructor(private httpKlijent: HttpClient,private router:Router,private narudzbaService:NarudzbaService) {
  }

  ngOnInit(): void {
    this.testirajWebApi();
  }
  testirajWebApi() :void
  {
    let parametri={
      page_number: this.page,
      items_per_page:this.limit
    }
    JSON.stringify(parametri)
    this.httpKlijent.get(mojConfig.adresa_servera+ "/Proizvod/GetAllPaged",
      {params:parametri}).subscribe((x:any)=>{
      this.proizvodi = x['dataItems'];
      this.total=x['totalCount'];
      this.loading=false;
      this.proizvodi.forEach((a:any) => {


      });

    });
  }
  getProizvodPodatci() {
    if (this.proizvodi == null)
      return [];
    return this.proizvodi;
  }

  dodajUKorpu(p:any) {
      this.narudzbaService.addtoCart(p);
  }

  Prikazi(p: any) {
    this.odabraniProizvod=p;
    this.odabraniProizvod.prikazi=true;
  }
  loginInfo(): LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  goToPrevious(): void {

    this.page--;
    this.testirajWebApi();
  }

  goToNext(): void {

    this.page++;
    this.testirajWebApi();
  }

  goToPage(n: number): void {
    this.page = n;
    this.testirajWebApi();
  }
}
