using Docker.DotNet;
using Docker.DotNet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using applit_web_api.Services;



[ApiController]
[Route("api/[controller]")]
public class RunCodeController : ControllerBase
{
    public DockerClient dockerClient;
    public DockerService dockerService;
    
    public RunCodeController()
    {
        dockerClient = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient();
        dockerService = new DockerService("python", dockerClient);
    }


    [HttpPost]
    public async Task<IActionResult> RunPythonScript()
    {
        string p = System.IO.File.ReadAllText(@".\app\code.py");

        var startParams = new ContainerStartParameters
        {
            DetachKeys = "ctrl-p,ctrl-q"
        };

        var container = await GetRunningContainer();

        if (container != null)
        {
            await dockerService.StartContainerAsync(container, startParams);
            // var output = await _pythonService.ExecuteAsync(container.ID, Path.Combine(containerDirectoryPath, code.FilePath), code.Arguments);
            // // Create the command to execute the Python code inside the container
            var execParams = new ContainerExecCreateParameters
            {
                Cmd = new List<string> { "python", "-c", p },
                AttachStdout = true,
                AttachStderr = true,
            };
            System.Console.WriteLine($"This is exe : {container} ");
            // Create the exec command
            var createExecParams = new ContainerExecCreateParameters
            {
                AttachStderr = true,
                AttachStdout = true,
                Cmd = new List<string> { "python", "-c", p },
                Tty = true,
            };

            // Start the exec command
            var execId = await dockerService.ContainerExecCreateAsync(container, execParams);
            var s = await dockerService.ContainerExecStartAsync(container, execId, CancellationToken.None);


            // // Start the command and wait for it to finish
            var exec = await dockerService.ContainerExecCreateAsync(container, execParams);
            System.Console.WriteLine($"This is exe : {exec}");
            var st = await s.ReadOutputToEndAsync(CancellationToken.None);
            if (st.stderr != "")
            {
                System.Console.WriteLine($"This is exe : {st.stderr}");
                return Ok(st.stderr);
            }
            else if (st.stdout != "")
            {
                System.Console.WriteLine($"This is exe : {st.stdout}");
                return Ok(st.stdout);
            }
            else
            {
                return Ok("Write some code");
            }
            // // Read any output from the container logs
            // var logs = await dockerService.GetContainerLogs(container); 
            // var reader = new StreamReader(logs);
            // var containerOutput = reader.ReadToEnd();    
            // System.Console.WriteLine( $"This is st : {containerOutput}");

        }

        return Ok("No Data");
    }

    public async Task<string?> GetRunningContainer()
    {

        var containers = await dockerClient.Containers.ListContainersAsync(new ContainersListParameters { });
        var container1 = containers.FirstOrDefault(c => c.State == "running" && c.Image == "python");
        if (container1 != null)
        {
            System.Console.WriteLine($"This is exe : {container1.ID} ");
            return container1.ID;
        }
        return null;
    }

}

