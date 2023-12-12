using KnowledgePool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Dynamic;
using System.Drawing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Runtime.CompilerServices;
using NuGet.Common;
using Newtonsoft.Json;
using System.Reflection;
using System.Reflection.Emit;

namespace KnowledgePool.Controllers
{
    public class DataViewerController : Controller
    {
        private readonly AllPrintingsContext _context;

        public DataViewerController(AllPrintingsContext context)
        {
            _context= context;
        }

        public ActionResult Index(List<string> setIds, string dataType, string singleChart = "false")
        {
            var isSingleChart = singleChart == "true";
            var isAverage = dataType?.Equals("average", StringComparison.OrdinalIgnoreCase) ?? true;
            var sets = new List<Set>();

            sets = _context.Sets
                .Where(_ => _.Type == "expansion" || _.Type == "core")
                .OrderByDescending(_ => _.ReleaseDate)
                .ToList();

            var setListSelection = new List<SelectListItem>();
            foreach (var s in sets)
            {
                setListSelection.Add(new SelectListItem { Text = s.Name + $" ({s.Code})", Value = s.Code });
            }

            var setCodes = setIds;

            if (!setCodes.Any())
                    setCodes.Add(_context.Sets
                        .Where(_ => _.Type == "expansion" || _.Type == "core")
                        .OrderByDescending(_ => _.ReleaseDate).Select(_ => _.Code)
                        .First());

            var powerData = new Dictionary<string, string>();
            var toughnessData = new Dictionary<string, string>();
            var scatterData = new Dictionary<string, string>();

            if (!isSingleChart)
            {
                foreach (var setCode in setCodes)
                {
                    var cards = _context.Cards.AsEnumerable().Where(_ => _.SetCode == setCode).DistinctBy(_ => _.Name).ToList();

                    var creatures = cards
                        .Where(_ =>
                        _.Type.Contains("creature", StringComparison.OrdinalIgnoreCase) && 
                        !_.Power.Contains("*") && 
                        !_.Toughness.Contains("*") && 
                        !(_.Power == "0" || _.Toughness == "0"));

                    var amd = GetAverageMedianDataByColor(creatures);

                    if (isAverage)
                    {
                        var averagePowers = new Dictionary<string, List<double>>(amd.Select(_ => new KeyValuePair<string, List<double>>
                            (
                                _.Key,
                                _.Value.PowerAverages.ToList()
                            )));

                        var averageToughnesses = new Dictionary<string, List<double>>(amd.Select(_ => new KeyValuePair<string, List<double>>
                            (
                                _.Key,
                                _.Value.ToughnessAverages.ToList()
                            )));

                        powerData.Add(setCode, GetAverageOrMedianJsonByColor(averagePowers, "Power", setCode));
                        toughnessData.Add(setCode, GetAverageOrMedianJsonByColor(averageToughnesses, "Toughness", setCode));
                    }

                    else
                    {
                        var medianPowers = new Dictionary<string, List<int>>(amd.Select(_ => new KeyValuePair<string, List<int>>
                        (
                            _.Key,
                            _.Value.PowerMedians.ToList()
                        )));

                        var medianToughnesses = new Dictionary<string, List<int>>(amd.Select(_ => new KeyValuePair<string, List<int>>
                        (
                            _.Key,
                            _.Value.ToughnessMedians.ToList()
                        )));

                        powerData.Add(setCode, GetAverageOrMedianJsonByColor(medianPowers, "Power", setCode));
                        toughnessData.Add(setCode, GetAverageOrMedianJsonByColor(medianToughnesses, "Toughness", setCode));
                    }

                    scatterData.Add(setCode, GetBubbleJson(setCode));
                } 
            }

            else
            {
                var cards = _context.Cards.AsEnumerable().Where(_ => setCodes.Contains(_.SetCode)).DistinctBy(_ => _.Name).ToList();

                var creatures = cards
                    .Where(_ =>
                    _.Type.Contains("creature", StringComparison.OrdinalIgnoreCase) &&
                        !_.Power.Contains("*") &&
                        !_.Toughness.Contains("*") &&
                        !(_.Power == "0" || _.Toughness == "0"));

                var amd = GetAverageMedianDataBySet(creatures, setCodes);

                if (isAverage)
                {
                    var averagePowers = new Dictionary<string, List<double>>(amd.Select(_ => new KeyValuePair<string, List<double>>
                        (
                            _.Key,
                            _.Value.PowerAverages.ToList()
                        )));

                    var averageToughnesses = new Dictionary<string, List<double>>(amd.Select(_ => new KeyValuePair<string, List<double>>
                        (
                            _.Key,
                            _.Value.ToughnessAverages.ToList()
                        )));

                    powerData.Add("POWER", GetAverageOrMedianJsonBySet(averagePowers, "Power"));
                    toughnessData.Add("TOUGHNESS", GetAverageOrMedianJsonBySet(averageToughnesses, "Toughness"));
                }

                else
                {
                    var medianPowers = new Dictionary<string, List<int>>(amd.Select(_ => new KeyValuePair<string, List<int>>
                    (
                        _.Key,
                        _.Value.PowerMedians.ToList()
                    )));

                    var medianToughnesses = new Dictionary<string, List<int>>(amd.Select(_ => new KeyValuePair<string, List<int>>
                    (
                        _.Key,
                        _.Value.ToughnessMedians.ToList()
                    )));

                    powerData.Add("POWER", GetAverageOrMedianJsonBySet(medianPowers, "Power"));
                    toughnessData.Add("TOUGHNESS", GetAverageOrMedianJsonBySet(medianToughnesses, "Toughness"));
                }
            }

            ViewBag.setList = setListSelection;

            ViewData["PowerData"] = powerData;
            ViewData["ToughnessData"] = toughnessData;
            ViewData["ScatterData"] = scatterData;

            return View();
        }

