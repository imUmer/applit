using Docker.DotNet;
using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace YourNamespace
{
    public class DockerClient
    {
        private DockerClientConfiguration _config;
        private IDockerClient _client;

        public DockerClient()
        {
            _config = new DockerClientConfiguration(new Uri("tcp://localhost:2375"));
            _client = _config.CreateClient();
        }

        public async Task<string> CreateContainerAsync(CreateContainerParameters parameters)
        {
            var response = await _client.Containers.CreateContainerAsync(parameters);
            return response.ID;
        }

        public async Task StartContainerAsync(string containerId, ContainerStartParameters parameters)
        {
            await _client.Containers.StartContainerAsync(containerId, parameters);
        }

        public async Task<Stream> GetContainerLogsAsync(string containerId)
        {
            var response = await _client.Containers.GetContainerLogsAsync(containerId, new ContainerLogsParameters { ShowStdout = true, ShowStderr = true });
            return response;
        }

        // public async Task<string> CopyToContainerAsync(string containerId, string sourcePath, string destinationPath)
        // {
            // var response = await _client.Filesystem.CopyToContainerAsync(containerId, sourcePath, destinationPath);
            // return response;
        // }

        // public async Task<Stream> AttachContainerAsync(string containerId, bool tty = false)
        // {
        //     var response = await _client.Containers.AttachContainerAsync(containerId, new ContainerAttachParameters { Stdin = true, Stdout = true, Stderr = true, Stream = true, Logs = true });
        //     return response;
        // }
    }
}
