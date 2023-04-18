using System.Net.NetworkInformation;
namespace KioskManager.Services

{
    public static class PingService 
    { 

        public static async Task<PingReply> PingDevice(string iPAddress) {
            try
            {
                var pinger = new Ping();
                return pinger.Send(iPAddress);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }
    }
}
