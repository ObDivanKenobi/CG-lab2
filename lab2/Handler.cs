using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDrawingMethods;
//using MatrixNamespace;

namespace lab2
{
    class Handler
    {
        static double eps = 0.0001;

        Bitmap canvas; //куда рисуем
        Graphics graphics;
        Point3D light;
        Pen pen;
        Brush brush;
        Cylinder cylinder;
        Matrix transform = new Matrix(3, 3, new double[,] { { 1, 0, 0 },
                                                            { 0, 1, 0 },
                                                            { 0, 0, 1 } });

        private static Matrix RotationCoordinateSystemAroundXMatrix(double degrees)
        {
            double radians = degrees * Math.PI / 180;
            return new Matrix(3, 3, new double[,] { { 1 , 0, 0 },
                                                  {  0, Math.Cos(radians), Math.Sin(radians) },
                                                  { 0 , -Math.Sin(radians), Math.Cos(radians) } });

        }

        //private static Matrix RotationCoordinateSystemAroundYMatrix(double degrees)
        //{
        //    double radians = degrees * Math.PI / 180;
        //    return new Matrix(3, 3, new double[,] { { Math.Cos(radians),  0, Math.Sin(radians) },
        //                                            {       0,            1,          0        },
        //                                            { -Math.Sin(radians), 0, Math.Cos(radians) } });

        //}

        private static Matrix RotationCoordinateSystemAroundZMatrix(double degrees)
        {
            double radians = degrees * Math.PI / 180;
            return new Matrix(3, 3, new double[,] { { Math.Cos(radians), Math.Sin(radians), 0 },
                                                    { -Math.Sin(radians),  Math.Cos(radians), 0 },
                                                    {       0,            0,                 1 } });

        }

        double phi; //горизонтальный угол
        double psi; //вертикальный угол
        int brightness = 255; //яркость источника света;
        int ambient = 0; //фоновая освещённость

        public int ImageHeight { get { return canvas.Height; } }
        public int ImageWidth { get { return canvas.Width; } }

        public Handler(int bitmap_width, int bitmap_height, int radius = 4, int height = 7)
        {
            canvas = new Bitmap(bitmap_width, bitmap_height);
            cylinder = new Cylinder(radius, height, new Point3D());
            light = new Point3D();
            graphics = Graphics.FromImage(canvas);
            brush = new SolidBrush(Color.Gray);
            pen = new Pen(Color.Black, 1);
        }

        public Handler(int bitmap_width, int bitmap_height, Point3D light, int radius = 4, int height = 7)
        {
            canvas = new Bitmap(bitmap_width, bitmap_height);
            cylinder = new Cylinder(radius, height, new Point3D(1, 1, 1));
            this.light = light;
            graphics = Graphics.FromImage(canvas);
            brush = new SolidBrush(Color.Gray);
            pen = new Pen(Color.Black, 1);
        }

        Point3D ToNewSystem(Point3D point)
        {
            Point3D tmp = point.Transform(transform);
            return new Point3D(tmp.X + ImageWidth / 2, tmp.Y + ImageHeight / 2, -tmp.Z);
        }

        Vector3D ToNewSystem(Vector3D v)
        {
            Point3D tmp = new Point3D(v.X, v.Y, v.Z).Transform(transform);
            return new Vector3D(tmp.X, tmp.Y, -tmp.Z);
        }

        void DrawPoly(Polygon poly, Color c)
        {
            int zero = cylinder.Step / 2 + 1;
            Point[] points = new Point[poly.Points.Length];
            for (int i = 0; i < points.Length; ++i)
            {
                points[i] = ToNewSystem(poly.Points[i]).ToPoint();
                points[i].X -= 1;
                points[i].Y -= 1;
            }

            brush = new SolidBrush(c);
            graphics.FillPolygon(brush, points);
        }

