using Ipv4Filter;
using System.Text.Json;
using Ipv4Filter.Models;
using Ipv4Filter.Implementations;
using System.Collections.Concurrent;
using System.Text.Json.Serialization;

Options options = null;
try
{
    options = InputManager.GetOptions(args);
}
catch (ArgumentException ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(ex.Message);
    Console.ForegroundColor = ConsoleColor.White;
    return;
}


var filterBuilder = new Ipv4FilterBuilder();
var filter = filterBuilder
    .AddCondition((options, ipModel) => options?.StartTime == null ? true : ipModel.RequestTime > options?.StartTime)
    .AddCondition((options, ipModel) => options?.EndTime == null ? true : ipModel.RequestTime < options?.EndTime)
    .AddCondition((options, ipModel) =>
    {
        var mergedBits = ipModel.IpAddressInt & options.AddressRangeUInt;
        return mergedBits == ipModel.IpAddressInt;
    })
    .Build();

var result = new ConcurrentDictionary<string, uint>();
using (var streamReader = new StreamReader(options.InputFilePath))
{
    while (streamReader.Peek() > -1)
    {
        var line = await streamReader.ReadLineAsync();
        if (line == null)
        {
            continue;
        }

        var lineData = line.Split(' ');
        var ipAddressString = lineData[0];
        var dateTimeString = lineData[1];
        if (lineData.Length < 2)
        {
            continue;
        }

        var ipModel = new IpModel(ipAddressString, dateTimeString);
        if (ipModel.IsValid && filter.Execute(ipModel, options))
        {
            result.AddOrUpdate(ipAddressString, 1, (ip, value) => ++value);
        }
    }
}

var logModel = new Ipv4LogResultModel
{
    FilterOptios = options,
    Logs = new LinkedList<RequestCount>()
};

logModel.Logs = result.Select((kvp) => new RequestCount { IpAddress = kvp.Key, Count = kvp.Value });

var jsonResult = JsonSerializer.Serialize(logModel, new JsonSerializerOptions()
{
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
});

using (StreamWriter sw = File.CreateText(options.OutputFilePath))
{
    sw.Write(jsonResult);
}


