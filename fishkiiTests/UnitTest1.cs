using fishkii;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;


namespace fishkiiTests
{
    [TestClass]
    public class DatabaseClassTests
    {
        

        [TestMethod]
        public void AddChip_NotEmpty()
        {
            Chip chipToAdd = new Chip(ColorOfChip.Red, 0);
            Database.AddChip(chipToAdd);
            List<Chip> chips = Database.GetAllChips();
            Assert.IsNotNull(chips);
            Assert.IsTrue(chips.Count > 0);
            File.Delete("chips.json");
        }

        [TestMethod]
        public void GetAllChips_NotEmpty_returned_list()
        {
        
        List<Chip> expectedChips = new List<Chip>
        {
            new Chip(ColorOfChip.Red, 0),
            new Chip(ColorOfChip.Blue, 1),
            new Chip(ColorOfChip.Black, 2)
        };
            Database.AddChip(expectedChips[0]);
            Database.AddChip(expectedChips[1]);
            Database.AddChip(expectedChips[2]);

            List<Chip> actualChips = Database.GetAllChips();
            Assert.IsNotNull(actualChips);
            File.Delete("chips.json");
        }


        [TestMethod]
        public void GetAllChips_Empty_returned_null()
        {
            List<Chip> actualChips = Database.GetAllChips();
            Assert.AreEqual(actualChips, null);
            File.Delete("chips.json");
        }

        [TestMethod]
        public void GetCurrentChip_NotEmpty()
        {
            Chip chip = new Chip
            {
                Position = 0,
                Color = ColorOfChip.Red,
                IsCurrent = true
            };
            Chip chip1 = new Chip(ColorOfChip.Black, 0);
            List<Chip> chips = new List<Chip>();
            chips.Add(chip);
            chips.Add(chip1);
            Database.AddChip(chip);
            Database.AddChip(chip1);

            Assert.AreEqual(chip, chips[Database.GetCurrentChip()]);
            File.Delete("chips.json");
        }
        [TestMethod]
        public void SetCurrentChip_IsCurrent()
        {
            Chip chip = new Chip
            {
                Position = 0,
                Color = ColorOfChip.Red,
                IsCurrent = true
            };
            Chip chip1 = new Chip(ColorOfChip.Black, 0);
            List<Chip> chips = new List<Chip>();
            Database.AddChip(chip);
            Database.AddChip(chip1);

            Database.SetCurrentChip(1);
            List<Chip> actuals = Database.GetAllChips();
            Assert.AreNotEqual(chip1, actuals[1]);//теперь объекты разные из за IsCurrent
            Assert.AreNotEqual(chip, actuals[0]);//теперь объекты разные из за IsCurrent
            File.Delete("chips.json");
        }

        [TestMethod]
        public void SetAmountPoints_FileExist()
        {
            using (FileStream fs = File.Create("amount.json")) { }
            Database.SetAmountPoints(20);
            string a = File.ReadAllText("amount.json");
            Assert.AreEqual(20, Int32.Parse(a));
            File.Delete("amount.json");
        }
        [TestMethod]
        public void SetAmountPoints_NotFileExist()
        {
            Database.SetAmountPoints(20);
            Database.SetAmountPoints(20);
            string a = File.ReadAllText("amount.json");
            Assert.AreEqual(20, Int32.Parse(a));
            File.Delete("amount.json");

        }

        [TestMethod]
        public void SetPoints()
        {
            using (FileStream fs = File.Create("points.json")) { }

            Point point = new Point(new Condition(Conditions.Normal));
            Database.SetPoints(10);
            List<Point> points = System.Text.Json.JsonSerializer.Deserialize<List<Point>>(File.ReadAllText("points.json"));
            Assert.AreEqual(point.ToString(), points[1].ToString());
            File.Delete("points.json");

        }

        

        [TestMethod]
        public void GetPoints_returned_list()
        {
            using (FileStream fs = File.Create("points.json")) { }
            Point poin = new Point(new Condition(Conditions.Normal));
            Point point = new Point(new Condition(Conditions.Normal));
            Database.SetPoints(2);
            List<Point> points = Database.GetPoints();

            Assert.IsNotNull(points);
            File.Delete("points.json");

        }
        [TestMethod]
        public void GetPoints_returned_null()
        {
            List<Point> points = Database.GetPoints();
            Assert.IsNull(points);
            File.Delete("points.json");

        }
        [TestMethod]
        public void ChangeCondition()
        {
            Point point = new Point(new Condition(Conditions.Normal));
            Database.SetPoints(1);
            Database.ChangePoint(0, new Condition(Conditions.Againstep));
            List<Point> points = System.Text.Json.JsonSerializer.Deserialize<List<Point>>(File.ReadAllText("points.json"));
            Assert.AreNotEqual(points[0], point);
            File.Delete("points.json");
        }
        [TestMethod]
        public void GetPositionChip()
        {
            using (FileStream fs = File.Create("chips.json")) { }
            List<Chip> chips = new List<Chip>
            {
                new Chip(ColorOfChip.White, 0),
                new Chip(ColorOfChip.White, 1),
                new Chip(ColorOfChip.White, 0)
            };
            Database.AddChip(chips[0]);
            Database.AddChip(chips[1]);
            Database.AddChip(chips[2]);
            Assert.AreEqual(1, Database.GetPositionChip(1));//позиции равны
            File.Delete("chips.json");
        }

