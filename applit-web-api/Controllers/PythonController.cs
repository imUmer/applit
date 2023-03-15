// using Microsoft.AspNetCore.Mvc;
// using System.Threading.Tasks;
// using applit_web_api.Services;
// using Docker.DotNet;
// using Docker.DotNet.Models;

// namespace applit_web_api.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class PythonController : ControllerBase
//     {
//         private readonly DockerService _dockerService;
//         private readonly PythonService _pythonService;

//         public PythonController(DockerService dockerService, PythonService pythonService)
//         {
//             _dockerService = dockerService;
//             _pythonService = pythonService;
//         }

//         [HttpPost("run")]
//         public async Task<IActionResult> RunPythonScript(string image, string cName,[FromBody] string script)
//         {
//             var containerId = await _dockerService.CreateContainerAsync(image, cName);
//             await _dockerService.StartContainerAsync(containerId);
//             var result = await _pythonService.RunScriptAsync(containerId, script); 
//             await _dockerService.RemoveContainerAsync(containerId);
//             return Ok(result);
//         }

//         [HttpPost("compile")]
//         public async Task<IActionResult> CompilePythonScript(string image, string cName,[FromBody] string script)
//         {
//             var containerId = await _dockerService.CreateContainerAsync(image, cName);
//             await _dockerService.StartContainerAsync(containerId);
//             var result = await _pythonService.RunScriptAsync(containerId, script); 
//             await _dockerService.RemoveContainerAsync(containerId);
//             return Ok(result);
//         }
//     }
// }
