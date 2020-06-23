﻿using System;
using System.Linq;
using Couchbase.Linq.Extensions;
using Couchbase.Linq.Filters;
using Couchbase.Linq.IntegrationTests.Documents;
using Couchbase.Linq.Versioning;
using NUnit.Framework;

namespace Couchbase.Linq.IntegrationTests
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    public class BucketContextTests
    {
        [Test]
        public void Test_Basic_Query()
        {
            var db = new BucketContext(TestSetup.Bucket);
            var query = from x in db.Query<Beer>()
                where x.Type == "beer"
                select x;

            var beer = query.FirstOrDefault();
            Assert.IsNotNull(beer);
        }

        [Test]
        public void BeerSampleContext_Tests()
        {
            var db = new BeerSample();
            var beers = from b in db.Beers
                select b;

            var beer = beers.Take(1);
            Assert.IsNotNull(beer);
        }

        [Test]
        public void BeerSample_Tests()
        {
            var db = new BeerSample();
            var query = from beer in db.Beers
                        join brewery in db.Breweries
                        on beer.BreweryId equals N1QlFunctions.Key(brewery)
                        select new { beer.Name, beer.Abv, BreweryName = brewery.Name };

            foreach (var beer in query.Take(1))
            {
                Console.WriteLine(beer.Name);
            }
        }

        #region Helper Classes

        [DocumentTypeFilter("BucketContextTests_Sample")]
        public class Sample
        {
            [System.ComponentModel.DataAnnotations.Key]
            public string Id { get; set; }

            public string Type
            {
                get { return "BucketContextTests_Sample"; }
            }

            public int Value { get; set; }
        }

        #endregion
    }
}

#region [ License information          ]

/* ************************************************************
 *
 *    @author Couchbase <info@couchbase.com>
 *    @copyright 2015 Couchbase, Inc.
 *
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 *
 *        http://www.apache.org/licenses/LICENSE-2.0
 *
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 *
 * ************************************************************/

#endregion
