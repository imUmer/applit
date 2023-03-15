using Docker.DotNet;
using Docker.DotNet.Models;
using DockerAPI.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace applit_web_api
{
    public class DockerService1
    {
        private readonly DockerClient _dockerClient;

        public DockerService1()
        {
            _dockerClient = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient();
        }

        public async Task<string> CreateContainerAsync(string imageName, string containerName)
        {
            var createContainerParameters = new CreateContainerParameters
            {
                Image = imageName,
                Name = containerName
            };

            var response = await _dockerClient.Containers.CreateContainerAsync(createContainerParameters);

            return response.ID;
        }

        public async Task<bool> StartContainerAsync(string containerId)
        {
            var containerStartParameters = new ContainerStartParameters();

            return await _dockerClient.Containers.StartContainerAsync(containerId, containerStartParameters);
        }

        public async Task<string> RunCommandInContainerAsync(string containerId, string command)
        {
            var execCreateParameters = new CreateContainerParameters 
            {
                AttachStderr = true,
                AttachStdout = true,
                Cmd = new List<string> { "sh", "-c", command },
                Tty = true
            };

            var response = await _dockerClient.Containers.CreateContainerAsync(execCreateParameters);
            var startParams = new ContainerStartParameters
            {
                DetachKeys = "ctrl-p,ctrl-q"
            };

            await _dockerClient.Containers.StartContainerAsync(response.ID,startParams, CancellationToken.None);
            var containerDetails = await _dockerClient.Containers.InspectContainerAsync(response.ID); 
            // using (var reader = new StreamReader(containerDetails))
            // {
            //     return await reader.ReadToEndAsync();
            // }
            return "Done";
        }

        public async Task<bool> RemoveContainerAsync(string containerId)
        {
            var removeContainerParameters = new ContainerRemoveParameters
            {
                Force = true,
                RemoveVolumes = true
            };
            await _dockerClient.Containers.RemoveContainerAsync (containerId,removeContainerParameters);
            return true;
        }
    }
}
