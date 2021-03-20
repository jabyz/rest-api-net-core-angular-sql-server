using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using rest_api_net_core_crud_demo.Models;
using rest_api_net_core_crud_demo.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace rest_api_net_core_crud_demo.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DepartmentController : ControllerBase
  {
    private readonly IConfiguration _conf;

    public DepartmentController(IConfiguration conf)
    {
      _conf = conf;
    }
    [HttpGet]
    public JsonResult Get()
    {
      string query = "select * from dbo.Department";
      try
      {
        SqlCommand myCmd = new SqlCommand(query);

        myCmd.CommandType = CommandType.Text;
        Sql mySqlHelper = new Sql(_conf);

        DataTable dt = mySqlHelper.cmdToDataTable(myCmd);

        //return new JsonResult(new { a = query, b = _conf.GetConnectionString("localHost"), c = "holamundo" });
        return new JsonResult(dt);
      }
      catch (Exception ex)
      {

        return new JsonResult(new { a = query, b = _conf.GetConnectionString("localHost"), c = ex.Message});
      }
      
    }

    [HttpPost]
    public JsonResult Post(Department dep)
    {
      string query = "insert into Department values(@DepartmentName)";
      SqlCommand myCmd = new SqlCommand(query);
      myCmd.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);

      myCmd.CommandType = CommandType.Text;
      Sql mySqlHelper = new Sql(_conf);
      mySqlHelper.xQuery(myCmd);

      return new JsonResult("Added Successfully");
    }

    [HttpPut]
    public JsonResult Put(Department dep)
    {
      string query = @"update Department set DepartmentName = @DepartmentName where DepartmentId = @DepartmentId";

      SqlCommand myCmd = new SqlCommand(query);
      myCmd.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
      myCmd.Parameters.AddWithValue("@DepartmentId", dep.DepartmentId);

      myCmd.CommandType = CommandType.Text;
      Sql mySqlHelper = new Sql(_conf);
      mySqlHelper.xQuery(myCmd);

      return new JsonResult("Updated Successfully");
    }

    [HttpDelete("{id}")]
    public JsonResult Delete(int id)
    {
      string query = "delete Department where DepartmentId = @DepartmentId";
      SqlCommand myCmd = new SqlCommand(query);
      myCmd.Parameters.AddWithValue("@DepartmentId", id);

      myCmd.CommandType = CommandType.Text;
      Sql mySqlHelper = new Sql(_conf);
      mySqlHelper.xQuery(myCmd);

      return new JsonResult("Deleted Successfully");
    }
  }
}
