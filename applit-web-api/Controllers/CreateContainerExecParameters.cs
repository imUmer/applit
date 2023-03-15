namespace DockerAPI.Controllers
{
    internal class CreateContainerExecParameters
    {
        public string[] Cmd { get; set; }
        public bool AttachStdout { get; set; }
        public bool AttachStderr { get; set; }
        public bool Tty { get; set; }
    }
}