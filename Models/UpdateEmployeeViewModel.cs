﻿namespace EmployeeManagement.Models
{
    public class UpdateEmployeeViewModel
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }

    }
}