        public string GetAverageOrMedianJsonBySet<T>(Dictionary<string, List<T>> data, string PorT)
        {
            var labelType = typeof(T) == typeof(double) ? "Average" : "Median";

            return JsonConvert.SerializeObject(new
            {
                type = "line",
                data = new
                {
                    labels = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7+" },
                    datasets = data.Select(_ => new
                    {
                        label = _.Key,
                        data = _.Value,
                    })
                },

                options = new
                {
                    scales = new
                    {
                        x = new
                        {
                            title = new
                            {
                                display = true,
                                text = "Mana Value"
                            }
                        },
                        y = new
                        {
                            title = new
                            {
                                display = true,
                                text = PorT
                            }
                        }
                    },

                    plugins = new
                    {
                        title = new
                        {
                            display = true,
                            text = $"{labelType} {PorT} by Mana Cost",
                            font = new
                            {
                                weight = "bold",
                                size = 18
                            }
                        }
                    }
                }
            });
        }

        public string GetAverageOrMedianJsonByColor<T>(Dictionary<string, List<T>> data, string PorT, string setCode)
        {
            var labelType = typeof(T) == typeof(double) ? "Average" : "Median";

            var setName = _context.Sets.First(_ => _.Code == setCode).Name;

            return JsonConvert.SerializeObject(new
            {
                type = "line",
                data = new
                {
                    labels = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7+" },
                    datasets = new[]
                    {
                        new
                        {
                            label = "White",
                            data = data["W"],
                            borderColor = Colors.White,
                            backgroundColor = Colors.White,
                            hidden = true
                        },

                        new
                        {
                            label = "Blue",
                            data = data["U"],
                            borderColor = Colors.Blue,
                            backgroundColor = Colors.Blue,
                            hidden = true
                        },

                        new
                        {
                            label = "Black",
                            data = data["B"],
                            borderColor = Colors.Black,
                            backgroundColor = Colors.Black,
                            hidden = true
                        },

                        new
                        {
                            label = "Red",
                            data = data["R"],
                            borderColor = Colors.Red,
                            backgroundColor = Colors.Red,
                            hidden = true
                        },

                        new
                        {
                            label = "Green",
                            data = data["G"],
                            borderColor = Colors.Green,
                            backgroundColor = Colors.Green,
                            hidden = true
                        },

                        new
                        {
                            label = "Gold",
                            data = data["M"],
                            borderColor = Colors.Gold,
                            backgroundColor = Colors.Gold,
                            hidden = true
                        },

                        new
                        {
                            label = "Colorless",
                            data = data["C"],
                            borderColor = Colors.Colorless,
                            backgroundColor = Colors.Colorless,
                            hidden = true
                        },

                        new
                        {
                            label = "All Creatures",
                            data = data["O"],
                            borderColor = Colors.All,
                            backgroundColor = Colors.All,
                            hidden = false
                        }
                    }
                },

                options = new
                {
                    scales = new
                    {
                        x = new
                        {
                            title = new
                            {
                                display = true,
                                text = "Mana Value"
                            }
                        },
                        y = new
                        {
                            title = new
                            {
                                display = true,
                                text = PorT
                            }
                        }
                    },

                    plugins = new
                    {
                        title = new
                        {
                            display = true,
                            text = $"{labelType} {PorT} by Mana Cost: {setName}",
                            font = new
                            {
                                weight = "bold",
                                size = 18
                            }
                        }
                    }
                }
            });
        }

