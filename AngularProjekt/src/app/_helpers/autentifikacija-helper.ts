import {LoginInformacije} from "./login-informacije";
import {HttpClient} from "@angular/common/http";
import {Korisnik} from "./registracija-informacije";
import {mojConfig} from "../moj-config";
import {Injectable} from "@angular/core";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn:'root'
})
export class AutentifikacijaHelper {
  baseUrl: string = "https://localhost:44304/";
  constructor(private _http: HttpClient) { }

  static setLoginInfo(x: LoginInformacije): void {
    if (x == null)
      x = new LoginInformacije();
    localStorage.setItem("autentifikacija-token", JSON.stringify(x));
  }

  static getLoginInfo(): LoginInformacije {
    let x = localStorage.getItem("autentifikacija-token");
    if (x === "")
      return new LoginInformacije();

    try {
      let loginInformacije: LoginInformacije = JSON.parse(x);
      if (loginInformacije == null)
        return new LoginInformacije();
      return loginInformacije;
    } catch (e) {
      return new LoginInformacije();
    }
  }
  public registerUser = (body: Korisnik) => {
    return this._http.post(mojConfig.adresa_servera+"/Korisnik/Add", body);
  }
  confirmEmail(model: any) {
    return this._http.post(mojConfig.adresa_servera + '/Korisnik/ConfirmEmail', model);
  }
}
