using System.ComponentModel.DataAnnotations;

namespace KioskManager.Models
{
    public class Kiosk
    {
        public int Id { get; set; }
        [Display(Name = "PC Id")]
        public string PCId { get; set; } = string.Empty;
        [Display(Name = "IP Address")]
        public string? ActualIPAddress { get; set; }
        [Display(Name = "Status")]
        public bool isOnline { get; set; }
        [Display(Name = "Registered")]
        public DateTime Registered { get; set; }
        [Display(Name = "Last Online")]
        public DateTime? LastOnline { get; set; }

        //Settings:
        [Display(Name = "Host Name")]
        public string SettingHostName { get; set; } = string.Empty;
        [Display(Name = "Home Page")]
        [DataType(DataType.Url)]
        public string SettingHomePage { get; set; } = string.Empty;
        [Display(Name = "Config Url")]
        [DataType(DataType.Url)]
        public string SettingKioskConfig { get; set; } = string.Empty;
        [Display(Name = "Scheduled Actions")]
        public string SettingScheduledAction { get; set; } = string.Empty;
        [Display(Name = "Refresh Page Time")]
        [DataType(DataType.Duration)]
        public TimeSpan SettingRefreshPage { get; set; }
        [Display(Name = "Root Password")]
        [DataType(DataType.Password)]
        public string SettingRootPassword { get; set; } = string.Empty;
        [Display(Name = "RTC Wake")]
        public string SettingRtcWake { get; set; } = string.Empty;
        [Display(Name = "Screen Settings")]
        public string SettingScreenSettings { get; set; } = string.Empty;
        [Display(Name = "Time Zone")]
        public string SettingTimeZone { get; set; } = string.Empty;
        public string GetSettings()
        {
            return $"connection=wired\r\n" +
                    $"dhcp=yes\r\n" +
                    $"proxy=\r\n" +
                    $"browser=firefox\r\n" +
                    $"homepage={this.SettingHomePage}\r\n" +
                    $"hostname={this.SettingHostName}\r\n" +
                    $"allow_icmp_protocol=yes\r\n" +
                    $"disable_input_devices=yes\r\n" +
                    $"disable_navigation_bar=yes\r\n" +
                    $"kiosk_config={this.SettingKioskConfig}\r\n" +
                    $"scheduled_action={this.SettingScheduledAction}\r\n" +
                    $"refresh_webpage={this.SettingRefreshPage}\r\n" +
                    $"root_password={this.SettingRootPassword}\r\n" +
                    $"rtc_wake={this.SettingRtcWake}\r\n" +
                    $"screen_settings={this.SettingScreenSettings}\r\n" +
                    $"shutdown_menu=lock reboot restart-session shutdown sleep \r\n" +
                    $"timezone={this.SettingTimeZone}\r\n" +
                    $"wake_on_lan=yes\r\n" +
                    $"disable_zoom_controls=yes\r\n" +
                    $"additional_components=uefi.zip 08-ssh.xzm initrdpxe.xz";
        }
        public void Offline()
        {
            this.isOnline = false;
            this.LastOnline = DateTime.Now;
        }
        public void Online()
        {
            this.isOnline = true;
        }
    }
}
