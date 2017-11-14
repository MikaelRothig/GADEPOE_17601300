using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace RTSGame
{
    abstract class Unit
    {
        protected int xPosition;
        protected int yPosition;
        protected int health;
        protected int speed;
        protected bool attack;
        protected int attackRange;
        protected string symbol;
        protected string faction;
        protected string name;

        public Unit(int xPosition, int yPosition, int health, int speed, bool attack, int attackRange, string faction, string symbol, string name)
        {
            this.xPosition = xPosition;
            this.yPosition = yPosition;
            this.health = health;
            this.speed = speed;
            this.attack = attack;
            this.attackRange = attackRange;
            this.faction = faction;
            this.symbol = symbol;
            this.name = name;
        }

        //Destructor
        ~Unit()
        {
        }

        //Accessors
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

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public bool Attack
        {
            get { return attack; }
            set { attack = value; }
        }

        public int AttackRange
        {
            get { return attackRange; }
            set { attackRange = value; }
        }

        public string Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        public string Faction
        {
            get { return faction; }
            set { faction = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public abstract void move(int xPosition, int yPosition);      
        public abstract void combat(Unit enemy);      
        public abstract bool isWithinAttackRange(Unit enemy);   
        public abstract Unit nearestUnit(List<Unit> list);
        public abstract bool isAlive();
        public abstract string toString();
        public abstract void save();
    }
}
