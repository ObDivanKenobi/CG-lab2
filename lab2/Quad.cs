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
    class Quadrilateral3D: Polygon
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

        public override Point3D[] Points
        {
            get
            {
                return new Point3D[] { A, B, C, D };
            }
        }

        public Quadrilateral3D(Point3D a, Point3D b, Point3D c, Point3D d)
        {
            if ((a == null) || (b == null) || (c == null) || (d == null))
                throw new ArgumentNullException();
            A = a;
            B = b;
            C = c;
            D = d;
        }

        /// <summary>
        /// Возвращает вектор нормали четырехугольника, равный [AB, AD]
        /// </summary>
        public override Vector3D NormalVector()
        {
            if ((A == B) && (A == C) || (A == B) && (A == D) || (A == C) && (A == D) || (B == C) && (B == D))
                throw new Exeption3D("Три точки лежат на одной прямой.");

            return Vector3D.CrossProduct(new Vector3D(A, B), new Vector3D(A, D));
        }

        /// <summary>
        /// Нахождение центра масс.
        /// </summary>
        /// <returns>Точка, соответствующая центру масс четырехугольника.</returns>
        public override Point3D Barycenter()
        {
            return new Point3D((A.X + B.X + C.X + D.X) / 4, (A.Y + B.Y + C.Y + D.Y) / 4, (A.Z + B.Z + C.Z + D.Z) / 4);
        }

        /// <summary>
        /// Нахождение косинуса угла между нормалью и вектором барицентр-точка.
        /// </summary>
        /// <param name="light"></param>
        /// <returns></returns>
        public override double Cos(Point3D light)
        {
            if (light == null)
                throw new ArgumentNullException();
            return Vector3D.Cos(NormalVector(), new Vector3D(Barycenter(), light));
        }
    }
}
