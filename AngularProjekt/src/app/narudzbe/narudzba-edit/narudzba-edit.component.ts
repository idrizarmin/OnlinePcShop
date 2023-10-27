import {Component, Input, OnInit} from '@angular/core';
import {mojConfig} from "../../moj-config";
import {HttpClient} from "@angular/common/http";

declare function porukaSuccess(s:string):any;

@Component({
  selector: 'app-narudzba-edit',
  templateUrl: './narudzba-edit.component.html',
  styleUrls: ['./narudzba-edit.component.css']
})

export class NarudzbaEditComponent implements OnInit {
 @Input()
 urediNarudzbu:any;
 narudzbePodatci:any=null;
  dostavljac: any;
  narucioc: any;
  constructor(private httpKlijent:HttpClient) {
    this.httpKlijent.get(mojConfig.adresa_servera+"/Dostavljac/GetAll").subscribe((x:any)=>{
      this.dostavljac=x;
    });
    this.httpKlijent.get(mojConfig.adresa_servera+"/Korisnik/GetAll").subscribe((x:any)=>{
      this.narucioc=x;
    });
  }

  ngOnInit(): void {
    this.testirajWebApi();

  }
  snimi() {
    if(this.urediNarudzbu.id==null){
      this.urediNarudzbu.aktivna=false;//definirano kao false jer ukoliko ne promjenimo vrijednost checkboxa salje se 0 umjseto true ili false
      this.urediNarudzbu.potvrdjena=false;//definirano kao false jer ukoliko ne promjenimo vrijednost checkboxa salje se 0 umjseto true ili false
      this.httpKlijent.post(mojConfig.adresa_servera+"/Narudzba/Add",this.urediNarudzbu).subscribe((x:any)=>{
        porukaSuccess("Narudzba uspjesno kreirana" +x.Aktivna);
        this.urediNarudzbu.prikazi=false;
        this.testirajWebApi();
        }
      );
      this.testirajWebApi();
    }
    else{
    this.httpKlijent.post(mojConfig.adresa_servera+ "/Narudzba/Update/" + this.urediNarudzbu.id, this.urediNarudzbu).subscribe((x:any) => {
      porukaSuccess("uredu..." + x.aktivna);
      this.urediNarudzbu.prikazi = false;

    });

    }
    this.testirajWebApi();
  }
  testirajWebApi() :void
  {
    this.httpKlijent.get(mojConfig.adresa_servera+ "/Narudzba/GetAll",mojConfig.http_opcije()).subscribe(x=>{
      this.narudzbePodatci = x;
    });
  }
}
