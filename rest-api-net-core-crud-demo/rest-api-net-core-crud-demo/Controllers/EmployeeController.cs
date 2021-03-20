using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using rest_api_net_core_crud_demo.Models;
using rest_api_net_core_crud_demo.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace rest_api_net_core_crud_demo.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EmployeeController : ControllerBase
  {
    private readonly IConfiguration _conf;
    private readonly IWebHostEnvironment _env;

    public EmployeeController(IConfiguration conf, IWebHostEnvironment env)
    {
      _conf = conf;
      _env = env;
    }
    [HttpGet]
    public JsonResult Get()
    {
      string query = @"
                    select EmployeeId, EmployeeName, Department,
                    convert(varchar(10),DateOfJoining,120) as DateOfJoining
                    ,PhotoFileName
                    ,eMail
                    from dbo.Employee
                    ";
      SqlCommand myCmd = new SqlCommand(query);

      myCmd.CommandType = CommandType.Text;
      Sql mySqlHelper = new Sql(_conf);

      DataTable dt = mySqlHelper.cmdToDataTable(myCmd);

      return new JsonResult(dt);
    }


    [HttpPost]
    public JsonResult Post(Employee emp)
    {
      string query = "insert into dbo.Employee (EmployeeName,Department,DateOfJoining,PhotoFileName,eMail) values (@EmployeeName,@Department,@DateOfJoining,@PhotoFileName,@eMail)";
      SqlCommand myCmd = new SqlCommand(query);
      myCmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
      myCmd.Parameters.AddWithValue("@Department", emp.Department);
      myCmd.Parameters.AddWithValue("@DateOfJoining", emp.DateOfJoining);
      myCmd.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
      myCmd.Parameters.AddWithValue("@eMail", emp.eMail);

      myCmd.CommandType = CommandType.Text;
      Sql mySqlHelper = new Sql(_conf);
      mySqlHelper.xQuery(myCmd);

      return new JsonResult("Added Successfully");
    }


    [HttpPut]
    public JsonResult Put(Employee emp)
    {
      string query = @"
                    update dbo.Employee set 
                    EmployeeName = @EmployeeName
                    ,Department = @Department
                    ,DateOfJoining = @DateOfJoining
                    ,eMail = @eMail
                    where EmployeeId = @EmployeeId
                    ";
      SqlCommand myCmd = new SqlCommand(query);
      myCmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
      myCmd.Parameters.AddWithValue("@Department", emp.Department);
      myCmd.Parameters.AddWithValue("@DateOfJoining", emp.DateOfJoining);
      myCmd.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
      myCmd.Parameters.AddWithValue("@eMail", emp.eMail);

      myCmd.CommandType = CommandType.Text;
      Sql mySqlHelper = new Sql(_conf);
      mySqlHelper.xQuery(myCmd);

      return new JsonResult("Updated Successfully");
    }


    [HttpDelete("{id}")]
    public JsonResult Delete(int id)
    {
      string query = "delete from dbo.Employee where EmployeeId = @EmployeeId";
      SqlCommand myCmd = new SqlCommand(query);
      myCmd.Parameters.AddWithValue("@EmployeeId", id);

      myCmd.CommandType = CommandType.Text;
      Sql mySqlHelper = new Sql(_conf);
      mySqlHelper.xQuery(myCmd);

      return new JsonResult("Deleted Successfully");
    }


    [Route("SaveFile")]
    [HttpPost]
    public JsonResult SaveFile()
    {
      try
      {
        var httpRequest = Request.Form;
        var postedFile = httpRequest.Files[0];
        string filename = postedFile.FileName;
        var physicalPath = _env.ContentRootPath + "/pics/" + filename;
        physicalPath = Path.Combine(_env.ContentRootPath, "pics", filename);
        
        using (var stream = new FileStream(physicalPath, FileMode.Create))
        {
          postedFile.CopyTo(stream);
        }

        return new JsonResult(filename);
      }
      catch (Exception ex)
      {

        return new JsonResult("anonymous.png " + ex.Message);
      }
    }


    [Route("GetAllDepartmentNames")]
    public JsonResult GetAllDepartmentNames()
    {
      string query = @"select DepartmentName from dbo.Department";
      SqlCommand myCmd = new SqlCommand(query);

      myCmd.CommandType = CommandType.Text;
      Sql mySqlHelper = new Sql(_conf);

      DataTable dt = mySqlHelper.cmdToDataTable(myCmd);

      return new JsonResult(dt);
    }
  }
}
