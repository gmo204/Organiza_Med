import { EnvironmentProviders, makeEnvironmentProviders } from '@angular/core';
import { AuthService } from './services/auth.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { LocalStorageService } from './services/local-storage.service';
import { AuthInterceptor } from './services/auth.interceptor';
import { UsuarioService } from './services/usuario.service';

export const provideAuthentication = (): EnvironmentProviders => {
  return makeEnvironmentProviders([
    AuthService,
    LocalStorageService,
    UsuarioService,

    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
  ]);
};
