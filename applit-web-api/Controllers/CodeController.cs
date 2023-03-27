using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading.Tasks;


[ApiController]
[Route("api/[controller]")]
public class CodeController : ControllerBase
{
    private readonly DockerClient _dockerClient;

    public CodeController()
    {
        // Create a Docker client using the default configuration
        _dockerClient = new DockerClientConfiguration().CreateClient();
    }

    [HttpPost]
    public IActionResult SaveCode([FromForm] string code)
    {
        try
        {
            // Set the path to the file where the code will be saved applit-web-api\app\code.py
            string filePath =  @".\app\code.py"; 
            // Write the code to the file 
            System.IO.File.WriteAllText(filePath, code);


            return Ok("Code saved successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
}

public class RunContainerRequest
{
    public string Name { get; set; }
    public string Image { get; set; }
    public string[] Cmd { get; set; }
}
