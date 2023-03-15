using Docker.DotNet;
using Docker.DotNet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace applit_web_api.Services
{
    public class DockerContainerService
    {
        private readonly DockerClient _dockerClient;

        public DockerContainerService()
        {
            // Connect to Docker API
            var dockerClientConfiguration = new DockerClientConfiguration();
            _dockerClient = dockerClientConfiguration.CreateClient();
        }

        public async Task<string> StartContainerAsync(string imageName, string containerName, int hostPort, int containerPort)
        {
            // Create a container
            var container = await _dockerClient.Containers.CreateContainerAsync(new CreateContainerParameters
            {
                Image = imageName,
                Name = containerName,
                HostConfig = new HostConfig
                {
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        { $"{containerPort}/tcp", new List<PortBinding> { new PortBinding { HostPort = $"{hostPort}" } } }
                    }
                }
            });

            // Start the container
            await _dockerClient.Containers.StartContainerAsync(container.ID, new ContainerStartParameters());

            return container.ID;
        }
    }
}
