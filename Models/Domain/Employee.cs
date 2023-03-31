using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models.Domain
{
   

    public class Employee
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
       
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }     
        [Required]

        public decimal Salary { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public int Department { get; set; }
    }
}
