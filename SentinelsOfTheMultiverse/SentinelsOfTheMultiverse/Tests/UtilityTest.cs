using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SentinelsOfTheMultiverse.Data;
using System.Windows.Media;
using System.Windows.Controls;

namespace SentinelsOfTheMultiverse.Tests
{
    [TestFixture]
    class UtilityTest
    {
        [Test, RequiresSTA]
        public void testImageInitializer()
        {
            string imageLocation = "Images/Hero/Haka/NonPlayable/haka_back.png";
            ImageSource testImage = Utility.getImageSource(imageLocation);
            Assert.NotNull(testImage);
        }

        [Test, RequiresSTA]
        public void testSelectionHighlight()
        {
            Image testImage = new Image();
            testImage.Effect = Utility.selectionGlowHero();
            Assert.NotNull(testImage.Effect);
        }

        [Test, RequiresSTA]
        public void testAddElementToGrid()
        {
            Button blankUIEle = new Button();
            Grid testGrid = new Grid();
            Utility.addElementToGrid(blankUIEle, 0, 0, testGrid);

            Assert.AreEqual(testGrid.Children.Count, 1);
        }
    }
}
