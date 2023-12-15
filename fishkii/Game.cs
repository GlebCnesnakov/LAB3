using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fishkii
{
    public static class Field
    {

        public static List<Chip> chips { get; set; } = new List<Chip>();
        public static List<Point> points { get; set; } = new List<Point>();

        public static void ChangeCurrentChip()
        {
            Database.ChangeCurrentChip();
        }
        public static void DeleteAllChipsAndPoints()
        {
            Database.DeleteAllChipsAndPoints();
        }
        public static bool IsCurrentChipWon()
        {
            return Database.IsCurrentChipWon();
        }

        public static Point GetPointByChipPosition()
        {
            return Database.GetPointByChipPosition(GetCurrentChip());
        }
        public static void ChangePositionBackCurrentChip(int number)
        {
            Database.ChangePositionBackCurrentChip(number);
        }
        public static void ChangePositionCurrentChip(int number)//меняется позиция конкретной фишки
        {
            Database.ChangePositionCurrentChip(number);
        }
        public static int GetPositioinCurrentChip()//получаем позицию текущей фишки
        {
            return Database.GetPositionChip(GetCurrentChip());
        }
        public static int GetCurrentChip()//получаем порядковый номер фишки как таковой
        {
            return Database.GetCurrentChip();//вернули фишку, которая ходит
        }
        static public void GetAllChips()
        {
            chips = Database.GetAllChips();
        }
        static public int ThrowDice()
        {
            Dice dice = new Dice();
            return dice.Count;
        }
        static public void ChangeConditionOfPoint(int number, Condition condition)
        {
            Database.ChangePoint(number, condition);
        }


        static public void GetPoints()
        {
            points = Database.GetPoints();
        }

        static public void SetPoints(int amountOfPoints)
        {
            Database.SetPoints(amountOfPoints);
        }




        static public void AddChip()
        {
            if (chips == null || chips.Count <= 10)
            {
                int color = 0;
                Chip newChip = new Chip();
                while (true)
                {
                    Console.WriteLine("Создание фишки\n1 - Красная\n2 - Синяя\n3 - Чёрная\n4 - Белая\n5 - Розовая\n6 - Серая");
                    try
                    {
                        color = Int32.Parse(Console.ReadLine());
                        if (color < 0 || color > 6) throw new Exception();
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Введите цифру");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Цифре не соответствует цвет");
                    }
                }
                switch (color)
                {
                    case 1:

                        newChip = new Chip(ColorOfChip.Red, 0);
                        break;
                    case 2:
                        newChip = new Chip(ColorOfChip.Blue, 0);
                        break;
                    case 3:
                        newChip = new Chip(ColorOfChip.Black, 0);
                        break;
                    case 4:
                        newChip = new Chip(ColorOfChip.White, 0);
                        break;
                    case 5:
                        newChip = new Chip(ColorOfChip.Pink, 0);
                        break;
                    case 6:
                        newChip = new Chip(ColorOfChip.Gray, 0);
                        break;
                    default: break;
                }
                if (chips == null || chips.Count == 0)//когда начинаем игру начинаем с 1 фишки
                {
                    newChip.IsCurrent = true;
                }
                Database.AddChip(newChip);
                GetAllChips();
                Console.WriteLine($"Создана фишка {chips.Count}");
            }
            else
            {
                Console.WriteLine("Фишек не может быть больше 10!");
            }
        }
    }
}
