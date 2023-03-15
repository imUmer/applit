// using System;
// using System.IO;
// using System.Threading.Tasks;
// using applit_web_api.Service;
// using Docker.DotNet.Models;

// namespace applit_web_api.Services
// {
//     public class PythonService : IPythonService
//     {
//         private readonly IDockerService _dockerService;

//         public PythonService(IDockerService dockerService)
//         {
//             _dockerService = dockerService;
//         }

//         public Task<string> ExecuteScriptAsync(string containerId, string script)
//         {
//             throw new NotImplementedException();
//         }

//         public async Task<string> RunScriptAsync(string script, string arguments)
//         {
//             // Get Docker client
//             // var dockerClient = await _dockerService.GetDockerClientAsync();

//             // Create container
//             var createParameters = new CreateContainerParameters
//             {
//                 Image = "python:3.10.0-buster",
//                 Tty = true,
//                 Cmd = new[] { "python", "-c", script }
//             };

//             var container = await dockerClient.Containers.CreateContainerAsync(createParameters);

//             // Start container
//             var startParameters = new ContainerStartParameters();
//             await dockerClient.Containers.StartContainerAsync(container.ID, startParameters);

//             // Attach to container's output stream
//             // var stream = await dockerClient.Containers.AttachContainerAsync(
//             //     container.ID,
//             //     false,
//             //     new ContainerAttachParameters
//             //     {
//             //         Stdout = true,
//             //         Stderr = true,
//             //         Stream = true 
//             //     });

//             // // Read output stream to string
//             // using var reader = new StreamReader(stream, leaveOpen: true);
//             // var output = await reader.ReadToEndAsync();

//             using var stream = await dockerClient.Exec.StartAndAttachContainerExecAsync(container.ID, false);
//             using var memoryStream = new MemoryStream();
//             await stream.CopyFromAsync(memoryStream, CancellationToken.None);
//             memoryStream.Position = 0;
//             using var streamReader = new StreamReader(memoryStream);
//             var output = await streamReader.ReadToEndAsync();
//             return output;


//             // Remove container
//             // await dockerClient.Containers.RemoveContainerAsync(container.ID,
//             //     new ContainerRemoveParameters
//             //     {
//             //         Force = true
//             //     });
 
//         }
//     }
// }
