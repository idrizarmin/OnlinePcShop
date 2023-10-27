import {HttpHeaders} from "@angular/common/http";
import {AutentifikacijaToken} from "./_helpers/login-informacije";
import {AutentifikacijaHelper} from "./_helpers/autentifikacija-helper";
export  class mojConfig{

   static adresa_servera="https://localhost:44304";
 //static adresa_servera="https://backend.p2089.app.fit.ba";
  static http_opcije=function (){
    let autentifikacijaToken:AutentifikacijaToken = AutentifikacijaHelper.getLoginInfo().autentifikacijaToken;
    let mojToken = "";

    if (autentifikacijaToken!=null)
      mojToken = autentifikacijaToken.vrijednost;
    return{
      headers:{
        'autentifikacija-token': mojToken,
      }
    };
  }
}
