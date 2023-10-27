import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {mojConfig} from "../moj-config";

declare function porukaSuccess(a: string):any;
declare function porukaError(a:string):any;
@Component({
  selector: 'app-narudzbe',
  templateUrl: './narudzbe.component.html',
  styleUrls: ['./narudzbe.component.css']
})
export class NarudzbeComponent implements OnInit {
  title:string='angular';
  narudzbePodatci :any;
  ime:any='';
  odabranaNarudzba: any=null;
  private configSettings: any=null;

  constructor(private httpKlijent: HttpClient) {
  }

  testirajWebApi() :void
  {
    this.httpKlijent.get(mojConfig.adresa_servera+ "/Narudzba/GetAll",mojConfig.http_opcije()).subscribe(x=>{
      this.narudzbePodatci = x;
    });
  }
  getNarudzbaPodaci() {
    if (this.narudzbePodatci == null)
      return [];
    return this.narudzbePodatci;
  }

  ngOnInit(): void {
    this.testirajWebApi();
  }

  detalji(n:any) {
   this.odabranaNarudzba=n;
   this.odabranaNarudzba.prikazi=true;
  }

  snimi() {
    //this.odabranaNarudzba
    this.httpKlijent.post(mojConfig.adresa_servera+ "/Narudzba/Update/" + this.odabranaNarudzba.id,this.odabranaNarudzba)
      .subscribe((x:any)=>{
        alert("Uredu"+x.potvrdjena);

      });
    this.testirajWebApi();
  }

  btnNovi() {
  this.odabranaNarudzba={
    prikazi:true,
    dostavljacID:0,
    naruciocID:0,
    potvrdjena:0,
    aktivna:0
  }

  }

  obrisi(n:any) {
    this.httpKlijent.post(mojConfig.adresa_servera+"/Narudzba/Delete/"+n.id,null)
      .subscribe((x:any)=>{
        const index=this.narudzbePodatci.indexOf(n);
        if(index>-1){
          this.narudzbePodatci.splice(index,1);
        }
        porukaSuccess("Narudba uspjesno obrisana");
      })
  }



}



