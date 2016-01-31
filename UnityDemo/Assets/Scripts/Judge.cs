using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

//namespace Assets.Scripts
//{
public class Judge : MonoBehaviour
    {
        public static String winnerName;
        public static double leftScore, rightScore;
        public Text text;

        void Start()
        {
            updateStats();
            text.text = winnerName;
        }

        public static void updateStats()
        {
            leftScore = (360 - getDifference(Cauldron.Hue, LeftPotion.h)) / 360 * 100;
            rightScore = (360 - getDifference(Cauldron.Hue, RightPotion.h)) / 360 * 100;
            Debug.Log(leftScore);
            Debug.Log(rightScore);
        if (leftScore == rightScore) winnerName = "Draw";
        else if (leftScore > rightScore) winnerName = "Left";
        else winnerName = "Right";
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
//}
