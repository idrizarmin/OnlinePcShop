import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-administrator-postovi',
  templateUrl: './administrator-postovi.component.html',
  styleUrls: ['./administrator-postovi.component.css']
})
export class AdministratorPostoviComponent implements OnInit {
  @Input()urediPost:any;
  constructor() { }

  ngOnInit(): void {
  }

}
