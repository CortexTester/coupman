import { NgModule } from "@angular/core";
import { Repository } from "./repository";
import { HttpClientModule } from "@angular/common/http";
import { UserService } from './user.service';
@NgModule({
    providers: [Repository, UserService],
    imports: [HttpClientModule]
})
export class ModelModule { }