        [TestMethod]
        public void GetPointByChipPosition()
        {
            using (FileStream fs = File.Create("chips.json")) { }
            using (FileStream fs = File.Create("points.json")) { }
            Database.SetPoints(3);
            List<Chip> chips = new List<Chip>
            {
                new Chip(ColorOfChip.White, 0),
                new Chip(ColorOfChip.White, 1),
                new Chip(ColorOfChip.White, 0)
            };
            Database.AddChip(chips[0]);
            Database.AddChip(chips[1]);
            Database.AddChip(chips[2]);
            Point point = Database.GetPoints()[1];
            Assert.AreEqual(Database.GetPointByChipPosition(1).ToString(), point.ToString());
            File.Delete("chips.json");
            File.Delete("points.json");
        }

        [TestMethod]
        public void ChangePositionForward()
        {
            List<Chip> chips = new List<Chip>
            {
                new Chip
            {
                Position = 0,//позиция
                Color = ColorOfChip.Red,
                IsCurrent = true
            },
            new Chip(ColorOfChip.White, 0),
                new Chip(ColorOfChip.White, 1),
                new Chip(ColorOfChip.White, 0)
            };
            Database.AddChip(chips[0]);
            Database.AddChip(chips[1]);
            Database.AddChip(chips[2]);
            Database.AddChip(chips[3]);
            Database.ChangePositionCurrentChip(2);
            List<Chip> chips1 = Database.GetAllChips();
            Assert.AreEqual(2, chips1[Database.GetCurrentChip()].Position);//позиция изменилась
            File.Delete("chips.json");
        }

        [TestMethod]
        public void ChangePositionBack()
        {
            List<Chip> chips = new List<Chip>
            {
                new Chip
            {
                Position = 0,//позиция
                Color = ColorOfChip.Red,
                IsCurrent = true
            },
            new Chip(ColorOfChip.White, 0),
                new Chip(ColorOfChip.White, 1),
                new Chip(ColorOfChip.White, 0)
            };
            Database.AddChip(chips[0]);
            Database.AddChip(chips[1]);
            Database.AddChip(chips[2]);
            Database.AddChip(chips[3]);
            Database.ChangePositionBackCurrentChip(2);
            List<Chip> chips1 = Database.GetAllChips();
            Assert.AreEqual(0, chips1[Database.GetCurrentChip()].Position);//позиция изменилась на 0
            File.Delete("chips.json");
        }

        [TestMethod]
        public void IsWon_returned_true()
           
        {
            Database.SetAmountPoints(20);
            using (FileStream fs = File.Create("chips.json")) { }
            List<Chip> chips = new List<Chip>
            {
                new Chip
            {
                Position = 100,//позиция
                Color = ColorOfChip.Red,
                IsCurrent = true
            },
            new Chip(ColorOfChip.White, 0),
                new Chip(ColorOfChip.White, 1),
                new Chip(ColorOfChip.White, 0)
            };

            Database.AddChip(chips[0]);
            Database.AddChip(chips[1]);
            Database.AddChip(chips[2]);
            Database.AddChip(chips[3]);
            Assert.AreEqual(true, Database.IsCurrentChipWon());
            File.Delete("chips.json");
            File.Delete("amount.json");
        }
        [TestMethod]
        public void IsWon_returned_false()
        {
            Database.SetAmountPoints(20);
            using (FileStream fs = File.Create("chips.json")) { }
            List<Chip> chips = new List<Chip>
            {
                new Chip
            {
                Position = 0,//позиция
                Color = ColorOfChip.Red,
                IsCurrent = true
            },
            new Chip(ColorOfChip.White, 0),
                new Chip(ColorOfChip.White, 1),
                new Chip(ColorOfChip.White, 0)
            };

            Database.AddChip(chips[0]);
            Database.AddChip(chips[1]);
            Database.AddChip(chips[2]);
            Database.AddChip(chips[3]);
            Assert.AreEqual(false, Database.IsCurrentChipWon());
            File.Delete("chips.json");
            File.Delete("amount.json");
        }
        [TestMethod]
        public void Delete()
        {
            using (FileStream fs = File.Create("chips.json")) { }
            using (FileStream fs = File.Create("points.json")) { }
            using (FileStream fs = File.Create("amount.json")) { }
            Database.SetPoints(20);
            List<Chip> chips = new List<Chip>
            {
                new Chip
            {
                Position = 0,//позиция
                Color = ColorOfChip.Red,
                IsCurrent = true
            },
            new Chip(ColorOfChip.White, 0),
                new Chip(ColorOfChip.White, 1),
                new Chip(ColorOfChip.White, 0)
            };

            Database.AddChip(chips[0]);
            Database.AddChip(chips[1]);
            Database.AddChip(chips[2]);
            Database.AddChip(chips[3]);
            Database.DeleteAllChipsAndPoints();
            Assert.IsFalse(File.Exists("chips.json"));
            Assert.IsFalse(File.Exists("points.json"));
            Assert.IsFalse(File.Exists("amount.json"));
        }

        [TestMethod]
        public void GetAmountOfPoints()
        {
            using (FileStream fs = File.Create("amount.json")) { }
            File.WriteAllText("amount.json", "2");
            Assert.AreEqual(2, Database.GetAmountOfPoints());
            File.Delete("amount.json");
        }
        [TestMethod]
        public void ChangeCurrentChip()
        {
            using (FileStream fs = File.Create("chips.json")) { }
            List<Chip> chips = new List<Chip>
            {
                new Chip
            {
                Position = 0,//позиция
                Color = ColorOfChip.Red,
                IsCurrent = true
            },
            new Chip(ColorOfChip.White, 0),
                new Chip(ColorOfChip.White, 1),
                new Chip(ColorOfChip.White, 0)
            };

            Database.AddChip(chips[0]);
            Database.AddChip(chips[1]);
            Database.AddChip(chips[2]);
            Database.AddChip(chips[3]);
            List<Chip> chips1 = Database.GetAllChips();
            Assert.AreEqual(chips[1].Position, chips1[1].Position);
            File.Delete("chips.json");
        }
    }
}
