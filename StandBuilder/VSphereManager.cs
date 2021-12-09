using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace StandBuilder
{
    public class VSphereManager
    {
        private NetworkModel Model { get; set; }
        private List<VirtualMachine> VmList { get; set; }

        private static readonly string CurDir = Environment.CurrentDirectory + @"\Configs";
        private readonly string RoutesPath = CurDir + @"\Routes.txt";
        private readonly string ScriptPath = CurDir + @"\Script.ps1";
        private readonly string MacPath = CurDir + @"\MAC.txt";
        private readonly string FolderPropertiesPath = CurDir + @"\FolderProperties.txt";
        private readonly string VmIdsPath = CurDir + @"\VmIds.txt";
        private readonly string ServerPropertiesPath = CurDir + @"\ServerProperties.txt";

        #region Public methods

        public void InitModel(string fileName)
        {
            Model = new NetworkModel();

            Model.GetModelFromFile(fileName);

            Parser.ParseRoutes(RoutesPath, Model.Routes);
        }

        public void InitProperties(Dictionary<string, string> args, ICollection<string> datacenters,
            ICollection<string> datastores, ICollection<string> hosts)
        {
            VmList = new List<VirtualMachine>();

            Console.WriteLine(nameof(RunInitScript));
            RunInitScript(args);

            Parser.ParseServerProperties(ServerPropertiesPath, datacenters, datastores, hosts);
        }

        public void BuildStand(Dictionary<string, string> args)
        {
            Console.WriteLine(nameof(RunFoldersInitScript));
            RunFoldersInitScript(args);

            Parser.ParseFolderProperties(FolderPropertiesPath, Model, VmList);

            foreach (var vm in VmList) vm.InitNetSettings(Model);

            Console.WriteLine(nameof(RunStandBuildingScript));
            RunStandBuildingScript(args);

            Parser.ParseVmId(VmIdsPath, VmList);

            Console.WriteLine(nameof(RunAdaptersAddingScript));
            RunAdaptersAddingScript(args);

            Parser.ParseMac(MacPath, VmList);

            Console.WriteLine(nameof(RunNetConfiguringScript));
            RunNetConfiguringScript(args);

            Console.WriteLine(nameof(RunRoutesConfiguringScript));
            RunRoutesConfiguringScript(args, false);

            Console.WriteLine(nameof(RunRepoConfiguringScript));
            RunRepoConfiguringScript(args);

            Console.WriteLine(nameof(RunServicesConfiguringScript));
            RunServicesConfiguringScript(args);

            Console.WriteLine(nameof(RunRoutesConfiguringScript));
            RunRoutesConfiguringScript(args, true);
        }

        #endregion

        #region Private methods

        private void RunInitScript(IReadOnlyDictionary<string, string> args)
        {
            File.Create(ScriptPath).Close();

            var resArgs = new List<string>
            {
                @"$null = Set-PowerCLIConfiguration -Scope AllUsers -ParticipateInCEIP:$false -Confirm:$false",
                $@"$Server = Connect-VIServer -Server '{args["Server"]}' -Protocol https -User '{args["Username"]}' -Password '{args["Password"]}'",
                @"$Datacenters = Get-Datacenter -Server $Server",
                @"$Datastores = Get-Datastore -Server $Server",
                @"$Hosts = Get-VMHost -Server $Server",
                @"ForEach ($item in $Datacenters){$Out = 'Datacenter\r\n' + $item.Name; " +
                $@"Out-File -Filepath '{ServerPropertiesPath}' -Append:$true -InputObject $Out}}",
                @"ForEach ($item in $Datastores){$Out = 'Datastore\r\n' + $item.Name; " +
                $@"Out-File -Filepath '{ServerPropertiesPath}' -Append:$true -InputObject $Out}}",
                @"ForEach ($item in $Hosts){$Out = 'Host\r\n' + $item.Name; " +
                $@"Out-File -Filepath '{ServerPropertiesPath}' -Append:$true -InputObject $Out}}",
                @"$null = Disconnect-VIServer -Server $Server -Force -Confirm:$false"
            };

            if (Equals(args["AutoClose"], "False"))
                resArgs.AddRange(new[]
                {
                    @"Write-Host -NoNewLine 'Press any key to continue...'",
                    @"$Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown')"
                });

            File.WriteAllLines(ScriptPath, resArgs);

            RunScript();
        }

        private void RunFoldersInitScript(IReadOnlyDictionary<string, string> args)
        {
            File.Create(ScriptPath).Close();

            var resArgs = new List<string>
            {
                @"$null = Set-PowerCLIConfiguration -Scope AllUsers -ParticipateInCEIP:$false -Confirm:$false",
                $@"$Server = Connect-VIServer -Server '{args["Server"]}' -Protocol https -User '{args["Username"]}' -Password '{args["Password"]}'",
                $@"$Datacenter = Get-Datacenter -Server $Server -VMHost '{args["Host"]}' -Name '{args["Datacenter"]}'",
                $@"$GroupFolder = Get-Folder -Server $Server -Location $Datacenter -Type VM -Name '{args["GroupFolder"]}'",
                @"$Folders = Get-Folder -Server $Server -Type VM -NoRecursion:$true -Location $GroupFolder",
                $@"ForEach ($item in $Folders){{$newFolder = New-Folder -Name '{args["Stand"]}' -Location $item; " +
                @"$Out = $item.Name + '\r\n' + $newFolder.Id; " +
                $@"Out-File -Filepath '{FolderPropertiesPath}' -Append:$true -InputObject $Out}}",
                @"$null = Disconnect-VIServer -Server $Server -Force -Confirm:$false"
            };

            if (Equals(args["AutoClose"], "False"))
                resArgs.AddRange(new[]
                {
                    @"Write-Host -NoNewLine 'Press any key to continue...'",
                    @"$Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown')"
                });

            File.WriteAllLines(ScriptPath, resArgs);

            RunScript();
        }

        private void RunStandBuildingScript(IReadOnlyDictionary<string, string> args)
        {
            File.Create(ScriptPath).Close();

            var resArgs = new List<string>
            {
                @"$null = Set-PowerCLIConfiguration -Scope AllUsers -ParticipateInCEIP:$false -Confirm:$false",
                $@"$Server = Connect-VIServer -Server '{args["Server"]}' -Protocol https -User '{args["Username"]}' -Password '{args["Password"]}'"
            };

            foreach (var vm in VmList)
                resArgs.AddRange(new[]
                {
                    $@"$Folder = Get-Folder -Server $Server -Id '{vm.StandFolderId}'",
                    $@"$TemplatesFolder = Get-Folder -Server $Server -Type VM -Name '{args["TemplatesFolder"]}'",
                    $"$Vm = Get-VM -Server $Server -Location $TemplatesFolder -Name '{vm.Figure.Sys.Os}'",
                    $@"$Snapshot = Get-Snapshot -Server $Server -VM $Vm -Name '{args["Snapshot"]}'",
                    $@"$newVm = New-VM -Server $Server -VMHost '{args["Host"]}' -Datastore '{args["Datastore"]}' -VM $Vm " +
                    $@"-Name '{vm.Figure.Name}' -Location $Folder -LinkedClone:$true -ReferenceSnapshot $Snapshot",
                    $@"$Out = '{vm.StandFolderId}\r\n{vm.Figure.Name}\r\n' + $newVm.Id",
                    $@"Out-File -Filepath '{VmIdsPath}' -Append:$true -InputObject $Out",
                    $@"Set-VM -VM $newVm -MemoryMB '{vm.Figure.Memory}' -NumCpu '{vm.Figure.Cpu}' -Confirm:$false"
                });

            resArgs.Add(@"$null = Disconnect-VIServer -Server $Server -Force -Confirm:$false");

            if (Equals(args["AutoClose"], "False"))
                resArgs.AddRange(new[]
                {
                    @"Write-Host -NoNewLine 'Press any key to continue...'",
                    @"$Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown')"
                });

            File.WriteAllLines(ScriptPath, resArgs);

            RunScript();
        }

        private void RunAdaptersAddingScript(IReadOnlyDictionary<string, string> args)
        {
            File.Create(ScriptPath).Close();

            var resArgs = new List<string>
            {
                @"$null = Set-PowerCLIConfiguration -Scope AllUsers -ParticipateInCEIP:$false -Confirm:$false",
                $@"$Server = Connect-VIServer -Server '{args["Server"]}' -Protocol https -User '{args["Username"]}' -Password '{args["Password"]}'",
                $@"$VDSwitch = Get-VDSwitch -Server $Server -Name '{args["VDSwitch"]}'"
            };

            foreach (var vm in VmList)
            {
                resArgs.Add($"$Vm = Get-VM -Server $Server -Id '{vm.Id}'");

                foreach (var setting in vm.NetSettings)
                    resArgs.AddRange(new[]
                    {
                        $@"$PortGroup = Get-VDPortgroup -Server $Server -VDSwitch $VDSwitch -Name '{setting.PortGroupName}'",
                        @"$Adapter = New-NetworkAdapter -VM $Vm -Portgroup $PortGroup -StartConnected:$true",
                        $@"$Out = '{vm.Id}\r\n{setting.PortGroupName}\r\n' + $Adapter.MacAddress",
                        $@"Out-File -Filepath '{MacPath}' -Append:$true -InputObject $Out"
                    });

                resArgs.Add(@"$null = Start-VM -VM $Vm -Confirm:$false");
            }

            resArgs.Add(@"$null = Disconnect-VIServer -Server $Server -Force -Confirm:$false");

            if (Equals(args["AutoClose"], "False"))
                resArgs.AddRange(new[]
                {
                    @"Write-Host -NoNewLine 'Press any key to continue...'",
                    @"$Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown')"
                });

            File.WriteAllLines(ScriptPath, resArgs);

            RunScript();
        }

        private void RunNetConfiguringScript(IReadOnlyDictionary<string, string> args)
        {
            File.Create(ScriptPath).Close();

            var resArgs = new List<string>
            {
                @"$null = Set-PowerCLIConfiguration -Scope AllUsers -ParticipateInCEIP:$false -Confirm:$false",
                $@"$Server = Connect-VIServer -Server '{args["Server"]}' -Protocol https -User '{args["Username"]}' -Password '{args["Password"]}'"
            };

            foreach (var vm in VmList)
            {
                resArgs.AddRange(new[]
                {
                    $"$Vm = Get-VM -Server $Server -Id '{vm.Id}'",
                    "$null = Wait-Tools -VM $Vm"
                });
                var os = vm.Figure.Sys.Os.ToLower();

                switch (os.Substring(0, os.IndexOf(' ')))
                {
                    case "windows":
                    {
                        resArgs.Add(
                            $"Invoke-VMScript -VM $Vm -GuestUser '{vm.Figure.Sys.Login}' -GuestPassword '{vm.Figure.Sys.Password}' " +
                            "-Confirm:$false -ToolsWaitSecs 60 -ScriptText \"");

                        var index = resArgs.Count - 1;

                        foreach (var setting in vm.NetSettings)
                        {
                            resArgs[index] +=
                                $"`$a = getmac /v /fo csv | Select-String -Pattern '{setting.Mac.Replace(':', '-').ToUpper()}'; " +
                                "`$a = `$a -split `\"```\"`\"; ";

                            if (!Equals(setting.Ip, "DHCP"))
                            {
                                resArgs[index] +=
                                    $"netsh interface ip set address name=(`$a[1]) static '{setting.Ip}' '{setting.Netmask}'";

                                if (!string.IsNullOrEmpty(setting.DefaultGateway))
                                    resArgs[index] += $" '{setting.DefaultGateway}'";

                                resArgs[index] += "; ";
                            }

                            if (!string.IsNullOrEmpty(setting.DnsServers))
                            {
                                var set = setting.DnsServers.Split();

                                resArgs[index] += $"netsh interface ipv4 set dns name=(`$a[1]) static '{set[0]}'; ";

                                if (!Equals(set.Length, 1))
                                    resArgs[index] += $"netsh interface ipv4 add dns name=(`$a[1]) '{set[1]}'; ";
                            }

                            resArgs[index] += @"netsh interface set interface name=(`$a[1]) admin=disable; Wait-Event -Timeout 1; " +
                                              @"netsh interface set interface name=(`$a[1]) admin=enable; ";

                            resArgs[index] += "netsh interface ip show address name=(`$a[1]); ";
                        }

                        resArgs[index] += "\" -ScriptType PowerShell";
                        break;
                    }
                    case "centos":
                    {
                        resArgs.Add(
                            $"Invoke-VMScript -VM $Vm -GuestUser 'root' -GuestPassword '{vm.Figure.Sys.Password}' " +
                            "-Confirm:$false -ToolsWaitSecs 60 -ScriptText \"" +
                            @"echo -e `""[Unit]\nDescription=rc-local service\nAfter=multi-user.target\n[Service]\nType=forking\nRestartSec=1\n" +
                            @"Restart=on-failure\nExecStart=/etc/rc.d/rc.local start\n[Install]\nWantedBy=multi-user.target`"" > " +
                            @"/lib/systemd/system/rc-local.service; chmod 777 /etc/rc.d/rc.local; systemctl daemon-reload;  systemctl enable rc-local.service; ");

                        var index = resArgs.Count - 1;

                        foreach (var setting in vm.NetSettings)
                        {
                            resArgs[index] +=
                                $"IFACE=`$(ifconfig -a | grep -B 4 {setting.Mac} | egrep -o `\"e(th|ns|np)[0-9]+[a-z]*[0-9]*`\"); ";

                            if (Equals(setting.Ip, "DHCP"))
                                resArgs[index] +=
                                    @"echo -e `""DEVICE=`$IFACE\nBOOTPROTO=dhcp\nONBOOT=yes`"" > /etc/sysconfig/network-scripts/ifcfg-`$IFACE; ";
                            else
                                resArgs[index] +=
                                    $@"echo -e `""DEVICE=`$IFACE\nBOOTPROTO=static\nIPADDR={setting.Ip}\nNETMASK={setting.Netmask}\nONBOOT=yes`"" " +
                                    @"> /etc/sysconfig/network-scripts/ifcfg-`$IFACE; ";

                            if (!string.IsNullOrEmpty(setting.DefaultGateway))
                                resArgs[index] +=
                                    $@"echo -e `""GATEWAY={setting.DefaultGateway}`"" >> /etc/sysconfig/network-scripts/ifcfg-`$IFACE; ";

                            if (!string.IsNullOrEmpty(setting.DnsServers))
                            {
                                var set = setting.DnsServers.Split();

                                resArgs[index] +=
                                    $@"echo -e `""DNS1={set[0]}`"" >> /etc/sysconfig/network-scripts/ifcfg-`$IFACE; ";

                                if (!Equals(set.Length, 1))
                                    resArgs[index] +=
                                        $@"echo -e `""DNS2={set[1]}`"" >> /etc/sysconfig/network-scripts/ifcfg-`$IFACE; ";
                            }

                            if (setting.Outer)
                                resArgs[index] +=
                                    @"echo -e `""/sbin/iptables -t nat -A POSTROUTING -o `$IFACE " +
                                    $@"-s {setting.Subnet}/{setting.Netmask} -j MASQUERADE"" >> /etc/rc.d/rc.local; " +
                                    $@"iptables -t nat -A POSTROUTING -o `$IFACE -s {setting.Subnet}/{setting.Netmask} -j MASQUERADE; ";

                            if (Equals(setting.Ip, "DHCP"))
                                foreach (var unused in from figure in Model.AllFigures
                                    let sett =
                                        figure.NetSettings.Find(x => Equals(x.PortGroupName, setting.PortGroupName))
                                    where Equals(sett, default) || Equals(figure.Services, default) ||
                                          !figure.Services.ContainsKey("DHCP") ||
                                          !Equals(figure.Services["DHCP"]["IfaceIP"], sett.Ip)
                                    select figure)
                                    resArgs[index] += "ifdown `$IFACE; ifup `$IFACE; ";
                            else
                                resArgs[index] += "ifdown `$IFACE; ifup `$IFACE; ";

                            resArgs[index] += "ifconfig `$IFACE; ";
                        }

                        if (Equals(vm.Figure.Style, "Router"))
                            resArgs[index] +=
                                @"FORWARD=`$(cat /proc/sys/net/ipv4/ip_forward); if [ `$FORWARD -eq 0 ]; " +
                                @"then echo -e `""net.ipv4.ip_forward=1`"" >> /etc/sysctl.conf; sysctl -p /etc/sysctl.conf; " +
                                @"sysctl -w net.ipv4.ip_forward=1; fi; FORWARD=`$(cat /proc/sys/net/ipv6/conf/all/forwarding); if [ `$FORWARD -eq 0 ]; " +
                                @"then echo -e `""net.ipv6.conf.all.forwarding=1`"" >> /etc/sysctl.conf; sysctl -p /etc/sysctl.conf; " +
                                @"sysctl -w net.ipv6.conf.all.forwarding=1; fi; ";

                        resArgs[index] += "\" -ScriptType Bash";
                        break;
                    }
                    default:
                    {
                        resArgs.Add(
                            $"Invoke-VMScript -VM $Vm -GuestUser 'root' -GuestPassword '{vm.Figure.Sys.Password}' " +
                            "-Confirm:$false -ToolsWaitSecs 60 -ScriptText \"");

                        var index = resArgs.Count - 1;

                            resArgs[index] +=
                            @"echo -e `""auto lo\niface lo inet loopback`"" > /etc/network/interfaces; ";

                        foreach (var setting in vm.NetSettings)
                        {
                            resArgs[index] +=
                                $"IFACE=`$(ifconfig -a | grep -B 4 {setting.Mac} | egrep -o `\"e(th|ns|np)[0-9]+[a-z]*[0-9]*`\"); ";

                            if (Equals(setting.Ip, "DHCP"))
                                resArgs[index] +=
                                    @"echo -e `""\nauto `$IFACE\niface `$IFACE inet dhcp`"" >> /etc/network/interfaces; ";
                            else
                                resArgs[index] +=
                                    $@"echo -e `""\nauto `$IFACE\niface `$IFACE inet static\naddress {setting.Ip}\nnetmask {setting.Netmask}`"" " +
                                    @">> /etc/network/interfaces; ";

                            if (!string.IsNullOrEmpty(setting.DefaultGateway))
                                resArgs[index] +=
                                    $@"echo -e `""gateway {setting.DefaultGateway}`"" >> /etc/network/interfaces; ";

                            if (!string.IsNullOrEmpty(setting.DnsServers))
                            {
                                var set = setting.DnsServers.Split();

                                resArgs[index] +=
                                    $@"echo -e `""dns-nameservers {setting.DnsServers}`"" >> /etc/network/interfaces; " +
                                    @"mkdir -p /etc/resolvconf/resolv.conf.d; ";

                                resArgs[index] +=
                                    $@"echo -e `""nameserver {set[0]}\n`"" >> /etc/resolvconf/resolv.conf.d/base; " +
                                    $@"echo -e `""\nnameserver {set[0]}`"" >> /etc/resolv.conf; ";

                                if (!Equals(set.Length, 1))
                                    resArgs[index] +=
                                        $@"echo -e `""nameserver {set[1]}\n`"" >> /etc/resolvconf/resolv.conf.d/base; " +
                                        $@"echo -e `""\nnameserver {set[1]}`"" >> /etc/resolv.conf; ";
                            }

                            if (setting.Outer)
                                resArgs[index] +=
                                    @"echo -e `""post-up /sbin/iptables -t nat -A POSTROUTING -o `$IFACE " +
                                    $@"-s {setting.Subnet}/{setting.Netmask} -j MASQUERADE"" >> /etc/network/interfaces; " +
                                    $@"iptables -t nat -A POSTROUTING -o `$IFACE -s {setting.Subnet}/{setting.Netmask} -j MASQUERADE; ";

                            if (Equals(setting.Ip, "DHCP"))
                                foreach (var unused in from figure in Model.AllFigures
                                    let sett =
                                        figure.NetSettings.Find(x => Equals(x.PortGroupName, setting.PortGroupName))
                                    where Equals(sett, default) || Equals(figure.Services, default) ||
                                          !figure.Services.ContainsKey("DHCP") ||
                                          !Equals(figure.Services["DHCP"]["IfaceIP"], sett.Ip)
                                    select figure)
                                    resArgs[index] += "ifdown `$IFACE; ifup `$IFACE; ";
                            else
                                resArgs[index] += "ifdown `$IFACE; ifup `$IFACE; ";

                            resArgs[index] += "ifconfig `$IFACE; ";
                        }

                        if (Equals(vm.Figure.Style, "Router"))
                            resArgs[index] +=
                                @"FORWARD=`$(cat /proc/sys/net/ipv4/ip_forward); if [ `$FORWARD -eq 0 ]; " +
                                @"then echo -e `""net.ipv4.ip_forward=1`"" >> /etc/sysctl.conf; sysctl -p /etc/sysctl.conf; " +
                                @"sysctl -w net.ipv4.ip_forward=1; fi; FORWARD=`$(cat /proc/sys/net/ipv6/conf/all/forwarding); if [ `$FORWARD -eq 0 ]; " +
                                @"then echo -e `""net.ipv6.conf.all.forwarding=1`"" >> /etc/sysctl.conf; sysctl -p /etc/sysctl.conf; " +
                                @"sysctl -w net.ipv6.conf.all.forwarding=1; fi; ";

                        resArgs[index] += "\" -ScriptType Bash";
                        break;
                    }
                }
            }

            resArgs.Add(@"$null = Disconnect-VIServer -Server $Server -Force -Confirm:$false");

            if (Equals(args["AutoClose"], "False"))
                resArgs.AddRange(new[]
                {
                    @"Write-Host -NoNewLine 'Press any key to continue...'",
                    @"$Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown')"
                });

            resArgs.RemoveAll(x => x.Contains("-ScriptText \"\""));

            File.WriteAllLines(ScriptPath, resArgs);

            RunScript();
        }

        private void RunRoutesConfiguringScript(IReadOnlyDictionary<string, string> args, bool isDhcpReady)
        {
            File.Create(ScriptPath).Close();

            var resArgs = new List<string>
            {
                @"$null = Set-PowerCLIConfiguration -Scope AllUsers -ParticipateInCEIP:$false -Confirm:$false",
                $@"$Server = Connect-VIServer -Server '{args["Server"]}' -Protocol https -User '{args["Username"]}' -Password '{args["Password"]}'"
            };

            foreach (var vm in VmList)
            {
                resArgs.AddRange(new[]
                {
                    $"$Vm = Get-VM -Server $Server -Id '{vm.Id}'",
                    "$null = Wait-Tools -VM $Vm"
                });

                var os = vm.Figure.Sys.Os.ToLower();

                if (Equals(os.Substring(0, os.IndexOf(' ')), "windows"))
                {
                    resArgs.Add(
                        $"Invoke-VMScript -VM $Vm -GuestUser '{vm.Figure.Sys.Login}' -GuestPassword '{vm.Figure.Sys.Password}' " +
                        "-Confirm:$false -ToolsWaitSecs 60 -ScriptText \"");

                    var index = resArgs.Count - 1;

                    if (!isDhcpReady)
                        foreach (var set in vm.Routes.Select(route => route.Split('>'))
                            .Where(set => !vm.NetSettings.Exists(x =>
                                Equals(x.Ip, "DHCP") && Equals(x.Subnet, Parser.GetSubnet(set[2], set[3])))))
                            resArgs[index] += $"route -p add {set[0]} mask {set[1]} {set[2]}; ";
                    else
                        foreach (var setting in vm.NetSettings.Where(setting =>
                            Equals(setting.Ip, "DHCP") && vm.Routes.Exists(x =>
                                Equals(setting.Subnet, Parser.GetSubnet(x.Split('>')[2], x.Split('>')[3])))))
                        {
                            resArgs[index] +=
                                $"`$a = getmac /v /fo csv | Select-String -Pattern '{setting.Mac.Replace(':', '-').ToUpper()}'; " +
                                "`$a = `$a -split `\"```\"`\"; " +
                                @"netsh interface set interface name=(`$a[1]) admin=disable; " +
                                @"netsh interface set interface name=(`$a[1]) admin=enable; ";

                            foreach (var set in vm.Routes.Select(route => route.Split('>'))
                                .Where(set => Equals(setting.Subnet, Parser.GetSubnet(set[2], set[3]))))
                                resArgs[index] += $"route -p add {set[0]} mask {set[1]} {set[2]}; ";
                        }

                    resArgs[index] += "route print; ";

                    resArgs[index] += "\" -ScriptType PowerShell";
                }
                else
                {
                    resArgs.Add(
                        $"Invoke-VMScript -VM $Vm -GuestUser 'root' -GuestPassword '{vm.Figure.Sys.Password}' " +
                        "-Confirm:$false -ToolsWaitSecs 60 -ScriptText \"");

                    var index = resArgs.Count - 1;

                    if (!isDhcpReady)
                    {
                        resArgs[index] += @"echo '#!/bin/bash' > /home/user/myroutes.sh; ";

                        foreach (var set in vm.Routes.Select(route => route.Split('>'))
                            .Where(set => !vm.NetSettings.Exists(x =>
                                Equals(x.Ip, "DHCP") && Equals(x.Subnet, Parser.GetSubnet(set[2], set[3])))))
                            resArgs[index] +=
                                $@"echo `""ip route add {set[0]}/{set[1]} via {set[2]};`"" >> /home/user/myroutes.sh; ";

                        AppendScriptAutostart(os, resArgs, index);
                    }
                    else
                        foreach (var setting in vm.NetSettings.Where(setting =>
                            Equals(setting.Ip, "DHCP") && vm.Routes.Exists(x =>
                                Equals(setting.Subnet, Parser.GetSubnet(x.Split('>')[2], x.Split('>')[3])))))
                        {
                            resArgs[index] +=
                                $"IFACE=`$(ifconfig -a | grep -B 4 {setting.Mac} | egrep -o `\"e(th|ns|np)[0-9]+[a-z]*[0-9]*`\"); " +
                                "ifdown `$IFACE; ifup `$IFACE; ";

                            foreach (var set in vm.Routes.Select(route => route.Split('>'))
                                .Where(set => Equals(setting.Subnet, Parser.GetSubnet(set[2], set[3]))))
                                resArgs[index] +=
                                    $@"echo `""ip route add {set[0]}/{set[1]} via {set[2]};`"" >> /home/user/myroutes.sh; ";
                        }


                    resArgs[index] += "chmod 777 /home/user/myroutes.sh; /home/user/myroutes.sh; ";

                    resArgs[index] += "\" -ScriptType Bash";
                }
            }

            resArgs.Add(@"$null = Disconnect-VIServer -Server $Server -Force -Confirm:$false");

            if (Equals(args["AutoClose"], "False"))
                resArgs.AddRange(new[]
                {
                    @"Write-Host -NoNewLine 'Press any key to continue...'",
                    @"$Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown')"
                });

            resArgs.RemoveAll(x => x.Contains("-ScriptText \"\""));

            File.WriteAllLines(ScriptPath, resArgs);

            RunScript();
        }

        private void RunRepoConfiguringScript(IReadOnlyDictionary<string, string> args)
        {
            File.Create(ScriptPath).Close();

            var resArgs = new List<string>
            {
                @"$null = Set-PowerCLIConfiguration -Scope AllUsers -ParticipateInCEIP:$false -Confirm:$false",
                $@"$Server = Connect-VIServer -Server '{args["Server"]}' -Protocol https -User '{args["Username"]}' -Password '{args["Password"]}'"
            };

            foreach (var vm in VmList
                .Where(vm => !Equals(vm.Figure.Sys.Os.ToLower().Substring(0, vm.Figure.Sys.Os.IndexOf(' ')), "windows"))
                .Where(vm => !Equals(vm.Figure.Repository, default)))
            {
                resArgs.AddRange(new[]
                {
                    $"$Vm = Get-VM -Server $Server -Id '{vm.Id}'", "$null = Wait-Tools -VM $Vm",
                    $"Invoke-VMScript -VM $Vm -GuestUser 'root' -GuestPassword '{vm.Figure.Sys.Password}' " +
                    "-Confirm:$false -ToolsWaitSecs 60 -ScriptText \""
                });

                var index = resArgs.Count - 1;
                var os = vm.Figure.Sys.Os.ToLower();

                switch (os.Substring(0, os.IndexOf(' ')))
                {
                    case "centos":
                        var configs = new Dictionary<string, List<string>>();

                        var dirs = Directory.GetDirectories(CurDir + @"\Repositories", @"*centos*");

                        foreach (var dir in dirs)
                        foreach (var file in Directory.GetFiles(dir))
                            ReadConfig(file, vm.Figure.Repository, configs);

                        InvokeFiles(vm, resArgs, ref index, configs, false);

                        resArgs[index] += "yum -y update; yum -y upgrade; ";

                        break;
                    default:
                        resArgs[index] +=
                            $@"echo -e `""\n{vm.Figure.Repository}\n`"" >> /etc/apt/sources.list; " +
                            "apt -y update; apt -y upgrade; ";
                        break;
                }

                resArgs[index] += "\" -ScriptType Bash";
            }

            resArgs.Add(@"$null = Disconnect-VIServer -Server $Server -Force -Confirm:$false");

            if (Equals(args["AutoClose"], "False"))
                resArgs.AddRange(new[]
                {
                    @"Write-Host -NoNewLine 'Press any key to continue...'",
                    @"$Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown')"
                });

            resArgs.RemoveAll(x => x.Contains("-ScriptText \"\""));

            File.WriteAllLines(ScriptPath, resArgs);

            RunScript();
        }

        private void RunServicesConfiguringScript(IReadOnlyDictionary<string, string> args)
        {
            File.Create(ScriptPath).Close();

            var resArgs = new List<string>
            {
                @"$null = Set-PowerCLIConfiguration -Scope AllUsers -ParticipateInCEIP:$false -Confirm:$false",
                $@"$Server = Connect-VIServer -Server '{args["Server"]}' -Protocol https -User '{args["Username"]}' -Password '{args["Password"]}'"
            };

            foreach (var vm in VmList.Where(vm =>
                !Equals(vm.Figure.Sys.Os.ToLower().Substring(0, vm.Figure.Sys.Os.IndexOf(' ')), "windows")))
            {
                resArgs.AddRange(new[]
                {
                    $"$Vm = Get-VM -Server $Server -Id '{vm.Id}'",
                    "$null = Wait-Tools -VM $Vm"
                });

                foreach (var service in vm.Figure.Services)
                {
                    var os = vm.Figure.Sys.Os.ToLower();
                    var configs = new Dictionary<string, List<string>>();

                    var dirs = Directory.GetDirectories(CurDir + $@"\Services\{service.Key}", $@"*{os.Substring(0, os.IndexOf(' '))}*");

                    foreach (var dir in dirs)
                    foreach (var file in Directory.GetFiles(dir))
                        ReadConfig(file, vm.FolderIndex.ToString(), configs);

                    resArgs.Add(
                        $"Invoke-VMScript -VM $Vm -GuestUser 'root' -GuestPassword '{vm.Figure.Sys.Password}' " +
                        "-Confirm:$false -ToolsWaitSecs 60 -ScriptText \"");

                    var index = resArgs.Count - 1;

                    switch (service.Key)
                    {
                        case "DHCP":
                            switch (os.Substring(0, os.IndexOf(' ')))
                            {
                                case "centos":
                                    resArgs[index] += "yum -y install dhcp-server; " +
                                                      $"IFACE=`$(ifconfig -a | grep -B 4 {service.Value["IfaceIP"].Replace("x", $"{vm.FolderIndex}")} " +
                                                      @"| egrep -o `""e(th|ns|np)[0-9]+[a-z]*[0-9]*`""); echo -e `""\n`""DHCPDARGS=`$IFACE " +
                                                      @">> /etc/sysconfig/dhcpd; ";

                                    InvokeFiles(vm, resArgs, ref index, configs, false);

                                    resArgs[index] +=
                                        "systemctl restart dhcpd; systemctl enable dhcpd; ";
                                    break;
                                default:
                                    resArgs[index] += "apt -y install isc-dhcp-server; " +
                                                      $"IFACE=`$(ifconfig -a | grep -B 4 {service.Value["IfaceIP"].Replace("x", $"{vm.FolderIndex}")} " +
                                                      @"| egrep -o `""e(th|ns|np)[0-9]+[a-z]*[0-9]*`""); echo -e `""\n`""INTERFACES='`""'`$IFACE'`""' " +
                                                      @">> /etc/default/isc-dhcp-server; echo -e `""supersede domain-name-servers";

                                    foreach (var setting in vm.NetSettings) resArgs[index] += $@" {setting.DnsServers}";

                                    resArgs[index] += @";`"" >> /etc/dhcp/dhclient.conf; ";

                                    InvokeFiles(vm, resArgs, ref index, configs, false);

                                    resArgs[index] +=
                                        "rm /var/run/dhcpd.pid; systemctl restart isc-dhcp-server; systemctl enable isc-dhcp-server; ";
                                    break;
                            }
                            break;

                        case "DNS":
                            switch (os.Substring(0, os.IndexOf(' ')))
                            {
                                case "centos":
                                    resArgs[index] += "yum -y install bind bind-utils; ";

                                    InvokeFiles(vm, resArgs, ref index, configs, false);

                                    resArgs[index] +=
                                        "systemctl restart named; named-checkconf; named-checkconf -z; systemctl enable named; ";
                                    break;
                                case "debian":
                                    resArgs[index] +=
                                        "apt -y install bind9 dnsutils bind9utils bind9-doc bind9-host; ";

                                    InvokeFiles(vm, resArgs, ref index, configs, false);

                                    resArgs[index] +=
                                        "systemctl restart bind9; named-checkconf; named-checkconf -z; systemctl enable bind9; ";
                                    break;
                                default:
                                    resArgs[index] +=
                                        "apt -y install bind9 dnsutils bind9-dnsutils bind9utils bind9-doc bind9-host; ";

                                    InvokeFiles(vm, resArgs, ref index, configs, false);

                                    resArgs[index] +=
                                        "systemctl restart bind9; named-checkconf; named-checkconf -z; systemctl enable bind9; ";
                                    break;
                            }
                            break;

                        case "FTP":
                            switch (os.Substring(0, os.IndexOf(' ')))
                            {
                                case "centos":
                                    resArgs[index] += "yum -y install vsftpd ftp; ";

                                    InvokeFiles(vm, resArgs, ref index, configs, false);

                                    resArgs[index] += "systemctl restart vsftpd; systemctl enable vsftpd; ";
                                    break;

                                default:
                                    resArgs[index] += "apt -y install vsftpd ftp; ";

                                    InvokeFiles(vm, resArgs, ref index, configs, false);

                                    resArgs[index] += "systemctl restart vsftpd; systemctl enable vsftpd; ";
                                    break;
                            }

                            break;
                            

                        case "WEB":
                            switch (os.Substring(0, os.IndexOf(' ')))
                            {
                                case "centos":
                                    resArgs[index] += "yum -y install httpd; ";

                                    InvokeFiles(vm, resArgs, ref index, configs, false);

                                    resArgs[index] +=
                                        "systemctl restart httpd; systemctl enable httpd; ";
                                    break;
                                default:
                                    resArgs[index] += "apt -y install apache2; ";

                                    InvokeFiles(vm, resArgs, ref index, configs, false);

                                    resArgs[index] +=
                                        "systemctl restart apache2; systemctl enable apache2; ";
                                    break;
                            }

                            break;

                        case "Postfix":
                            InvokeFiles(vm, resArgs, ref index, configs, true);
                            break;
                    }

                    resArgs[index] += "\" -ScriptType Bash";
                }
            }

            resArgs.Add(@"$null = Disconnect-VIServer -Server $Server -Force -Confirm:$false");

            if (Equals(args["AutoClose"], "False"))
                resArgs.AddRange(new[]
                {
                    @"Write-Host -NoNewLine 'Press any key to continue...'",
                    @"$Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown')"
                });

            resArgs.RemoveAll(x => x.Contains("-ScriptText \"\""));

            File.WriteAllLines(ScriptPath, resArgs);

            RunScript();
        }

        private void RunScript()
        {
            var pShell = new Process
            {
                StartInfo =
                {
                    FileName = @"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe",
                    Arguments = ScriptPath
                }
            };
            pShell.Start();
            pShell.WaitForExit();

            File.Delete(ScriptPath);
            if (Directory.Exists($@"{CurDir}\Invoking")) Directory.Delete($@"{CurDir}\Invoking", true);
            Console.WriteLine(@"Success");
        }

        private static void ReadConfig(string filePath, string replacement, IDictionary<string, List<string>> dict)
        {
            try
            {
                using (var sr = new StreamReader(filePath))
                {
                    string line;

                    var path = sr.ReadLine();

                    if (Equals(path, null)) throw new NullReferenceException();

                    path = path.Replace("xXx", replacement);

                    dict.Add(path, new List<string>());

                    while (!Equals(line = sr.ReadLine(), null))
                    {
                        line = line.Replace("xXx", replacement);

                        dict[path].Add(line.Replace("xXx", replacement));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"The file could not be read:");

                Console.WriteLine(e.Message);
            }
        }

        private static void InvokeFiles(VirtualMachine vm, IList<string> resArgs, ref int index, Dictionary<string, List<string>> configs, bool areScripts)
        {
            Directory.CreateDirectory($@"{CurDir}\Invoking\{vm.Id}");

            foreach (var config in configs)
            {
                resArgs[index] += $"rm {config.Key}; \" -ScriptType Bash";

                var fileName = config.Key.Split(new[] {"/"}, StringSplitOptions.RemoveEmptyEntries).Last();

                try
                {
                    using (var sw = new StreamWriter($@"{CurDir}\Invoking\{vm.Id}\{fileName}"))
                    {
                        sw.NewLine = "\n";

                        foreach (var str in config.Value)
                            sw.WriteLine(str);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(@"The file could not be written:");

                    Console.WriteLine(e.Message);
                }

                resArgs.Add($@"Copy-VMGuestFile -Source ""{CurDir}\Invoking\{vm.Id}\{fileName}"" -Destination ""{config.Key.Replace(fileName, "")}"" -VM $Vm -LocalToGuest " +
                            $@"-GuestUser 'root' -GuestPassword '{vm.Figure.Sys.Password}'");

                resArgs.Add(
                    $"Invoke-VMScript -VM $Vm -GuestUser 'root' -GuestPassword '{vm.Figure.Sys.Password}' " +
                    "-Confirm:$false -ToolsWaitSecs 60 -ScriptText \"");

                index = resArgs.Count - 1;

                if (areScripts) resArgs[index] += $"chmod 777 {config.Key}; {config.Key}; ";
            }
        }

        private static void AppendScriptAutostart(string os, IList<string> resArgs, int index)
        {
            switch (os.Substring(0, os.IndexOf(' ')))
            {
                case "centos":
                    resArgs[index] += @"echo -e `""sh /home/user/myroutes.sh`"" >> /etc/rc.d/rc.local; ";
                    break;
                default:
                    resArgs[index] +=
                        @"echo -e `""[Unit]\nDescription=My Routes Service\nAfter=multi-user.target\n[Service]\n" +
                        @"ExecStart=/home/user/myroutes.sh\n[Install]\nWantedBy=multi-user.target`"" > /lib/systemd/system/myroutes.service; " +
                        @"chmod 777 /lib/systemd/system/myroutes.service; systemctl daemon-reload;  systemctl enable myroutes.service; ";
                    break;
            }
        }

        #endregion
    }
}