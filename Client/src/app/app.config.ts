import { ApplicationConfig, inject, provideAppInitializer, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { AccountService } from './core/services/account.service';
import { lastValueFrom } from 'rxjs';
import { authInterceptor } from './core/interceptors/auth.interceptor';


function initializeApp() {
  const accountService = inject(AccountService);
  return lastValueFrom(accountService.getUserInfo());

}

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(withInterceptors([
      authInterceptor
    ])),
    provideAppInitializer(initializeApp)
  ]


};
