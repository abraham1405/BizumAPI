using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Xml;
using System.Xml.Linq;

[Route("api/[controller]")]
[ApiController]
public class XmlController : ControllerBase
{
    private readonly string _connectionString;

    public XmlController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    [HttpGet("execute2")]
    public IActionResult ExecuteProcedure([FromQuery] string userId) 
    {
        
        try
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("sp_GetUserData", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userId", userId);
                    //command.Parameters.AddWithValue("@p1", p1);
                    using (var reader = command.ExecuteXmlReader())
                    {
                        if (reader != null && reader.Read())
                        {
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.Load(reader);
                            return Content(xmlDoc.OuterXml, "application/xml");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }

        return BadRequest("No XML data returned.");
    }
}
