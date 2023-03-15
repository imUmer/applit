// using Docker.DotNet;
// using Docker.DotNet.Models;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc; 

// namespace applit_web_api.Controllers
// {
//     [ApiController]
//     [Route("[controller]")]
//     public class DockerStatsController : ControllerBase
//     {
//          private readonly DockerClient _dockerClient;

//         public DockerStatsController()
//         {
//             // Initialize the Docker client
//             _dockerClient = new DockerClientConfiguration(new Uri("unix:///var/run/docker.sock"))
//                 .CreateClient();
//         }

//         [HttpPost]
//         public async Task<IActionResult> RunPythonCode([FromBody] string code)
//         {
//             // Get the ID of the running Python container
//             var containerId = await GetRunningPythonContainerId();

//             if (string.IsNullOrEmpty(containerId))
//             {
//                 return NotFound("No running Python container found.");
//             }

//             // Create a new command to run the Python code
//             var command = new[] { "python", "-c", code };

//             // Create the parameters for the command execution
//             var parameters = new ContainerExecCreateParameters
//             {
//                 AttachStdout = true,
//                 Cmd = command
//             };

//             // Start a new exec instance in the container
//             var execId = await _dockerClient.Containers.ExecCreateContainerAsync(containerId, parameters);

//             // Start the exec instance
//             var startParameters = new ContainerExecStartParameters
//             {
//                 Detach = false,
//                 Tty = false
//             };
//             var response = await _dockerClient.Containers.ExecStartAsync(execId.ID, startParameters);

//             // Read the output from the response stream
//             var output = await new StreamReader(response).ReadToEndAsync();

//             // Return the output as a string
//             return Ok(output);
//         }

//         private async Task<string> GetRunningPythonContainerId()
//         {
//             // Get all running containers
//             var containers = await _dockerClient.Containers.ListContainersAsync(
//                 new ContainersListParameters { Filters = new Dictionary<string, IDictionary<string, bool>> { { "status", new Dictionary<string, bool> { { "running", true } } } } });

//             // Look for a container with the "python" image name
//             foreach (var container in containers)
//             {
//                 if (container.Image == "python")
//                 {
//                     return container.ID;
//                 }
//             }

//             // No running Python container found
//             return null;
//         }
        
//     }
// }
