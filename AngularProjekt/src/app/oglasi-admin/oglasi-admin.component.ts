import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {mojConfig} from "../moj-config";

declare function porukaSuccess(x:string):any;

@Component({
  selector: 'app-oglasi-admin',
  templateUrl: './oglasi-admin.component.html',
  styleUrls: ['./oglasi-admin.component.css']
})
export class OglasiAdminComponent implements OnInit {
  odabraniOglas: any=null;
  oglasiPodaci:any;
  total:number = 1;
  page:number = 1;
  limit:number = 8;
  loading:boolean = false;
  constructor(private httpKlijent:HttpClient) { }

  testirajWebApi():void {
    let parametri={
      Page: this.page,
      PageSize:this.limit
    }
    JSON.stringify(parametri)
    this.httpKlijent.get(mojConfig.adresa_servera+ "/Oglasi/GetAall",
      {params:parametri},).subscribe((x:any)=>{
      //this.oglasiPodatci=x;
      this.oglasiPodaci=x['data'];
      console.log(this.oglasiPodaci)
      this.total=x['pagedResult']['totalItems'];
      console.log(this.oglasiPodaci)
      this.loading=false;
      console.log(this.page)

    });
  }

  getOglasiPodaci() {
    if (this.oglasiPodaci==null)
      return[];
    return this.oglasiPodaci;
  }
  ngOnInit(): void {
    this.testirajWebApi();
  }
  detalji(p:any) {
    this.odabraniOglas=p;
    this.odabraniOglas.prikazi=true;
  }
  snimi() {
    this.httpKlijent.post(mojConfig.adresa_servera+ "/Oglasi/Update/" + this.odabraniOglas.id,this.odabraniOglas)
      .subscribe((x:any)=>{
        alert("Uredu"+x.potvrdjena)
      })
  }
  btnNovi() {
    this.odabraniOglas=
      {
        prikazi:true,
        naslov:"",
        sadrzaj:"",
        brojPozicja:0,
        lokacija:"",
        datumObjave:new Date().getDate(),
        trajanjeOglasa:0,
        datumIsteka:new Date().getDate(),
        autorOglasaID:2,
        aktivan:false,
      }
  }
  obrisi(p:any) {
    this.httpKlijent.post(mojConfig.adresa_servera+"/Oglasi/Delete/"+p.id,null)
      .subscribe((x:any)=>{
        const index=this.oglasiPodaci.indexOf(p);
        if(index>-1){
          this.oglasiPodaci.splice(index,1);
        }
        porukaSuccess("Oglas uspjesno obrisan");
      })
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
    console.log(this.page)
    this.testirajWebApi();
  }
}
