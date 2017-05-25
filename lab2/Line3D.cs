using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    class ArrowLine3D
    {
        public Point3D Start { get; private set; }
        public Point3D End { get; private set; }
        public Triangle3D Arrow { get; private set; }

        public ArrowLine3D(Point3D start, Point3D end, Triangle3D arrow)
        {
            Start = start;
            End = end;
            Arrow = arrow;
        }
    }
}
