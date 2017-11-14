using System;
using System.Collections.Generic;
using System.IO;

namespace RTSGame
{
    class Map
    {
        private const int MAX_RANDOM_UNITS = 50;
        private const int MAX_RANDOM_BUILDINGS = 6;
        public const string FIELD_SYMBOL = ".";
        public string[,] grid = new string[20, 20];
        private List<Unit> unitsOnMap = new List<Unit>();
        private int numberOfUnitsOnMap = 0;
        private List<Building> buildingsOnMap = new List<Building>();
        private int numberOfBuildingsOnMap = 0;

        public string[,] Grid
        {
            get
            {
                return grid;
            }
        }

        public List<Unit> UnitsOnMap
        {
            get
            {
                return unitsOnMap;
            }
        }

        public int NumberOfUnitsOnMap
        {
            get
            {
                return numberOfUnitsOnMap;
            }
        }

        public void clearField()
        {
            //CLEAR FIELD
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    grid[i, j] = FIELD_SYMBOL;
                }
            }
        }

        public void populate()
        {
            Random rnd = new Random();
            int numberRandomUnits = rnd.Next(0, MAX_RANDOM_UNITS) + 1;
            int Ux, Uy, randomAttackRange;
            bool attackOption;
            string team;

            int numberRandomBuildings = rnd.Next(0, MAX_RANDOM_BUILDINGS) + 1;
            int Bx, By;

            clearField();

            for (int k = 1; k <= numberRandomUnits; k++)
            {
                //ENSURE X, Y IS NOT OCCUPIED BY ANOTHER UNIT
                do
                {
                    Ux = rnd.Next(0, 20);
                    Uy = rnd.Next(0, 20);
                } while (grid[Ux, Uy] != FIELD_SYMBOL);

                //GENERATE RANDOMLY EITHER A MELEEUNIT OR RANGEDUNITAND PLACE ON MAP
                if (rnd.Next(1, 3) == 1)
                {
                    attackOption = rnd.Next(0, 2) == 1 ? true : false; //RANDOMISE ATTACK OPTION
                    team = rnd.Next(0, 2) == 1 ? "RED" : "GREEN";
                    Unit mU = new MeleeUnit(Ux, Uy, 100, -1, attackOption, 1, team, "M", "Brawler");
                    unitsOnMap.Add(mU);

                    grid[Ux, Uy] = mU.Symbol;
                    //UPDATE ARRAY SIZE
                    numberOfUnitsOnMap++;
                }
                else
                {
                    attackOption = rnd.Next(0, 2) == 1 ? true : false;
                    randomAttackRange = rnd.Next(1, 6);
                    team = rnd.Next(0, 2) == 1 ? "RED" : "GREEN";
                    Unit rU = new RangedUnit(Ux, Uy, 100, -1, attackOption, randomAttackRange, team, "R", "Sniper");
                    unitsOnMap.Add(rU);

                    grid[Ux, Uy] = rU.Symbol;
                    //UPDATE ARRAY SIZE
                    numberOfUnitsOnMap++;
                }
            }

            for (int i = 0; i < numberRandomBuildings; i++)
            {
                //ENSURE X, Y IS NOT OCCUPIED BY ANOTHER UNIT
                do
                {
                    Bx = rnd.Next(0, 20);
                    By = rnd.Next(0, 20);
                } while (grid[Bx, By] != FIELD_SYMBOL);

                if (rnd.Next(1, 3) == 1)
                {
                    team = rnd.Next(0, 2) == 1 ? "RED" : "GREEN";
                    ResourceBuilding rB = new ResourceBuilding(Bx, By, 100, team, "$", "Gold", 5, 20);
                    buildingsOnMap.Add(rB);

                    grid[Bx, By] = rB.Symbol;
                    //UPDATE ARRAY SIZE
                    numberOfBuildingsOnMap++;
                }
                else
                {
                    team = rnd.Next(0, 2) == 1 ? "RED" : "GREEN";
                    FactoryBuilding fB = new FactoryBuilding(Bx, By, 100, team, "@", 10, 1, Bx, By);
                    buildingsOnMap.Add(fB);

                    grid[Bx, By] = fB.Symbol;
                    //UPDATE ARRAY SIZE
                    numberOfBuildingsOnMap++;
                }
            }
        }

        public void moveOnMap(Unit u, int newX, int newY)
        {
            grid[u.XPosition, u.YPosition] = FIELD_SYMBOL;
            grid[newX, newY] = u.Symbol;
        }

        public void update(Unit u, int newX, int newY)
        {
            if ((newX >= 0 && newX < 20) && (newY >= 0 && newY < 20))
            {
                moveOnMap(u, newX, newY);
                u.move(newX, newY);
            }
        }

        public void checkHealth()
        {
            for (int i = 0; i < numberOfUnitsOnMap; i++)
            {
                if (!unitsOnMap[i].isAlive())
                {
                    grid[unitsOnMap[i].XPosition, unitsOnMap[i].YPosition] = FIELD_SYMBOL;
                    unitsOnMap.RemoveAt(i);
                    numberOfUnitsOnMap--;
                }
            }
        }

        public void saveMap()
        {
            try
            {
                File.Delete(@"Files\MeleeUnit.txt");
                File.Delete(@"Files\RangedUnit.txt");

                foreach (Unit u in unitsOnMap)
                {
                    u.save();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            try
            {
                File.Delete(@"Files\ResourceBuilding.txt");
                File.Delete(@"Files\FactoryBuilding.txt");

                foreach (Building b in buildingsOnMap)
                {
                    b.save();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }

        public void loadMap()
        {
            //Variables
            bool success = true;
            string input;
            int xPosition;
            int yPosition;
            int health;
            int speed;
            bool attack;
            int attackRange;
            string faction;
            string symbol;
            string name;
            string resourseType;
            int resourcesPerTick;
            int resourcesRemaining;
            int unitsToProduce;
            int unitsPerTick;
            int spawnPointX;
            int spawnPointY;

            clearField();

            //Load Melee Objects From File
            try
            {
                FileStream inFile = new FileStream(@"Files\MeleeUnit.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(inFile);
                input = reader.ReadLine();

                while (input != null)
                {
                    xPosition = int.Parse(input);
                    yPosition = int.Parse(reader.ReadLine());
                    health = int.Parse(reader.ReadLine());
                    speed = int.Parse(reader.ReadLine());
                    attack = bool.Parse(reader.ReadLine());
                    attackRange = int.Parse(reader.ReadLine());
                    faction = reader.ReadLine();
                    symbol = reader.ReadLine();
                    name = reader.ReadLine();

                    MeleeUnit mU = new MeleeUnit(xPosition, yPosition, health, speed, attack, attackRange, faction, symbol, name);
                    unitsOnMap.Add(mU);

                    grid[xPosition, yPosition] = mU.Symbol;

                    //Update Array Size
                    numberOfUnitsOnMap++;
                    input = reader.ReadLine();
                }

                inFile.Close();
                reader.Close();
            }
            catch (IOException fe)
            {
                success = false;
                Console.WriteLine("IOEXCEPTION: " + fe.Message);
            }

            //Load Ranged Objects From File
            try
            {
                FileStream inFile = new FileStream(@"Files\RangedUnit.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(inFile);
                input = reader.ReadLine();

                while (input != null)
                {
                    xPosition = int.Parse(input);
                    yPosition = int.Parse(reader.ReadLine());
                    health = int.Parse(reader.ReadLine());
                    speed = int.Parse(reader.ReadLine());
                    attack = bool.Parse(reader.ReadLine());
                    attackRange = int.Parse(reader.ReadLine());
                    faction = reader.ReadLine();
                    symbol = reader.ReadLine();
                    name = reader.ReadLine();

                    RangedUnit rU = new RangedUnit(xPosition, yPosition, health, speed, attack, attackRange, faction, symbol, name);
                    unitsOnMap.Add(rU);

                    grid[xPosition, yPosition] = rU.Symbol;

                    //Update Array Size
                    numberOfUnitsOnMap++;
                    input = reader.ReadLine();
                }

                inFile.Close();
                reader.Close();
            }
            catch (IOException fe)
            {
                success = false;
                Console.WriteLine("IOEXCEPTION: " + fe.Message);
            }

            //Load Resource Building Objects From File
            try
            {
                FileStream inFile = new FileStream(@"Files\ResourceBuilding.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(inFile);
                input = reader.ReadLine();

                while (input != null)
                {
                    xPosition = int.Parse(input);
                    yPosition = int.Parse(reader.ReadLine());
                    health = int.Parse(reader.ReadLine());
                    faction = reader.ReadLine();
                    symbol = reader.ReadLine();
                    resourseType = reader.ReadLine();
                    resourcesPerTick = int.Parse(reader.ReadLine());
                    resourcesRemaining = int.Parse(reader.ReadLine());


                    ResourceBuilding rB = new ResourceBuilding(xPosition, yPosition, health, faction, symbol, resourseType, resourcesPerTick, resourcesRemaining);
                    buildingsOnMap.Add(rB);

                    grid[xPosition, yPosition] = rB.Symbol;

                    //Update Array Size
                    numberOfBuildingsOnMap++;
                    input = reader.ReadLine();
                }

                inFile.Close();
                reader.Close();
            }
            catch (IOException fe)
            {
                success = false;
                Console.WriteLine("IOEXCEPTION: " + fe.Message);
            }

            //Load Factory Building Objects From File
            try
            {
                FileStream inFile = new FileStream(@"Files\FactoryBuilding.txt", FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(inFile);
                input = reader.ReadLine();

                while (input != null)
                {
                    xPosition = int.Parse(input);
                    yPosition = int.Parse(reader.ReadLine());
                    health = int.Parse(reader.ReadLine());
                    faction = reader.ReadLine();
                    symbol = reader.ReadLine();
                    unitsToProduce = int.Parse(reader.ReadLine());
                    unitsPerTick = int.Parse(reader.ReadLine());
                    spawnPointX = int.Parse(reader.ReadLine());
                    spawnPointY = int.Parse(reader.ReadLine());


                    FactoryBuilding fB = new FactoryBuilding(xPosition, yPosition, health, faction, symbol, unitsToProduce, unitsPerTick, spawnPointX, spawnPointY);
                    buildingsOnMap.Add(fB);

                    grid[xPosition, yPosition] = fB.Symbol;

                    //Update Array Size
                    numberOfBuildingsOnMap++;
                    input = reader.ReadLine();
                }

                inFile.Close();
                reader.Close();
            }
            catch (IOException fe)
            {
                success = false;
                Console.WriteLine("IOEXCEPTION: " + fe.Message);
            }
        }

    }
}

        
  

