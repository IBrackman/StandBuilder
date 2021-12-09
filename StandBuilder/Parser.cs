using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StandBuilder
{
    public static class Parser
    {
        #region Public methods

        public static void Parse(string fileName, ICollection<Figure> allFigures)
        {
            try
            {
                using (var sr = new StreamReader(fileName))
                {
                    string line;

                    while (!Equals(line = sr.ReadLine(), null))
                        if (line.Contains("## Figures & Connectors Section:"))
                            break;

                    Figure creatingFigure = null;

                    var isReadingMemo = false;
                    var memoExists = false;

                    var memoText = string.Empty;

                    while (!Equals(line = sr.ReadLine(), null))
                    {
                        if (line.Contains("##")) break;

                        ParseFigure(line, ref creatingFigure, allFigures, ref isReadingMemo, ref memoText, ref memoExists);

                        if (!isReadingMemo && !Equals(memoText, string.Empty) && !Equals(creatingFigure, null))
                            ParseMemo(creatingFigure, ref memoText);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"The file could not be read:");

                Console.WriteLine(e.Message);
            }
        }

        public static void ParseFolderProperties(string folderPropertiesPath, NetworkModel model, List<VirtualMachine> vmList)
        {
            try
            {
                using (var sr = new StreamReader(folderPropertiesPath))
                {
                    string line;

                    var index = 0;

                    while (!Equals(line = sr.ReadLine(), null))
                    {
                        var set = line.Split(new[] { @"\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                        vmList.AddRange(model.AllFigures.Select(figure => new VirtualMachine
                        {
                            Figure = figure, OwnerFolderName = set[0], FolderIndex = index, StandFolderId = set[1]
                        }));

                        index++;
                    }
                }

                File.Delete(folderPropertiesPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(@"The file could not be read:");

                Console.WriteLine(e.Message);
            }
        }

        public static void ParseServerProperties(string serverPropertiesPath, ICollection<string> datacenters, ICollection<string> datastores,
            ICollection<string> hosts)
        {
            try
            {
                using (var sr = new StreamReader(serverPropertiesPath))
                {
                    string line;

                    while (!Equals(line = sr.ReadLine(), null))
                    {
                        var set = line.Split(new[] { @"\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                        switch (set[0])
                        {
                            case "Datacenter":
                                datacenters.Add(set[1]);
                                break;
                            case "Datastore":
                                datastores.Add(set[1]);
                                break;
                            case "Host":
                                hosts.Add(set[1]);
                                break;
                        }
                    }
                }

                File.Delete(serverPropertiesPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(@"The file could not be read:");

                Console.WriteLine(e.Message);
            }
        }

        public static void ParseVmId(string vmIdsPath, List<VirtualMachine> vmList)
        {
            try
            {
                using (var sr = new StreamReader(vmIdsPath))
                {
                    string line;

                    while (!Equals(line = sr.ReadLine(), null))
                    {
                        var set = line.Split(new[] {@"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
                        var vm = vmList.Find(x => Equals(x.StandFolderId, set[0]) && Equals(x.Figure.Name, set[1]));
                        vm.Id = set[2];
                    }
                }

                File.Delete(vmIdsPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(@"The file could not be read:");

                Console.WriteLine(e.Message);
            }
        }

        public static void ParseMac(string macPath, List<VirtualMachine> vmList)
        {
            try
            {
                using (var sr = new StreamReader(macPath))
                {
                    string line;

                    while (!Equals(line = sr.ReadLine(), null))
                    {
                        var set = line.Split(new[] { @"\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        var vm = vmList.Find(x => Equals(x.Id, set[0]));
                        var setting = vm.NetSettings.Find(x => Equals(x.PortGroupName, set[1]));
                        setting.Mac = set[2];
                    }
                }

                File.Delete(macPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(@"The file could not be read:");

                Console.WriteLine(e.Message);
            }
        }

        public static string GetSubnet(string ip, string mask)
        {
            var ipSet = ip.Split(new[] { "." }, StringSplitOptions.None);
            var maskSet = mask.Split(new[] { "." }, StringSplitOptions.None).ToList();

            for (var i = 0; i < 4; i++)
                if (Equals(maskSet[i], "0"))
                    ipSet[i] = "0";

            var okt = maskSet.Find(x => !Equals(x, "255") && !Equals(x, "0"));
            var index = maskSet.IndexOf(okt);

            if (string.IsNullOrEmpty(okt)) return $"{ipSet[0]}.{ipSet[1]}.{ipSet[2]}.{ipSet[3]}";

            var sum = 128;
            var hostBits = 7;
            var hosts = 128;

            while (!Equals(sum, Convert.ToInt32(okt)))
            {
                hostBits--;
                hosts = Convert.ToInt32(Math.Pow(2, hostBits));
                sum += hosts;
            }

            ipSet[index] = ((Convert.ToInt32(okt) / hosts) * hosts).ToString();

            return $"{ipSet[0]}.{ipSet[1]}.{ipSet[2]}.{ipSet[3]}";
        }

        public static void ParseRoutes(string routesPath, List<string> routes)
        {
            try
            {
                using (var sr = new StreamReader(routesPath))
                {
                    string line;

                    while (!Equals(line = sr.ReadLine(), null)) routes.Add(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"The file could not be read:");

                Console.WriteLine(e.Message);
            }
        }
        
        #endregion

        #region Private methods

        private static void ParseFigure(string line, ref Figure creatingFigure, ICollection<Figure> allFigures,
            ref bool isReadingMemo, ref string memoText, ref bool memoExists)
        {
            if (line.Contains("Figure ") && Equals(creatingFigure, null))
            {
                creatingFigure = new Figure();

                return;
            }

            if (line.Contains("  Label TRUE"))
            {
                creatingFigure = null;

                memoExists = false;

                return;
            }

            if (line.Contains("  Style ") && !Equals(creatingFigure, null))
            {
                creatingFigure.Style = line.Substring(9, line.Length - 10);

                return;
            }

            if (line.Contains("  MemoText ") && !Equals(creatingFigure, null))
            {
                memoText = line.Substring(12, line.Length - 13);

                isReadingMemo = !line.EndsWith("\"");

                memoExists = true;

                return;
            }

            if (isReadingMemo && !Equals(creatingFigure, null))
            {
                memoText += line.Substring(0, line.Length - 1);

                isReadingMemo = !line.EndsWith("\"");

                return;
            }

            if (!line.Contains("}") || Equals(creatingFigure, null)) return;

            if (memoExists) allFigures.Add(creatingFigure.Clone() as Figure);

            creatingFigure = null;

            memoExists = false;
        }

        private static void ParseMemo(Figure creatingFigure, ref string memoText)
        {
            var memoStrings = memoText.Split(new[] {@"\r\n"}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var str in memoStrings)
            {
                var spacePos = str.IndexOf(' ');

                switch (str.Substring(0, spacePos))
                {
                    case "Name":
                        creatingFigure.Name = str.Substring(spacePos + 1);
                        break;

                    case "OS":
                        creatingFigure.Sys = new SysSetting {Os = str.Substring(spacePos + 1)};
                        break;

                    case "Login":
                        if (Equals(creatingFigure.Sys, null)) throw new NullReferenceException("OS weren't defined");
                        creatingFigure.Sys.Login = str.Substring(spacePos + 1);
                        break;

                    case "Password":
                        if (Equals(creatingFigure.Sys, null)) throw new NullReferenceException("OS weren't defined");
                        creatingFigure.Sys.Password = str.Substring(spacePos + 1);
                        break;

                    case "CPU":
                        creatingFigure.Cpu = Convert.ToInt32(str.Substring(spacePos + 1));
                        break;

                    case "Memory":
                        creatingFigure.Memory = Convert.ToInt32(str.Substring(spacePos + 1));
                        break;

                    case "Repository":
                        creatingFigure.Repository = str.Substring(spacePos + 1);
                        break;

                    case "Services":
                        creatingFigure.Services = new Dictionary<string, Dictionary<string, string>>();
                        foreach (var service in str.Substring(spacePos + 1)
                            .Split(new[] {", "}, StringSplitOptions.None))
                        {
                            var set = service.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                            var servSettingsDict = new Dictionary<string, string>();

                            if (!Equals(set.Length, 1))
                            {
                                var servSettings = set[1].Split(new[] {">", ","},
                                    StringSplitOptions.RemoveEmptyEntries);

                                for (var i = 0; i < servSettings.Length; i += 2)
                                    servSettingsDict.Add(servSettings[i], servSettings[i + 1]);
                            }

                            creatingFigure.Services.Add(set[0], servSettingsDict);
                        }
                        break;

                    case "NetSettings":
                        var settings = str.Substring(spacePos + 1).Split(new[] {", "}, StringSplitOptions.None);

                        creatingFigure.NetSettings = new List<NetSetting>();

                        foreach (var setting in settings)
                        {
                            var set = setting.Split(new[] {">"}, StringSplitOptions.None).ToList();

                            var outer = Equals(set.Last(), "outer");

                            string ip, subnet;

                            if (outer)
                            {
                                ip = "DHCP";
                                subnet = set[1];
                            }
                            else
                            {
                                ip = set[1];
                                subnet = string.Empty;
                            }

                            var netmask = set.Exists(x => x.Contains("DHCP")) ? string.Empty : set[2];

                            var gateway = set.Find(x => x.Contains("gateway ")) ?? string.Empty;

                            var dns = set.Find(x => x.Contains("dns ")) ?? string.Empty;

                            creatingFigure.NetSettings.Add(new NetSetting
                            {
                                PortGroupName = set[0],
                                Ip = ip,
                                Netmask = netmask,
                                DefaultGateway = gateway.Replace("gateway ", ""),
                                DnsServers = dns.Replace("dns ", ""),
                                Subnet = subnet,
                                Outer = outer
                            });
                        }
                        break;
                }
            }

            memoText = string.Empty;
        }

        #endregion
    }
}