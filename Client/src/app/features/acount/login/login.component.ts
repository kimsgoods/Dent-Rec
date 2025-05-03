import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatCard } from '@angular/material/card';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { AccountService } from '../../../core/services/account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { SnackbarService } from '../../../core/services/snackbar.service';

@Component({
  selector: 'app-login',
  imports: [
    ReactiveFormsModule,
    MatCard,
    MatFormField,
    MatInput,
    MatLabel,
    MatButton
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  private fb = inject(FormBuilder);
  private accountService = inject(AccountService);
  private router = inject(Router);
  private activatedRoute = inject(ActivatedRoute);
  private snackbar = inject(SnackbarService);

  returnUrl = "/";

  constructor() {
    const url = this.activatedRoute.snapshot.queryParams["returnUrl"];
    if (url) this.returnUrl = url;
  }

  loginForm = this.fb.group({
    email: [""],
    password: [""]
  });

  onSubmit() {
    this.accountService.login(this.loginForm.value).subscribe({
      next: () => {
        this.snackbar.success("Login successful");
        this.accountService.getUserInfo().subscribe();
        this.router.navigateByUrl(this.returnUrl);
      },
      error: (err) => {
        if (err.status === 401) {
          this.snackbar.error("Invalid email or password");
        } else {
          this.snackbar.error("An error occurred during login");
        }
      }
    })
  }
}
