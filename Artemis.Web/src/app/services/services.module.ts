import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiServicesInterceptor } from './apiservices.interceptor';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { TavernService } from './tavern.service';



@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
  ],
  providers: [{
      provide: TavernService
    },
    {
    provide: HTTP_INTERCEPTORS,
    useClass: ApiServicesInterceptor,
    multi: true
  }],
})
export class ServicesModule { }
