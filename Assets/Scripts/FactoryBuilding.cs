using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace RTSGame
{
    class FactoryBuilding : Building
    {
        private int unitsToProduce;
        private int unitsPerTick;
        private int spawnPointX;
        private int spawnPointY;

        public FactoryBuilding(int xPosition, int yPosition, int health, string faction, string symbol, int unitsToProduce, int unitsPerTick, int spawnPointX, int spawnPointY)
            : base(xPosition, yPosition, health, faction, symbol)
        {
            this.unitsToProduce = unitsToProduce;
            this.unitsPerTick = unitsPerTick;
            this.spawnPointX = spawnPointX;
            this.spawnPointY = spawnPointY;
        }

        public Unit spawnNewUnit()
        {
            if (unitsToProduce > 0)
            {
                Random rnd = new Random();
                if (rnd.Next(0, 2) == 0)
                {
                    //Melee Unit
                    MeleeUnit mU = new MeleeUnit(spawnPointX, spawnPointY, 100, -1, true, 1, this.Faction, "M", "Brawler");
                    unitsToProduce--;
                    return mU;
                }
                else
                {
                    //Ranged Unit
                    RangedUnit rU = new RangedUnit(spawnPointX, spawnPointY, 100, -1, true, 1, this.Faction, "R", "Sniper");
                    unitsToProduce--;
                    return rU;
                }
            }
            else
                return null;
        }

        public override string toString()
        {
            string output = "xPosition : " + XPosition + Environment.NewLine
                + "yPosition : " + YPosition + Environment.NewLine
                + "Health : " + Health + Environment.NewLine
                + "Faction: " + Faction + Environment.NewLine
                + "Symbol: " + Symbol + Environment.NewLine
                + "Units To Produce" + unitsToProduce + Environment.NewLine
                + "Units Per Tick" + unitsPerTick + Environment.NewLine
                + "Spawn Point X" + spawnPointX + Environment.NewLine
                + "Spawn Point Y" + spawnPointY + Environment.NewLine;

            return output;
        }

        public override void save()
        {
            FileStream outFile = null;
            StreamWriter writer = null;

            try
            {
                outFile = new FileStream(@"Files\FactoryBuilding.txt", FileMode.Append, FileAccess.Write);
                writer = new StreamWriter(outFile);

                writer.WriteLine(xPosition);
                writer.WriteLine(yPosition);
                writer.WriteLine(health);
                writer.WriteLine(faction);
                writer.WriteLine(symbol);
                writer.WriteLine(unitsToProduce);
                writer.WriteLine(unitsPerTick);
                writer.WriteLine(spawnPointX);
                writer.WriteLine(spawnPointY);

                writer.Close();
                outFile.Close();
            }
            catch (IOException fe)
            {
                Console.WriteLine("IOEXCEPTION: " + fe.Message);
            }
            finally
            {
                if (outFile != null)
                    outFile.Close();
                if (writer != null)
                    writer.Close();
            }
        }
    }
}
