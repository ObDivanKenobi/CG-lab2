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
        class Cell
        {
            public double z = double.NegativeInfinity;
            public Color c = default(Color);
        }

        static double eps = 0.0001;

        Bitmap canvas; //куда рисуем
        Graphics graphics;
        Point3D light;
        Pen pen;
        Brush brush;
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
        const int brightness = 255; //яркость источника света;
        double ambient = 0; //фоновая освещённость

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

        Point3D ToNewSystem(Point3D point, bool inactive)
        {
            double x1 = point.X * Math.Cos(phi) + point.Y * Math.Sin(phi);
            double y1 = point.Y * Math.Cos(phi) - point.X * Math.Sin(phi);

            double y2 = y1 * Math.Cos(psi) + point.Z * Math.Sin(psi);
            double z2 = point.Z * Math.Cos(psi) - y1 * Math.Sin(psi);
            return new Point3D(x1 + ImageWidth / 2, y2 + ImageHeight / 2, z2);
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
            //if (newA.Z <= zero && newB.Z <= zero && newC.Z <= zero)
            //DrawingMethods.FillPolygon(canvas, c, points);
            brush = new SolidBrush(c);
            graphics.FillPolygon(brush, points);
            //graphics.DrawPolygon(pen, points);

            //DrawingMethods.DrawPolygon(canvas, c, points);
        }

        void DrawAxis(ArrowLine3D axis, Color c)
        {
            Point[] points = new Point[2];
            points[0] = ToNewSystem(axis.Start).ToPoint();//new Point((int)axis.Start.X + ImageWidth / 2, (int)axis.Start.Y + ImageHeight / 2);//
            points[1] = ToNewSystem(axis.End).ToPoint();//new Point((int)axis.End.X + ImageWidth / 2, (int)axis.End.Y + ImageHeight / 2);//

            Pen axisPen = new Pen(c, 1);
            graphics.DrawLine(axisPen, points[0], points[1]);
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

        ///удаление нелицевых граней  v1 не работает._.
        private Bitmap Draw()
        {
            graphics.Clear(Color.LightCyan);

            //матрица перехода к новым координатам
            transform = RotationCoordinateSystemAroundXMatrix(psi) * RotationCoordinateSystemAroundZMatrix(phi);

            Point3D directionA = new Point3D(0, 0, 0),
                    directionB = new Point3D(0, 0, -1);

            //направление проектирования противоположно направлено с нормалью картинной плоскости XoY
            Vector3D projectionsDirection = new Vector3D(directionA, directionB);

            DrawAxis();
            Pen axisPen = new Pen(Color.HotPink, 1);
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

                DrawPoly(cylinder[i], Color.Black);

                cos = Vector3D.Cos(cylinder[i].NormalVector(), new Vector3D(cylinder[i].Barycenter(), light));
                //cos = Vector3D.Cos(norm, new Vector3D(cylinder[i].Barycenter(), light));
                double intensity = ambient + brightness * cos;
                int illumination = intensity > 0 ? Math.Min((int)intensity, 255) : 0;
                DrawPoly(cylinder[i], Color.FromArgb(255, illumination, illumination, illumination));
                //if (i < cylinder.Radius * 5)
                //    DrawPoly(cylinder[i], Color.Aqua);
                //else if (i == cylinder.Radius * 5)
                //    DrawPoly(cylinder[i], Color.DeepPink);
                //else
                //    DrawPoly(cylinder[i], Color.Black);
            }

            if (directionA.ToPoint() == directionB.ToPoint())
                canvas.SetPixel(directionA.ToPoint().X, directionA.ToPoint().Y, Color.Violet);
            else
                graphics.DrawLine(new Pen(Color.Violet, 1), directionA.ToPoint(), directionB.ToPoint());
            return canvas;
        }

        ///удаление нелицевых граней v2
        //private Bitmap Draw()
        //{
        //    graphics.Clear(Color.LightCyan);
        //    //матрица перехода к новым координатам
        //    transform = RotationCoordinateSystemAroundXMatrix(psi) * RotationCoordinateSystemAroundZMatrix(phi);

        //    Point3D camera = ToNewSystem(new Point3D(0, 0, -(Math.Max(cylinder.Height, cylinder.Radius)+500)));

        //    DrawAxis();
        //    Pen axisPen = new Pen(Color.HotPink, 1);
        //    for (int i = 0; i < cylinder.Count; i++)
        //    {
        //        Vector3D norm = cylinder[i].NormalVector().Normalize();
        //        Point3D center = cylinder[i].Barycenter(),
        //                endNormal = new Point3D(norm.X * 50 + center.X, norm.Y * 50 + center.Y, norm.Z * 50 + center.Z);

        //        if (!IsVisible(cylinder[i], camera))
        //        {
        //            //рисуем нормали
        //            graphics.DrawLine(new Pen(Color.Red, 1), ToNewSystem(center).ToPoint(), ToNewSystem(endNormal).ToPoint());
        //            continue;
        //        }

        //        //рисуем нормали
        //        graphics.DrawLine(new Pen(Color.Lime, 1), ToNewSystem(center).ToPoint(), ToNewSystem(endNormal).ToPoint());

        //        DrawPoly(cylinder[i], Color.Black);

        //        //cos = Vector3D.Cos(cylinder[i].NormalVector(), new Vector3D(cylinder[i].Barycenter(), light));
        //        //double intensity = ambient + brightness * cos;
        //        //double d_illumination = 0;
        //        //if (cos > 0)
        //        //    d_illumination += cos * brightness;
        //        //int illumination = Math.Min((int)d_illumination, 255);
        //        //DrawPoly(cylinder[i], Color.FromArgb(255, illumination, illumination, illumination));
        //        //if (i < cylinder.Radius * 5)
        //        //    DrawPoly(cylinder[i], Color.Aqua);
        //        //else if (i == cylinder.Radius * 5)
        //        //    DrawPoly(cylinder[i], Color.DeepPink);
        //        //else
        //        //    DrawPoly(cylinder[i], Color.Black);
        //    }

        //    return canvas;
        //}

        ///с z-буфером
        //private Bitmap Draw()
        //{
        //    int h = canvas.Height,
        //        w = canvas.Width;
        //    canvas = new Bitmap(w, h);
        //    Cell[,] z_buf = new Cell[h, w];
        //    for (int i = 0; i < h; ++i)
        //        for (int j = 0; j < w; ++j)
        //            z_buf[i, j] = new Cell();

        //    for (int i = 0; i < cylinder.Count; i++)
        //    {
        //        double cos = Vector3D.Cos(cylinder[i].NormalVector(), new Vector3D(cylinder[i].Barycenter(), light));
        //        double intensity = ambient + brightness * cos;
        //        if (intensity < 0)
        //            continue;

        //        int illumination = Math.Min((int)intensity, 255);

        //        Color filling = Color.FromArgb(255, illumination, illumination, illumination);
        //        FillZBuf(cylinder[i], filling, z_buf);
        //    }

        //    for (int i = 0; i < h; ++i)
        //        for (int j = 0; j < w; ++j)
        //            canvas.SetPixel(j, i, double.IsNegativeInfinity(z_buf[i, j].z) ? Color.Black : z_buf[i, j].c);

        //    return canvas;
        //}

        private void FillZBuf(Polygon p, Color c, Cell[,] zbuf)
        {
            Triangle3D t = p as Triangle3D;
            Quadrilateral3D q = p as Quadrilateral3D;

            if (t != null)
                FillZBuf(ToNewSystem(t.A), ToNewSystem(t.B), ToNewSystem(t.C), c, zbuf);
            else
            {
                FillZBuf(ToNewSystem(q.A), ToNewSystem(q.B), ToNewSystem(q.C), c, zbuf);
                FillZBuf(ToNewSystem(q.C), ToNewSystem(q.D), ToNewSystem(q.A), c, zbuf);
            }
        }

        private void FillZBuf(Point3D t0, Point3D t1, Point3D t2, Color c, Cell[,] zbuf)
        {
            //вырожденные треугольники не нужны
            if (t0.Y == t1.Y && t0.Y == t2.Y)
                return;

            //Сортируем точки по возрастанию координаты Y
            if (t0.Y > t1.Y) Swap(t0, t1);
            if (t0.Y > t2.Y) Swap(t0, t2);
            if (t1.Y > t2.Y) Swap(t1, t2);

            //заполняем первую половину треугольника
            int totalHeight = (int)(t2.Y - t0.Y);

            for(int i=0; i < totalHeight; ++i)
            {
                bool isSecondHalf = i > t1.Y - t0.Y || t1.Y == t0.Y;
                int segmentHeight = isSecondHalf ? (int)(t2.Y - t1.Y) : (int)(t1.Y - t0.Y);

                double alpha = (double)i/totalHeight,
                      beta = (double)(i-(isSecondHalf?t1.Y-t0.Y:0))/segmentHeight;

                Point3D A = t0 + ((t2 - t1) * alpha),
                        B = isSecondHalf ? t1 + ((t2-t1)*beta) : t0 + ((t1-t0)*beta);

                if (A.X > B.X) Swap(A, B);
                for (int j = (int)A.X; j <= (int)B.X; ++j)
                {
                    double phi = B.X == A.X ? 1 : (j - A.X) / (B.X - A.X);
                    Point3D P = A + ((B - A) * phi);
                    if (zbuf[(int)P.X, (int)P.Y].z < (int)P.Z)
                    {
                        zbuf[(int)P.X, (int)P.Y].z = (int)P.Z;
                        zbuf[(int)P.X, (int)P.Y].c = c;
                    }
                }
            }
        }

        ///Отрисовка треугольника
        void DrawTriangle(Point t0, Point t1, Point t2, Color color)
        {
            //Сортируем точки по возрастанию координаты Y
            if (t0.Y > t1.Y) Swap(ref t0, ref t1);
            if (t0.Y > t2.Y) Swap(ref t0, ref t2);
            if (t1.Y > t2.Y) Swap(ref t1, ref t2);

            //заполняем первую половину треугольника
            int height = t2.Y - t0.Y;
            int segmentHeight = t1.Y - t0.Y + 1;
            for(int y = t0.Y; y<=t1.Y; ++y)
            {
                float alpha = (float)(y - t0.Y) / height;
                float beta = (float)(y - t0.Y) / segmentHeight;
                Point A = new Point(t0.X + (t2.X - t0.X) * (int)alpha, t0.Y + (t2.Y - t0.Y) * (int)alpha);
                Point B = new Point(t1.X + (t2.X - t1.X) * (int)beta, t1.Y + (t2.Y - t1.Y) * (int)beta);
                if (A.X > B.X) Swap(ref A, ref B);
                for (int j = A.X; j <= B.X; ++j)
                    canvas.SetPixel(j, y, color);
            }

            //заполняем вторую половину треугольника
            segmentHeight = t2.Y - t1.Y + 1;
            for (int y = t0.Y; y <= t1.Y; ++y)
            {
                float alpha = (float)(y - t0.Y) / height;
                float beta = (float)(y - t0.Y) / segmentHeight;
                Point A = new Point(t0.X + (t2.X - t0.X) * (int)alpha, t0.Y + (t2.Y - t0.Y) * (int)alpha);
                Point B = new Point(t1.X + (t2.X - t1.X) * (int)beta, t1.Y + (t2.Y - t1.Y) * (int)beta);
                if (A.X > B.X) Swap(ref A, ref B);
                for (int j = A.X; j <= B.X; ++j)
                    canvas.SetPixel(j, y, color);
            }
        }

        /// <summary>
        /// Обмен значениями двух точек.
        /// </summary>
        void Swap(ref Point a, ref Point b)
        {
            Point tmp = a;
            a = b;
            b = tmp;
        }

        /// <summary>
        /// Обмен значениями двух точек.
        /// </summary>
        void Swap(Point3D a, Point3D b)
        {
            Point3D tmp = a;
            a = b;
            b = tmp;
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
        /// 
        /// </summary>
        /// <param name="radius"></param>
        /// <returns></returns>
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
