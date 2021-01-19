import { Category } from "./category";
import { City } from "./city";

export class Coupon {
    constructor(
        public couponId?: number,
        public title?: string,
        public accountId?:number,
        public status?: string,
        public description?: string,
        public offerType?:string,
        public offerValue?:string,
        public isSomeConditionApply?:boolean,
        public isNotValidWithOtherPromotion?:boolean,
        public customCondition?:string,
        public startDate?: Date,
        public endDate?: Date,
        // public supplier?: Supplier,
        public couponCategories?: Category[],
        public couponCities?: City[],
        // public ratings?: Rating[] 
        ) { }
}