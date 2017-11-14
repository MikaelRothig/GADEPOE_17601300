using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace RTSGame
{
    class ResourceBuilding : Building
    {
        private string resourceType;
        private int resourcesPerTick;
        private int resourcesRemaining;

        public ResourceBuilding(int xPosition, int yPosition, int health, string faction, string symbol, string resourceType, int resourcesPerTick, int resourcesRemaining)
            : base(xPosition, yPosition, health, faction, symbol)
        {
            this.resourceType = resourceType;
            this.resourcesPerTick = resourcesPerTick;
            this.resourcesRemaining = resourcesRemaining;
        }

        public void generateResources()
        {
            if (resourcesRemaining >= 0)
                resourcesRemaining -= resourcesPerTick;
        }

        public override string toString()
        {
            string output = "xPosition : " + XPosition + Environment.NewLine
                + "yPosition : " + YPosition + Environment.NewLine
                + "Health : " + Health + Environment.NewLine
                + "Faction: " + Faction + Environment.NewLine
                + "Symbol: " + Symbol + Environment.NewLine
                + "Resource Type" + resourceType + Environment.NewLine
                + "Resources Per Tick" + resourcesPerTick + Environment.NewLine
                + "Resources Remaining" + resourcesRemaining + Environment.NewLine;

            return output;
        }

        public override void save()
        {
            FileStream outFile = null;
            StreamWriter writer = null;

            try
            {
                outFile = new FileStream(@"Files\ResourceBuilding.txt", FileMode.Append, FileAccess.Write);
                writer = new StreamWriter(outFile);

                writer.WriteLine(xPosition);
                writer.WriteLine(yPosition);
                writer.WriteLine(health);
                writer.WriteLine(faction);
                writer.WriteLine(symbol);
                writer.WriteLine(resourceType);
                writer.WriteLine(resourcesPerTick);
                writer.WriteLine(resourcesRemaining);

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