        public Dictionary<string, AverageMedianData> GetAverageMedianDataBySet(IEnumerable<Card> creatures, List<string> setCodes)
        {
            var data = new Dictionary<string, AverageMedianData>();

            foreach (var code in setCodes)
            {
                var powerValues = new List<KeyValuePair<int, int>>();
                var toughnessValues = new List<KeyValuePair<int, int>>();

                powerValues = creatures
                    .Where(_ => _.SetCode == code)
                    .Select(_ => new KeyValuePair<int, int>(
                        (int)_.ManaValue,
                        Int32.Parse(_.Power)))
                    .ToList();

                toughnessValues = creatures
                    .Where(_ => _.SetCode == code)
                    .Select(_ => new KeyValuePair<int, int>(
                        (int)_.ManaValue,
                        Int32.Parse(_.Toughness)))
                    .ToList();

                var powerAverages = new List<double>();
                var powerMedians = new List<int>();

                var toughnessAverages = new List<double>();
                var toughnessMedians = new List<int>();

                foreach (var mv in new List<int>(Enumerable.Range(0, 8)))
                {
                    var powerValuesByMv = new List<int>();
                    var toughnessValuesByMv = new List<int>();

                    if (mv == 7)
                    {
                        powerValuesByMv = powerValues
                            .Where(_ => _.Key >= mv)
                            .Select(_ => _.Value)
                            .OrderBy(_ => _)
                            .ToList();

                        toughnessValuesByMv = toughnessValues
                            .Where(_ => _.Key >= mv)
                            .Select(_ => _.Value)
                            .OrderBy(_ => _)
                            .ToList();
                    }

                    else
                    {
                        powerValuesByMv = powerValues
                            .Where(_ => _.Key == mv)
                            .Select(_ => _.Value)
                            .OrderBy(_ => _)
                            .ToList();

                        toughnessValuesByMv = toughnessValues
                            .Where(_ => _.Key == mv)
                            .Select(_ => _.Value)
                            .OrderBy(_ => _)
                            .ToList();
                    }

                    if (powerValuesByMv.Any())
                    {
                        powerAverages.Add(powerValuesByMv.Average());
                        powerMedians.Add(powerValuesByMv.ElementAt(powerValuesByMv.Count() / 2));

                        toughnessAverages.Add(toughnessValuesByMv.Average());
                        toughnessMedians.Add(toughnessValuesByMv.ElementAt(toughnessValuesByMv.Count() / 2));
                    }

                    else
                    {
                        powerAverages.Add(0);
                        powerMedians.Add(0);

                        toughnessAverages.Add(0);
                        toughnessMedians.Add(0);
                    }
                }

                var amd = new AverageMedianData
                {
                    PowerAverages = powerAverages,
                    PowerMedians = powerMedians,
                    ToughnessAverages = toughnessAverages,
                    ToughnessMedians = toughnessMedians
                };

                data.Add(code, amd);
            }

            return data;
        }

