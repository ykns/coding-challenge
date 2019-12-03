using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        private readonly List<Shirt> _shirts = new List<Shirt>
        {
            new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
            new Shirt(Guid.NewGuid(), "White - Small", Size.Small, Color.White),
            new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
            new Shirt(Guid.NewGuid(), "Blue - Small", Size.Small, Color.Blue),
            new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            new Shirt(Guid.NewGuid(), "Black - Large", Size.Large, Color.Black),
        };
        
        [TestCaseSource("SearchOptionsCases")]
        public void FindShirts__Given_SearchOptions__Then__Should_Return_Search_Results_And_Option_Counts(SearchOptions searchOptions)
        {
            var searchEngine = new SearchEngine(_shirts);

            var foundShirts = searchEngine.GetShirts(searchOptions);

            AssertResults(foundShirts, searchOptions);
        }

        [TestCaseSource("SearchOptionsCases")]
        public void GetColorCounts__Given_SearchOptions__Then__Should_Return_Color_Counts_Excluding_SearchOptions(SearchOptions searchOptions)
        {
            var searchEngine = new SearchEngine(_shirts);

            var colorCounts = searchEngine.GetColorCounts(searchOptions);

            AssertColorCounts(_shirts, searchOptions, colorCounts);
        }

        [TestCaseSource("SearchOptionsCases")]
        public void GetSizeCounts__Given_SearchOptions__Then__Should_Return_Size_Counts_Excluding_SearchOptions(SearchOptions searchOptions)
        {
            var searchEngine = new SearchEngine(_shirts);

            var sizeCounts = searchEngine.GetSizeCounts(searchOptions);

            AssertSizeCounts(_shirts, searchOptions, sizeCounts);
        }

        [TestCaseSource("SearchOptionsCases")]
        public void Search__Given_SearchOptions__Then__Should_Return_Search_Results_Containing_Shirts_That_Match_Search_Options_And_Color_And_Size_Counts(SearchOptions searchOptions)
        {
            var searchEngine = new SearchEngine(_shirts);

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(_shirts, searchOptions, results.ColorCounts);
        }

        static SearchOptions[] SearchOptionsCases =
        {
            new SearchOptions(),
            new SearchOptions
            {
                Colors = new List<Color> { Color.Red }
            },
            new SearchOptions
            {
                Colors = new List<Color> { Color.Blue, Color.White }
            },
            new SearchOptions
            {
                Sizes = new List<Size> { Size.Large },
            },
            new SearchOptions
            {
                Sizes = new List<Size> { Size.Small, Size.Medium },
            },
            new SearchOptions
            {
                Colors = new List<Color> { Color.Black },
                Sizes = new List<Size> { Size.Large, Size.Medium },
            },
            new SearchOptions
            {
                Colors = new List<Color> { Color.Black, Color.Blue },
                Sizes = new List<Size> { Size.Small, Size.Medium },
            },
        };
    }
}
