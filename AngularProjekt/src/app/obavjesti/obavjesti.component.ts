import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {mojConfig} from "../moj-config";
import {HttpClient} from "@angular/common/http";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";



@Component({
  selector: 'app-obavjesti',
  templateUrl: './obavjesti.component.html',
  styleUrls: ['./obavjesti.component.css']
})
export class ObavjestiComponent implements OnInit {

@Input()
  prikazObavjesti:any;
  obavjestiPodatci:any;
  korisnikId: any;
  brojObavjesti:number;
  id:any;
  deletedData:any=null;


  constructor(private httpKlijent:HttpClient) {
  }

  ngOnInit(): void {
      this.ucitajObavjesti()


  }


  ucitajObavjesti(): void {
    if(AutentifikacijaHelper.getLoginInfo().isPermisijaKorisnik && AutentifikacijaHelper.getLoginInfo().isLogiran) {
      this.korisnikId = AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.korisnickiNalog.id;
      let korinsnik = {
        id: this.korisnikId
      }
      this.httpKlijent.get(mojConfig.adresa_servera + "/Obavjest/GetUserNotifications",
        {params: korinsnik}).subscribe((x: any) => {
        this.obavjestiPodatci = x['data'];
        this.brojObavjesti = this.obavjestiPodatci.length;

      });
    }
  }

  getObavjesti(){
    if(this.obavjestiPodatci==null)
      return [];
    return this.obavjestiPodatci;
  }

  dismissModal() {
      this.prikazObavjesti=false;

  }

  setDeleted(o: any) {
    if(AutentifikacijaHelper.getLoginInfo().isPermisijaKorisnik && AutentifikacijaHelper.getLoginInfo().isLogiran) {
      this.id = o.id;
      this.httpKlijent.post(mojConfig.adresa_servera + "/Obavjest/SetObavjestiAsDeleted/" + this.id, this.id)
        .subscribe(data =>
            this.deletedData=data
    );


      this.ucitajObavjesti();
    }
  }
  loginInfo(): LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
}
