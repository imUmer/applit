// using Docker.DotNet.Models;
// using Docker.DotNet;
// using Microsoft.AspNetCore.Mvc;
// using System.ComponentModel;  
// using System.Text;
// using System;
// using applit_web_api.Service;


// namespace applit_web_api
// {
//     [ApiController]
//     [Route("[controller]")]
//     public class ContainersController : ControllerBase
//     {
//         private readonly IDockerClient _dockerClient;
//         //private readonly DockerSetup _dockerSetup;
//         private readonly IDockerService _dockerService;

//         public ContainersController(IDockerClient DockerClient, IDockerService DockerService)
//         {
//             _dockerClient = DockerClient;
//             _dockerService = DockerService;
//         }
//         //[HttpGet]
//         [HttpGet("name")]
//         public async Task<ActionResult<IEnumerable<string>>> Get()
//         {
//             var containers = await _dockerClient.Containers.ListContainersAsync(new ContainersListParameters());
//             return Ok(containers.Select(x => x.Names));
//         }
//         //[HttpPost]
//         [HttpPost("post")]
//         public async Task<IActionResult> StartContainer([FromBody] ContainerStartRequest request)
//         {
//             if (request == null || string.IsNullOrEmpty(request.Image))
//             {
//                 return BadRequest("Invalid request payload");
//             }

//             var createParams = new CreateContainerParameters
//             {
//                 Image = request.Image,
//                 Name = request.Name,
//                 Cmd = new List<string> { "/bin/sh","-c", "C:/Users/arbaz/Desktop/myfile.py" },
//                 HostConfig = new HostConfig
//                 {
//                     PortBindings = new Dictionary<string, IList<PortBinding>>
//                     {
//                         {
//                             "8080/tcp",
//                             new List<PortBinding>
//                             {
//                                 new PortBinding
//                                 {
//                                     HostPort = "8080"
//                                 }
//                             }
//                         }
//                     },
//                     Binds = new List<string>
//                     {
//                         "/path/on/host:/path/on/container"
//                     }
//                 }
//             };

//             var container = await _dockerClient.Containers.CreateContainerAsync(createParams);

//             if (container == null)
//             {
//                 return BadRequest("Error creating container");
//             }

//             var startParams = new ContainerStartParameters();

//             await _dockerClient.Containers.StartContainerAsync(container.ID, startParams);
//             var containerDetails = await _dockerClient.Containers.InspectContainerAsync(container.ID);
//             while (containerDetails.State.Status != "running")
//             {
//                 Thread.Sleep(9999);
//                 containerDetails = await _dockerClient.Containers.InspectContainerAsync(container.ID);
//             }

//             return Ok($"Container with ID {container.ID} started successfully");
            
//         }
//         [HttpPost("{id}/execute")]
//         public async Task<IActionResult> ExecuteCode(string id, [FromBody] ExecuteCodeRequest request, [FromQuery] string language)
//         {
//             var container = await _dockerService.GetContainerById(id);
//             if (container == null)
//             {
//                 return NotFound();
//             }

//             // Check if the file exists
//             if (!System.IO.File.Exists(request.FilePath))
//             {
//                 return BadRequest($"File {request.FilePath} not found");
//             }

//             string runtimeCommand;
//             switch (language.ToLower())
//             {
//                 case "python":
//                     runtimeCommand = $"python {request.FilePath}";
//                     break;
//                 case "node":
//                     runtimeCommand = $"node {request.FilePath} && tail -f /dev/null\"";
//                     break;
//                 // Add more cases for other languages
//                 default:
//                     return BadRequest($"Unsupported language: {language}");
//             }

//             // Run the code in the container
//             var output = await _dockerService.RunCommandInContainer(container, runtimeCommand);
//             Console.WriteLine(output);
//             return Ok(output);
//         }

//     }
// }