        private Bitmap DrawRemoveUnseenEdges(Cylinder cylinder, Vector3D projectionsDirection)
        {
            for (int i = 0; i < cylinder.Count; i++)
            {
                Vector3D norm = cylinder[i].NormalVector().Normalize();

                Point3D center = cylinder[i].Barycenter(),
                        endNormal = new Point3D(norm.X * 50 + center.X, norm.Y * 50 + center.Y, norm.Z * 50 + center.Z);

                Point3D movedDirectionA = ToNewSystem(new Point3D(center.X, center.Y, center.Z)),
                        movedDirectionB = ToNewSystem(new Point3D(center.X, center.Y, center.Z - 1));
                Vector3D movedDirection = new Vector3D(movedDirectionA, movedDirectionB);

                double cos = Vector3D.Cos(ToNewSystem(norm), projectionsDirection);

                bool isVisible = cos > eps;
                //считаем косинус угла между нормалью полигона и направлением проектирования
                //если угол тупой или прямой (косинус <= 0), то грань мы не видим
                if (!isVisible)
                {
                    ////рисуем нормали
                    //graphics.DrawLine(new Pen(Color.Red, 1), ToNewSystem(center).ToPoint(), ToNewSystem(endNormal).ToPoint());
                    continue;
                }

                ////рисуем нормали
                //graphics.DrawLine(new Pen(Color.Lime, 1), ToNewSystem(center).ToPoint(), ToNewSystem(endNormal).ToPoint());

                cos = Vector3D.Cos(cylinder[i].NormalVector(), new Vector3D(cylinder[i].Barycenter(), light));
                //cos = Vector3D.Cos(norm, new Vector3D(cylinder[i].Barycenter(), light));
                double intensity = brightness * cos;
                int illumination = intensity > 0 ? Math.Min((int)(intensity+ambient), 255) : Math.Min((int)ambient, 255);
                DrawPoly(cylinder[i], Color.FromArgb(255, illumination, illumination, illumination));
            }

            return canvas;
        }

        ///удаление нелицевых граней !!! НАКОНЕЦ-ТО РАБОТАЕТ
        private Bitmap Draw()
        {
            graphics.Clear(Color.LightCyan);

            //матрица перехода к новым координатам
            transform = RotationCoordinateSystemAroundXMatrix(psi) * RotationCoordinateSystemAroundZMatrix(phi);

            Point3D directionA = new Point3D(0, 0, 0),
                    directionB = new Point3D(0, 0, -1);

            //направление проектирования противоположно направлено с нормалью картинной плоскости XoY
            Vector3D projectionsDirection = new Vector3D(directionA, directionB);

            DrawRemoveUnseenEdges(cylinder, projectionsDirection);

            return canvas;
        }

        ///Алгоритм художника
        //private Bitmap Draw()
        //{
        //    graphics.Clear(Color.LightCyan);

        //    DrawAxis();

        //    List<Polygon> polygons = new List<Polygon>();
        //    for (int i = 0; i < cylinder.Count; ++i)
        //        polygons.Add(cylinder[i]);

        //    polygons.Sort((p1, p2) => { return ToNewSystem(p2.Barycenter()).Z.CompareTo(ToNewSystem(p1.Barycenter()).Z); });
        //    foreach (var p in polygons)
        //    {
        //        double d_illumination = 0;
        //        Vector3D norm = p.NormalVector();
        //        //if (IsInvertedNormal(p, norm, cylinder.Center))
        //        //    norm = new Vector3D(-norm.X, -norm.Y, -norm.Z);
        //        double cos = Vector3D.Cos(norm, new Vector3D(p.Barycenter(), light));
        //        double intensity = ambient + brightness * cos;
        //        if (cos > 0)
        //            d_illumination += cos * brightness;
        //        int illumination = Math.Min((int)d_illumination, 255);
        //        DrawPoly(p, Color.FromArgb(255, illumination, illumination, illumination));
        //    }

        //    return canvas;
        //}

        /// <summary>
        /// задает радиус цилиндра
        /// </summary>
        /// <param name="radius"></param>
        /// <returns></returns>
        public Bitmap SetRadius(int radius)
        {
            cylinder.Radius = radius;
            return Draw();
        }

        // задает высоту цилиндра
        public Bitmap SetHeight(int height)
        {
            cylinder.Height = height;
            return Draw();
        }

        //изменяет источник освещения
        public Bitmap SetLight(Point3D point)
        {
            light = point;
            return Draw();
        }

        //изменяет яркость источника света
        public Bitmap SetBrightness(int brightness)
        {
            this.brightness = brightness;
            return Draw();
        }

        //изменяет фоновое освещение
        public Bitmap SetAmbient(int ambient)
        {
            this.ambient = ambient;
            return Draw();
        }

        // задает углы поворота
        public Bitmap SetAngles(double phi, double psi)
        {
            this.phi = phi;
            this.psi = psi;
            return Draw();
        }

        // задает размер изображения
        public Bitmap SetSize(int w, int h)
        {
            canvas = new Bitmap(w, h);
            graphics = Graphics.FromImage(canvas);
            return Draw();
        }
    }
}
