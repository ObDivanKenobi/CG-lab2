using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class Cylinder
    {
        // Пикселов на условную единицу
        static double step = 30;

        //Радиус основания в условных единицах.
        int radius;

        // Высота в у.е.
        int height;

        //список вершин
        List<Point3D> points;
     
        //множитель числа вершин в основании
        static int n = 10;

        //центр нижнего основания
        Point3D center;

        /// <summary>
        /// Количество полигонов в цилиндре.
        /// </summary>
        public int Count
        {
            get { return 4 * radius * n; } //по radius*n в основаниях и 2*radius*n (потому что они треугольные) в боковой поверхности
        }

        /// <summary>
        /// Радиус
        /// </summary>
        public int Radius
        {
            get { return radius; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Радиус должен быть больше нуля.");

                radius = value;
                CreatePoints();
            }
        }

        /// <summary>
        /// Центр тяжести цилиндра
        /// </summary>
        public Point3D Center
        {
            get { return center; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                center = value;
                CreatePoints();
            }
        }

        /// <summary>
        /// Высота
        /// </summary>
        public int Height
        {
            get { return height; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Радиус должен быть больше нуля.");

                height = value;
                CreatePoints();
            }
        }

        public int Step { get { return (int)step; } }

        /// <summary>
        /// Доступ на чтение к i-му полигону цилиндра
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Triangle3D this[int i]
        {
            get
            {
                if ((i < 0) || (i >= Count))
                    throw new ArgumentOutOfRangeException();
                ++i; //чтобы не было нулевого полигона

                int polygonsInBasement = radius * n;
                //берется полигон из нижнего основания
                if (i < polygonsInBasement)
                    return new Triangle3D(points.First(), points[i], points[i + 1]);
                else if (i == polygonsInBasement)
                    return new Triangle3D(points.First(), points[i], points[1]);
                //берётся полигон из нижнего ряда боковой поверхности
                else if (i < 2*polygonsInBasement)
                    return new Triangle3D(points[i], points[i - polygonsInBasement], points[i - polygonsInBasement + 1]);
                else if (i == 2 * polygonsInBasement)
                    return new Triangle3D(points[i], points[i - polygonsInBasement], points[(i + 1 - polygonsInBasement) % polygonsInBasement]);
                //берётся не последний полигон из верхнего ряда боковой поверхности
                else if (i < 3*polygonsInBasement)
                {
                    i = i - polygonsInBasement;
                    return new Triangle3D(points[i], points[i + 1], points[i - polygonsInBasement + 1]);
                }
                //берётся последний полигон из верхнего ряда боковой поверхности
                else if (i == 3 * polygonsInBasement)
                {
                    i = i - polygonsInBasement;
                    return new Triangle3D(points[i], points[polygonsInBasement + 1], points[1]);
                }
                //берется не последний полигон из верхнего основания
                else if (i < 4 * polygonsInBasement)
                {
                    i = i - 2 * polygonsInBasement;
                    return new Triangle3D(points[i], points[i + 1], points.Last());
                }
                //берется последний полигон из верхнего основания
                else
                {
                    i = i - 2 * polygonsInBasement;
                    return new Triangle3D(points[i], points[polygonsInBasement + 1], points.Last());
                }
            }
            set
            {

            }
        }

        public Cylinder(int radius, int height, Point3D center)
        {
            if (center == null)
                throw new ArgumentNullException("Центр не может быть null!");
            if (radius <= 0)
                throw new ArgumentException("Радиус должен быть больше 0!");
            if (height <= 0)
                throw new ArgumentException("Высота должна быть больше 0!");

            this.radius = radius;
            this.height = height;
            this.center = center;
            CreatePoints();
        }

        private void CreatePoints()
        {
            int smoothness = radius * n; // вершин в основании
            double phi = 0;
            double realRadius = radius * step; //радиус в пикселях
            points = new List<Point3D>();
            points.Capacity = 2 * smoothness + 2; // по radius * n точек в основаниях + центры оснований

            double Z = center.Z - step * height / 2;
            points.Add(new Point3D(center.X, center.Y, Z));

            for(int i = 1; i <= smoothness; ++i)
            {
                phi = 2 * (i + 1) * Math.PI / smoothness; // увеличиваем угол
                points.Add(new Point3D(realRadius * Math.Cos(phi),
                                       realRadius * Math.Sin(phi),
                                       Z));
            }
            Z = center.Z + step * height / 2;
            phi = 0;
            for (int i = 1; i <= smoothness; ++i)
            {
                phi = 2 * (i + 1) * Math.PI / smoothness; // увеличиваем угол
                points.Add(new Point3D(realRadius * Math.Cos(phi),
                                       realRadius * Math.Sin(phi),
                                       Z));
            }
            points.Add(new Point3D(center.X, center.Y, Z));
        }
    }
}
