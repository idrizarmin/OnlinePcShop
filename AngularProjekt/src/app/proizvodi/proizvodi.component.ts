import { Component, OnInit } from '@angular/core';
import {mojConfig} from "../moj-config";
import {HttpClient} from "@angular/common/http";

declare function porukaSuccess(a: string):any;
declare function porukaError(a:string):any;

@Component({
  selector: 'app-proizvodi',
  templateUrl: './proizvodi.component.html',
  styleUrls: ['./proizvodi.component.css']
})
export class ProizvodiComponent implements OnInit {

  proizvodiPodatci:any;
  odabraniProizvod:any=null;
  naziv:string='';
  total:number = 1;
  page:number = 1;
  limit:number = 8;
  loading:boolean = false;
  constructor(private httpKlijent: HttpClient) {
  }


  testirajWebApi():void {
    let parametri={
      page_number: this.page,
      items_per_page:this.limit
    }
    JSON.stringify(parametri)
    this.httpKlijent.get(mojConfig.adresa_servera+ "/Proizvod/GetAllPaged",
      {params:parametri}).subscribe((x:any)=>{
      this.proizvodiPodatci = x['dataItems'];
      this.total=x['totalCount'];
      this.loading=false;
      this.proizvodiPodatci.forEach((a:any) => {
      });

    });
  }

  getProizvodiPodaci() {
    if (this.proizvodiPodatci==null)
      return[];
    return this.proizvodiPodatci.filter((x: any)=> x.nazivProizvoda.length==0 || (x.nazivProizvoda).toLowerCase().startsWith(this.naziv.toLowerCase()));
  }
  ngOnInit(): void {
    this.testirajWebApi();
  }
  detalji(p:any) {
    this.odabraniProizvod=p;
    this.odabraniProizvod.prikazi=true;
  }
  snimi() {
    //this.odabranaNarudzba
    this.httpKlijent.post(mojConfig.adresa_servera+ "/Proizvod/Update/" + this.odabraniProizvod.id,this.odabraniProizvod)
      .subscribe((x:any)=>{
        alert("Uredu"+x.potvrdjena)
      })
  }
  btnNovi() {
    this.odabraniProizvod=
      {
        prikazi:true,
        nazivProizvoda:"",
        cijena:0,
        kolicina:1,
        opis:"",
        kategorijaID:0,
        lokacijaSlike:"",
        snizen:0,
        proizvodjacID:0,
        naStanju:0,
      }
  }
  obrisi(p:any) {
    this.httpKlijent.post(mojConfig.adresa_servera+"/Proizvod/Delete/"+p.proizvodID,null)
      .subscribe((x:any)=>{
        const index=this.proizvodiPodatci.indexOf(p);
        if(index>-1){
          this.proizvodiPodatci.splice(index,1);
        }
        porukaSuccess("Proizvod uspjesno obrisan");
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
    this.testirajWebApi();
  }
}
