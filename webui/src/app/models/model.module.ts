import { NgModule } from "@angular/core";
import { Repository } from "./repository";
import { HttpClientModule } from "@angular/common/http";
@NgModule({
    providers: [Repository],
    imports: [HttpClientModule]
})
export class ModelModule { }