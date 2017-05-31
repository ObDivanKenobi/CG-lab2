using MatrixNamespace;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class Point3D
    {
        #region поля
        /// <summary>
        /// Задает допустимую погрешность при проверке
        /// двух точек на равенство.
        /// </summary>
        private static double E = 0.001;
        #endregion
        #region свойства
        /// <summary>
        /// Задает или возвращает координату X.
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        /// Задает или возвращает координату Y.
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        /// Задает или возвращает координату Z.
        /// </summary>
        public double Z { get; private set; }
        #endregion
        #region конструкторы
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="x">Координата X.</param>
        /// <param name="y">Координата Y.</param>
        /// <param name="z">Координата Z.</param>
        public Point3D(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Копирующий конструктор.
        /// </summary>
        /// <param name="p">Конструктор создаст копию данного значения.</param>
        public Point3D(Point3D p)
        {
            if (p == null)
                throw new ArgumentNullException();
            this.X = p.X;
            this.Y = p.Y;
            this.Z = p.Z;
        }

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public Point3D()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }
        #endregion
        #region методы
        public override string ToString()
        {
            return "(" + this.X.ToString() + "; " + this.Y.ToString() + "; " + this.Z.ToString() + ")";
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Point3D p = obj as Point3D;
            if ((this.X - p.X > E) || (this.X - p.X < -E))
                return false;
            if ((this.Y - p.Y > E) || (this.Y - p.Y < -E))
                return false;
            if ((this.Z - p.Z > E) || (this.Z - p.Z < -E))
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            return (int)(this.X) * 10201 + (int)(this.Y) * 101 + (int)(this.Z);
        }

        public static bool operator ==(Point3D p1, Point3D p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Point3D p1, Point3D p2)
        {
            return !p1.Equals(p2);
        }

        public static Point3D operator +(Point3D p1, Point3D p2)
        {
            if ((p1 == null) || (p2 == null))
                throw new ArgumentNullException();
            Point3D result = new Point3D(p1);
            result.X += p2.X;
            result.Y += p2.Y;
            result.Z += p2.Z;
            return result;
        }
        
        public static Point3D operator *(Point3D p1, double a)
        {
            if ((p1 == null))
                throw new ArgumentNullException();
            return new Point3D(p1.X*a, p1.Y*a, p1.Z*a);
        }

        public static Point3D operator -(Point3D p1, Point3D p2)
        {
            if ((p1 == null) || (p2 == null))
                throw new ArgumentNullException();
            Point3D result = new Point3D(p1);
            result.X -= p2.X;
            result.Y -= p2.Y;
            result.Z -= p2.Z;
            return result;
        }

        /// <summary>
        /// Отбрасывает координату Z и округляет до целоых значений
        /// координаты X и Y.
        /// </summary>
        /// <returns></returns>
        public Point ToPoint()
        {
            return new Point((int)X, (int)Y);
        }

        /// <summary>
        /// Преобразует точку в вектор-столбец
        /// </summary>
        /// <returns></returns>
        public Matrix ToMatrix()
        {
            Matrix point = new Matrix(m: 3, n: 1);
            point[0, 0] = X;
            point[1, 0] = Y;
            point[2, 0] = Z;

            return point;
        }

        /// <summary>
        /// Преобразует координаты точки согласно переданной матрице преобразования
        /// </summary>
        /// <param name="transformationMatrix">матрица преобразования</param>
        /// <returns></returns>
        public Point3D Transform(Matrix transformationMatrix)
        {
            if (!transformationMatrix.IsSquare || transformationMatrix.N != 3)
                throw new ArgumentException("Матрица трансформации должна быть 3x3 матрицей!");

            if (transformationMatrix == null)
                throw new ArgumentNullException("Матрица трансформации не должна быть null!");

            Matrix point = ToMatrix();
            point = 1 / Math.Sqrt(6) * transformationMatrix * point;
            return new Point3D(point[0, 0], point[1, 0], point[2, 0]);
        }
        #endregion
    }
}
