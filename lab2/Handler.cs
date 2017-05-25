using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDrawingMethods;

namespace lab2
{
    class Handler
    {
        Bitmap canvas; //куда рисуем
        Point3D light;
        Cylinder cylinder;
        ArrowLine3D axisX = new ArrowLine3D(new Point3D(0, 0, 0), new Point3D(150, 0, 0),
                                    new Triangle3D(new Point3D(150, 0, 0), new Point3D(140, -10, 0), new Point3D(140, 10, 0))),
                    axisY = new ArrowLine3D(new Point3D(0, 0, 0), new Point3D(0, 150, 0),
                                    new Triangle3D(new Point3D(0, 150, 0), new Point3D(-10, 140, 0), new Point3D(10, 140, 0))),
                    axisZ = new ArrowLine3D(new Point3D(0, 0, 0), new Point3D(0, 0, 150),
                                    new Triangle3D(new Point3D(0, 0, 150), new Point3D(-10, 10, 140), new Point3D(10, -10, 140)));
        Color axisXcolor = Color.Red,
              axisYcolor = Color.Green,
              axisZcolor = Color.Blue;

        double phi; //горизонтальный угол
        double psi; //вертикальный угол
        const int brightness = 255; //яркость источника света;
        double ambient = 0; //фоновая освещённость

        public int ImageHeight { get { return canvas.Height; } }
        public int ImageWidth { get { return canvas.Width; } }

        public Handler(int bitmap_width, int bitmap_height, int radius = 4, int height = 7)
        {
            canvas = new Bitmap(bitmap_width, bitmap_height);
            cylinder = new Cylinder(radius, height, new Point3D());
            light = new Point3D();
        }

        public Handler(int bitmap_width, int bitmap_height, Point3D light, int radius = 4, int height = 7)
        {
            canvas = new Bitmap(bitmap_width, bitmap_height);
            cylinder = new Cylinder(radius, height, new Point3D());
            this.light = light;
        }

        Point3D ToNewSystem(Point3D point)
        {
            double x1 = point.X * Math.Cos(phi) + point.Y * Math.Sin(phi);
            double y1 = point.Y * Math.Cos(phi) - point.X * Math.Sin(phi);

            double y2 = y1 * Math.Cos(psi) + point.Z * Math.Sin(psi);
            double z2 = point.Z * Math.Cos(psi) - y1 * Math.Sin(psi);
            return new Point3D(x1 + ImageWidth / 2, z2 + ImageHeight / 2, y2);
        }

        void DrawPoly(Triangle3D poly, Color c)
        {
            int zero = cylinder.Step / 2 + 1;
            Point[] points = new Point[3];
            Point3D newA = ToNewSystem(poly.A),
                    newB = ToNewSystem(poly.B),
                    newC = ToNewSystem(poly.C);

            points[0] = ToNewSystem(poly.A).ToPoint();
            points[1] = ToNewSystem(poly.B).ToPoint();
            points[2] = ToNewSystem(poly.C).ToPoint();
            //if (newA.Z <= zero && newB.Z <= zero && newC.Z <= zero)
            //DrawingMethods.FillPolygon(canvas, c, points);
            DrawingMethods.DrawPolygon(canvas, c, points);

            //DrawingMethods.DrawPolygon(canvas, c, points);
        }

        void DrawAxis(ArrowLine3D axis, Color c)
        {
            Point[] points = new Point[2];
            points[0] = ToNewSystem(axis.Start).ToPoint();
            points[1] = ToNewSystem(axis.End).ToPoint();

            DrawingMethods.DrawLine(canvas, c, points[0], points[1]);
        }

        void DrawAxis()
        {
            DrawAxis(axisX, axisXcolor);
            DrawPoly(axisX.Arrow, axisXcolor);
            DrawAxis(axisY, axisYcolor);
            DrawPoly(axisY.Arrow, axisYcolor);
            DrawAxis(axisZ, axisZcolor);
            DrawPoly(axisZ.Arrow, axisZcolor);
        }

        private bool IsVisible(Triangle3D edge, Vector3D norm, Point3D point)
        {
            double d = -(norm.X * edge.A.X + norm.Y * edge.A.Y + norm.Z * edge.A.Z);
            return point.X * norm.X + point.Y * norm.Y + norm.Z * point.Z + d > 0;
        }

        //удаление нелицевых граней не работает ._.
        //private Bitmap Draw()
        //{
        //    int h = canvas.Height,
        //        w = canvas.Width;
        //    canvas = new Bitmap(w, h);

        //    //направление проектирования противоположно направлено с нормалью картинной плоскости XoY
        //    Vector3D projectionsDirection = new Vector3D(ToNewSystem(new Point3D(0, 0, 0)), ToNewSystem(new Point3D(0, 0, 1)));

        //    DrawAxis();
        //    for (int i = 0; i < cylinder.Count; i++)
        //    {
        //        Vector3D norm = cylinder[i].NormalVector().Normalize();
        //        if (IsVisible(cylinder[i], norm, cylinder.Center))
        //            norm = new Vector3D(-norm.X, -norm.Y, -norm.Z);

        //        double cos = Vector3D.Cos(norm, projectionsDirection);
        //        //считаем косинус угла между нормалью полигона и направлением проектирования
        //        //если угол тупой (косинус < 0), то грань мы не видим
        //        if (cos <= 0)
        //            continue;

