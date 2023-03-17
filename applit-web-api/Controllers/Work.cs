using Docker.DotNet;
using Docker.DotNet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Docker.DotNet;
using Docker.DotNet.Models;


[ApiController]
[Route("[controller]")]
public class MyController : ControllerBase
{
   public DockerClient dockerClient; 
    public DockerService dockerService;
    public MyController ()
    {
        dockerClient = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient();
        dockerService = new DockerService("python", dockerClient);
    }
    
    [HttpGet]
    public async Task<string?> GetRunningContainer()
    {
        
         var containers = await dockerClient.Containers.ListContainersAsync(new ContainersListParameters { }); 
            // var container1 = containers.FirstOrDefault(c => c.Image == "python");
            // var container1 = containers.FirstOrDefault(c => c.Names.Contains($"/priceless_volhard"));
            var container1 = containers.FirstOrDefault(c => c.State == "running" && c.Image == "python");
            if (container1 != null)
            {
                System.Console.WriteLine( $"This is exe : {container1.ID} "); 
                return container1.ID;
            }
            return null;
    }

     [HttpPost]
        public async Task<IActionResult> RunPythonScript()
        {
            string pythonCode = System.IO.File.ReadAllText(@"D:\od\intern\applit\applit-web-api\app\h.py");
 

            // Set up the parameters for the container
            // var createParams = new CreateContainerParameters
            // {
            //     Image = "python",
            //     Cmd = new[] { "tail", "-f", "/dev/null" },
            //     AttachStdout = true,
            //     AttachStderr = true,
            // };
            var startParams = new ContainerStartParameters
            {
                DetachKeys = "ctrl-p,ctrl-q"
            };

            // Create the container
            var container = await GetRunningContainer();
            // // Start the container
            
            
            if (container != null)
            {
                await dockerService.StartContainerAsync(container,startParams);
                // var output = await _pythonService.ExecuteAsync(container.ID, Path.Combine(containerDirectoryPath, code.FilePath), code.Arguments);
                // // Create the command to execute the Python code inside the container
                var execParams = new ContainerExecCreateParameters
                {
                    Cmd = new List<string> { "python", "-c", pythonCode },
                    AttachStdout = true,
                    AttachStderr = true,
                };
                System.Console.WriteLine( $"This is exe : {container} ");
                // Create the exec command
                var createExecParams = new ContainerExecCreateParameters
                {
                    AttachStderr = true,
                    AttachStdout = true,
                    Cmd = new List<string> { "python", "-c", pythonCode },
                    Tty = true,
                };

                // Start the exec command
                var execId = await dockerService.ContainerExecCreateAsync(container, execParams);
                var s = await dockerService.ContainerExecStartAsync(container , execId, CancellationToken.None);
                

                // // Start the command and wait for it to finish
                var exec = await dockerService.ContainerExecCreateAsync(container, execParams);
                System.Console.WriteLine( $"This is m stream : {s}");
                System.Console.WriteLine( $"This is exe : {container}");
                System.Console.WriteLine( $"This is exe : {exec}");
                var st = await s.ReadOutputToEndAsync(CancellationToken.None);

                // // Read any output from the container logs
                // var logs = await dockerService.GetContainerLogs(container); 
                // var reader = new StreamReader(logs);
                // var containerOutput = reader.ReadToEnd();    
                // System.Console.WriteLine( $"This is st : {containerOutput}");
                return Ok(st.stdout);
            }
            
            return Ok("No Data");
        }
    

}



public class DockerService 
{
    private readonly DockerClient _dockerClient;
    private readonly string _containerImage;

