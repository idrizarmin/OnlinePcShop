import {Component, Input, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {mojConfig} from "../moj-config";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {FormBuilder, Validators,FormControl,NgForm,FormGroup} from "@angular/forms";
import {Korisnik} from "../_helpers/registracija-informacije";
import {Router} from "@angular/router";

declare function porukaSuccess(x:string):any;


@Component({
  selector: 'app-registracija',
  templateUrl: './registracija.component.html',
  styleUrls: ['./registracija.component.css']
})
export class RegistracijaComponent implements OnInit {
  @Input()
  kreirajKorisnik:any;
  drzavePodatci:any=null;
  KorisnickaForma:any;
  message:string;
  data=false;
  proba: string;

  constructor(private httpKlijent:HttpClient, private formbulider: FormBuilder,private autentifikacijaRegistracija:AutentifikacijaHelper,private router: Router,) { }

  ngOnInit() {
    this.KorisnickaForma=this.formbulider.group({
      ime:new FormControl( ''),
      prezime: new FormControl(''),
      drzavaID:  new FormControl('', [Validators.required]),
      email: new FormControl('',[Validators.required, Validators.email]),
      spol:  new FormControl(''),
      datumRodjenja: new FormControl('', [Validators.required]),
      korisnickoIme:new  FormControl( '',[Validators.required]),
      lozinka: new FormControl('',[Validators.required, Validators.minLength(8)])
    })
    this.testirajWebApi();
  }

  public validateControl = (controlName: string) => {
    return this.KorisnickaForma.controls[controlName].invalid && this.KorisnickaForma.controls[controlName].touched
  }
  public hasError = (controlName: string, errorName: string) => {
    return this.KorisnickaForma.controls[controlName].hasError(errorName)
  }

  getDrzave(){
    if (this.drzavePodatci == null)
      return [];
    return this.drzavePodatci;
  }

  onFormSubmit()
  {

    const korisnik = this.KorisnickaForma.value;

    const user: Korisnik = {
      ime: korisnik.ime,
      prezime: korisnik.prezime,
      email: korisnik.email,
      lozinka: korisnik.lozinka,
      drzavaID:korisnik.drzavaID,
      spol:korisnik.spol,
      datumRodjenja:korisnik.datumRodjenja,
      korisnickoIme: korisnik.korisnickoIme,
      adresa1:"",
      adresa2:""

    };
    this.KreirajKorisnika(user);
    porukaSuccess("Uspjesno ste kreirali korisniči račun");
  }
  KreirajKorisnika(register:Korisnik)
  {
    this.autentifikacijaRegistracija.registerUser(register).subscribe(
      ()=>
      {
        this.data = true;
        this.message = 'Data saved Successfully';
        this.KorisnickaForma.reset();
      });
  }


  testirajWebApi() :void
  {
    this.httpKlijent.get(mojConfig.adresa_servera+ "/Drzava/GetAll_ForCmb",mojConfig.http_opcije()).subscribe(x=>{
      this.drzavePodatci = x;
    });
  }

}