        public Dictionary<string, AverageMedianData> GetAverageMedianDataByColor(IEnumerable<Card> creatures)
        {
            var data = new Dictionary<string, AverageMedianData>();

            foreach (var color in new List<string> { "W", "U", "B", "R", "G", "M", "C", "O"})
            {
                var powerValues = new List<KeyValuePair<int, int>>();
                var toughnessValues = new List<KeyValuePair<int, int>>();

                if (color == "O")
                {
                    powerValues = creatures
                        .Select(_ => new KeyValuePair<int, int>(
                            (int) _.ManaValue,
                            Int32.Parse(_.Power)))
                        .ToList();

                    toughnessValues = creatures
                        .Select(_ => new KeyValuePair<int, int>(
                            (int)_.ManaValue,
                            Int32.Parse(_.Toughness)))
                        .ToList();
                }

                else if (color == "M")
                {
                    powerValues = creatures
                        .Where(_ => _.Colors.Length > 1)
                        .Select(_ => new KeyValuePair<int, int>(
                            (int)_.ManaValue,
                            Int32.Parse(_.Power)))
                        .ToList();

                    toughnessValues = creatures
                        .Where(_ => _.Colors.Length > 1)
                        .Select(_ => new KeyValuePair<int, int>(
                            (int)_.ManaValue,
                            Int32.Parse(_.Toughness)))
                        .ToList();
                }

                else if (color == "C")
                {
                    powerValues = creatures
                        .Where(_ => _.Colors == string.Empty)
                        .Select(_ => new KeyValuePair<int, int>(
                            (int)_.ManaValue,
                            Int32.Parse(_.Power)))
                        .ToList();

                    toughnessValues = creatures
                        .Where(_ => _.Colors == string.Empty)
                        .Select(_ => new KeyValuePair<int, int>(
                            (int)_.ManaValue,
                            Int32.Parse(_.Toughness)))
                        .ToList();
                }

                else
                {
                    powerValues = creatures
                        .Where(_ => _.Colors == color)
                        .Select(_ => new KeyValuePair<int, int>(
                            (int)_.ManaValue,
                            Int32.Parse(_.Power)))
                        .ToList();

                    toughnessValues = creatures
                        .Where(_ => _.Colors == color)
                        .Select(_ => new KeyValuePair<int, int>(
                            (int)_.ManaValue,
                            Int32.Parse(_.Toughness)))
                        .ToList();
                }

                var powerAverages = new List<double>();
                var powerMedians = new List<int>();

                var toughnessAverages = new List<double>();
                var toughnessMedians = new List<int>();

                foreach (var mv in new List<int>(Enumerable.Range(0, 8))) 
                {
                    var powerValuesByMv = new List<int>();
                    var toughnessValuesByMv = new List<int>();

                    if (mv == 7)
                    {
                        powerValuesByMv = powerValues
                            .Where(_ => _.Key >= mv)
                            .Select(_ => _.Value)
                            .OrderBy(_ => _)
                            .ToList();

                        toughnessValuesByMv = toughnessValues
                            .Where(_ => _.Key >= mv)
                            .Select(_ => _.Value)
                            .OrderBy(_ => _)
                            .ToList();
                    }

                    else
                    {
                        powerValuesByMv = powerValues
                            .Where(_ => _.Key == mv)
                            .Select(_ => _.Value)
                            .OrderBy(_ => _)
                            .ToList();

                        toughnessValuesByMv = toughnessValues
                            .Where(_ => _.Key == mv)
                            .Select(_ => _.Value)
                            .OrderBy(_ => _)
                            .ToList();
                    }

                    if (powerValuesByMv.Any())
                    {
                        powerAverages.Add(powerValuesByMv.Average());
                        powerMedians.Add(powerValuesByMv.ElementAt(powerValuesByMv.Count() / 2));

                        toughnessAverages.Add(toughnessValuesByMv.Average());
                        toughnessMedians.Add(toughnessValuesByMv.ElementAt(toughnessValuesByMv.Count() / 2));
                    }

                    else
                    {
                        powerAverages.Add(0);
                        powerMedians.Add(0);

                        toughnessAverages.Add(0);
                        toughnessMedians.Add(0);
                    }
                }

                var amd = new AverageMedianData
                {
                    PowerAverages = powerAverages,
                    PowerMedians = powerMedians,
                    ToughnessAverages = toughnessAverages,
                    ToughnessMedians = toughnessMedians
                };

                data.Add(color, amd);
            }

            return data;
        }

