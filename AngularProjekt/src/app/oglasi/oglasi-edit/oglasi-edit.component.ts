import {Component, Input, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {mojConfig} from "../../moj-config";
import {AutentifikacijaHelper} from "../../_helpers/autentifikacija-helper";
import {LoginInformacije} from "../../_helpers/login-informacije";

declare function porukaSuccess(s:string):any;

@Component({
  selector: 'app-oglasi-edit',
  templateUrl: './oglasi-edit.component.html',
  styleUrls: ['./oglasi-edit.component.css']
})
export class OglasiEditComponent implements OnInit {
  @Input()
  urediOglas:any;

  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
  }

  snimi() {
    if(this.urediOglas.id==null){
      this.urediOglas.aktivan=false;//definirano kao false jer ukoliko ne promjenimo vrijednost checkboxa salje se 0 umjseto true ili false
      this.urediOglas.autorOglasaID= this.loginInfo().autentifikacijaToken.korisnickiNalog.id;
      this.httpKlijent.post(mojConfig.adresa_servera+"/Oglasi/Add",this.urediOglas).subscribe((x:any)=>{
          porukaSuccess("Oglas za " +x.naslov +" uspjesno kreiran");
          this.urediOglas.prikazi=false;
        }
      )
    }
    else{
      this.httpKlijent.post(mojConfig.adresa_servera+ "/Oglasi/Update/" + this.urediOglas.id, this.urediOglas).subscribe((x:any) => {
        porukaSuccess("Oglas sa naslovom '" + x.naslov+"' uspjesno editovan");
        this.urediOglas.prikazi = false;

      });
  }
  }
  loginInfo(): LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

}
