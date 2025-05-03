import { HttpClient, HttpParams } from '@angular/common/http';
import { computed, inject, Injectable, signal } from '@angular/core';
import { map, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { User } from '../../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);
  currentUser = signal<User | null>(null);
  isAdmin = computed(() => {
    const roles = this.currentUser()?.roles;
    return Array.isArray(roles) ? roles.includes("Admin") : roles === "Admin";
  })

  login(values: any) {
    let params = new HttpParams();
    params = params.append("useCookies", true);
    return this.http.post<User>(this.baseUrl + "login", values, { params })
  }

  getUserInfo() {
    return this.http.get<User>(this.baseUrl + "account/user-info",).pipe(
      map(user => {
        this.currentUser.set(user);
        return user;
      })
    )
  }

  getAuthState() {
    return this.http.get<{ isAuthenticated: boolean }>(this.baseUrl + "account/auth-status");
  }
  
  logout() {
    return this.http.post(this.baseUrl + "account/logout", {});
  }

}
