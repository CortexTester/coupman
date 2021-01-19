import { Component } from '@angular/core';
import { FieldType } from '@ngx-formly/core';

@Component({
  selector: 'formly-field-custom-input',
  template: `
    <quill-editor [styles]="editorStyle" [formControl]="formControl" [formlyAttributes]="field">
    </quill-editor>
  `,
})
export class FieldQuillType extends FieldType {
    editorStyle = {
        height: '200px'
      };
}