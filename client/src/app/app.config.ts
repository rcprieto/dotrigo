import {
  ApplicationConfig,
  provideZoneChangeDetection,
  isDevMode,
  importProvidersFrom,
  LOCALE_ID,
} from '@angular/core';
import localePT from '@angular/common/locales/pt';
import { provideRouter, withInMemoryScrolling } from '@angular/router';
import { routes } from './routes';
import { provideServiceWorker } from '@angular/service-worker';
import { provideHttpClient, withInterceptors, HttpClient } from '@angular/common/http';
import { NgxSpinnerModule } from 'ngx-spinner';
import { provideToastr } from 'ngx-toastr';
import { errorInterceptor } from '../_interceptors/error.interceptor';
import { jwtInterceptor } from '../_interceptors/jwt.interceptor';
import { loadingInterceptor } from '../_interceptors/loading.interceptor';
import { registerLocaleData } from '@angular/common';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

registerLocaleData(localePT);

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideAnimationsAsync(),
    provideRouter(
      routes,
      withInMemoryScrolling({
        scrollPositionRestoration: 'enabled',
      })
    ),
    provideHttpClient(withInterceptors([errorInterceptor, jwtInterceptor, loadingInterceptor])),
    provideToastr({
      positionClass: 'toast-bottom-right',
    }),
    { provide: LOCALE_ID, useValue: 'pt-BR' },
    importProvidersFrom(NgxSpinnerModule),
    provideServiceWorker('ngsw-worker.js', {
      enabled: !isDevMode(),
      registrationStrategy: 'registerWhenStable:30000',
    }),
  ],
};
