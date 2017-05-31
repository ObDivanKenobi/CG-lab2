using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    /// <summary>
    /// Вектор в трёхмерном пространстве
    /// </summary>
    public class Vector3D
    {
        #region свойства
        /// <summary>
        /// Координата Х
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        /// Координата Y
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        /// Координата Z
        /// </summary>
        public double Z { get; private set; }
        #endregion
        #region конструкторы
        /// <summary>
        /// Конструктор по координатам вектора.
        /// </summary>
        /// <param name="dx">dX = X_кон - X_нач.</param>
        /// <param name="dy">dY = Y_кон - Y_нач.</param>
        /// <param name="dz">dZ = Z_кон - Z_нач.</param>
        public Vector3D(double dx, double dy, double dz)
        {
            this.X = dx;
            this.Y = dy;
            this.Z = dz;
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="start">Начало вектора.</param>
        /// <param name="finish">Конец вектора.</param>
        public Vector3D(Point3D start, Point3D finish)
        {
            X = finish.X - start.X;
            Y = finish.Y - start.Y;
            Z = finish.Z - start.Z;
        }

        /// <summary>
        /// Копирующий конструктор.
        /// </summary>
        public Vector3D(Vector3D v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }
        #endregion
        #region методы

        /// <summary>
        /// Приведение вектора к единичной длине
        /// </summary>
        /// <returns>Вектор единичной длины, сонаправленный данному</returns>
        public Vector3D Normalize()
        {
            double invertedLength = 1 / Length();
            return new Vector3D(X * invertedLength, Y * invertedLength, Z * invertedLength);
        }

        /// <summary>
        /// Скалярное произведение векторов.
        /// </summary>
        public static double DotProduct(Vector3D v1, Vector3D v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static Vector3D CrossProduct(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
        }

        /// <summary>
        /// Длина (норма) вектора.
        /// </summary>
        public double Length()
        {
            return Math.Pow(X * X + Y * Y + Z * Z, 0.5);
        }

        /// <summary>
        /// Возвращает косинус угла между векторами.
        /// </summary>
        /// <param name="v1">Vector3D</param>
        /// <param name="v2">Vector3D</param>
        /// <returns></returns>
        public static double Cos(Vector3D v1, Vector3D v2)
        {
            if ((v1 == null) || (v2 == null))
                throw new ArgumentNullException();
            double tmp = v1.Length() * v2.Length();
            if (tmp == 0)
                return 0;
            return DotProduct(v1, v2) / tmp;
        }

        public static Vector3D operator +(Vector3D v1, Vector3D v2)
        {
            if ((v1 == null) || (v2 == null))
                throw new ArgumentNullException();
            Vector3D result = new Vector3D(v1);
            result.X += v2.X;
            result.Y += v2.Y;
            result.Z += v2.Z;
            return result;
        }

        public static Vector3D operator -(Vector3D v1, Vector3D v2)
        {
            if ((v1 == null) || (v2 == null))
                throw new ArgumentNullException();
            Vector3D result = new Vector3D(v1);
            result.X -= v2.X;
            result.Y -= v2.Y;
            result.Z -= v2.Z;
            return result;
        }

        public static Vector3D operator *(Vector3D v, double val)
        {
            if (v == null)
                throw new ArgumentNullException();
            return new Vector3D(v.X * val, v.Y * val, v.Z * val);
        }

        public static Vector3D operator *(double val, Vector3D v)
        {
            return v * val;
        }
        #endregion
    }
}
