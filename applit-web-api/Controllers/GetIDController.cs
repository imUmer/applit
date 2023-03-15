using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourProjectNamespace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetIDController : ControllerBase
    {
        private DockerClient client;

        
        [HttpPost]
        public async Task<IActionResult> GetContainers(string imageName)
        {
            client = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient();
            var containers = await client.Containers.ListContainersAsync(new ContainersListParameters { }); 
       
            foreach (var container in containers)
            { 
                if( imageName == "python")
                {
                    Console.WriteLine("Container ID: " + container.ID);
                    return Ok(container.ID);
                }
            } 
           
           
            return Ok("No Data");
        }
    }
}
