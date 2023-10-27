import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from "@angular/router";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Injectable()
export class AutorizacijaKorisnikProvjera implements CanActivate {

  constructor(private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {

    try {
      //nedovrseno privremeno rjesenje
      if (AutentifikacijaHelper.getLoginInfo().isPermisijaKorisnik)

        return true;
    }catch (e) {
    }

    // not logged in so redirect to login page with the return url
    this.router.navigate(['login'], { queryParams: { returnUrl: state.url }});
    return false;
  }
}
