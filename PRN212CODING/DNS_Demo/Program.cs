using System;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Xml.Linq;

class DnsTestProgram
{
    static void Main(string[] args)
    {
        Console.WriteLine(new string('*', 30));
        Console.WriteLine("DNS Lookup for www.contoso.com:");

        // Lấy DNS entry theo tên miền
        var domainEntry = Dns.GetHostEntry("www.contoso.com");
        Console.WriteLine($"HostName: {domainEntry.HostName}");
        Console.WriteLine("IP Addresses:");
        foreach (var ip in domainEntry.AddressList)
        {
            Console.WriteLine(ip);
        }

        Console.WriteLine(new string('*', 30));
        Console.WriteLine("DNS Lookup for IP 127.0.0.1:");

        // Lấy DNS entry theo địa chỉ IP
        var domainEntryByAddress = Dns.GetHostEntry("127.0.0.1");
        Console.WriteLine($"HostName: {domainEntryByAddress.HostName}");
        Console.WriteLine("IP Addresses:");
        foreach (var ip in domainEntryByAddress.AddressList)
        {
            Console.WriteLine(ip);
        }

        Console.WriteLine(new string('*', 30));
        Console.WriteLine("Press Enter to exit...");
        Console.ReadLine();
    }
}
//******************************
//DNS Lookup for www.contoso.com:
//HostName: www.contoso.com
//IP Addresses:
//13.77.161.179
//* *****************************
//DNS Lookup for IP 127.0.0.1:
//HostName: your - pc - name
//IP Addresses:
//127.0.0.1
//::1
//* *****************************
//Press Enter to exit...
