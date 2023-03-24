using Docker.DotNet;
using Docker.DotNet.Models;

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

            var response = await _dockerClient.Exec.StartAndAttachContainerExecAsync(execId, false, cancellationToken);


            return response;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error occurred while starting the exec: {ex.Message}", ex);
        }
    }


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
        ContainerStatsParameters sp = new ContainerStatsParameters();
        ContainerLogsParameters lp = new ContainerLogsParameters();
        // Get the logs from the container  
        var logsStream = await _dockerClient.Containers.GetContainerLogsAsync(containerId, lp, CancellationToken.None);
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
        var execCreateParameters = command;


        var execResponse = await _dockerClient.Exec.ExecCreateContainerAsync(containerId, execCreateParameters);

        return execResponse.ID;
    }


    public async Task<MultiplexedStream> ContainerExecAttachAsync(string containerId, string pythonCode, bool tty = false, CancellationToken cancellationToken = default)
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
        var stream = await _dockerClient.Exec.StartAndAttachContainerExecAsync(containerId, true, cancellationToken);

        // Attach to the exec command output stream
        // var stream = await _dockerClient.Exec.StartContainerExecAsync(execId.ID,  cancellationToken);

        return stream;
    }




}