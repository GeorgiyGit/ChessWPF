﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTestChess
{
    public struct PointInt
    {
        public int X { get; set; }
        public int Y { get; set; }

        public PointInt(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
