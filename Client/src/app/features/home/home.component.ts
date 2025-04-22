import { Component, inject, OnInit } from '@angular/core';
import { WeatherService } from '../../core/services/weather.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [
    CommonModule
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  currentTime: string = '';
  weather: string = "";
  weatherIcon: string = "";
  temperature: string = "";
  temperatureFeelsLike: string = "";
  humidity: string = "";
  private intervalId: any;
  private weatherService = inject(WeatherService);

  ngOnInit() {
    this.updateTime(); // initialize immediately
    this.intervalId = setInterval(() => {
      this.updateTime();
    }, 1000); // update every second
    this.getWeather();
  }

  ngOnDestroy() {
    if (this.intervalId) {
      clearInterval(this.intervalId); // clean up
    }
  }

  getGreeting(): string {
    const hour = new Date().getHours();
    if (hour < 12) {
      return 'Good Morning';
    } else if (hour < 18) {
      return 'Good Afternoon';
    } else {
      return 'Good Evening';
    }
  }

  private updateTime() {
    this.currentTime = new Date().toLocaleTimeString([], {
      hour: '2-digit',
      minute: '2-digit',
      second: '2-digit',
    });
  }

  private getWeather() {
    this.weatherService.getWeather().subscribe((data) => {
      this.weather = `${data.weather[0].main} - ${data.weather[0].description}`;
      this.weatherIcon = `https://openweathermap.org/img/wn/${data.weather[0].icon}@2x.png`;
      this.temperature = `${data.main.temp} °C`;
      this.temperatureFeelsLike = `${data.main.feels_like} °C`
      this.humidity = data.main.humidity
    });
  }
}
