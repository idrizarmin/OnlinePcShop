import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {Router} from "@angular/router";
import {Korisnik} from "../_helpers/registracija-informacije";
import {mojConfig} from "../moj-config";

declare function porukaSuccess(s:string):any;

@Component({
  selector: 'app-korisnicki-profil',
  templateUrl: './korisnicki-profil.component.html',
  styleUrls: ['./korisnicki-profil.component.css']
})
export class KorisnickiProfilComponent implements OnInit {

  lokacija:string;
  korisnikPodatci={
    lokacijaSlike:"",
    ime:"",
    prezime:"",
    spol:"",
    drzavaID:1,
    korisnickoIme:"",
    adresa1: "",
    adresa2: "",
    email:"",
    datumRodjenja:new Date()
  }
  drzavePodatci:any;
  id:any;

  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
   this.getKorisnika();
    this.ucitajDrzave();
  }

  onfileSelected(event:any) {
    let slika=event.target.files[0].name;
    this.lokacija="assets/img/Korisnici/"+slika;
    this.korisnikPodatci.lokacijaSlike=this.lokacija;
  }
getKorisnika(){
    if(AutentifikacijaHelper.getLoginInfo().isPermisijaKorisnik) {
      this.id = AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.korisnickiNalog.id;
      this.httpKlijent.get(mojConfig.adresa_servera+"/Korisnik/Get/"+this.id,mojConfig.http_opcije())
        .subscribe((x:any)=>{
          this.korisnikPodatci=x;

        });
    }
}

  ucitajDrzave() :void
  {
    this.httpKlijent.get(mojConfig.adresa_servera+ "/Drzava/GetAll_ForCmb",mojConfig.http_opcije()).subscribe(x=>{
      this.drzavePodatci = x;
    });
  }
  getDrzave(){
    if(this.drzavePodatci==null)
      return[];
    return this.drzavePodatci;
  }
  spasiPromjene(){
    this.httpKlijent.post(mojConfig.adresa_servera+ "/Korisnik/Update/" + this.id, this.korisnikPodatci).subscribe((x:any) => {
      porukaSuccess(x.korisnickoIme + " vaše promjene su uspješno pohranjene");
    });
  }
}
