using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fishkii
{
    public class Program
    {
        public static void Main()
        {

            while (true)
            {
                Field.GetAllChips();
                Field.GetPoints();
                if (Field.chips != null && Field.chips.Count != 0 && Field.points != null && Field.points.Count != 0)
                {
                    int choice = 0;
                    Console.WriteLine("1 - Играть\n2 - Изменить состояние точек\n3 - Выйти");
                    while (true)
                    {
                        try
                        {
                            choice = Int32.Parse(Console.ReadLine());
                            if (choice <= 0 || choice > 2) throw new Exception();
                            break;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Неверный ввод");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Неверный ввод");
                        }
                    }
                    if (choice == 1)
                    {

                        while (true)
                        {
                            int q;
                            Console.WriteLine($"-----------------------------------------------------------\nХод {Field.GetCurrentChip() + 1} фишки\nПозиция фишки: {Field.GetPositioinCurrentChip()}");
                            Console.WriteLine("1 - Бросить кость\n2 - Выйти");
                            while (true)
                            {
                                try
                                {
                                    q = Int32.Parse(Console.ReadLine());
                                    if (q != 1) throw new Exception();
                                    break;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Неверный ввод");
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Неверный ввод");
                                }
                            }
                            if (q == 1)
                            {
                                int diceAmount = Field.ThrowDice();//бросили кубик
                                Console.WriteLine($"На кубике выпало: {diceAmount}");

                                Field.ChangePositionCurrentChip(diceAmount);//работает
                                                                            //if (Field.IsCurrentChipWon())//фишка выиграла
                                                                            //{
                                                                            //    Console.WriteLine($"Игра завершена. Победила фишка под номером {Field.GetCurrentChip() + 1}");
                                                                            //    Field.DeleteAllChipsAndPoints();//удалили всё

                                //    break;
                                //}
                                Console.WriteLine($"Фишка сделала ход на точку под номером {Field.GetPositioinCurrentChip()}");
                                Condition currentCondition = new Condition();
                                try
                                {
                                    currentCondition = Field.GetPointByChipPosition().condition;
                                    if (Field.GetPositioinCurrentChip() == (Database.GetAmountOfPoints() - 1)) throw new ArgumentOutOfRangeException();
                                }
                                catch (ArgumentOutOfRangeException)
                                {
                                    Console.WriteLine($"Игра завершена. Победила фишка под номером {Field.GetCurrentChip() + 1}");
                                    Field.DeleteAllChipsAndPoints();//удалили всё


                                    break;
                                }
                                if (currentCondition.condition == Conditions.Normal)
                                {
                                    Console.WriteLine($"Точка - обычная\nПозиция фишки: {Field.GetPositioinCurrentChip()}\n-----------------------------------------------------------");
                                }
                                else if (currentCondition.condition == Conditions.Doubleforwardstep)
                                {

                                    Field.ChangePositionCurrentChip(2 * diceAmount);
                                    Console.WriteLine($"Точка - шаг вперёд х2\nПозиция фишки: {Field.GetPositioinCurrentChip()}\n-----------------------------------------------------------");
                                }
                                else if (currentCondition.condition == Conditions.Doublebackstep)
                                {
                                    Field.ChangePositionBackCurrentChip(diceAmount * 2);
                                    Console.WriteLine($"Точка - шаг назад х2\nПозиция фишки: {Field.GetPositioinCurrentChip()}\n-----------------------------------------------------------");
                                }
                                else if (currentCondition.condition == Conditions.Againstep)
                                {
                                    Field.ChangePositionBackCurrentChip(diceAmount);
                                    Console.WriteLine($"Точка - ход назад\nПозиция фишки: {Field.GetPositioinCurrentChip()}\n-----------------------------------------------------------");
                                }


                                if (Field.IsCurrentChipWon())//фишка выиграла
                                {
                                    Console.WriteLine($"Игра завершена. Победила фишка под номером {Field.GetCurrentChip() + 1}");
                                    Field.DeleteAllChipsAndPoints();//удалили всё
                                    break;
                                }

                                Field.ChangeCurrentChip();
                            }
                            else if (q == 2)
                            {
                                break;
                            }
                        }
                    }
                    else if (choice == 2)
                    {
                        int h = Database.GetAmountOfPoints();
                        int r;
                        Console.WriteLine("Сколько точек подлежит изменению?");
                        while (true)
                        {
                            try
                            {
                                r = Int32.Parse(Console.ReadLine());
                                if (r < 0 || r > Database.GetAmountOfPoints() - 1) throw new Exception();
                                break;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Неверный ввод");
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Неверный ввод");
                            }
                        }
                        int e;
                        for (int i = 0; i < r; i++)
                        {
                            Console.WriteLine("Точка для изменения:");
                            while (true)
                            {
                                try
                                {
                                    e = Int32.Parse(Console.ReadLine());
                                    if (e < 1 || e > Database.GetAmountOfPoints() - 1) throw new Exception();
                                    break;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Неверный ввод");
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Неверный ввод");
                                }
                            }
                            int ha;
                            Console.WriteLine("1 - Шаг вперёд х2\n2 - Шаг назад х2\n3 - Ход обратно");
                            while (true)
                            {
                                try
                                {
                                    ha = Int32.Parse(Console.ReadLine());
                                    if (ha < 1 || ha > 3) throw new Exception();
                                    break;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Неверный ввод");
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Неверный ввод");
                                }
                            }
                            switch (ha)
                            {
                                case 1:
                                    Field.ChangeConditionOfPoint(e, new Condition(Conditions.Doubleforwardstep));
                                    break;
                                case 2:
                                    Field.ChangeConditionOfPoint(e, new Condition(Conditions.Doublebackstep));
                                    break;
                                case 3:
                                    Field.ChangeConditionOfPoint(e, new Condition(Conditions.Againstep));
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (choice == 3)
                    {
                        break;
                    }
                }







                //если игра началась
                else
                {
                    int choic = 0;
                    int countChips = 0;
                    Console.WriteLine("Создание игры");
                    while (true)
                    {
                        Console.WriteLine("Сколько фишек будет учавствовать в игре?");
                        try
                        {
                            countChips = Int32.Parse(Console.ReadLine());
                            if (countChips <= 0 || countChips >= 10) throw new Exception();
                            break;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Неверный ввод");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Количество фишек должно быть положительным числом меньше 10");
                        }

                    }
                    for (int i = 1; i <= countChips; i++)
                    {
                        Field.AddChip();
                    }
                    int h = 0;
                    while (true)
                    {
                        Console.WriteLine("Сколько точек будет на поле?");
                        try
                        {
                            h = Int32.Parse(Console.ReadLine());
                            if (h < 1 || h > 100) throw new Exception();
                            Database.SetAmountPoints(h + 1);
                            break;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Неверный ввод");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Неверный ввод");
                        }
                    }
                    Field.SetPoints(h + 1);//Создали фишки с condition normal
                    Console.WriteLine("1 - Изменить состояние точек\n2 - Оставить точки обычными");
                    int y;
                    while (true)
                    {
                        try
                        {
                            y = Int32.Parse(Console.ReadLine());
                            if (y < 1 || y > 2) throw new Exception();
                            break;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Неверный ввод");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Неверный ввод");
                        }
                    }
                    if (y == 1)
                    {
                        int r;
                        Console.WriteLine("Сколько точек подлежит изменению?");
                        while (true)
                        {
                            try
                            {
                                r = Int32.Parse(Console.ReadLine());
                                if (r < 0 || r > Database.GetAmountOfPoints() - 1) throw new Exception();
                                break;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Неверный ввод");
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Неверный ввод");
                            }
                        }
                        int e;
                        for (int i = 0; i < r; i++)
                        {
                            Console.WriteLine("Точка для изменения:");
                            while (true)
                            {
                                try
                                {
                                    e = Int32.Parse(Console.ReadLine());
                                    if (e < 1 || e > h) throw new Exception();
                                    break;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Неверный ввод");
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Неверный ввод");
                                }
                            }
                            int ha;
                            Console.WriteLine("1 - Шаг вперёд х2\n2 - Шаг назад х2\n3 - Ход обратно");
                            while (true)
                            {
                                try
                                {
                                    ha = Int32.Parse(Console.ReadLine());
                                    if (ha < 1 || ha > 3) throw new Exception();
                                    break;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Неверный ввод");
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Неверный ввод");
                                }
                            }
                            switch (ha)
                            {
                                case 1:
                                    Field.ChangeConditionOfPoint(e, new Condition(Conditions.Doubleforwardstep));
                                    break;
                                case 2:
                                    Field.ChangeConditionOfPoint(e, new Condition(Conditions.Doublebackstep));
                                    break;
                                case 3:
                                    Field.ChangeConditionOfPoint(e, new Condition(Conditions.Againstep));
                                    break;
                                default:
                                    break;
                            }
                        }
                    }



                }
            }


        }
    }
}
