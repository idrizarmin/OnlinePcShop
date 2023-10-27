import { Component, OnInit } from '@angular/core';
import {mojConfig} from "../moj-config";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  txtLozinka: any;
  txtKorisnickoIme: any;


  constructor(private httpKlijent: HttpClient ,private router:Router) {
  }

  ngOnInit(): void {
  }

  btnLogin() {
    let saljemo = {
      korisnickoIme:this.txtKorisnickoIme,
      lozinka: this.txtLozinka
    };
    this.httpKlijent.post<LoginInformacije>(mojConfig.adresa_servera+ "/Autentifikacija/Login/", saljemo)
      .subscribe((x:LoginInformacije) =>{
        if (x.isLogiran) {
          AutentifikacijaHelper.setLoginInfo(x);
         this.Otvori()
        }
        else
        {
          AutentifikacijaHelper.setLoginInfo(null)
          porukaError("Došlo je do greške, pokušajte ponovno");
        }
      });
  }
  Otvori() {
    this.router.navigateByUrl("/pocetna")
      .then(() => {
        window.location.reload();
      });
  }
}
