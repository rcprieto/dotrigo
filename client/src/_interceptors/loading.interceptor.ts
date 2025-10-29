import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { delay, finalize, identity } from 'rxjs';
import { BusyService } from '../app/_services/busy.service';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const busyService = inject(BusyService);
  busyService.busy();
  return next(req).pipe(
    //environment.production ? identity : delay(50),
    finalize(() => {
      busyService.idle();
    })
  );
};

//https://napster2210.github.io/ngx-spinner/   https://github.com/Napster2210/ngx-spinner?tab=readme-ov-file#readme
//npm install ngx-spinner --save
//ng g interceptor _interceptors/loading --skip-tests
//No angular.json styles
//"./node_modules/ngx-spinner/animations/square-jelly-box.css",
