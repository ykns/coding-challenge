using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;
        private readonly Dictionary<SearchGroup, List<Shirt>> _shirtsBySearchGroup;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;

            _shirtsBySearchGroup = shirts
                                .GroupBy(g => new SearchGroup(g.Size, g.Color))
                                .ToDictionary(g => g.Key, g => g.ToList());
        }

        public List<Shirt> GetShirts(SearchOptions options)
        {
            var foundShirts = _shirtsBySearchGroup.Where(kvp => (!options.Colors.Any() || options.Colors.Contains(kvp.Key.Color))
                                  && (!options.Sizes.Any() || options.Sizes.Contains(kvp.Key.Size)))
                                  .Select(x => x.Value)
                                  .SelectMany(x => x)
                                  .ToList();
            return foundShirts;
        }

        public List<SizeCount> GetSizeCounts(SearchOptions options)
        {
            var shirtsBySizeGroup = _shirtsBySearchGroup.Where(x => !options.Colors.Any() || options.Colors.Contains(x.Key.Color))
                .GroupBy(g => g.Key.Size)
                .Select((x, y) => new { Size = x.Key, x.SelectMany(i => i.Value).ToList().Count })
                .ToDictionary(d => d.Size, d => d.Count);
            foreach (var size in Size.All)
            {
                if (!shirtsBySizeGroup.ContainsKey(size))
                {
                    shirtsBySizeGroup.Add(size, 0);
                }
            }

            return shirtsBySizeGroup.Select(d => new SizeCount() { Size = d.Key, Count = d.Value }).ToList();
        }

        public List<ColorCount> GetColorCounts(SearchOptions options)
        {
            var shirtsByColorGroup = _shirtsBySearchGroup.Where(s => !options.Sizes.Any() || options.Sizes.Contains(s.Key.Size))
                .GroupBy(g => g.Key.Color)
                .Select((x, y) => new { Color = x.Key, x.SelectMany(i => i.Value).ToList().Count })
                .ToDictionary(d => d.Color, d => d.Count);
            foreach (var color in Color.All)
            {
                if (!shirtsByColorGroup.ContainsKey(color))
                {
                    shirtsByColorGroup.Add(color, 0);
                }
            }

            return shirtsByColorGroup.Select(d => new ColorCount() { Color = d.Key, Count = d.Value }).ToList();
        }


        public SearchResults Search(SearchOptions options)
        {
            var foundShirts = GetShirts(options);
            var sizeCounts = GetSizeCounts(options);
            var colorCounts = GetColorCounts(options);

            return new SearchResults
            {
                Shirts = foundShirts,
                SizeCounts = sizeCounts,
                ColorCounts = colorCounts
            };
        }
    }
}