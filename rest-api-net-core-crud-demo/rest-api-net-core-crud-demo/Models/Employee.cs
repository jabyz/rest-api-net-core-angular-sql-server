using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rest_api_net_core_crud_demo.Models
{
  public class Employee
  {
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public string Department { get; set; }
    public string DateOfJoining { get; set; }
    public string PhotoFileName { get; set; }
    public string eMail { get; set; }
  }
}
