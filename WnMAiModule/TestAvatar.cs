using System;
using System.Collections.Generic;
using System.Drawing;
using WnMAiModuleLib;

namespace WnMAiModule
{
    public class TestAvatar
    {
        public int Side = 0;//0 is allie, 1 is hostile
        public int Position = 0;
        public Color Color { get; set; } = Color.White;
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public string Talking { get; set; } = "";
        public int ToX { get; set; } = 0;
        public int ToY { get; set; } = 0;
        public WsMachine SMachine { get; set; }

        public bool MoveEnd
        {
            get { return Math.Abs(ToX - X) <= 1 && Math.Abs(ToY - Y) <= 1; }
        }

        private float _talkTimer = 1f;

        public void Update()
        {
            if (_talkTimer <= 0)
            {
                Talking = "";
            }
            else
            {
                _talkTimer -= Tester.TimeStep;
            }
            AiMove(Tester.TimeStep);
            SMachine.UpdatePerFrame(Tester.TimeStep, this);
        }

        public void MoveTo(int x, int y)
        {
            ToX = x;
            ToY = y;
        }

        public void Talk(string str)
        {
            Talking = str;
            _talkTimer = 1f;
        }

        private void AiMove(float timeStep)
        {
            if (MoveEnd) return;
            CalculateNextPoint(50, timeStep);
        }

        /// <summary>
        /// 直线插值
        /// </summary>
        private int LineInterpolation(int left, int right, float step)
        {
            var ret = (right - left) * step + left;
            return (int) ret;
        }

        private void CalculateNextPoint(double speed, double timeStep)
        {
            double distance = Math.Sqrt((ToX - X) * (ToX - X) + (ToY - Y) * (ToY - Y));
            float step = (float)(speed * timeStep / distance);
            X = LineInterpolation(X, ToX, step);
            Y = LineInterpolation(Y, ToY, step);
        }
    }
}