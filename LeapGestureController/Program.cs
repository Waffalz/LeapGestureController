using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Diagnostics;

using Leap;

namespace LeapGestureController
{
    static class Program
    {
        public const int FRAMES_PER_SECOND = 60; //the amount of update cycles per second. The higher the number, the greater the precision
        private static Boolean isRunning = true; //whether the update loop is running

        private static Stopwatch timer; //var for time elapsed

        private static Thread loopThread;

        private static Controller cont;

        private static float altitude = 0f;
        private static float speed = 10f;

        static void Main(string[] args)
        {
            cont = new Controller();

            loopThread = new Thread(BeginLoop);
            loopThread.Start();

            //Thread.Sleep(30 * 1000);

            //StopLoop();
            loopThread.Join();
        }

        /**
         * Is called to several times per second to fetch the state of the controller
         * 
         */
        public static void Update(TimeSpan time)
        {
            Frame currentFrame = cont.Frame();

            float timeElapsed = (float)(time.TotalMilliseconds / 1000); //amound of time elapsed in seconds

            
            foreach(Hand hand in currentFrame.Hands)
            {
                //Console.Write(hand.Direction);
                altitude += timeElapsed * speed * hand.Direction.y;
                Console.WriteLine(hand.Direction.y);

            }
            //Console.WriteLine(altitude);

            //Console.WriteLine(time.Milliseconds);

        }

        /**
         * Initializes the update loops and adjusts time accordingly
         */
        public static void BeginLoop()
        {
            isRunning = true;
            timer = new Stopwatch();
            timer.Start();

            Stopwatch updateTimer = new Stopwatch();

            while (isRunning)
            {
                timer.Stop();
                TimeSpan elapsedTime = timer.Elapsed; //stop the timer and record the amount of time passed
                timer.Restart();
                updateTimer.Start();
                Update(elapsedTime); //call to Update and pass the amount of time since the last call to Update
                updateTimer.Stop();
                Thread.Sleep(Math.Max(1000/FRAMES_PER_SECOND -  (int)timer.ElapsedMilliseconds, 0)); //wait out the remaining time for this update cycle
                updateTimer.Restart();
            };

            timer.Stop();
        }

        /**
         * Stops the loop
         */
        public static void StopLoop()
        {
            isRunning = false;
        }
    }
    
}
