using Microsoft.AspNetCore.SignalR;

namespace Topic3_RazorPages_DBF1.Hubs
{
    public class SignalRServer : Hub
    {
        public void RefreshData()
        {
            Clients.All.SendAsync("ReloadData");
        }
    }
}
