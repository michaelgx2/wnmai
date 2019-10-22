using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WnMAiModuleLib;

namespace WnMAiModule
{
    public static class Tester
    {
        public static List<TestAvatar> Hostiles = new List<TestAvatar>();
        public static List<TestAvatar> Allies = new List<TestAvatar>();
        public static float TimeStep = 0;
        public static Random Rnd = new Random();
        public static string GameState = "start";

        public static void InitTesters()
        {
            for (int i = 0; i < 5; i++)
            {
                TestAvatar ta = new TestAvatar()
                {
                    Color = Color.Red,
                    Side = 1,
                    Position = i,
                    SMachine = new WsMachine().SetStates(new DemoTemplate()),
                    X = 800-32
                };
                ta.SMachine.ChangeToState("start", ta);
                Hostiles.Add(ta);
            }

            for (int i = 0; i < 5; i++)
            {
                TestAvatar ta = new TestAvatar()
                {
                    Color = Color.Cyan,
                    Side = 0,
                    Position = i,
                    SMachine = new WsMachine().SetStates(new DemoTemplate())
                };
                ta.SMachine.ChangeToState("start", ta);
                Allies.Add(ta);
            }
        }
    }
}
