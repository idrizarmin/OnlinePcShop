import {Component, Input, OnInit} from '@angular/core';
import {LoginInformacije} from "../../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../../_helpers/autentifikacija-helper";
import {ActivatedRoute} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {mojConfig} from "../../moj-config";
import {NarudzbaService} from "../../_helpers/narudzba.service";

@Component({
  selector: 'app-proizvod-detaljno',
  templateUrl: './proizvod-detaljno.component.html',
  styleUrls: ['./proizvod-detaljno.component.css']
})
export class ProizvodDetaljnoComponent implements OnInit {
  @Input()
  sub:any;
  proizvodGet:any;
  private id:number;


  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute,private narudzbaService :NarudzbaService) {}

  ngOnInit(): void {

    this.sub = this.route.params.subscribe(params => {
      this.id = +params['id']; // (+) converts string 'id' to a number

  });
    this.preuzmiPodakte();
  }

  loginInfo(): LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

   preuzmiPodakte() {
    this.httpKlijent.get(mojConfig.adresa_servera+ `/Proizvod/Get/${this.id}`, mojConfig.http_opcije()).subscribe(x=>{
      this.proizvodGet = x


    });
  }
  getProizvod(){
    if (this.proizvodGet==null)
      return ;
    return this.proizvodGet;
  }
  dodajUKorpu(p:any) {
    this.narudzbaService.addtoCart(p);
  }
}
