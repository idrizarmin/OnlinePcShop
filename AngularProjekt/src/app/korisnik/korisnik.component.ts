import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {mojConfig} from "../moj-config";
declare function porukaSuccess(x:string):any;

@Component({
  selector: 'app-korisnik',
  templateUrl: './korisnik.component.html',
  styleUrls: ['./korisnik.component.css']
})
export class KorisnikComponent implements OnInit {

  korisnikPodaci:any;
  odabraniKorisnik:any=null;
  ime:string='';
  noviAdmin:any=null;


  constructor(private httpKlijent:HttpClient) { }

  testirajWebApi() {
    this.httpKlijent.get(mojConfig.adresa_servera+ "/Korisnik/GetAll").subscribe(x=>{
      this.korisnikPodaci = x;
    });
  }

  getProizvodiPodaci() {
    if (this.korisnikPodaci==null)
      return[];
    return this.korisnikPodaci.filter((x: any)=> this.ime=="" || (x.ime + " " + x.prezime).toLowerCase().startsWith(this.ime.toLowerCase()) || (x.prezime + " " + x.ime).toLowerCase().startsWith(this.ime.toLowerCase()));
  }
  ngOnInit(): void {
    this.testirajWebApi();
  }
  detalji(p:any) {
    this.odabraniKorisnik=p;
    this.odabraniKorisnik.prikazi=true;
  }
  snimi() {
    this.httpKlijent.post(mojConfig.adresa_servera+ "/Korisnik/Update/" + this.odabraniKorisnik.id,this.odabraniKorisnik)
      .subscribe((x:any)=>{
        alert("Uredu"+x.potvrdjena)
      })
  }

  obrisi(p:any) {
    this.httpKlijent.post(mojConfig.adresa_servera+"/Korisnik/Delete/"+p.id,null)
      .subscribe((x:any)=>{
        const index=this.korisnikPodaci.indexOf(p);
        if(index>-1){
          this.korisnikPodaci.splice(index,1);
        }
        porukaSuccess("Oglas uspjesno obrisan");
      })
  }


  btnNovi() {
    this.noviAdmin={
      prikazi:true,
      korisnickoIme:"" ,
      ime: "",
      prezime: "",
      spol: "muski",
      datumRodjenja: "",
      drzavaID: 1,
      lozinka: "",
      trajanjeUgovora:""
    }
  }
}
