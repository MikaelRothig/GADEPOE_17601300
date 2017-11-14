using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace RTSGame
{
    class RangedUnit : Unit
    {
        private const int DAMAGE = 1; //CHECK DAMAGE

        //CONSTRUCTOR
        public RangedUnit(int xPosition, int yPosition, int health, int speed, bool attack, int attackRange, string faction, string symbol, string name)
            : base(xPosition, yPosition, health, speed, attack, attackRange, faction, symbol, name)
        {
        }

        //OTHER OVERRIDE METHODS
        public override void move(int xPosition, int yPosition)
        {
            if (xPosition >= 0 && xPosition < 20)
                XPosition = xPosition;
            if (yPosition >= 0 && yPosition < 20)
                YPosition = yPosition;
        }

        public override void combat(Unit enemy)
        {
            if (this.isWithinAttackRange(enemy))
            {
                enemy.Health -= DAMAGE;
            }
        }

        public override bool isWithinAttackRange(Unit enemy)
        {
            if ((Math.Abs(this.XPosition - enemy.XPosition) <= this.AttackRange) || (Math.Abs(this.YPosition - enemy.YPosition) <= this.AttackRange))
                return true;
            else
                return false;
        }

        public override Unit nearestUnit(List<Unit> list)
        {
            Unit closest = null;
            int attackRangeX, attackRangeY;
            int shortestRange = 5000;

            foreach (Unit u in list)
            {
                attackRangeX = Math.Abs(this.XPosition - u.XPosition);
                attackRangeY = Math.Abs(this.YPosition - u.YPosition);

                if (attackRangeX < shortestRange)
                {
                    shortestRange = attackRangeX;
                    closest = u;
                }
                if (attackRangeY < shortestRange)
                {
                    shortestRange = attackRangeY;
                    closest = u;
                }
            }
            return closest;
        }

        public override bool isAlive()
        {
            if (this.Health <= 0)
                return false;
            else
                return true;
        }

        public override string toString()
        {
            string output = "xPosition : " + XPosition + Environment.NewLine
                + "yPosition : " + YPosition + Environment.NewLine
                + "Health : " + Health + Environment.NewLine
                + "Speed : " + Speed + Environment.NewLine
                + "Attack : " + Attack + Environment.NewLine
                + "Attack Range: " + AttackRange + Environment.NewLine
                + "Faction: " + Faction + Environment.NewLine
                + "Symbol: " + Symbol + Environment.NewLine
                + "Name: " + Name + Environment.NewLine;

            return output;
        }

        public override void save()
        {
            FileStream outFile = null;
            StreamWriter writer = null;

            try
            {
                outFile = new FileStream(@"Files\RangedUnit.txt", FileMode.Append, FileAccess.Write);
                writer = new StreamWriter(outFile);

                writer.WriteLine(xPosition);
                writer.WriteLine(yPosition);
                writer.WriteLine(health);
                writer.WriteLine(speed);
                writer.WriteLine(attack);
                writer.WriteLine(attackRange);
                writer.WriteLine(faction);
                writer.WriteLine(symbol);
                writer.WriteLine(name);

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
