using NUnit.Framework;
using OpenWeathermapAPI;
using System;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Response stat = Helpers.SendRequest();
            var diffs = 0;
            var diff = 0;
            int i = 0;
            string city_shortest = null;
            foreach (var el in stat.list)
            {
                diff = el.sys.sunset - el.sys.sunrise;
                if (i == 0)
                    diffs = diff;
                Console.WriteLine($"{ el.name } : {el.sys.sunset} - {el.sys.sunset} = {diff} ");
                if (diffs > diff)
                {
                    diffs = diff;
                    city_shortest = el.name;
                }
                ++i;
            }
            diffs = 0;
            diff = 0;
            i = 0;
            string city_longest = null;
            foreach (var el in stat.list)
            {
                diff = el.sys.sunset - el.sys.sunrise;
                if (i == 0)
                    diffs = diff;
                Console.WriteLine($"{ el.name } : {el.sys.sunset} - {el.sys.sunset} = {diff} ");
                if (diffs < diff)
                {
                    diffs = diff;
                    city_longest = el.name;
                }
                ++i;
            }
            Console.WriteLine($"Shortest is {city_shortest}");
            Console.WriteLine($"Longest is {city_longest}");
        }
    }
}