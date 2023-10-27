import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {mojConfig} from "../moj-config";
import {Oglas} from "../_helpers/Oglas";



declare function porukaSuccess(a: string):any;


@Component({
  selector: 'app-oglasi',
  templateUrl: './oglasi.component.html',
  styleUrls: ['./oglasi.component.css']
})

export class OglasiComponent implements OnInit {


  oglasiPodatci:any;
  total:number = 1;
  page:number = 1;
  limit:number = 8;
  loading:boolean = false;


  constructor(private httpKlijent:HttpClient ) {
  }


  testirajWebApi() {

   let parametri={
     Page: this.page,
     PageSize:this.limit
   }
   JSON.stringify(parametri)
      this.httpKlijent.get(mojConfig.adresa_servera+ "/Oglasi/GetAall",
      {params:parametri},).subscribe((x:any)=>{
        //this.oglasiPodatci=x;
        this.oglasiPodatci=x['data'];
        console.log(this.oglasiPodatci)
        this.total=x['pagedResult']['totalItems'];
        console.log(this.oglasiPodatci)
        this.loading=false;
  console.log(this.page)

    });
  }

  getOglasiPodaci() {
    if (this.oglasiPodatci == null)
      return [];
    return this.oglasiPodatci;
  }

  ngOnInit(): void {
    this.testirajWebApi();
  }

  goToPrevious(): void {
    this.page--;
    this.testirajWebApi();
  }

  goToNext(): void {
    this.page++;
    this.testirajWebApi();
  }

  goToPage(n: number): void {
    this.page = n;
    console.log(this.page)
    this.testirajWebApi();
  }

}
