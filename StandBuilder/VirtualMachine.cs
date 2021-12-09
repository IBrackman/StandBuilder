using System;
using System.Collections.Generic;
using System.Linq;

namespace StandBuilder
{
    public class VirtualMachine
    {
        public Figure Figure { get; set; }
        public string Id { get; set; }
        public string OwnerFolderName { get; set; }
        public int FolderIndex { get; set; }
        public string StandFolderId { get; set; }
        public List<NetSetting> NetSettings { get; private set; }

        public List<string> Routes { get; private set; }

        public void InitNetSettings(NetworkModel model)
        {
            NetSettings = new List<NetSetting>();
            Routes = new List<string>();

            foreach (var setting in Figure.NetSettings)
            {
                var portGroupName = setting.PortGroupName.Replace("xXx", $"{OwnerFolderName}");

                var ip = setting.Ip.Replace("x", $"{FolderIndex}");

                var netmask = setting.Netmask;

                var gateway = setting.DefaultGateway.Replace("x", $"{FolderIndex}");

                var dns = setting.DnsServers.Replace("x", $"{FolderIndex}");

                var subnet = setting.Subnet.Replace("x", $"{FolderIndex}");

                var outer = setting.Outer;

                if (Equals(ip, "DHCP"))
                    foreach (var sett in from figure in model.AllFigures
                        let sett = figure.NetSettings.Find(x => Equals(x.PortGroupName, setting.PortGroupName))
                        where !Equals(sett, default) && !Equals(figure.Services, default) &&
                              figure.Services.ContainsKey("DHCP") && Equals(figure.Services["DHCP"]["IfaceIP"], sett.Ip)
                        select sett)
                    {
                        subnet = Parser.GetSubnet(sett.Ip.Replace("x", $"{FolderIndex}"), sett.Netmask);
                        break;
                    }
                else
                    subnet = Parser.GetSubnet(ip, netmask);

                NetSettings.Add(new NetSetting
                {
                    PortGroupName = portGroupName,
                    Ip = ip,
                    Netmask = netmask,
                    DefaultGateway = gateway,
                    DnsServers = dns,
                    Subnet = subnet,
                    Outer = outer
                });
            }

            foreach (var res in from route in model.Routes
                select route.Replace("x", $"{FolderIndex}").Split(new[] {">", "="}, StringSplitOptions.None)
                into set
                let res = $"{set[1]}>{set[2]}>{set[3]}>{set[4]}"
                where NetSettings.Exists(x => Equals(x.Subnet, set[0])) &&
                      !NetSettings.Exists(x => Equals(x.Subnet, set[1])) &&
                      !NetSettings.Exists(x => Equals(x.Ip, set[3])) && !Routes.Contains(res) &&
                      !NetSettings.Exists(x => Equals(x.DefaultGateway, set[3]))
                select res)
                Routes.Add(res);
        }
    }
}