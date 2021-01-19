import { Injectable } from '@angular/core';
import { CouponService } from '@app/services/coupon.service';
import { FormlyFieldConfig, FormlyFormOptions } from '@ngx-formly/core';
@Injectable({
    providedIn: 'root'
})
export class FormConfig {
    constructor(private couponService: CouponService) { }

    getCouponFormConfig() {
        return [
            {
                key: 'title',
                type: 'input',
                templateOptions: {
                    label: 'Title',
                    placeholder: 'Enter Title',
                    required: true,
                },
            },
            {
                key: 'description',
                type: 'quill',
                templateOptions: {
                    label: 'Description'
                },
            },
            {
                key: 'offerType',
                type: 'radio',
                templateOptions: {
                    label: 'Offer Type',
                    options: [
                        {
                            value: 'Percentage', label: 'Percentage'
                        },
                        {
                            value: 'Dollar', label: 'Dollar'
                        },
                        {
                            value: 'Custom', label: 'Custom'
                        }
                    ],
                },
            },
            {
                key: 'offerValue',
                type: 'input',
                templateOptions: {
                    label: 'Offer Value',
                    placeholder: 'Enter Offer Value',
                    required: true,
                }
            },
            {
                key: 'couponCategories', /* suppose this field as "my_select"  */
                type: 'ng-select',
                templateOptions: {
                    label: 'categories',
                    multiple: true,
                    options: [
                        { value: 1, label: "Air Conditioning And Cooling" },
                        { value: 2, label: "Appliance Repair and Service" },
                        { value: 3, label: "Asphalt" },
                        { value: 4, label: "Bathroom Renovation" },
                        { value: 5, label: "Cabinetry" },
                        { value: 6, label: "Carpentry and Carpenters" },
                        { value: 7, label: "Carpet Cleaning" },
                        { value: 8, label: "Carpet Installation And Repair" },
                        { value: 9, label:  "Construction Services" },
                        { value: 10, label:  "Deck Services" },
                        { value: 11, label: "Drywall" },
                        { value: 12, label:  "Ducts And Vents" },
                        { value: 13, label:  "Electrician" },
                        { value: 14, label:  "Exterior Painting" },
                        { value: 15, label:  "Fence" },
                        { value: 16, label:  "Fireplaces" },
                        { value: 17, label:  "Flooring" },
                        { value: 18, label: "Furnace And Central Hearing" },
                        { value: 19, label: "Garage Door" },
                        { value: 20, label:  "Gardening" },
                        { value: 21, label:  "General Contractor" },
                        { value: 22, label:  "Handyman" },
                        { value: 23, label:  "Hardwood Floorin" },
                        { value: 24, label:  "Home Security And Alarms" },
                        { value: 25, label:  "House Cleaning" },
                        { value: 26, label: "Insulation" },
                        { value: 27, label:  "Interior Paining" },
                        { value: 28, label:  "Junk Removal" },
                        { value: 29, label:  "Kitchen Re-modelling" },
                        { value: 30, label:  "Landscaping" },
                        { value: 31, label:  "Lawn Cleanup" },
                        { value: 32, label:  "Masonry Contractors" },
                        { value: 33, label:  "Milwork" },
                        { value: 34, label: "Plumbing" },
                        { value: 35, label:  "Roofing" },
                        { value: 36, label:  "Siding" },
                        { value: 37, label:  "Steam Cleaning" },
                        { value: 38, label:  "Swimming Pool Cleaning and Repair"},
                        { value: 39, label:  "Swimming Pool Installation" },
                        { value: 40, label:  "Water Heater Services" },
                        { value: 41, label: "Window Cleaning" },
                        { value: 42, label:  "Windows And Doors" }

                    ]
                }
            },
            // {
            //     fieldGroupClassName: 'display-flex',
            //     fieldGroup: [
            //         {
            //             className: 'flex-1',
            //             key: 'offerType',
            //             type: 'radio',
            //             templateOptions: {
            //                 label: 'Offer Type',
            //                 options: [
            //                     {
            //                         value: 'Percentage', label: 'Percentage'
            //                     },
            //                     {
            //                         value: 'Dollar', label: 'Dollar'
            //                     },
            //                     {
            //                         value: 'Custom', label: 'Custom'
            //                     }
            //                 ],
            //             },
            //         },
            //         {
            //             className: 'flex-4',
            //             key: 'offerValue',
            //             type: 'input',
            //             templateOptions: {
            //                 label: 'Offer Value',
            //                 placeholder: 'Enter Offer Value',
            //                 required: true,
            //             }
            //         },
            //     ]
            // },
            {
                fieldGroupClassName: 'display-flex',
                fieldGroup: [
                    {
                        className: 'flex-3',
                        key: 'isSomeConditionApply',
                        type: 'checkbox',
                        templateOptions: {
                            label: 'is some condition apply'
                        }
                    },
                    {
                        className: 'flex-3',
                        key: 'isNotValidWithOtherPromotion',
                        type: 'checkbox',
                        templateOptions: {
                            label: 'is not valid with other promotion'
                        }
                    }
                ]
            },
            {
                key: 'couponCities', /* suppose this field as "my_select"  */
                type: 'ng-select',
                templateOptions: {
                    label: 'Service Cities',
                    multiple: true,
                    options: [
                        // { value: 1, label: "Barrie" },
                        // { value: 2, label: "Belleville" },
                        // { value: 3, label: "Brampton" },
                        // { value: 4, label: "Brant" },
                        // { value: 5, label: "Brockville" },
                        // { value: 6, label: "Burlington" }
                        { value: 1, label: "Barrie" },
                        { value: 2, label: "Brampton" },
                        { value: 3, label: "Brant" },
                        { value: 4, label: "Brockville" },
                        { value: 5, label: "Burlington" },
                        { value: 6, label: "Belleville" },
                        { value: 7, label: "Cambridge" },
                        { value: 8, label: "Clarence - Rockland" },
                        { value: 9, label: "Cornwall" },
                        { value: 10, label: "Dryden[ON 5]" },
                        { value: 11, label: "Elliot Lake" },
                        { value: 12, label: "Greater Sudbury" },
                        { value: 13, label: "Guelph" },
                        { value: 14, label: "Haldimand County" },
                        { value: 15, label: "Hamilton" },
                        { value: 16, label: "Kawartha Lakes" },
                        { value: 17, label: "Kenora" },
                        { value: 18, label: "Kingston" },
                        { value: 19, label: "Kitchener" },
                        { value: 20, label: "London" },
                        { value: 21, label: "Markham" },
                        { value: 22, label: "Mississauga" },
                        { value: 23, label: "Niagara Falls" },
                        { value: 24, label: "Norfolk County" },
                        { value: 25, label: "North Bay" },
                        { value: 26, label: "Orillia" },
                        { value: 27, label: "Oshawa" },
                        { value: 28, label: "Ottawa" },
                        { value: 29, label: "Owen Sound" },
                        { value: 30, label: "Pembroke" },
                        { value: 31, label: "Peterborough" },
                        { value: 32, label: "Pickering" },
                        { value: 33, label: "Port Colborne" },
                        { value: 34, label: "Prince Edward County" },
                        { value: 35, label: "Quinte West" },
                        { value: 36, label: "Sarnia" },
                        { value: 37, label: "Sault Ste. Marie" },
                        { value: 38, label: "St. Catharines" },
                        { value: 39, label: "St. Thomas" },
                        { value: 40, label: "Stratford" },
                        { value: 41, label: "Temiskaming Shores" },
                        { value: 42, label: "Thorold" },
                        { value: 43, label: "Thunder Bay" },
                        { value: 44, label: "Timmins" },
                        { value: 45, label: "Toronto" },
                        { value: 46, label: "Vaughan" },
                        { value: 47, label: "Waterloo" },
                        { value: 48, label: "Welland" },
                        { value: 49, label: "Windsor" },
                        { value: 50, label: "Woodstock" },
                    ]
                }
            },
            {
                key: 'customCondition',
                type: 'input',
                templateOptions: {
                    label: 'Custom Condition',
                    placeholder: 'Enter Custom Condition',
                    required: true,
                },
            },
           
            {
                fieldGroupClassName: 'display-flex',
                fieldGroup: [
                    {
                        className: 'flex-3',
                        key: 'startDate',
                        type: 'datepicker',
                        templateOptions: {
                            label: 'Start Date',
                        },

                    },
                    {
                        className: 'flex-3',
                        key: 'endDate',
                        type: 'datepicker',
                        templateOptions: {
                            label: 'endDtae',
                        }

                    },
                ]
            }

        ]
    }
}

