import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {mojConfig} from "../moj-config";

@Component({
  selector: 'app-postovi',
  templateUrl: './postovi.component.html',
  styleUrls: ['./postovi.component.css']
})
export class PostoviComponent implements OnInit {

  postoviPodatci:any;
  total:number = 1;
  page:number = 1;
  limit:number = 4;
  loading:boolean = false;
  constructor(private httpKlijent: HttpClient) {
  }


  ngOnInit(): void {
    this.testirajWebApi();
  }
  testirajWebApi() {

    let parametri={
      page_number: this.page,
      items_per_page:this.limit
    }
    JSON.stringify(parametri)
    this.httpKlijent.get(mojConfig.adresa_servera+ "/Post/GetAllPaged",
      {params:parametri},).subscribe((x:any)=>{
      this.postoviPodatci=x['dataItems'];
      this.total=x['totalCount'];
      this.loading=false;
      console.log(this.page)

    });
  }
  getPostoviPodaci() {
    if (this.postoviPodatci == null)
      return [];
    return this.postoviPodatci;
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
