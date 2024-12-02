export interface RegistrarUsuarioViewModel {
  userName: string;
  email: string;
  password: string;
}

export interface AutenticarUsuarioViewModel {
  userName: string;
  password: string;
}

export interface TokenViewModel {
  chave: string;
  dataExpiracao: Date;

  usuario: UsuarioTokenViewModel;
}

export interface UsuarioTokenViewModel {
  id: string;
  userName: string;
  email: string;
}
