namespace NewsMVC.Areas.Admin.Services
{
    public interface IConnectRepository
    {
        Task<string> ConnectAsync(string URL);    
    }
}
