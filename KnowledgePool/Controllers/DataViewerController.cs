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

        public ActionResult Index(string setList, string searchString)
        {
            var sets = new List<Set>();

            if (searchString is not null)
            {
                sets = _context.Sets
                    .Where(_ => (EF.Functions.Like(_.Name, $"%{searchString}%") || EF.Functions.Like(_.Name, $"%{searchString}%")) && (_.Type == "expansion" || _.Type == "core"))
                    .OrderByDescending(_ => _.ReleaseDate)
                    .ToList();

            }

            else
            {
                sets = _context.Sets
                    .Where(_ => _.Type == "expansion" || _.Type == "core")
                    .OrderByDescending(_ => _.ReleaseDate)
                    .ToList();
            }

            var setListSelection = new List<SelectListItem>();
            foreach (var set in sets)
            {
                setListSelection.Add(new SelectListItem { Text = set.Name + $" ({set.Code})", Value = set.Code });
            }

            ViewBag.setList = setListSelection;

            var cards = new List<Card>();

            if (setList is not null) cards = _context.Cards.AsEnumerable().Where(_ => _.SetCode == setList).DistinctBy(_ => _.Name).ToList();

            else
            {
                var latestSetCode = sets.Select(_ => _.Code).First();

                cards = _context.Cards.AsEnumerable().Where(_ => _.SetCode ==  latestSetCode).DistinctBy(_ => _.Name).ToList();
            }

            var cardStats = new CardStats(cards);

            ViewData["CreatureCounts"] = cardStats.CreatureCount;
            ViewData["CardAverages"] = cardStats.Averages;
            ViewData["CardMedians"] = cardStats.Medians;

            var avgData1 = cardStats.Averages.Where(_ => _.Key.Item1 == "O").Select(_ => _.Value.Item1);
            var avgData2 = cardStats.Averages.Where(_ => _.Key.Item1 == "O").Select(_ => _.Value.Item2);

            var medData1 = cardStats.Medians.Where(_ => _.Key.Item1 == "O").Select(_ => _.Value.Item1);
            var medData2 = cardStats.Medians.Where(_ => _.Key.Item1 == "O").Select(_ => _.Value.Item2);

            var setCode = setList;
            if (setCode is null) setCode = _context.Sets
                    .Where(_ => _.Type == "expansion" || _.Type == "core")
                    .OrderByDescending(_ => _.ReleaseDate).Select(_ => _.Code)
                    .First();

            var creatures = cards
                .Where(_ => _.SetCode == setCode 
                    && _.Type.Contains("creature", StringComparison.OrdinalIgnoreCase) 
                    && !_.Power.Contains("*") 
                    && !_.Toughness.Contains("*")
                    && !(_.Power == "0" || _.Toughness == "0"));


            ViewData["TestAverage"] = GetAverageOrMedianJson<double>(GetAverageMedianData(creatures));
            ViewData["OverallAverage"] = GetAverageOrMedianJson(avgData1, avgData2);
            ViewData["OverallMedian"] = GetAverageOrMedianJson(medData1, medData2);
            ViewData["ScatterData"] = GetBubbleJson(setList);

            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }

        //public string GetPowerJson(string? setCode, bool isAverage = true)
        //{
        //    var labelType = isAverage ? "Average" : "Median";

        //    if (setCode is null) setCode = _context.Sets.Where(_ => _.Type == "expansion" || _.Type == "core").OrderByDescending(_ => _.ReleaseDate).Select(_ => _.Code).First();

        //    var creatures = _context.Cards
        //        .Where(_ => _.SetCode == setCode && EF.Functions.Like(_.Type, "%creature%") && !EF.Functions.Like(_.Power, "%*%") && !EF.Functions.Like(_.Toughness, "%*%"));



        //    return string.Empty;
        //}

        public string GetAverageOrMedianJson<T>(Dictionary<string, AverageMedianData> amd)
        {
            var x = JsonConvert.SerializeObject(new
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
                            data = amd["W"].Averages,
                            borderColor = Colors.White,
                            hidden = true
                        },

                        new
                        {
                            label = "Blue",
                            data = amd["U"].Averages,
                            borderColor = Colors.Blue,
                            hidden = true
                        },

                        new
                        {
                            label = "Black",
                            data = amd["B"].Averages,
                            borderColor = Colors.Black,
                            hidden = true
                        },

                        new
                        {
                            label = "Red",
                            data = amd["R"].Averages,
                            borderColor = Colors.Red,
                            hidden = true
                        },

                        new
                        {
                            label = "Green",
                            data = amd["G"].Averages,
                            borderColor = Colors.Green,
                            hidden = true
                        },

                        new
                        {
                            label = "Gold",
                            data = amd["M"].Averages,
                            borderColor = Colors.Gold,
                            hidden = true
                        },

                        new
                        {
                            label = "Colorless",
                            data = amd["C"].Averages,
                            borderColor = Colors.Colorless,
                            hidden = true
                        },

                        new
                        {
                            label = "All Creatures",
                            data = amd["O"].Averages,
                            borderColor = Colors.All,
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
                                text = "Power"
                            }
                        }
                    },

                    plugins = new
                    {
                        title = new
                        {
                            display = true,
                            text = "Power by Mana Cost",
                            font = new
                            {
                                weight = "bold",
                                size = 18
                            }
                        }
                    }
                }
            });

            return x;
        }

        public string GetAverageOrMedianJson<T>(IEnumerable<T> data1, IEnumerable<T> data2)
        {
            var labelType = data1 is IEnumerable<double> ? "Average" : "Median";

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
                            label = "Power",
                            data = data1,
                            borderColor = "#36A2EB"
                        },

                        new
                        {
                            label = "Toughness",
                            data = data2,
                            borderColor = "#FF6384"
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
                                text = "Power or Toughness"
                            }
                        }
                    },

                    plugins = new
                    {
                        title = new
                        {
                            display = true,
                            text = $"{labelType} Power and Toughness by Mana Cost",
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

        public string GetBubbleJson(string? setCode)
        {
            if (setCode is null) setCode = _context.Sets.Where(_ => _.Type == "expansion" || _.Type == "core").OrderByDescending(_ => _.ReleaseDate).Select(_ => _.Code).First();

            var creatures = _context.Cards
                .Where(_ => _.SetCode == setCode && EF.Functions.Like(_.Type, "%creature%") && !EF.Functions.Like(_.Power, "%*%") && !EF.Functions.Like(_.Toughness, "%*%"));

            var creaturesDict = new Dictionary<string, List<ScatterDataPoint>>();

            var creaturesAll = creatures
                .GroupBy(_ => new {_.Power, _.Toughness})
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
                            text = $"Power and Toughness Bubble Map",
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

        public Dictionary<string, AverageMedianData> GetAverageMedianData(IEnumerable<Card> creatures, bool isPower = true)
        {
            var data = new Dictionary<string, AverageMedianData>();

            foreach (var color in new List<string> { "W", "U", "B", "R", "G", "M", "C", "O"})
            {
                var values = new List<KeyValuePair<int, int>>();

                if (color == "O")
                {
                    values = creatures
                        .Select(_ => new KeyValuePair<int, int>(
                            (int) _.ManaValue,
                            Int32.Parse(isPower ? _.Power : _.Toughness)))
                        .ToList();
                }

                else if (color == "M")
                {
                    values = creatures
                        .Where(_ => _.Colors.Length > 1)
                        .Select(_ => new KeyValuePair<int, int>(
                            (int)_.ManaValue,
                            Int32.Parse(isPower ? _.Power : _.Toughness)))
                        .ToList();
                }

                else if (color == "C")
                {
                    values = creatures
                        .Where(_ => _.Colors is null)
                        .Select(_ => new KeyValuePair<int, int>(
                            (int)_.ManaValue,
                            Int32.Parse(isPower ? _.Power : _.Toughness)))
                        .ToList();
                }

                else
                {
                    values = creatures
                        .Where(_ => _.Colors == color)
                        .Select(_ => new KeyValuePair<int, int>(
                            (int)_.ManaValue,
                            Int32.Parse(isPower ? _.Power : _.Toughness)))
                        .ToList();
                }

                var averages = new List<double>();
                var medians = new List<int>();

                foreach (var mv in new List<int>(Enumerable.Range(0, 8))) 
                {
                    var valuesByMv = new List<int>();

                    if (mv == 7)
                    {
                        valuesByMv = values
                            .Where(_ => _.Key >= mv)
                            .Select(_ => _.Value)
                            .OrderBy(_ => _)
                            .ToList();
                    }

                    else
                    {
                        valuesByMv = values
                            .Where(_ => _.Key == mv)
                            .Select(_ => _.Value)
                            .OrderBy(_ => _)
                            .ToList(); 
                    }

                    if (valuesByMv.Any())
                    {
                        averages.Add(valuesByMv.Average());
                        medians.Add(valuesByMv.ElementAt(valuesByMv.Count() / 2));
                    }

                    else
                    {
                        averages.Add(0);
                        medians.Add(0);
                    }
                }

                var amd = new AverageMedianData
                {
                    Averages = averages,
                    Medians = medians
                };

                data.Add(color, amd);
            }

            return data;
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
        public IEnumerable<int>? Medians { get; set; }
        public IEnumerable<double>? Averages { get; set; }
    }

    public static class Colors
    {
        public const string White = "#FDFF7C";
        public const string Blue = "#2800FE";
        public const string Black = "#000000";
        public const string Red = "#FF0000";
        public const string Green = "#008C28";
        public const string Gold = "#FC7703";
        public const string Colorless = "#705336";
        public const string All = "#A60092";
    }

    public class CardStats
    {
        //the key represents the color and mana value, the value represents count
        public Dictionary<Tuple<string, int>, int> CreatureCount { get; set; }

        //key is same as above, value is power and toughness
        public Dictionary<Tuple<string, int>, Tuple<double, double>> Averages { get; set; }
        public Dictionary<Tuple<string, int>, Tuple<int, int>> Medians { get; set; }

        public CardStats()
        {
            CreatureCount = new Dictionary<Tuple<string, int>, int>();

            Averages = new Dictionary<Tuple<string, int>, Tuple<double, double>>();
            Medians = new Dictionary<Tuple<string, int>, Tuple<int, int>>();

        }
        public CardStats(IEnumerable<Card> cards)
        {
            CreatureCount = new Dictionary<Tuple<string, int>, int>();
            Averages = new Dictionary<Tuple<string, int>, Tuple<double, double>>();
            Medians = new Dictionary<Tuple<string, int>, Tuple<int, int>>();

            var creatures = cards.Where(_ => _.Type.Contains("creature", StringComparison.OrdinalIgnoreCase));

            var colors = new List<string> { "W", "U", "B", "R", "G", "M", "C", "O" };

            foreach (var color in colors)
            {
                var mvs = new List<int>(Enumerable.Range(0, 8));

                foreach (var mv in mvs) AddStatTuples(color, creatures, mv);
            }
        }

        private void AddStatTuples(string color, IEnumerable<Card> creatures, int mv)
        {
            double PowerAvg = 0.0;
            double ToughnessAvg = 0.0;

            int PowerMedian = 0;
            int ToughnessMedian = 0;

            int creatureCount = 0;

            var filteredCreatures = creatures.Where(_ => !_.Power.Contains("*") && !_.Toughness.Contains("*") && (_.Power != "0" && _.Toughness != "0"));

            if (mv >= 0) filteredCreatures = filteredCreatures.Where(_ => mv >= 7 ? (int)_.ManaValue >= mv : (int)_.ManaValue == mv);

            if (color == "M")
            {
                var multicolorCreatures = filteredCreatures.Where(_ => _.Colors.Contains(","));

                if (multicolorCreatures.Any())
                {
                    PowerAvg = multicolorCreatures.Select(_ => Int32.Parse(_.Power)).Average();
                    ToughnessAvg = multicolorCreatures.Select(_ => Int32.Parse(_.Toughness)).Average();

                    creatureCount = multicolorCreatures.Count();

                    PowerMedian = multicolorCreatures.Select(_ => Int32.Parse(_.Power)).OrderBy(_ => _).ElementAt(creatureCount / 2);
                    ToughnessMedian = multicolorCreatures.Select(_ => Int32.Parse(_.Toughness)).OrderBy(_ => _).ElementAt(creatureCount / 2);
                }
            }

            if (color == "C")
            {
                var colorlessCreatures = filteredCreatures.Where(_ => _.Colors == string.Empty);

                if (colorlessCreatures.Any())
                {
                    PowerAvg = colorlessCreatures.Select(_ => Int32.Parse(_.Power)).Average();
                    ToughnessAvg = colorlessCreatures.Select(_ => Int32.Parse(_.Toughness)).Average();

                    creatureCount = colorlessCreatures.Count();

                    PowerMedian = colorlessCreatures.Select(_ => Int32.Parse(_.Power)).OrderBy(_ => _).ElementAt(creatureCount / 2);
                    ToughnessMedian = colorlessCreatures.Select(_ => Int32.Parse(_.Toughness)).OrderBy(_ => _).ElementAt(creatureCount / 2);
                }
            }

            if (color == "O")
            {
                if (filteredCreatures.Any())
                {
                    PowerAvg = filteredCreatures.Select(_ => Int32.Parse(_.Power)).Average();
                    ToughnessAvg = filteredCreatures.Select(_ => Int32.Parse(_.Toughness)).Average();

                    creatureCount = filteredCreatures.Count();

                    PowerMedian = filteredCreatures.Select(_ => Int32.Parse(_.Power)).OrderBy(_ => _).ElementAt(creatureCount / 2);
                    ToughnessMedian = filteredCreatures.Select(_ => Int32.Parse(_.Toughness)).OrderBy(_ => _).ElementAt(creatureCount / 2);
                }
            }

            else
            {
                var colorCreatures = filteredCreatures.Where(_ => _.Colors == color);

                if (colorCreatures.Any())
                {
                    PowerAvg = colorCreatures.Select(_ => Int32.Parse(_.Power)).Average();
                    ToughnessAvg = colorCreatures.Select(_ => Int32.Parse(_.Toughness)).Average();

                    creatureCount = colorCreatures.Count();

                    PowerMedian = colorCreatures.Select(_ => Int32.Parse(_.Power)).OrderBy(_ => _).ElementAt(creatureCount / 2);
                    ToughnessMedian = colorCreatures.Select(_ => Int32.Parse(_.Toughness)).OrderBy(_ => _).ElementAt(creatureCount / 2);
                }
            }

            var dictKey = new Tuple<string, int>(color, mv);

            var averageTuple = new Tuple<double, double>(PowerAvg, ToughnessAvg);
            var medianTuple = new Tuple<int, int>(PowerMedian, ToughnessMedian);

            CreatureCount.Add(dictKey, creatureCount);
            Averages.Add(dictKey, averageTuple);
            Medians.Add(dictKey, medianTuple);

        }

        private Tuple<double, double> StatAverageTuple(string color, IEnumerable<Card> creatures)
        {
            double PowerAvg = 0.0;
            double ToughnessAvg = 0.0;

            var filteredCreatures = creatures.Where(_ => !_.Power.Contains("*") && !_.Toughness.Contains("*"));

            if (color == "M")
            {
                var multicolorCreatures = filteredCreatures.Where(_ => _.Colors.Contains(","));

                if (multicolorCreatures.Any())
                {
                    PowerAvg = multicolorCreatures.Select(_ => Int32.Parse(_.Power)).Average();
                    ToughnessAvg = multicolorCreatures.Select(_ => Int32.Parse(_.Toughness)).Average();
                }
            }

            if (color == "C")
            {
                var colorlessCreatures = filteredCreatures.Where(_ => _.Colors == string.Empty);

                if (colorlessCreatures.Any())
                {
                    PowerAvg = colorlessCreatures.Select(_ => Int32.Parse(_.Power)).Average();
                    ToughnessAvg = colorlessCreatures.Select(_ => Int32.Parse(_.Toughness)).Average();
                }
            }

            else
            {
                var colorCreatures = filteredCreatures.Where(_ => _.Colors == color);

                if (colorCreatures.Any())
                {
                    PowerAvg = filteredCreatures.Select(_ => Int32.Parse(_.Power)).Average();
                    ToughnessAvg = filteredCreatures.Select(_ => Int32.Parse(_.Toughness)).Average();
                }
            }


            return new Tuple<double, double>(PowerAvg, ToughnessAvg);
        }
    }
}