// export function getCouponFormConfig() {
//     return [
//         {
//             key: 'title',
//             type: 'input',
//             templateOptions: {
//                 label: 'Title',
//                 placeholder: 'Enter Title',
//                 required: true,
//             },
//         },
//         {
//             key: 'description',
//             type: 'quill',
//             templateOptions: {
//                 label: 'Description'
//             },
//         },
//         {
//             fieldGroupClassName: 'display-flex',
//             fieldGroup: [
//                 {
//                     className: 'flex-1',
//                     key: 'offerType',
//                     type: 'radio',
//                     templateOptions: {
//                         label: 'Offer Type',
//                         options: [
//                             {
//                                 value: 'Percentage', label: 'Percentage'
//                             },
//                             {
//                                 value: 'Dollar', label: 'Dollar'
//                             },
//                             {
//                                 value: 'Custom', label: 'Custom'
//                             }
//                         ],
//                     },
//                 },
//                 {
//                     className: 'flex-4',
//                     key: 'offerValue',
//                     type: 'input',
//                     templateOptions: {
//                         label: 'Offer Value',
//                         placeholder: 'Enter Offer Value',
//                         required: true,
//                     }
//                 },
//             ]
//         },

//         {
//             key: 'isSomeConditionApply',
//             type: 'checkbox',
//             templateOptions: {
//                 label: 'is some condition apply'
//             }
//         },
//         {
//             key: 'isNotValidWithOtherPromotion',
//             type: 'checkbox',
//             templateOptions: {
//                 label: 'is not valid with other promotion'
//             }
//         },
//         {
//             key: 'category', /* suppose this field as "my_select"  */
//             type: 'ng-select',
//             templateOptions: {
//                 multiple: true,
//                 placeholder: 'Select Option',
//                 options: [
//                     { label: 'Option 1', value: 1 },
//                     { label: 'Option 2', value: 2 },
//                     { label: 'Option 3', value: 3 },
//                     { label: 'Option 4', value: 4 },
//                 ]
//             }
//         },
//         {
//             fieldGroupClassName: 'display-flex',
//             fieldGroup: [
//                 {
//                     className: 'flex-3',
//                     key: 'startDate',
//                     type: 'datepicker',
//                     templateOptions: {
//                         label: 'Start Date',
//                     },

