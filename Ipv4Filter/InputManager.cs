using Ipv4Filter.Models;

namespace Ipv4Filter
{
    public class InputManager
    {
        private static int startAddress = 0;
        private static int endAddress = 32;
        private static string inputFilePath = "";
        private static string outputFilePath = "./ipv4-filter-output.txt";
        private static DateTime? startTime = null;
        private static DateTime? endTime = null;

        public static Options GetOptions(string[] args)
        {
            for (int i = 0; i < args.Length - 1; i += 2)
            {
                var param = args[i];
                var arg = args[i + 1];
                switch (param)
                {
                    case "--file-input":
                        inputFilePath = arg;
                        if (!File.Exists(inputFilePath))
                        {
                            throw new ArgumentException("Input file does not exist.");
                        }
                        break;

                    case "--file-output":
                        outputFilePath = arg;
                        if (string.IsNullOrWhiteSpace(outputFilePath))
                        {
                            throw new ArgumentException("Output file path is empty.");
                        }

                        var directoryPath = Path.GetDirectoryName(outputFilePath);
                        if (!Directory.Exists(directoryPath))
                        {
                            throw new ArgumentException("Output file path does not exist.");
                        }

                        var fileName = Path.GetFileName(directoryPath);
                        if (string.IsNullOrEmpty(fileName))
                        {
                            throw new ArgumentException("Output file name is empty.");
                        }
                        break;

                    case "--from-time":
                        var validFromTime = DateTime.TryParse(arg, out var fromTime);
                        if (!validFromTime)
                        {
                            throw new ArgumentException($"Invalid {param}.");
                        }
                        startTime = fromTime;
                        break;

                    case "--to-time":
                        var validToTime = DateTime.TryParse(arg, out var toTime);
                        if (!validToTime)
                        {
                            throw new ArgumentException($"Invalid {param}.");
                        }
                        endTime = toTime;
                        break;

                    case "--address-start":
                        var validStartAddress = int.TryParse(arg, out startAddress);
                        if (!validStartAddress || startAddress < 0 || startAddress > 32)
                        {
                            throw new ArgumentException($"{param} must be number in range 1 to 32.");
                        }
                        break;

                    case "--address-end":
                        var validEndAddress = int.TryParse(arg, out endAddress);
                        if (!validEndAddress || endAddress < 0 || endAddress > 32)
                        {
                            throw new ArgumentException($"{param} must be number in range 0 to 32.");
                        }
                        break;

                    default: throw new ArgumentException($"Unknown argument: {param}.");
                }
            }

            if (string.IsNullOrEmpty(inputFilePath))
            {
                throw new ArgumentException($"--file-input is missing.");
            }

            if (startAddress > endAddress)
            {
                throw new ArgumentException($"Start address must be less than or equal to end address.");
            }

            if (startTime != null && startTime > endTime)
            {
                throw new ArgumentException($"--from-time must be less than --to-time.");
            }

            return new Options
            {
                InputFilePath = inputFilePath,
                OutputFilePath = outputFilePath,
                StartTime = startTime,
                EndTime = endTime,
                AddressRangeUInt = Utils.GetIPRange(startAddress, endAddress, out string startIp, out string endIp),
                FromIpAddress = startIp,
                ToIpAddress = endIp,
            };
        }
    }
}
