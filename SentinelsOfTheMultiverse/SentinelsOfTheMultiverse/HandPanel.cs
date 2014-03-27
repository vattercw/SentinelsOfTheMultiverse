using SentinelsOfTheMultiverse.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SentinelsOfTheMultiverse
{
    class HandPanel:Panel
    {
        private Collection<UIElement> list = new Collection<UIElement>();

        public HandPanel(List<Card> hand)
        {
            foreach (Card c in hand)
            {
                this.Children.Add(c.cardImage);
            }
        }
        
        /// <summary>
        /// Gets the index of the clicked element
        /// </summary>
        /// <param name="child">The child.</param>
        /// <returns></returns>
        private int GetClickedIndex(object child)
        {
            int i = 0;

            foreach (UIElement element in list)
            {
                if (element == child)
                {
                    return i;
                }
                else
                {
                    i++;
                }
            }

            return i;
        }
    }
}
