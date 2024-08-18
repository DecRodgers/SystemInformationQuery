using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using SystemInformationTest.Model;
using SystemInformationTest.SystemInfoReturn;

internal class Program
{
    private static void Main(string[] args)
    {
        FullSystemInfo systemData = GetInfo();
        //Console.WriteLine(systemData.ToString());
        
        string jsonString = JsonSerializer.Serialize(systemData, new JsonSerializerOptions { WriteIndented = true});        
        Console.WriteLine(jsonString);
    }

    private static FullSystemInfo GetInfo() 
    {
        SystemInfoReturn sysInfo = new();
         return sysInfo.ReturnSystemInfoObject;
    }
}