using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Windows;
using SentinelsOfTheMultiverse.Data;
using System.IO;

namespace SentinelsOfTheMultiverse.Tests
{
    [TestFixture]
    class DeckTest
    {
        List<Card> cardsTest = new List<Card>();
        string[] files = { "dominion", "elbow_smash", "enduring_intercession", "ground_pound", "haka_back",
                         "haka_death", "haka_hero", "haka_of_battle", "haka_of_restoration", "haka_of_shielding",
                         "mere", "punish_the_weak", "rampage", "savage_mana", "ta_moko", "taiaha", "vitality_surge"};

        [Test]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void TestDeckNull()
        {
            Deck testNull = new Deck("nothing");
        }

        [Test, RequiresSTA]
        public void TestImportNumOfCards()
        {
            int count = files.Length;
            Deck testHaka = new Deck("haka");
            Assert.IsTrue(testHaka.getCards().Count.Equals(count));
        }

        [Test]
        public void TestValidNamesCards()
        {
            bool pass = true;
            int numCards = cardsTest.Count;

            for(int k = 0; k < numCards; k++)
            {
                if (!files.Contains(cardsTest[k].getName()))
                {
                    pass = false;
                }
            }
            Assert.True(pass);
        }

        //[Test]
    }
}
