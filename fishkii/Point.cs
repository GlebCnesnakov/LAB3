using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fishkii
{
    public class Point
    {

        Condition _condition; // состояние точки
        //public int Number { get { return _number; } set { _number = value; } }
        public Condition condition
        {
            get { return _condition; }
            set { _condition = value; }
        }
        public Point(Condition condition)
        {
            //Number = number;
            this.condition = condition;
        }

        
    }
}