        public string GetBubbleJson(string? setCode)
        {
            if (setCode is null) setCode = _context.Sets.Where(_ => _.Type == "expansion" || _.Type == "core").OrderByDescending(_ => _.ReleaseDate).Select(_ => _.Code).First();

            var setName = _context.Sets.First(_ => _.Code == setCode).Name;

            var creatures = _context.Cards
                .Where(_ => _.SetCode == setCode && EF.Functions.Like(_.Type, "%creature%") && !EF.Functions.Like(_.Power, "%*%") && !EF.Functions.Like(_.Toughness, "%*%"));

            var creaturesDict = new Dictionary<string, List<ScatterDataPoint>>();

            var creaturesAll = creatures
                .GroupBy(_ => new { _.Power, _.Toughness })
                .Select(_ => new ScatterDataPoint
                {
                    x = _.Key.Power,
                    y = _.Key.Toughness,
                    r = _.Count()
                })
                .ToList();

            creaturesDict.Add("O", creaturesAll);

            foreach (var color in new List<string> { "W", "U", "B", "R", "G", "M", "C" })
            {
                if (color == "M")
                {
                    var creatureData = creatures
                        .Where(_ => _.Colors.Length > 1)
                        .GroupBy(_ => new { _.Power, _.Toughness })
                        .Select(_ => new ScatterDataPoint
                        {
                            x = _.Key.Power,
                            y = _.Key.Toughness,
                            r = _.Count()
                        })
                        .ToList();

                    creaturesDict.Add(color, creatureData);
                }

                else if (color == "C")
                {
                    var creatureData = creatures
                        .Where(_ => _.Colors == string.Empty)
                        .GroupBy(_ => new { _.Power, _.Toughness })
                        .Select(_ => new ScatterDataPoint
                        {
                            x = _.Key.Power,
                            y = _.Key.Toughness,
                            r = _.Count()
                        })
                        .ToList();

                    creaturesDict.Add(color, creatureData);
                }

                else
                {
                    var creatureData = creatures
                        .Where(_ => _.Colors == color)
                        .GroupBy(_ => new { _.Power, _.Toughness })
                        .Select(_ => new ScatterDataPoint
                        {
                            x = _.Key.Power,
                            y = _.Key.Toughness,
                            r = _.Count()
                        })
                        .ToList();

                    creaturesDict.Add(color, creatureData);
                }
            }


            return JsonConvert.SerializeObject(new
            {
                type = "bubble",
                data = new
                {
                    datasets = new[]
                    {
                        new
                        {
                            label = "White",
                            data = creaturesDict["W"],
                            hidden = true,
                            borderColor = Colors.Black,
                            backgroundColor = Colors.White + "80"
                        },

                        new
                        {
                            label = "Blue",
                            data = creaturesDict["U"],
                            hidden = true,
                            borderColor = Colors.Black,
                            backgroundColor = Colors.Blue + "80"
                        },
                        new
                        {
                            label = "Black",
                            data = creaturesDict["B"],
                            hidden = true,
                            borderColor = Colors.Black,
                            backgroundColor = Colors.Black + "80"
                        },

                        new
                        {
                            label = "Red",
                            data = creaturesDict["R"],
                            hidden = true,
                            borderColor = Colors.Black,
                            backgroundColor = Colors.Red + "80"
                        },

                        new
                        {
                            label = "Green",
                            data = creaturesDict["G"],
                            hidden = true,
                            borderColor = Colors.Black,
                            backgroundColor = Colors.Green + "80"
                        },

                        new
                        {
                            label = "Multicolor",
                            data = creaturesDict["M"],
                            hidden = true,
                            borderColor = "#000000",
                            backgroundColor = Colors.Gold + "80"
                        },

                        new
                        {
                            label = "Colorless",
                            data = creaturesDict["C"],
                            hidden = true,
                            borderColor = Colors.Black,
                            backgroundColor = Colors.Colorless + "80"
                        },

                        new
                        {
                            label = "All Creatures",
                            data = creaturesDict["O"],
                            hidden = false,
                            borderColor = Colors.Black,
                            backgroundColor = Colors.All + "80"

                        }
                    }
                },
                options = new
                {
                    scales = new
                    {
                        x = new
                        {
                            type = "linear",
                            position = "bottom",
                            min = 0,
                            max = 15,
                            ticks = new
                            {
                                autoSkip = false,
                                maxTicksLimit = 16
                            },

                            title = new
                            {
                                display = true,
                                text = "Power"
                            }
                        },

                        y = new
                        {
                            min = 0,
                            max = 15,
                            ticks = new
                            {
                                autoSkip = false,
                                maxTicksLimite = 16
                            },

                            title = new
                            {
                                display = true,
                                text = "Toughness"
                            }
                        }
                    },

                    plugins = new
                    {
                        title = new
                        {
                            display = true,
                            text = $"Power and Toughness Bubble Map: {setName}",
                            font = new
                            {
                                weight = "bold",
                                size = 18
                            }
                        }
                    },

                    elements = new
                    {
                        point = new
                        {
                            rmin = 20
                        }
                    }
                }
            });
        }

    }

    public class ScatterDataPoint
    {
        public string? x { get; set; }
        public string? y { get; set; }
        public int r { get; set; }
    }

    public class AverageMedianData
    {
        public IEnumerable<int>? PowerMedians { get; set; }
        public IEnumerable<double>? PowerAverages { get; set; }
        public IEnumerable<int>? ToughnessMedians { get; set; }
        public IEnumerable<double>? ToughnessAverages { get; set; }
    }

    public static class Colors
    {
        public const string White = "#E0E84A";
        public const string Blue = "#2800FE";
        public const string Black = "#000000";
        public const string Red = "#FF0000";
        public const string Green = "#008C28";
        public const string Gold = "#FC7703";
        public const string Colorless = "#705336";
        public const string All = "#A60092";
    }
}
