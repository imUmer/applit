// using applit_web_api.Service;
// using Docker.DotNet;
// using applit_web_api.Controllers;
// using Docker.DotNet.Models; 

// namespace applit_web_api.Service
// {
//     public class DockerService : IDockerService
//     {
//         private readonly IDockerClient _dockerClient;

//         public DockerService(IDockerClient dockerClient)
//         {
//             _dockerClient = dockerClient;
//         }

//         public async Task<ContainerListResponse> GetContainerById(string containerId)
//         {
//             var containers = await _dockerClient.Containers.ListContainersAsync(new ContainersListParameters());
// #pragma warning disable CS8603 // Possible null reference return.
//             return containers.FirstOrDefault(c => c.ID == containerId);
// #pragma warning restore CS8603 // Possible null reference return.
//         }

//         public async Task<string> RunCommandInContainer(ContainerListResponse container, string runtimecommand)
//         {
//             var execCreateParams = new ContainerExecCreateParameters
//             {
//                 AttachStdout = true,
//                 AttachStderr = true,
//                 Detach= true,
//                 Tty=true,
//                 Cmd = new List<string> { "sh", "-c", runtimecommand }
//             };

//             var execCreateResponse = await _dockerClient.Exec.ExecCreateContainerAsync(container.ID, execCreateParams);
//             var execId = execCreateResponse.ID;
            
            
//             using var stream = await _dockerClient.Exec.StartAndAttachContainerExecAsync(execId, false);
//             using var memoryStream = new MemoryStream();
//             await stream.CopyFromAsync(memoryStream, CancellationToken.None);
//             memoryStream.Position = 0;
//             using var streamReader = new StreamReader(memoryStream);
//             var output = await streamReader.ReadToEndAsync();
//             return output;
//         }
//     }
// }