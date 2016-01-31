using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class Judge
    {
        public static String winnerName;
        public static double leftScore, rightScore;

        public static void updateStats()
        {
            leftScore = (360 - getDifference(Cauldron.Hue, LeftPotion.h)) / 360 * 100;
            rightScore = (360 - getDifference(Cauldron.Hue, RightPotion.h)) / 360 * 100;
            if (leftScore > rightScore) winnerName = "Left"; else winnerName = "Right";
        }

        private static double getDifference(double one, double two)
        {
            double delta1;
            if (one > two)
            {
                delta1 = 360 - one + two;
            }
            else
            {
                delta1 = 360 - two + one;
            }

            double delta2 = Math.Abs(one - two);

            return Math.Min(delta1, delta2);
        }
    }
}
