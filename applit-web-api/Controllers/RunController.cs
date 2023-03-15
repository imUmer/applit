// using Docker.DotNet;
// using Docker.DotNet.Models;
// using System.Threading.Tasks;
// using System.Web.Http;
// namespace applit_web_api.Controllers;
 
// public class DockerController : ControllerBase
// {
//     private DockerClient _dockerClient;

//     public DockerController()
//     {
//         _dockerClient = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient();
//     }

//     [HttpPost]
//     public async Task<IHttpActionResult> StartContainer(string image)
//     {
//         var createParams = new CreateContainerParameters
//         {
//             Image = image
//         };

//         var container = await _dockerClient.Containers.CreateContainerAsync(createParams);

//         var startParams = new ContainerStartParameters
//         {
//             Id = container.ID
//         };

//         await _dockerClient.Containers.StartContainerAsync(startParams);

//         return Ok();
//     } 
// }
// using Microsoft.AspNetCore.Mvc;
// using System.Threading.Tasks;
// using applit_web_api.Services;
// namespace applit_web_api.Controllers;
//     [ApiController]
//     [Route("api/[controller]")]
//     public class DockerController : ControllerBase
//     {
//         private readonly DockerContainerService _dockerContainerService;

//         public DockerController(DockerContainerService dockerContainerService)
//         {
//             _dockerContainerService = dockerContainerService;
//         }

//         [HttpPost("startcontainer")]
//         public async Task<IActionResult> StartContainer([FromBody] DockerContainerRequest request)
//         {
//             var containerId = await _dockerContainerService.StartContainerAsync(request.ImageName, request.ContainerName, request.HostPort, request.ContainerPort);

//             return Ok(containerId);
//         }
//     }

//     public class DockerContainerRequest
//     {
//         public string ImageName { get; set; }
//         public string ContainerName { get; set; }
//         public int HostPort { get; set; }
//         public int ContainerPort { get; set; }
//     }

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace applit_web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RunController : ControllerBase
    {
        private DockerClient client;
        

        [HttpPost]
        public async Task<IActionResult> RunContainer(string image, string name)
        {
            client = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient();
            var response = await client.Containers.CreateContainerAsync(new CreateContainerParameters
            {
                Image = image,
                Name = name,
                Tty = true
            });

            await client.Containers.StartContainerAsync(response.ID, null);
            
            return Ok();
        }
    
    }
}
