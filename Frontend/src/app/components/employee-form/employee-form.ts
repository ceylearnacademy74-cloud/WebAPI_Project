import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EmployeeService } from '../../services/employee';

@Component({
  selector: 'app-employee-form',  
  templateUrl: './employee-form.html',
  styleUrls: ['./employee-form.scss']
})
export class EmployeeFormComponent {
  form: FormGroup;

  constructor(private fb: FormBuilder, private employeeService: EmployeeService) {
    this.form = this.fb.group({
      name: ['', Validators.required],
      position: [''],
      department: [''],
      salary: [0, Validators.required]
    });
  }

  onSubmit(): void {
    if (this.form.valid) {
      this.employeeService.addEmployee(this.form.value).subscribe(() => {
        alert('Employee added successfully!');
        this.form.reset();
      });
    }
  }
}
