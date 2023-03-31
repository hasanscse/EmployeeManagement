using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.Domain
{
    
    public class Department
    {
        [Key]
        public int DEPARTMENT_ID { get; set; }
        [DisplayName("Department Name")]
        [Required]     
        public string DEPARTMENT_NAME { get; set; }
      

    }
}
