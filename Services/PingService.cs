using System.Net.NetworkInformation;
namespace KioskManager.Services

{
    public static class PingService 
    { 

        public static async Task<PingReply> PingDevice(string iPAddress) {
            var pinger = new Ping();
            return pinger.Send(iPAddress);
        }
    }
}
