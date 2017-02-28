using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Media;

//Application Name: DrunkPC
//Description: Application that generates eratic mouse and keyboard movements and input
//and generates fake sounds and dialogs to confuse the user

namespace DrunkPC
{
    class Program
    {
        public static Random _random = new Random();

        public static int _startupDelaySeconds = 120;
        public static int _totalDurationSeconds = 3;

        /// <summary>
        /// Entry point for prank application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Drunk PC application by Benjamin Creem");

            if (args.Length == 2)
            {
                _startupDelaySeconds = Convert.ToInt32(args[0]);
                _totalDurationSeconds = Convert.ToInt32(args[1]);
            }

            while (true)
            {


                //Wait 10 seconds before starting
                DateTime future = DateTime.Now.AddSeconds(_startupDelaySeconds);
                while (future > DateTime.Now)
                {
                    Thread.Sleep(1000);
                }

                //Create all threads that manipulate all inputs and outputs for the system
                Thread drunkMouseThread = new Thread(new ThreadStart(DrunkMouseThread));
                Thread drunkKeyboardThread = new Thread(new ThreadStart(DrunkKeyboardThread));
                Thread drunkSoundThread = new Thread(new ThreadStart(DrunkSoundThread));
                Thread drunkPopupThread = new Thread(new ThreadStart(DrunkPopupThread));
                //Start threads
                drunkMouseThread.Start();
                drunkKeyboardThread.Start();
                drunkSoundThread.Start();
                drunkPopupThread.Start();

                //Ends 10 seconds after starting
                future = DateTime.Now.AddSeconds(_totalDurationSeconds);
                while (future > DateTime.Now)
                {
                    Thread.Sleep(1000);
                }

                drunkMouseThread.Abort();
                drunkKeyboardThread.Abort();
                drunkSoundThread.Abort();
                drunkPopupThread.Abort();
            }


        }



        #region ThreadFunctions
        //Randomly effects mouse movements to mess with user
        public static void DrunkMouseThread()
        {
            int moveX = 0;
            int moveY = 0;
            while (true)
            {
                //Generate the random ammount to move the cursor on x and y
                moveX = _random.Next(10) - 5;
                moveY = _random.Next(10) - 5;

                //Change mouse cursor posistion to new point
                Cursor.Position = new System.Drawing.Point(Cursor.Position.X + moveX, Cursor.Position.Y + moveY);
                Thread.Sleep(50);
            }
        }

        //Random Keybard inputs
        public static void DrunkKeyboardThread()
        {
            while (true)
            {
                //Generate a random lowercase letter 
                char key = (char)(_random.Next(25) + 97);
                SendKeys.SendWait(key.ToString());
                Thread.Sleep(500);
            }
        }

        //Random Sounds
        public static void DrunkSoundThread()
        {
            while (true)
            {
                switch (_random.Next(5))
                {
                    case 0:
                        SystemSounds.Asterisk.Play();
                        break;
                    case 1:
                        SystemSounds.Beep.Play();
                        break;
                    case 2:
                        SystemSounds.Exclamation.Play();
                        break;
                    case 3:
                        SystemSounds.Hand.Play();
                        break;
                    case 4:
                        SystemSounds.Question.Play();
                        break;
                }
                
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Every 10 seconds, 10% of the time a popup is created
        /// </summary>
        public static void DrunkPopupThread()
        {
            while (true)
            {
                if (_random.Next(100) > 50)
                {
                    MessageBox.Show(
                        "Bugsplat! League Of Legends has run into an error!",
                        "League Of Legends",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                Thread.Sleep(10000);
            }
        }

    }
}

#endregion
