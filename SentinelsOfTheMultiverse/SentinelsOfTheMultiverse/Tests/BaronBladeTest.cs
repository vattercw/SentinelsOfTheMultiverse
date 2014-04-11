using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SentinelsOfTheMultiverse.Data.Villains;

namespace SentinelsOfTheMultiverse.Tests
{
    class BaronBladeTest
    {
        [Test(), RequiresSTA]
        public void TestHastenDoom()
        {
            Start game = new Start();
            BaronBlade test = new BaronBlade();
            test.HastenDoom();
        }

        [Test(), RequiresSTA]
        public void TestDeviousDisruption()
        {
            //test
        }

        [Test(), RequiresSTA]
        public void TestFleshRepairNanites()
        {
            //test
        }

        [Test(), RequiresSTA]
        public void TestSlashAndBurn()
        {
            //test
        }

        [Test(), RequiresSTA]
        public void TestConsiderThePrice()
        {
            //test
        }

        [Test(), RequiresSTA]
        public void TestLivingForceField()
        {
            //test
        }

        [Test(), RequiresSTA]
        public void TestBackLashField()
        {
            //test
        }
    }
}
