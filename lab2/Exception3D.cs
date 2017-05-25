using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    /// <summary>
    /// Класс исключений для 3D примитивов и фигур.
    /// </summary>
    class Exeption3D : Exception
    {
        public Exeption3D()
        {
        }

        public Exeption3D(string message)
            : base(message)
        {
        }
    }
}