    public DockerService(string containerImage, DockerClient dockerClient)
    {
        _dockerClient = dockerClient;
        _containerImage = containerImage;
    }
    public async Task<string> CreateContainerAsync(CreateContainerParameters parameters)
{
    var response = await _dockerClient.Containers.CreateContainerAsync(parameters);
    return response.ID;
}
    public async Task StartContainerAsync(string containerId, ContainerStartParameters parameters)
{
    await _dockerClient.Containers.StartContainerAsync(containerId, parameters);
}

public async Task<MultiplexedStream> ContainerExecStartAsync(string containerId, string execId, CancellationToken cancellationToken)
{
    try
    {
        var startExecParams = new ContainerExecStartParameters
        {
            Detach = false,
            Tty = true
        };

        var response = await _dockerClient.Exec.StartAndAttachContainerExecAsync(execId,false, cancellationToken);
       

        return response;
    }
    catch (Exception ex)
    {
        throw new Exception($"Error occurred while starting the exec: {ex.Message}", ex);
    }
}

//     public async Task<ContainerExecResponse> ExecCreateAsync(string containerId, ContainerExecCreateParameters parameters)
//     {
//         return await _dockerClient.Containers.ExecCreateAsync(containerId, parameters);
//     }

//     public async Task<ContainerExecInspectResponse> ExecInspectAsync(string execId)
//     {
//         return await _dockerClient.Containers.ExecInspectAsync(execId);
//     }

//     public async Task<Stream> ExecStartAsync(string execId, ContainerExecStartParameters parameters)
//     {
//         return await _dockerClient.Containers.ExecStartAsync(execId, parameters);
//     }

//     public async Task<string> CreateContainer(string containerName, List<string> cmd, string localDirectoryPath)
//     {
//         // Get the container create parameters
//         var createContainerParameters = GetCreateContainerParameters(containerName, cmd, localDirectoryPath);

//         // Create the container
//         var response = await _dockerClient.Containers.CreateContainerAsync(createContainerParameters);

//         // Start the container
//         await _dockerClient.Containers.StartContainerAsync(response.ID, new ContainerStartParameters());

//         return response.ID;
//     }
//     public async Task<Stream> ContainerExecAttachAsync(string containerId, string command, CancellationToken cancellationToken)
// {
    // var execCreateResponse = await _dockerClient.Exec.CreateContainerExecAsync(containerId, new ContainerExecCreateParameters()
    // {
    //     AttachStdout = true,
    //     AttachStderr = true,
    //     Cmd = new List<string>() { command }
    // }, cancellationToken);

//     if (execCreateResponse == null)
//     {
//         throw new Exception($"Failed to create exec for container {containerId}");
//     }

//     var attachParameters = new ContainerExecAttachParameters
//     {
//         Detach = false,
//         Stream = true,
//         Stdin = false,
//         Stderr = true,
//         Stdout = true
//     };

//     return await _dockerClient.Exec.StartContainerExecAsync(execCreateResponse.ID, attachParameters, cancellationToken);
// }

// public async Task<ContainerExecInspectResponse> ContainerExecInspectAsync(string containerId, string execId, CancellationToken cancellationToken)
// {
//     return await _dockerClient.Exec.InspectContainerExecAsync(execId, cancellationToken);
// }

// public async Task ContainerExecStartAsync(string execId, CancellationToken cancellationToken)
// {
//     await _dockerClient.Exec.StartContainerExecAsync(execId, new ContainerExecStartParameters(), cancellationToken);
// }

// public async Task<string> ContainerLogsAsync(string containerId, ContainerLogsParameters parameters, CancellationToken cancellationToken)
// {
//     var response = await _dockerClient.Containers.GetContainerLogsAsync(containerId, parameters, cancellationToken);
//     return await response.ReadToEndAsync();
// }

    private CreateContainerParameters GetCreateContainerParameters(string containerName, List<string> cmd, string localDirectoryPath)
    {
        // Bind mount the local directory to the container
        var binds = new List<string> { $"{localDirectoryPath}:{localDirectoryPath}" };

        // Set the container create parameters
        var createContainerParameters = new CreateContainerParameters
        {
            Name = containerName,
            Image = _containerImage,
            Cmd = cmd,
            HostConfig = new HostConfig
            {
                Binds = binds
            }
        };

        return createContainerParameters;
    }

    public async Task<string> GetContainerLogs(string containerId)
    {
        ContainerStatsParameters  sp = new ContainerStatsParameters();
        ContainerLogsParameters  lp = new ContainerLogsParameters();
        // Get the logs from the container  
        var logsStream = await _dockerClient.Containers.GetContainerLogsAsync(containerId,lp , CancellationToken.None);
        // var logsStream = await _dockerClient.Containers.GetContainerLogsAsync.GetContainerStatsAsync(containerId,sp , CancellationToken.None);

        // Read the logs from the stream
        using (var reader = new StreamReader(logsStream))
        {
            var logs = await reader.ReadToEndAsync();
            return logs;
        }
    }

    public async Task RemoveContainer(string containerId)
    {
        // Stop and remove the container
        await _dockerClient.Containers.StopContainerAsync(containerId, new ContainerStopParameters());
        await _dockerClient.Containers.RemoveContainerAsync(containerId, new ContainerRemoveParameters());
    }

    public async Task<string> ContainerExecCreateAsync(string containerId, ContainerExecCreateParameters command)
{
    var execCreateParameters =   command;
    

    var execResponse = await _dockerClient.Exec.ExecCreateContainerAsync(containerId, execCreateParameters);
    
    return execResponse.ID;
}


public async Task<MultiplexedStream> ContainerExecAttachAsync(string containerId, string pythonCode,bool tty = false, CancellationToken cancellationToken = default)
{
    // Create the exec command parameters
    var execCommandParams = new ContainerExecCreateParameters
    {
        AttachStdin = true,
        AttachStdout = true,
        AttachStderr = true,
        Cmd = new List<string> { "python", "-c", pythonCode },
        Tty = tty
    };

    // Create the exec command in the container
    var execId = await _dockerClient.Exec.ExecCreateContainerAsync(containerId, execCommandParams, cancellationToken);

    // Start the exec command in the container
     var stream = await _dockerClient.Exec.StartAndAttachContainerExecAsync(containerId,true, cancellationToken);

    // Attach to the exec command output stream
    // var stream = await _dockerClient.Exec.StartContainerExecAsync(execId.ID,  cancellationToken);

    return stream;
}


//     public async Task<Stream> ContainerExecAttachAsync(string containerId, string command, CancellationToken cancellationToken)
// {
//     var execCreateResponse = await _dockerClient.Exec.CreateContainerExecAsync(containerId, new ContainerExecCreateParameters()
//     {
//         AttachStdout = true,
//         AttachStderr = true,
//         Cmd = new List<string>() { command }
//     }, cancellationToken);

//     if (execCreateResponse == null)
//     {
//         throw new Exception($"Failed to create exec for container {containerId}");
//     }

//     var attachParameters = new ContainerExecAttachParameters
//     {
//         Detach = false,
//         Stream = true,
//         Stdin = false,
//         Stderr = true,
//         Stdout = true
//     };

//     return await _dockerClient.Exec.StartContainerExecAsync(execCreateResponse.ID, attachParameters, cancellationToken);
// }

}