using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    /// <summary>
    /// Четырехугольник, задающийся координатами
    /// четырех вершин в пространстве.
    /// </summary>
    class Quadrilateral3D
    {
        /// <summary>
        /// Задает или возвращает точку A.
        /// </summary>
        public Point3D A { get; private set; }

        /// <summary>
        /// Задает или возвращает точку B.
        /// </summary>
        public Point3D B { get; private set; }

        /// <summary>
        /// Задает или возвращает точку C.
        /// </summary>
        public Point3D C { get; private set; }

        /// <summary>
        /// Задает или возвращает точку D.
        /// </summary>
        public Point3D D { get; private set; }

         public Quadrilateral3D(Point3D a, Point3D b, Point3D c, Point3D d)
        {
            if ((a == null) || (b == null) || (c == null) || (d == null))
                throw new ArgumentNullException();
            this.A = a;
            this.B = b;
            this.C = c;
            this.D = d;
        }

        /// <summary>
        /// Возвращает вектор нормали четырехугольника.
        /// </summary>
        public Vector3D NormalVector()
        {
            if ((A == B) && (A == C) || (A == B) && (A == D) || (A == C) && (A == D) || (B == C) && (B == D))
                throw new Exeption3D("Три точки лежат на одной прямой.");
            Point3D p1, p2, p3;
            if (A == B)
            {
                p1 = A;
                p2 = C;
                p3 = D;
            }
            else
            {
                p1 = A;
                p2 = B;
                p3 = C;
            }
            double dx = (p2.Y - p1.Y) * (p3.Z - p1.Z) - (p3.Y - p1.Y) * (p2.Z - p1.Z);
            double dy = (p2.X - p1.X) * (p3.Z - p1.Z) - (p3.X - p1.X) * (p2.Z - p1.Z);
            double dz = (p2.X - p1.X) * (p3.Y - p1.Y) - (p3.X - p1.X) * (p2.Y - p1.Y);
            return new Vector3D(dx, dy, dz);
        }

        /// <summary>
        /// Нахождение центра масс.
        /// </summary>
        /// <returns>Точка, соответствующая центру масс четырехугольника.</returns>
        public Point3D Barycenter()
        {
            return new Point3D((A.X + B.X + C.X + D.X) / 4, (A.Y + B.Y + C.Y + D.Y) / 4, (A.Z + B.Z + C.Z + D.Z) / 4);
        }

        /// <summary>
        /// Нахождение косинуса угла между нормалью и вектором барицентр-точка.
        /// </summary>
        /// <param name="light"></param>
        /// <returns></returns>
        public double Cos(Point3D light)
        {
            if (light == null)
                throw new ArgumentNullException();
            return Vector3D.Cos(this.NormalVector(), new Vector3D(Barycenter(), light));
        }
    }
}
