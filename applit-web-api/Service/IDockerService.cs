using Docker.DotNet.Models;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;

namespace applit_web_api.Service
{
    public interface IDockerService
    {
        Task<ContainerListResponse> GetContainerById(string containerId);
        Task<string> RunCommandInContainer(ContainerListResponse container, string runtimeCommand);
    }
}