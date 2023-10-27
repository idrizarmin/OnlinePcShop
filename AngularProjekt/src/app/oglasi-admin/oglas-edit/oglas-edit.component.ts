import {Component, Input, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {mojConfig} from "../../moj-config";
declare function porukaSuccess(x:string):any;
@Component({
  selector: 'app-oglas-edit',
  templateUrl: './oglas-edit.component.html',
  styleUrls: ['./oglas-edit.component.css']
})
export class OglasEditComponent implements OnInit {
  @Input()urediOglas:any;
  admini: any;
  constructor(private httpKlijent:HttpClient) {
    this.httpKlijent.get(mojConfig.adresa_servera+ "/Administrator/GetAll").subscribe(x=>{
      this.admini = x;
    });
  }

  ngOnInit(): void {
  }

  snimi() {
    if(this.urediOglas.id==null){
      this.urediOglas.snizen=false;//definirano kao false jer ukoliko ne promjenimo vrijednost checkboxa salje se 0 umjseto true ili false
      this.httpKlijent.post(mojConfig.adresa_servera+"/Oglasi/Add",this.urediOglas).subscribe((x:any)=>{
          porukaSuccess("Proizvod " +x.nazivProizvoda +" uspjesno dodat");
          this.urediOglas.prikazi=false;
        }
      )
    }
    else{
      this.httpKlijent.post(mojConfig.adresa_servera+ "/Oglasi/Update/" + this.urediOglas.id, this.urediOglas).subscribe((x:any) => {
        porukaSuccess("Proizvod sa naziviom '" + x.nazivProizvoda+"' uspjesno editovan");
        this.urediOglas.prikazi = false;

      });
    }
  }
}
