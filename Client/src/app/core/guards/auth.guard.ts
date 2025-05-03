import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import { map, of } from 'rxjs';
import { SnackbarService } from '../services/snackbar.service';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const router = inject(Router);
  const snackbar = inject(SnackbarService);

  if (accountService.currentUser()) {
    return of(true);
  } else {
    return accountService.getAuthState().pipe(
      map(auth => {
        if (auth.isAuthenticated && accountService.isAdmin()) {
          return true;
        } else {
          snackbar.error("Unathorized");
          router.navigate(["account/login"], { queryParams: { returnUrl: state.url } });
          return false;
        }
      })
    );

  }

};
