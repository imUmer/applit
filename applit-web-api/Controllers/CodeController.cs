using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> SaveCode([FromForm] string code)
    {
        try
        {
            // Set the path to the file where the code will be saved
            string filePath = @"D:\od\intern\applit\applit-web-api\app\code.py";
            // Write the code to the file 
            System.IO.File.WriteAllText(filePath, code);
          
          
            return Ok("Code saved successfully.");
        }
        catch (Exception ex)
        {
             return BadRequest(ex.Message);
        }
         
        return Ok();
    }
}

public class RunContainerRequest
{
    public string Name { get; set; }
    public string Image { get; set; }
    public string[] Cmd { get; set; }
}
