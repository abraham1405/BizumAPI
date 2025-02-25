using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Xml;

[Route("api/[controller]")]
[ApiController]
public class XmlController : ControllerBase
{
    private readonly string _connectionString;

    public XmlController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    [HttpGet("execute")]
    public IActionResult ExecuteProcedure([FromQuery] Guid userId) // Usar Guid en lugar de string
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("sp_GetUserData", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userId", userId); // Asegúrate de usar el nombre correcto del parámetro

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
