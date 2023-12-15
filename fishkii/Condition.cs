using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fishkii
{
    public enum Conditions
    {
        Doubleforwardstep, Doublebackstep, Againstep, Normal
    }

    public class Condition
    {
        Conditions _condition;
        public Conditions condition { get { return _condition; } set { _condition = value; } }
        public Condition(Conditions condition)
        {
            this.condition = condition;
        }
        public Condition() { }
        public override string ToString()
        {
            switch (condition)
            {
                case Conditions.Doubleforwardstep:
                    return "Ход вперед х2";
                case Conditions.Doublebackstep:
                    return "Ход назад x2";
                case Conditions.Againstep:
                    return "Ход в обратную сторону";
                default:
                    return "";
            }
        }
    }
}
