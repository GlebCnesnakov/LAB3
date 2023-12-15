using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fishkii
{
    public static class Database
    {
        static int amountOfPoints;
        static int numberOfCurrentChip;
        static List<Chip> chips;
        static string pathchips = "chips.json";
        static string pathpoints = "points.json";
        static string pathAmountPoints = "amount.json";

        public static void ChangeCurrentChip()
        {
            List<Chip> chips = System.Text.Json.JsonSerializer.Deserialize<List<Chip>>(File.ReadAllText(pathchips));
            chips[GetCurrentChip()].IsCurrent = false;
            if ((GetCurrentChip() + 1) < chips.Count)
            {
                chips[GetCurrentChip() + 1].IsCurrent = true;
            }
            else
            {
                chips[0].IsCurrent = true;
            }
            string jsonContent = System.Text.Json.JsonSerializer.Serialize(chips, new JsonSerializerOptions { WriteIndented = true, IncludeFields = true });
            File.WriteAllText(pathchips, jsonContent);
        }

        public static void SetAmountPoints(int amount)
        {
            if (File.Exists(pathAmountPoints))
            {
                string a = amount.ToString();
                File.WriteAllText(pathAmountPoints, a);
            }
            else
            {
                using (FileStream fs = File.Create(pathAmountPoints)) { }
                string a = amount.ToString();
                File.WriteAllText(pathAmountPoints, a);
            }
        }
        public static int GetAmountOfPoints()
        {
            string a = File.ReadAllText(pathAmountPoints);
            return Int32.Parse(a);
        }
        public static void DeleteAllChipsAndPoints()
        {
            List<Chip> chips = System.Text.Json.JsonSerializer.Deserialize<List<Chip>>(File.ReadAllText(pathchips));
            List<Point> points = System.Text.Json.JsonSerializer.Deserialize<List<Point>>(File.ReadAllText(pathpoints));
            chips.Clear();
            points.Clear();
            File.Delete(pathchips);
            File.Delete(pathpoints);
            File.Delete(pathAmountPoints);
        }

        public static bool IsCurrentChipWon()
        {
            List<Chip> chips = System.Text.Json.JsonSerializer.Deserialize<List<Chip>>(File.ReadAllText(pathchips));
            if (chips.Any(chip => chip.Position >= GetAmountOfPoints())) return true;
            else return false;
        }

        public static void ChangePositionBackCurrentChip(int number)
        {
            List<Chip> chips = System.Text.Json.JsonSerializer.Deserialize<List<Chip>>(File.ReadAllText(pathchips));
            chips[GetCurrentChip()].Position -= number;
            if (chips[GetCurrentChip()].Position < 0)
            {
                chips[GetCurrentChip()].Position = 0;
            }
            string jsonContent = System.Text.Json.JsonSerializer.Serialize(chips, new JsonSerializerOptions { WriteIndented = true, IncludeFields = true });
            File.WriteAllText(pathchips, jsonContent);
        }
        public static void ChangePositionCurrentChip(int number)
        {
            List<Chip> chips = System.Text.Json.JsonSerializer.Deserialize<List<Chip>>(File.ReadAllText(pathchips));
            chips[GetCurrentChip()].Position += number;
            if (chips[GetCurrentChip()].Position < 0)
            {
                chips[GetCurrentChip()].Position = 0;
            }
            string jsonContent = System.Text.Json.JsonSerializer.Serialize(chips, new JsonSerializerOptions { WriteIndented = true, IncludeFields = true });
            File.WriteAllText(pathchips, jsonContent);
        }

        static public Point GetPointByChipPosition(int number)//метод для возврата точки на которой стоит фишка
        {
            List<Chip> chips = System.Text.Json.JsonSerializer.Deserialize<List<Chip>>(File.ReadAllText(pathchips));
            List<Point> points = System.Text.Json.JsonSerializer.Deserialize<List<Point>>(File.ReadAllText(pathpoints));
            return points[chips[number].Position];//точка на которой  стоит фишка

        }

        static public int GetPositionChip(int number)
        {
            List<Chip> chips = System.Text.Json.JsonSerializer.Deserialize<List<Chip>>(File.ReadAllText(pathchips));
            return chips[number].Position;
        }

        static public void ChangePoint(int number, Condition condition)
        {
            List<Point> points = System.Text.Json.JsonSerializer.Deserialize<List<Point>>(File.ReadAllText(pathpoints));
            points[number].condition = condition;//теперь точка необычная
            string jsonContent = System.Text.Json.JsonSerializer.Serialize(points, new JsonSerializerOptions { WriteIndented = true, IncludeFields = true });
            File.WriteAllText(pathpoints, jsonContent);
        }


        static public List<Point> GetPoints()
        {
            if (File.Exists(pathpoints))
            {
                List<Point> points = System.Text.Json.JsonSerializer.Deserialize<List<Point>>(File.ReadAllText(pathpoints));
                return points;
            }
            else
            {
                using (FileStream fs = File.Create(pathpoints)) { }
                File.WriteAllText(pathpoints, "[]");
                return null;
            }
        }

        static public void SetPoints(int amountOfPoints)//создаются точки
        {
            //using (FileStream fs = File.Create(pathpoints)) { }//создаётся файл
            List<Point> points = new List<Point>();
            for (int i = 0; i < amountOfPoints; i++)
            {
                points.Add(new Point(new Condition(Conditions.Normal)));//все точки обычные
            }
            string jsonContent = System.Text.Json.JsonSerializer.Serialize(points, new JsonSerializerOptions { WriteIndented = true, IncludeFields = true });
            File.WriteAllText(pathpoints, jsonContent);
        }

        static public void SetCurrentChip(int number)//методы set и get для получения фишки, которая будет совершать ход
        {
            List<Chip> chips = System.Text.Json.JsonSerializer.Deserialize<List<Chip>>(File.ReadAllText(pathchips));
            foreach (Chip chip in chips)
            {
                chip.IsCurrent = false;
            }
            chips[number].IsCurrent = true;
            string jsonContent = System.Text.Json.JsonSerializer.Serialize(chips, new JsonSerializerOptions { WriteIndented = true, IncludeFields = true });
            File.WriteAllText(pathchips, jsonContent);
        }


        static public int GetCurrentChip()
        {
            List<Chip> chips = System.Text.Json.JsonSerializer.Deserialize<List<Chip>>(File.ReadAllText(pathchips));
            int a = chips.FindIndex(chip => chip.IsCurrent == true);
            return a;
        }


        static public List<Chip> GetAllChips()
        {
            if (File.Exists(pathchips))
            {
                List<Chip> chips = System.Text.Json.JsonSerializer.Deserialize<List<Chip>>(File.ReadAllText(pathchips));
                return chips;
            }
            else
            {
                using (FileStream fs = File.Create(pathchips)) { }
                File.WriteAllText(pathchips, "[]");
                return null;
            }
        }
        static public void AddChip(Chip chip)
        {
            if (File.Exists(pathchips))
            {
                string g;
                g = File.ReadAllText(pathchips);
                if (g != "")
                {
                    List<Chip> chips = System.Text.Json.JsonSerializer.Deserialize<List<Chip>>(File.ReadAllText(pathchips));
                    chips.Add(chip);
                    string jsonContent = System.Text.Json.JsonSerializer.Serialize(chips, new JsonSerializerOptions { WriteIndented = true, IncludeFields = true });
                    File.WriteAllText(pathchips, jsonContent);
                }
                else
                {
                    List<Chip> chips = new List<Chip>();
                    chips.Add(chip);
                    string jsonContent = System.Text.Json.JsonSerializer.Serialize(chips, new JsonSerializerOptions { WriteIndented = true, IncludeFields = true });
                    File.WriteAllText(pathchips, jsonContent);
                }
            }
            else
            {
                using (FileStream fs = File.Create(pathchips)) { }
                List<Chip> chips = new List<Chip>();
                chips.Add(chip);
                string jsonContent = System.Text.Json.JsonSerializer.Serialize(chips, new JsonSerializerOptions { WriteIndented = true, IncludeFields = true });
                File.WriteAllText(pathchips, jsonContent);
            }
        }



    }
}
