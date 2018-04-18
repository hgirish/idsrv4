import { Injectable } from '@angular/core';
import { UserManagerSettings } from 'oidc-client';

@Injectable()
export class AuthService {
  constructor() {}
}

export function getClientSettings(): UserManagerSettings {
  return {};
}
