import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  if(req.url.includes('/weather')) {
    return next(req)
  };
  const reqWithCredentials = req.clone({
    withCredentials: true
  })
  return next(reqWithCredentials);
};
