using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


[ApiController]
[Route("[controller]")]
public class CController : ControllerBase
{
    private readonly DockerClient _dockerClient;

    public CController()
    {
        // Create a Docker client using the default configuration
        _dockerClient = new DockerClientConfiguration().CreateClient();
    }

    // [HttpPost("{id}/execute")]
    // public async Task<IActionResult> ExecutePythonCode(string id, [FromBody] string code)
    // {
    //     var createResult = await _dockerClient.Containers.CreateContainerAsync ( new CreateContainerParameters 
    //     {
    //         AttachStdout = true,
    //         AttachStderr = true,
    //         Cmd = new[] { "python", "-c", code }
    //     },new CancellationToken());
    //     byte[]  buffer;
    //     // Start the execution instance and attach to its streams
    //     using (var stream = await _dockerClient.Exec.StartAndAttachContainerExecAsync (createResult.ID, false))
    //     {
    //         // Read the output from the stream
    //         var output = await stream.ReadOutputAsync (buffer);

    //         // Return the output as the response body
    //         return Ok(output);
    //     }
    // }
    [HttpPost]
    public async Task<IActionResult> RunContainer( )
    {
        // Create the container configuration
        // var containerConfig = new Config
        // {
        //     Image = request.Image,
        //     Cmd = request.Cmd,
        // };

        // // Create the container host configuration
        var hostConfig = new HostConfig
        {
            PublishAllPorts = true,
        };

        // // Create the container create parameters
        var createParams = new CreateContainerParameters
        {
            HostConfig = hostConfig,
        };
          
        // // Create the container
        // var response = await _dockerClient.Containers.CreateContainerAsync(createParams);
        var containerId = "705bb3ab4adf25677a2fee570c50d3a7b2c3bc0f51c1b9ce58d2c23c99f1ea1c";
        // Start the container
        await _dockerClient.Containers.StartContainerAsync(containerId, null);

        // Create the command to run inside the container
        var execConfig = new ExecConfig
        {
            AttachStdout = true,
            AttachStderr = true,
            Cmd = new[] { "python", "./h.py" },
        };

        // Start the command
        var execCreateResponse = await _dockerClient.Containers.CreateContainerAsync (
            createParams, new CancellationToken());
    
        var res =  await _dockerClient.Containers.StartContainerAsync (execCreateResponse.ID, null);

        return Ok(res);
    }
}

public class RunContainerRequest
{
    public string Name { get; set; }
    public string Image { get; set; }
    public string[] Cmd { get; set; }
}
