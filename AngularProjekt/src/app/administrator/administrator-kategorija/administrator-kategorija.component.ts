import {Component, Input, OnInit} from '@angular/core';
import {mojConfig} from "../../moj-config";
import {HttpClient} from "@angular/common/http";
declare function porukaSuccess(x:string):any;

@Component({
  selector: 'app-administrator-kategorija',
  templateUrl: './administrator-kategorija.component.html',
  styleUrls: ['./administrator-kategorija.component.css']
})
export class AdministratorKategorijaComponent implements OnInit {
  @Input()urediKategoriju:any;
  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
  }
  snimiKategoriju() {
    this.httpKlijent.post(mojConfig.adresa_servera+"/Kategorija/Add/",this.urediKategoriju).subscribe((x:any)=>{
      porukaSuccess("Uspjesno dodana kategorija");
      this.urediKategoriju.prikazi=false;
    });
  }
}
