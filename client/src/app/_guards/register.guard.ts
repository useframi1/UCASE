import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { map } from 'rxjs';

export const RegisterGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);

  return accountService.currentUser$.pipe(
    map((user) => {
      if (user) return false;
      else {
        return true;
      }
    })
  );
};