        //        //cos = Vector3D.Cos(cylinder[i].NormalVector(), new Vector3D(cylinder[i].Barycenter(), light));
        //        //double intensity = ambient + brightness * cos;
        //        //double d_illumination = 0;
        //        //double cos = Vector3D.Cos(cylinder[i].NormalVector(), new Vector3D(cylinder[i].Barycenter(), light));
        //        //if (cos > 0)
        //        //    d_illumination += cos * brightness;
        //        //int illumination = Math.Min((int)d_illumination, 255);
        //        //DrawPoly(cylinder[i], Color.FromArgb(255, illumination, illumination, illumination));
        //        if (i < cylinder.Radius * 5)
        //            DrawPoly(cylinder[i], Color.Aqua);
        //        else if (i == cylinder.Radius * 5)
        //            DrawPoly(cylinder[i], Color.DeepPink);
        //        else
        //            DrawPoly(cylinder[i], Color.Black);
        //    }
        //    return canvas;
        //}

        //с z-буфером

        private Bitmap Draw()
        {
            int h = canvas.Height,
                w = canvas.Width;
            canvas = new Bitmap(w, h);
            double[,] z_buf = new double[h, w];
            for (int i = 0; i < h; ++i)
                for (int j = 0; j < w; ++j)
                {
                    z_buf[i, j] = double.PositiveInfinity;
                    canvas.SetPixel(j, i, Color.Black);
                }

            for (int i = 0; i < cylinder.Count; i++)
            {
                double cos = Vector3D.Cos(cylinder[i].NormalVector(), new Vector3D(cylinder[i].Barycenter(), light));
                double intensity = 0;
                if (cos > 0)
                    intensity = ambient + brightness * cos;
                int illumination = Math.Min((int)intensity, 255);

                Color filling = Color.FromArgb(255, illumination, illumination, illumination);
                Point3D p1 = ToNewSystem(cylinder[i].A), 
                        p2 = ToNewSystem(cylinder[i].B), 
                        p3 = ToNewSystem(cylinder[i].C);
                double a = p1.Y * (p2.Z - p3.Z) + p2.Y * (p3.Z - p1.Z) + p3.Y * (p1.Z - p2.Z);
                double b = p1.Z * (p2.X - p3.X) + p2.Z * (p3.X - p1.X) + p3.Z * (p1.X - p2.X);
                double c = p1.X * (p2.Y * p3.Z - p2.Y * p2.Z) + p2.X * (p3.Y * p1.Z - p1.Y * p3.Z) + p3.X * (p1.Y * p2.Z - p2.Y * p1.Z);
                double d = -(a * p1.X + b * p1.Y + c * p1.Z);

                Dictionary<int, List<int>> x_lists = new Dictionary<int, List<int>>();
                Point[] points = { p1.ToPoint(), p2.ToPoint(), p3.ToPoint() };
                Point A, B;

                for (int j = 0; j < points.Count() - 1; ++j)
                {
                    A = points[j];
                    B = points[j + 1];

                    if (A.Y == B.Y)
                        break;

                    if (A.Y > B.Y)
                    {
                        Point tmp = A;
                        A = B;
                        B = tmp;
                    }

                    int y = A.Y;
                    double dx = (double)(B.X - A.X) / (B.Y - A.Y);
                    double x = A.X + dx * (y - A.Y);

                    while (y < B.Y)
                    {
                        if (!x_lists.Keys.Contains(y))
                            x_lists.Add(y, new List<int>());

                        x_lists[y].Add((int)Math.Round(x));
                        ++y;
                        x += dx;
                    }
                }

                A = points.Last();
                B = points.First();
                if (A.Y != B.Y)
                {
                    if (A.Y > B.Y)
                    {
                        Point tmp = A;
                        A = B;
                        B = tmp;
                    }

                    int y = A.Y;
                    double dx = (double)(B.X - A.X) / (B.Y - A.Y);
                    double x = A.X + dx * (y - A.Y);

                    while (y <= B.Y)
                    {
                        if (!x_lists.Keys.Contains(y))
                            x_lists.Add(y, new List<int>());

                        x_lists[y].Add((int)Math.Round(x));
                        ++y;
                        x += dx;
                    }
                }

                foreach (var p in x_lists)
                {
                    //если y вне bitmap, переходим к следующим у.
                    if (p.Key < 0 || p.Key > canvas.Height) continue;

                    var list = p.Value;
                    list.Sort();

                    if (list.Count() == 1) continue;

                    for (int k = 0; k <= list.Count() / 2; k += 2)
                    {
                        for (int j = list[k] + 1; j <= list[k + 1] && j < canvas.Height; ++j)
                        {
                            if (j < 0)
                                j = 0;
                            double z = -(a * j + b * p.Key + d) / c;
                            if (z_buf[j, p.Key] > z)
                            {
                                z_buf[j, p.Key] = z;
                                canvas.SetPixel(j, p.Key, filling);
                            }
                        }
                    }
                }
            }
            return canvas;
        }

        // задает радиус цилиндра
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


        // задает углы поворота
        public Bitmap SetAngles(double phi, double psi)
        {
            this.phi = phi * Math.PI / 180;
            this.psi = psi * Math.PI / 180;
            return this.Draw();
        }

        // задает размер изображения
        public Bitmap SetSize(int w, int h)
        {
            canvas = new Bitmap(w, h);
            return Draw();
        }
    }
}
