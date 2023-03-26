using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace applit_web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RunController : ControllerBase
    {
        private DockerClient client;
        

        [HttpPost]
        public async Task<IActionResult> RunContainer([FromForm] string image, [FromForm] string name)
        {
            System.Console.WriteLine(image+1);
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
