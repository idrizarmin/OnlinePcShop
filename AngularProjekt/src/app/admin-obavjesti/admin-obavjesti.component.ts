import {Component, Input, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {mojConfig} from "../moj-config";
import {LoginInformacije} from "../_helpers/login-informacije";

@Component({
  selector: 'app-admin-obavjesti',
  templateUrl: './admin-obavjesti.component.html',
  styleUrls: ['./admin-obavjesti.component.css']
})
export class AdminObavjestiComponent implements OnInit {

  @Input()
  prikazObavjesti:any;
  obavjestiPodatci:any;
  private administratorId: any;
  brojObavjesti:number;
  id:any;
  deletedData:any=null;


  constructor(private httpKlijent:HttpClient) {
  }

  ngOnInit(): void {
    this.ucitajObavjesti()


  }


  ucitajObavjesti(): void {
    if(AutentifikacijaHelper.getLoginInfo().isPermisijaAdmin && AutentifikacijaHelper.getLoginInfo().isLogiran) {
      this.administratorId = AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.korisnickiNalog.id;
      let administrator = {
        id: this.administratorId
      }
      this.httpKlijent.get(mojConfig.adresa_servera + "/Obavjest/GetAdministratorNotifications",
        {params: administrator}).subscribe((x: any) => {
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
    if(AutentifikacijaHelper.getLoginInfo().isPermisijaAdmin && AutentifikacijaHelper.getLoginInfo().isLogiran) {
      this.id = o.id;
      this.httpKlijent.post(mojConfig.adresa_servera + "/Obavjest/SetAministratorObavjestiAsDeleted/" + this.id, this.id)
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
