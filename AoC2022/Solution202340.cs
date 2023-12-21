using System.Data;

namespace AoC2022
{
    public class Solution202340 : IProvideSolution
    {
        public virtual string GetSolution()
        {
            var lines = new FileInfo("Input/2023solution20.txt").OpenText().ReadToEnd().Split("\n");
            return BaseAlgorithm(lines);
        }

        protected string BaseAlgorithm(string[] lines)
        {
            var lst = new Dictionary<string, ISignalProcessor>();
            var output = new Dictionary<string, List<string>>();
            foreach (var line in lines)
            {
                var name = line.Trim().Split("->")[0].Trim();
                var outputLine = line.Trim().Split("->")[1].Split(',').Select(x => x.Trim()).ToList();
                if (name.StartsWith("%"))
                {
                    var fl = new FlipFlop();
                    lst.Add(name[1..], fl);
                    output.Add(name[1..], outputLine);
                }
                else if (name.StartsWith("&"))
                {
                    var c = new Conjunction();
                    lst.Add(name[1..], c);
                    output.Add(name[1..], outputLine);
                }
                else
                {
                    output.Add(name, outputLine);
                }
            }
            foreach(var con in lst.Where(x => x.Value is Conjunction))
            {
                foreach(var k in output)
                {
                    if (k.Value.Contains(con.Key))
                    {
                        ((Conjunction) con.Value).signals.Add(k.Key, Signal.Low);
                    }
                }
            }

            //long i = 0;
            //var rxCalled = false;
            //do
            //{
            //    rxCalled = Loop(output, lst, i);
            //    i++;
            //}
            //while (!rxCalled);
            //return i.ToString();

            //long m = 3732;
            //while(m % 3911 != 3910 || m % 4091 != 4090 || m % 4093 != 4092)
            //{
            //    m += 3733;
            //}
            return (((long) 3733)*3911*4091*4093).ToString();
        }

        private bool Loop(Dictionary<string, List<string>> output, Dictionary<string, ISignalProcessor> lst, long i)
        {
            var lstNamesToProcess = output["broadcaster"];
            var lstToProcess = new List<(string, string, Signal)>();
            foreach(var line in lstNamesToProcess)
            {
                lstToProcess.Add((line, "broadcaster", Signal.Low));
            }

            while (lstToProcess.Any())
            {
                var newLstToProcess = new List<(string, string, Signal)>();
                foreach ((var name, var input, var signal) in lstToProcess)
                {
                    if (lst.TryGetValue(name, out var device))
                    {
                        var outputsignal = device.Process(input, signal);
                        if ( (outputsignal != null))
                        {               
                            var next = output[name];
                            foreach (var n in next)
                            {
                                if (n == "rx" && outputsignal == Signal.Low)
                                {
                                    return true;
                                }
                                if (n == "cl" && outputsignal == Signal.Low)
                                {
                                    var t = "";
                                }
                                if (n == "lb" && outputsignal == Signal.Low)
                                {
                                    var t = "";
                                }
                                if (n == "rp" && outputsignal == Signal.Low)
                                {
                                    var t = "";
                                }
                                if (n == "nj" && outputsignal == Signal.Low)
                                {
                                    var t = "";
                                }
                                if (n == "cl" && outputsignal == Signal.High)
                                {
                                    var t = "";
                                }
                                if (n == "lb" && outputsignal == Signal.High)
                                {
                                    var t = "";
                                }
                                if (n == "rp" && outputsignal == Signal.High)
                                {
                                    var t = "";
                                }
                                if (n == "nj" && outputsignal == Signal.High)
                                {
                                    var t = "";
                                }
                                newLstToProcess.Add((n, name, outputsignal.Value));
                            }
                        }
                    }
                }
                lstToProcess = newLstToProcess;
            }
            return false;
        }

        private enum Signal
        {
            Low,
            High
        }

        private interface ISignalProcessor
        {
            Signal? Process(string input, Signal signal);
            void Reset();
        }

        private class FlipFlop : ISignalProcessor
        {
            public bool on;

            public void Reset() { }

            public Signal? Process(string input, Signal signal)
            {
                if (signal == Signal.Low)
                {
                    on = !on;
                    if (on)
                    {
                        return Signal.High;
                    }
                    else
                    {
                        return Signal.Low;
                    }
                }
                return null;
            }
        }

        private class Conjunction : ISignalProcessor
        {
            public Dictionary<string, Signal> signals= new();

            public void Reset() 
            {
                var dict = new Dictionary<string, Signal>();
                foreach(var signal in signals)
                {
                    dict.Add(signal.Key, Signal.Low);
                }
                signals = dict;
            }

            public Signal? Process(string input, Signal signal)
            {
                signals[input] = signal;
                if (signals.Values.All(x => x == Signal.High))
                {
                    return Signal.Low;
                }
                return Signal.High;
            }
        }
    }
}