import {Component, Input, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {mojConfig} from "../../moj-config";
declare function porukaSuccess(x:string):any;
@Component({
  selector: 'app-administrator-edit',
  templateUrl: './administrator-edit.component.html',
  styleUrls: ['./administrator-edit.component.css']
})
export class AdministratorEditComponent implements OnInit {
  @Input()urediAdmina:any;

  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
  }

  snimi() {
    this.httpKlijent.post(mojConfig.adresa_servera+"/Administrator/Add/",this.urediAdmina).subscribe((x:any)=>{
      porukaSuccess("Uspjesno ste dodali novog admina");
    });
  }
}
