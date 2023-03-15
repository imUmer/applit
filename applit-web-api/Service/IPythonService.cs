namespace applit_web_api.Services
{
    public interface IPythonService
    {
        Task<string> ExecuteScriptAsync(string containerId, string script);
    }
}