//                 },
//                 {
//                     className: 'flex-3',
//                     key: 'endDate',
//                     type: 'datepicker',
//                     templateOptions: {
//                         label: 'endDtae',
//                     }

//                 },
//             ]
//         }

//         // {
//         //     key: 'terms',
//         //     type: 'checkbox',
//         //     templateOptions: {
//         //         label: 'Accept terms',
//         //         description: 'Please accept our terms',
//         //         required: true,
//         //     },
//         // },
//         // {
//         //     key: 'terms_1',
//         //     type: 'toggle',
//         //     templateOptions: {
//         //         label: 'Accept terms',
//         //         description: 'Please accept our terms',
//         //         required: true,
//         //     },
//         // },
//         // {
//         //     key: 'description',
//         //     type: 'textarea',
//         //     templateOptions: {
//         //         label: 'Description',
//         //         placeholder: 'Enter description',
//         //     }
//         // },
//         // {
//         //     key: 'gender',
//         //     type: 'radio',
//         //     templateOptions: {
//         //         label: 'Gender',
//         //         placeholder: 'Placeholder',
//         //         description: 'Fill in your gender',
//         //         options: [
//         //             { value: 1, label: 'Male' },
//         //             { value: 2, label: 'Female' },
//         //             { value: 3, label: 'I don\'t want to share that' },
//         //         ],
//         //     },
//         // },
//     ]
// }