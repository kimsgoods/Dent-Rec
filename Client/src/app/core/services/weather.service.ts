import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {

  private http = inject(HttpClient)
  private apiKey = environment.weatherApiKey;
  private apiUrl = 'https://api.openweathermap.org/data/2.5/weather';

  getWeather(): Observable<any> {
    const zip = '6521';
    const country = 'PH';
    const url = `${this.apiUrl}?zip=${zip},${country}&units=metric&appid=${this.apiKey}`;
    return this.http.get(url);
  }
}
