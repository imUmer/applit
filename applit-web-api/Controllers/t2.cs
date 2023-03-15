using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using YourProjectNamespace.Controllers;

[ApiController]
[Route("[controller]")]
public class ContainController : ControllerBase
{
    private readonly DockerClient _dockerClient;
    private object buffer;

    public ContainController()
    {
        // Create a Docker client using the default configuration
    }

  [HttpPost]
        public async Task<IActionResult> ExecuteCode()
        {
            DockerClient dockerClient = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient();
            var localDirectoryPath = "D:/internship/applit/applit-web-api/Controllers";
            var createParams = new CreateContainerParameters
            {
                Image = "python",
                // Cmd = new List<string>() {"/bin/sh","-c", "D:/internship/applit/applit-web-api/Controllers/h.py"},
                Cmd = new List<string> { "python", "-u", $"{localDirectoryPath}/h.py" },
                Tty = true,
                AttachStdout = true,
                AttachStderr = true,
                // HostConfig = new HostConfig
                // {
                //     Binds = new List<string>
                //     {
                //         // Bind the local directory containing the Python script to /app inside the container
                //         $"{localDirectoryPath}:/app"
                //     },
                // },
            };
            var startParams = new ContainerStartParameters
            {
                DetachKeys = "ctrl-p,ctrl-q"
            };
            ContainerStatsParameters  sp = new ContainerStatsParameters();
            var response = await dockerClient.Containers.CreateContainerAsync(createParams);
            
            var res = await dockerClient.Containers.StartContainerAsync(response.ID, null);
            var resq = await dockerClient.Containers.GetContainerStatsAsync(response.ID, sp, CancellationToken.None);
             
            using var streamReader1 = new StreamReader(resq);
            var output1 = await streamReader1.ReadToEndAsync();
            System.Console.WriteLine(output1);
            // var execParams = new CreateContainerParameters 
            // {
            //     AttachStdout = true,
            //     AttachStderr = true,
            //     Cmd = new List<string>() { "python", "-c", "input_data = input(); print(input_data)" },
            //     Tty = false,
            //     WorkingDir = "/app"
            // };
            var conAttachPrams = new ContainerAttachParameters
            {
                Stdin = true,
                Stdout = true,
                Stream = true 
            };


            // var execCreateResponse = await dockerClient.Containers.CreateContainerAsync( createParams);
            var multiplexedStream = await dockerClient.Containers.AttachContainerAsync(response.ID, false, conAttachPrams);
         
            using var memoryStream = new MemoryStream();
            await multiplexedStream.CopyFromAsync(memoryStream, CancellationToken.None);
            using var streamReader = new StreamReader(memoryStream);
            var output = await streamReader.ReadToEndAsync();
            System.Console.WriteLine(output);
            // var buffer = new byte[1024];
            // var bytesRead;

            // while ((bytesRead = await multiplexedStream.ReadOutputAsync(buffer, 0, buffer.Length, CancellationToken.None)) > 0)
            // {
            //     output.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            // }

            return Ok(output);

            // var input = "Hello, World!";
            // var inputBytes = Encoding.UTF8.GetBytes(input);
            
            // using (var stream11 = await dockerClient.Containers.AttachContainerAsync(response.ID, false, conAttachPrams))
            // {
            //     var buffer = Encoding.UTF8.GetBytes(input);
            //     await stream11.WriteAsync(buffer, 0, buffer.Length, CancellationToken.None);
            //     var output = await multiplexedStream.ReadOutputAsync(buffer, 0, buffer.Length,CancellationToken.None);
            //     System.Console.WriteLine(output);
            //     return Ok(output);
            // }

            // var stream1 = await multiplexedStream.WriteAsync(inputBytes, 0,inputBytes.Length, CancellationToken.None);
            // await multiplexedStream.CopyFromAsync(stream);

            


            // var parameters = new ContainerAttachParameters
            // {
            //     Stream = true,
            //     Stdout = true,
            //     Stderr = true
            // };

            // var stream = await dockerClient.Containers.AttachContainerAsync(response.ID, false, parameters);
            // // var output = await ReadStreamAsync(stream);

            // await dockerClient.Containers.StopContainerAsync(response.ID, new ContainerStopParameters());
            // await dockerClient.Containers.RemoveContainerAsync(response.ID, new ContainerRemoveParameters());
            
        }
}
