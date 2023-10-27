export interface Oglas{
  id: number;
  naslov:string;
  sadrzaj:string;
  brojPozicja:number;
  lokacija:string;
  datumObjave:Date;
  trajanjeOglasa:number;
  datumIsteka:Date;
  autorOglasaID:number
  aktivan:boolean;
  cvEmail:string;
}
