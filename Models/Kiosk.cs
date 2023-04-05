namespace KioskManager.Models
{
    public class Kiosk
    {
        public int Id { get; set; }
        public string PCId { get; set; } = string.Empty;
        public string? ActualIPAddress { get; set; }
        public bool isOnline { get; set; }
        public DateTime Registered { get; set; }
        public DateTime? LastOnline { get; set; }

        //Settings:
        public string SettingHostName { get; set; } = string.Empty;
        public string SettingHomePage { get; set; } = string.Empty;
        public string SettingKioskConfig { get; set; } = string.Empty;
        public string SettingScheduledAction { get; set; } = string.Empty;
        public string SettingRefreshPage { get; set; } = string.Empty;
        public string SettingRootPassword { get; set; } = string.Empty;
        public string SettingRtcWake { get; set; } = string.Empty;
        public string SettingScreenSettings { get; set; } = string.Empty;
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
