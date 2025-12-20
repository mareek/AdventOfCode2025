namespace AdventOfCode2025.Days;

internal class Day11(bool real) : Day(real)
{
    private const string You = "you";
    private const string Svr = "svr";
    private const string Dac = "dac";
    private const string Fft = "fft";
    private const string Out = "out";

    public override int DayDate => 11;

    public override string ExecuteFirst()
    {
        var devices = Lines.Select(Device.Parse).ToDictionary(d => d.Name);

        var routes = GetAllPossibleRoutes(devices, You, Out);

        return routes.Count.ToString();
    }

    public override string ExecuteSecond()
    {
        var lines = Real ? Lines : ReadLines(Real, DayDate, 2);
        var devices = lines.Select(Device.Parse).ToDictionary(d => d.Name);

        var svrToDacRoutes = GetAllPossibleRoutes(devices, Svr, Dac);
        var dacToFftRoutes = svrToDacRoutes.Any() ? GetAllPossibleRoutes(devices, Dac, Fft) : [];
        var fftToOutRoutes = dacToFftRoutes.Any() ? GetAllPossibleRoutes(devices, Fft, Out) : [];

        var svrToDacToFftToOutCount = svrToDacRoutes.Count * dacToFftRoutes.Count * fftToOutRoutes.Count;

        var svrToFftRoutes = GetAllPossibleRoutes(devices, Svr, Fft);
        var fftToDacRoutes = svrToFftRoutes.Any() ? GetAllPossibleRoutes(devices, Fft, Dac) : [];
        var dacToOutRoutes = svrToFftRoutes.Any() ? GetAllPossibleRoutes(devices, Dac, Out) : [];

        var svrToFftToDacToOutCount = svrToFftRoutes.Count * fftToDacRoutes.Count * dacToOutRoutes.Count;

        return (svrToDacToFftToOutCount + svrToFftToDacToOutCount).ToString();
    }

    private List<HashSet<string>> GetAllPossibleRoutes(Dictionary<string, Device> devices, string start, string finish)
    {
        List<HashSet<string>> routes = [];

        Recurse(devices[start], [start], finish);
        return routes;

        void Recurse(Device device, HashSet<string> previousDevices, string finish)
        {

            foreach (var next in device.NextDevices)
            {
                if (next == finish)
                {
                    routes.Add(previousDevices);
                    return;
                }
                if (next == Out || previousDevices.Contains(next))
                    return;

                Recurse(devices[next], [.. previousDevices, next], finish);
            }
        }
    }




    private class Device
    {
        public required string Name { get; init; }
        public required string[] NextDevices { get; init; }

        public static Device Parse(string input)
        {
            // ccc: ddd eee fff

            var splitSemicolon = input.Split(':', StringSplitOptions.TrimEntries);
            return new() { Name = splitSemicolon[0], NextDevices = splitSemicolon[1].Split(' ') };
        }
    }
}