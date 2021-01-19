import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core'
import { BrowserModule } from '@angular/platform-browser'

import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatStepperModule } from '@angular/material/stepper';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatChipsModule } from '@angular/material/chips';
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDividerModule } from '@angular/material/divider';
import { MatBadgeModule } from '@angular/material/badge';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatRadioModule } from '@angular/material/radio';
import { MatSliderModule } from '@angular/material/slider';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTabsModule } from '@angular/material/tabs';
import {MatToolbarModule} from '@angular/material/toolbar'; 
import {MatMenuModule} from '@angular/material/menu'; 
import {MatSidenavModule} from '@angular/material/sidenav'; 
import {MatTableModule} from '@angular/material/table';

import { FlexLayoutModule } from "@angular/flex-layout";

import { QuillModule } from 'ngx-quill'
import { MatQuill } from './mat-quill'

const MaterialComponents = [
  FormsModule,
  ReactiveFormsModule,
  MatCardModule,
  MatFormFieldModule,
  MatInputModule,
  MatAutocompleteModule,
  MatButtonModule,
  MatButtonToggleModule,
  MatBadgeModule,
  MatStepperModule,
  MatProgressBarModule,
  MatProgressSpinnerModule,
  MatRadioModule,
  MatSliderModule,
  MatSnackBarModule,
  MatIconModule,
  MatExpansionModule,
  MatChipsModule,
  MatBottomSheetModule,
  MatTooltipModule,
  MatDividerModule,
  MatDialogModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MatTabsModule,
  MatToolbarModule,
  MatMenuModule,
  MatSidenavModule,
  MatTableModule,
  
  QuillModule,
  FlexLayoutModule
]
@NgModule({
  declarations: [MatQuill],
  imports: [MaterialComponents],
  exports: [MaterialComponents, MatQuill],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class MaterialModule { }
