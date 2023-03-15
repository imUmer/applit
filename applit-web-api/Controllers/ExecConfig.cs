internal class ExecConfig
{
    public bool AttachStdout { get; set; }
    public bool AttachStderr { get; set; }
    public string[] Cmd { get; set; }
}