import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable, isDevMode  } from "@angular/core";
import { Observable } from "rxjs";

@Injectable()
export class ApiServicesInterceptor implements HttpInterceptor {

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (isDevMode()) {
      req = req.clone({ url: 'http://localhost:7008' + req.url });
    }
    return next.handle(req);
  }

}