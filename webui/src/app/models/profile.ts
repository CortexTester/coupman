import { City } from "./city";
import { Category } from "./category";
export enum week {
    Monday,
    Tuesday,
    Wenesday,
    Thursday,
    Friday,
    Saturday,
    Sunday
}
export class Social {
    constructor(
        public socialName?: string,
        public link?: string
    ) { }
}
export class Sechudle {
    constructor(
        public day?: week,
        public timeFrom?: string,
        public timeTo?: string,
        public customText?: string
    ) { }
}
export class Contact {
    constructor(
        public email?: string,
        public phone?: string,
        public webSite?: string,
        public person?: string,
        public address?: string,
        public city?: string,
    ) { }
}
export class Profile {
    constructor(
        public name?: string,
        public description?: string,
        public images?: string[],
        public videos?: string[],
        public hoursOfOperation?: Sechudle[],
        public contact?: Contact,
        public social?: Social[],
        public branding?: string,
        public categories?: Category[],
        public citysService?: City[]
    ) { }
}