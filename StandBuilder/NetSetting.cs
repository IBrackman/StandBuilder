namespace StandBuilder
{
    public class NetSetting
    {
        public string PortGroupName { get; set; }
        public string Ip { get; set; }
        public string Netmask { get; set; }
        public string DefaultGateway { get; set; }
        public string Subnet { get; set; }
        public string Mac { get; set; }
        public string DnsServers { get; set; }
        public bool Outer { get; set; }
    }
}