using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fishkii
{
    public enum ColorOfChip
    {
        Red, Green, Blue, Black, White, Pink, Gray
    }
    public class Chip
    {

        int _position = 1;
        ColorOfChip _color;
        bool _isCurrent;
        
        public int Number { get; set; }
        public bool IsCurrent { get { return _isCurrent; } set { _isCurrent = value; } }
        public ColorOfChip Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public int Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Chip(ColorOfChip color, int position)
        {
            Color = color;
            Position = position;
        }
        public Chip() { }
        public override string ToString()
        {
            return $"Фишка";
        }
    }
}
