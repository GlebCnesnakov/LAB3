using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fishkii
{
    public class Dice
    {
        int _count;
        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }
        public Dice()// создавая кубик автоматически бросаем его
        {
            Random random = new Random();
            _count = random.Next(1, 7);//игральный кубик бросается, результат от 1 до 6
        }
    }
}
