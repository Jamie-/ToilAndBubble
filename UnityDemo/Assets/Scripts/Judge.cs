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
            leftScore = (180 - getDifference(Cauldron.Hue, LeftPotion.h)) / 180 * 100;
            rightScore = (180 - getDifference(Cauldron.Hue, RightPotion.h)) / 180 * 100;
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

        /*returns a value from -100 to 100, where 0 means
         *the players are equally far(knot in the middle),
         *-100 means the cauldron matches left (knot all 
         *the way left), and 100 means cauldron matches right
         */
        public static double getTugOfWarDisplacement()
        {
            double deltaLeft = getDifference(Cauldron.Hue, LeftPotion.h);
            if ((int)deltaLeft == 0) return -100;
            double deltaRight = getDifference(Cauldron.Hue, RightPotion.h);
            if ((int)deltaRight == 0) return 100;

            return (Math.Log10(deltaLeft / deltaRight) / Math.Log10(180)) * 100;
        } 
    }
}
