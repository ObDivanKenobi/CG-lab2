using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixNamespace;

namespace lab2
{
    public abstract class Polygon
    {
        public abstract Point3D[] Points { get; }

        /// <summary>
        /// Возвращает вектор нормали полигона.
        /// </summary>
        public abstract Vector3D NormalVector();

        /// <summary>
        /// Нахождение центра масс.
        /// </summary>
        /// <returns>Точка, соответствующая центру масс полигона.</returns>
        public abstract Point3D Barycenter();

        public Point3D Center { get { return Barycenter(); } }

        /// <summary>
        /// Нахождение косинуса угла между нормалью и вектором барицентр-источник света.
        /// </summary>
        public abstract double Cos(Point3D light);
    }

    public class Triangle3D : Polygon
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

        public Triangle3D(Point3D a, Point3D b, Point3D c)
        {
            if ((a == null) || (b == null) || (c == null))
                throw new ArgumentNullException();
            A = a;
            B = b;
            C = c;
        }

        public override Point3D[] Points
        {
            get
            {
                return new Point3D[] { A, B, C };
            }
        }

        /// <summary>
        /// Возвращает вектор нормали треугольника. Это произведение векторов a=AB и b=AC
        /// </summary>
        public override Vector3D NormalVector()
        {
            if ((A == B) && (A == C) && (B == C))
                throw new Exeption3D("Три точки лежат на одной прямой.");

            return Vector3D.CrossProduct(new Vector3D(A, B), new Vector3D(A, C));
            //double dx = (p2.Y - p1.Y) * (p3.Z - p1.Z) - (p3.Y - p1.Y) * (p2.Z - p1.Z);
            //double dy = (p2.X - p1.X) * (p3.Z - p1.Z) - (p3.X - p1.X) * (p2.Z - p1.Z);
            //double dz = (p2.X - p1.X) * (p3.Y - p1.Y) - (p3.X - p1.X) * (p2.Y - p1.Y);
            //return new Vector3D(dx, dy, dz);
        }

        /// <summary>
        /// Нахождение центра масс.
        /// </summary>
        /// <returns>Точка, соответствующая центру масс треугольника.</returns>
        public override Point3D Barycenter()
        {
            return new Point3D((A.X + B.X + C.X) / 3, (A.Y + B.Y + C.Y ) / 3, (A.Z + B.Z + C.Z) / 3);
        }

        /// <summary>
        /// Нахождение косинуса угла между нормалью и вектором барицентр-источник света.
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
