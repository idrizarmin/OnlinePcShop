import {Component, Input, OnInit} from '@angular/core';
import {mojConfig} from "../../moj-config";
import {HttpClient} from "@angular/common/http";

declare function porukaSuccess(x:string):any;

@Component({
  selector: 'app-post-edit',
  templateUrl: './post-edit.component.html',
  styleUrls: ['./post-edit.component.css']
})
export class PostEditComponent implements OnInit {
  @Input()urediPost:any;
  admini: any;
  constructor(private httpKlijent:HttpClient) {
    this.httpKlijent.get(mojConfig.adresa_servera+"/Administrator/GetAll").subscribe((x:any)=>{
      this.admini=x;
    });
  }

  ngOnInit(): void {}

  snimi() {
    if(this.urediPost.id==null){
      this.urediPost.snizen=false;//definirano kao false jer ukoliko ne promjenimo vrijednost checkboxa salje se 0 umjseto true ili false
      this.httpKlijent.post(mojConfig.adresa_servera+"/Post/Add",this.urediPost).subscribe((x:any)=>{
          porukaSuccess("Post " +x.nazivProizvoda +" uspjesno dodat");
          this.urediPost.prikazi=false;
        }
      )
    }
    else {
      this.httpKlijent.post(mojConfig.adresa_servera + "/Post/Update/" + this.urediPost.id, this.urediPost).subscribe((x: any) => {
        porukaSuccess("Post sa naziviom '" + x.naslov + "' uspjesno editovan");
        this.urediPost.prikazi = false;

      });
    }
  }
}
