using System;
using RestSharp;
using NUnit.Framework;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Specialized;
using System.Collections.Generic;


namespace BeerAppTest
{
    class Program : BeerTestBase
    {
        [TestCase("12-2015")]
        public void TestCase_1(string date)    //has a valid 'abv'
        {
            var beers = GetBeerProducedAfter(date);
            Assert.IsTrue(beers.TrueForAll(x => !string.IsNullOrEmpty(x.abv)));
        }

        [TestCase("12-2015")]
        public void TestCase_2(string date)    //check if abv is double
        {
            System.Globalization.CultureInfo EnglishCulture = new System.Globalization.CultureInfo("en-EN");
            var beers = GetBeerProducedAfter(date);
            double result;
            Assert.IsTrue(beers.TrueForAll(x => Double.TryParse(x.abv, System.Globalization.NumberStyles.Float, EnglishCulture, out result)));
            Assert.IsTrue(beers.TrueForAll(x => System.Convert.ToDouble(x.abv, EnglishCulture) > 3.0));

        }

        [TestCase("12-2015")]
        public void TestCase_3(string date)    //has a valid 'name'
        {
            var beers = GetBeerProducedAfter(date);
            Assert.IsTrue(beers.TrueForAll(x => !string.IsNullOrEmpty(x.name)));
            Assert.IsTrue(true);
        }

        [TestCase("12-2015")]
        public void TestCase_4(string date)    //has a valid 'name'
        {
            var beers = GetBeerProducedAfter(date);
            Assert.IsTrue(beers.TrueForAll(x => !string.IsNullOrEmpty(x.name)));
        }

        [TestCase("12-2015")]
        public void TestCase_5(string date)    //validate proper date brewed returned
        {
            var beers = GetBeerProducedAfter(date);
            var dataTime = Convert.ToDateTime(date);
            Assert.IsTrue(beers.TrueForAll(x => DateTime.Compare(Convert.ToDateTime(x.first_brewed), dataTime)!= -1));
        }

        [Test]
        public void TestCase_6()    //validate random endpoint returns not empty beer
        {
            var beer = GetRundomBeer();
            Assert.IsTrue(beer != null && beer.Count.Equals(1));
            Assert.IsTrue(!string.IsNullOrEmpty(beer[0].name)
                       && !string.IsNullOrEmpty(beer[0].description)
                       && !string.IsNullOrEmpty(beer[0].volume.value.ToString())
                       && !string.IsNullOrEmpty(beer[0].volume.unit)
                       && !string.IsNullOrEmpty(beer[0].first_brewed)
                       && !string.IsNullOrEmpty(beer[0].tagline));
        }

        [TestCase(13)]
        [TestCase(34)]
        public void TestCase_7(int beerCount)    //validate endpoint returns exact number of beers
        {
            var beers = GetBeerInCount(beerCount);
            Assert.IsTrue(beers.Count().Equals(beerCount));
        }

        [TestCase(143, "Lizard Bride - Prototype Challenge")]
        [TestCase(158, "Hello My Name Is Zé (w/ 2Cabeças)")]
        public void TestCase_8(int beerId, string beerName)    //validate each time the same beer is returned using id
        {
            var beer = GetBeerWithId(beerId);
            Assert.IsTrue(beer[0].name.Equals(beerName));
        }

    }
}
