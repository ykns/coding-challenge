using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    class SearchGroupTests
    {
        [TestCaseSource("SearchGroupCases")]
        public void GetHashCode(SearchGroup a, SearchGroup b, bool expectedValue)
        {
            var result = a.GetHashCode() == b.GetHashCode();
            Assert.That(result == expectedValue);
        }


        [TestCaseSource("SearchGroupCases")]
        public void Equals(SearchGroup a, SearchGroup b, bool expectedValue)
        {
            var result = a.Equals(b);
            Assert.That(result == expectedValue);
        }

        static object[] SearchGroupCases =
        {
            new object[] { new SearchGroup(Size.Medium, Color.Red), new SearchGroup(Size.Medium, Color.Red), true },
            new object[] { new SearchGroup(null, Color.Red), new SearchGroup(null, Color.Red), true },
            new object[] { new SearchGroup(null, null), new SearchGroup(null, null), true },
            new object[] { new SearchGroup(null, null), new SearchGroup(null, Color.Red), false },
            new object[] { new SearchGroup(Size.Large, Color.Red), new SearchGroup(Size.Medium, Color.Red), false },
            new object[] { new SearchGroup(Size.Large, null), new SearchGroup(Size.Medium, Color.Red), false },
            new object[] { new SearchGroup(null, Color.Red), new SearchGroup(Size.Medium, Color.Red), false },
        };
    }
}
