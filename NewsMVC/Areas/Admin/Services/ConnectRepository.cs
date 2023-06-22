namespace NewsMVC.Areas.Admin.Services
{
    public class ConnectRepository : IConnectRepository
    {
        public async Task<string> ConnectAsync(string URL)
        {
            HttpClient httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetAsync(URL);

                var Result = await response.Content.ReadAsStringAsync();
                return Result;
            }
            catch
            {
                return null;
            }
            // StringContent httpContent = new StringContent(Encoding.UTF8, "application/json");
           
        }
    }
}
