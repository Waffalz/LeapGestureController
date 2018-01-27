using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Leap;

namespace LeapGestureController
{
    static class Program
    {
        public const int FRAMES_PER_SECOND = 60;
        public static Boolean isRunning = true;


        static void Main(string[] args)
        {
            Controller cont = new Controller();


        }

        public static void Update(float time)
        {
            while (isRunning)
            {

            };
        }

        public static void BeginLooping()
        {

        }
    }
    
}
