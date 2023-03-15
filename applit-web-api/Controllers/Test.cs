// // using Docker.DotNet;
// // using Docker.DotNet.Models;
// // using System;
// // using System.IO;
// // using System.Threading.Tasks;
// // using Microsoft.AspNetCore.Mvc;
// // using System.Text; 

// // using System.IO.Pipelines;
// // namespace DockerAPI.Controllers
// // {
// //     [ApiController]
// //     [Route("[controller]")]
// //     public class PythonController : ControllerBase
// //     {
// //         private DockerClient client; bool? isTTY = false;
// //         ContainerAttachParameters containerAttachParameters = null;
// //         private string containerId;
// //         private string command = "python3 ./h.py"; // Replace with your Python command

// //         public PythonController()
// //         {
// //             this.client = new DockerClientConfiguration().CreateClient();
// //             this.containerId = "6b3a75ddb3edad7b47df5acf9ab6dc0885d9e489518273b5dbc40971b2de2226"; // Replace with your container ID
// //         }

// //         [HttpPost]
// //         public async Task<IActionResult> Post( )
// //         {
// //             // Create the command to be executed inside the container
// //              var dockerClient = new DockerClientConfiguration().CreateClient();

// //     var containerListResponse = await dockerClient.Containers.ListContainersAsync(new ContainersListParameters());
// //     var containerId = containerListResponse[0].ID; // assuming the first container in the list

// //     var createExecParams = new   CreateContainerParameters 
// //     {
// //         AttachStderr = true,
// //         AttachStdout = true,
// //         Cmd = new List<string> { "python", "-c", "print('Hello, world!')" }, // replace with your Python code
// //     };
// //     containerAttachParameters = new ContainerAttachParameters
// //             {
// //                 Stream = true,
// //                 Stderr = false,
// //                 Stdin = true,
// //                 Stdout = true,
// //             };
// //     var execCreationResponse = await dockerClient.Containers.CreateContainerAsync ( createExecParams,new CancellationToken());
// //     var execId = execCreationResponse.ID;
// //     var token = default(CancellationToken);
// //     var startExecParams = new ContainerExecStartParameters
// //     {
// //         Detach = false,
// //         Tty = false,
// //     }; 
   

// // // Obtain a System.IO.Stream object that can be used to read from and write to the stream
   

// //     // Now you can use 'ioStream' like any other System.IO.Stream object
// //     ioStream.WriteByte(65);
// // // Assume that 'dockerClient' is a valid DockerClient object

// // // Attach to a running container and obtain a MultiplexedStream object
// // MultiplexedStream attachResponse = await dockerClient.Containers.AttachContainerAsync(containerId, isTTY.GetValueOrDefault(), new ContainerAttachParameters { Stream = true, Stdin = true }, token);
// //  var stream = attachResponse.CreateStream();
// //   var ioStream = stream.AsStream();
// // var dockerStream = attachResponse.Stream;

// // // Create a new stream within the multiplexed stream
// // var stream = dockerStream.CreateStream();

// // // Obtain a System.IO.Stream object that can be used to read from and write to the stream
// // var ioStream = stream.AsStream();

// // // Now you can use 'ioStream' like any other System.IO.Stream object
// // ioStream.WriteByte(65);
// //     var output = new StringBuilder();
    
// //     var dockerStream =  await dockerClient.Containers.AttachContainerAsync(execId,isTTY.GetValueOrDefault()  ,containerAttachParameters, token );
// //     var dockerStre = dockerStream.Stream;
// //     var stream = dockerStream.CreateStream();

// //     Stream streamr = dockerStream.CopyFromAsync();
// //     using (var reader = new   StreamReader(streamr))
// //     {
// //         while (!reader.EndOfStream)
// //         {
// //             var line = await reader.ReadLineAsync();
// //             output.AppendLine(line);
// //         }
// //     }

// //     return Ok(output.ToString());
// //         }
// //     }
// // }


// using Microsoft.AspNetCore.Mvc;
// using Docker.DotNet;
// using System.IO;
// using System.Threading.Tasks;
// using System.Text;
// using Docker.DotNet.Models;

// namespace MyApi.Controllers
// {
//     [ApiController]
//     [Route("[controller]")]
//     public class PythonController : ControllerBase
//     {
//         [HttpPost]
       
//         public async Task<IActionResult> RunPythonScript()
//         {
//             var dockerClient = new IDockerClientConfiguration().CreateClient();
//     var containerId = "6b3a75ddb3edad7b47df5acf9ab6dc0885d9e489518273b5dbc40971b2de2226";
//     var command = new[] { "python", "./h.py" };
    
//     var execConfig = new CreateContainerParameters 
//     {
//         Cmd = command,
//         AttachStdout = true,
//         AttachStderr = true,
//     };

//     var execResult = await dockerClient.Containers.CreateContainerAsync(  execConfig,new CancellationToken ());
//     if (execResult == null)
//     {
//         return BadRequest("Failed to create exec command.");
//     }

//     var startConfig = new ContainerExecStartParameters
//     {
//         Tty = true,
//     };
//     var createResult = await dockerClient.Containers.IExecCreateContainerAsync(id, new ContainerExecCreateParameters
//         {
//             AttachStdout = true,
//             AttachStderr = true,
//             Cmd = new[] { "python", "-c", code }
//         });
//     using (var stream = await dockerClient.StartAndAttachAsync(createResult.ID, false))
//         {
//             // Create a new MemoryStream to hold the output
//             var outputStream = new MemoryStream();

//             // Copy the data from the MultiplexedStream to the MemoryStream
//             await stream.CopyToAsync(outputStream);

//             // Reset the position of the MemoryStream to 0
//             outputStream.Position = 0;

//             // Use the MemoryStream as a regular Stream
//             // For example, you can read the contents of the stream using a StreamReader:
//             using (var reader = new StreamReader(outputStream))
//             {
//                 var output = await reader.ReadToEndAsync();
//                 return Ok(output);
//             }
//         }
//     // using var stream = await dockerClient.Containers.IAttachContainerAsync(containerId, true,new ContainerAttachParameters () ,new CancellationToken ());
//     // using var reader = new StreamReader(stream);
    
//     // System.Console.WriteLine(stream);

//     return Ok();
//     }
//     }
// }
