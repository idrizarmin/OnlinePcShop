import { Component, OnInit } from '@angular/core';
import {NarudzbaService} from "../_helpers/narudzba.service";
import {HttpClient} from "@angular/common/http";
import {mojConfig} from "../moj-config";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";


declare function porukaSuccess(s:string):any;

@Component({
  selector: 'app-korpa',
  templateUrl: './korpa.component.html',
  styleUrls: ['./korpa.component.css']
})
export class KorpaComponent implements OnInit {

  public products : any = [];
  public grandTotal !: number;
  nesto:any;
  proizvodi:any=[];
  id:number;
  kolicina:number;
  constructor(private httpKlijent: HttpClient,private narudzbaService: NarudzbaService) {  }

  ngOnInit(): void {
    this.narudzbaService.getProducts()
      .subscribe(res=>{
        this.products = res;
        this.grandTotal = this.narudzbaService.getTotalPrice();
      });
  }

  removeItem(item: any){
    this.narudzbaService.removeCartItem(item);
  }
  emptycart(){
    this.narudzbaService.removeAllCart();
  }
  naruci(){

    var proizvodiIDs:number[] = new Array(this.products.length);
    var kolicine:number[] = new Array(this.products.length);

    for(var i = 0;i<proizvodiIDs.length;i++) {
      proizvodiIDs[i] = this.products[i].proizvodID

    }
    for(var i = 0;i<kolicine.length;i++) {
      kolicine[i] = this.products[i].kolicina

    }
    this.id = AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.korisnickiNalog.id;
    var korpaStavke ={
      id:proizvodiIDs,
      kolicina:kolicine,
      korisnikID:this.id

    }


    this.httpKlijent.post(mojConfig.adresa_servera+"/Narudzba/Post",korpaStavke).subscribe((x:any) => {
      porukaSuccess("Vaša narudžba je uspjesno kreirana");
      this.nesto =x;

    });
  }
}
