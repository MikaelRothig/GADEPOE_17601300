using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace RTSGame
{
    abstract class Building
    {
        protected int xPosition;
        protected int yPosition;
        protected int health;
        protected string faction;
        protected string symbol;

        public Building(int xPosition, int yPosition, int health, string faction, string symbol)
        {
            this.xPosition = xPosition;
            this.yPosition = yPosition;
            this.health = health;
            this.faction = faction;
            this.symbol = symbol;
        }

        //Destructor
        ~Building()
        {
        }

        public int XPosition
        {
            get { return xPosition; }
            set { xPosition = value; }
        }

        public int YPosition
        {
            get { return yPosition; }
            set { yPosition = value; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public String Faction
        {
            get { return faction; }
            set { faction = value; }
        }

        public String Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        public abstract string toString();
        public abstract void save();
    }
}
