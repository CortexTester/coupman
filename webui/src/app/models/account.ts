import { Role } from './role';

export class Account {
    id: string;
    userName: string;
    firstName: string;
    lastName: string;
    email: string;
    role: Role;
    jwtToken?: string;
}