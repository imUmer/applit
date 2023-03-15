using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks; 
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class ContController : ControllerBase
{
    private readonly DockerClient _dockerClient;

    public ContController()
    {
        // Create a Docker client using the default configuration
        _dockerClient = new DockerClientConfiguration().CreateClient();
    }

    [HttpPost]
    
    // public async Task<IActionResult> ExecutePythonCode(string id, [FromBody] string code)
    public async Task<IActionResult> ExecutePythonCode()
    {
         
    using (var _dockerClient = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient())
    {
        // Create the exec command
        // var createExecCmd = new CreateContainerParameters 
        // {
        //     Cmd = new[] { "python", "-c" },
        //     AttachStdout = true,
        //     AttachStderr = true
        // };

         var createContainerParameters = new CreateContainerParameters
            {
                Image = "python:latest",
                Name = "mycontainer",
                Cmd = new List<string> { "echo", "Hello, world!" },
                Env = new List<string> { "VAR=value" },
                ExposedPorts = new Dictionary<string, EmptyStruct>
                {
                    { "80/tcp", default }
                },
                HostConfig = new HostConfig
                {
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        {
                            "80/tcp",
                            new List<PortBinding>
                            {
                                new PortBinding
                                {
                                    HostPort = "8080"
                                }
                            }
                        }
                    }
                }
            };

            var response = await _dockerClient.Containers
                .CreateContainerAsync(createContainerParameters);

                return Ok(response.ID);

        // Create the exec instance
        // var createExecResponse = await client.Containers.CreateContainerAsync(createExecCmd, new CancellationToken());

        // // Attach to the exec instance
        // var startExecParameters = new ContainerExecStartParameters
        // {
        //     Detach = false,
        //     Tty = false
        // };

        // var multiplexedStream = await client.Containers.StartContainerAsync(createExecResponse.ID, createExecCmd);
        
        // // Read the output from the multiplexed stream and return as a string
        // using (var reader = new StreamReader(multiplexedStream))
        // {
        //     return await reader.ReadToEndAsync();
        // }
    }
    }
}
