using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTSGame;
using System.Threading;

public class GameEngine : MonoBehaviour {

    Map map = new Map();
    Unit closest;

    int newX, newY;
    Vector2 newPos;

    public int height;
    public int width;


    // Use this for initialization
    void Start()
    {
        map.populate();
        map.checkHealth();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Gameplay();
        Thread.Sleep(500);        
    }

    public void Gameplay()
    {
        for (int j = 0; j < map.UnitsOnMap.Count; j++)
        {
            //RULES:
            if (!map.UnitsOnMap[j].Attack)
            {
                closest = map.UnitsOnMap[j].nearestUnit(map.UnitsOnMap);

                if (map.UnitsOnMap[j].XPosition < closest.YPosition)
                    newX = map.UnitsOnMap[j].XPosition + 1;
                else if (map.UnitsOnMap[j].XPosition > closest.XPosition)
                    newX = map.UnitsOnMap[j].XPosition - 1;
                else
                    newX = map.UnitsOnMap[j].XPosition;

                if (map.UnitsOnMap[j].YPosition < closest.YPosition)
                    newY = map.UnitsOnMap[j].YPosition + 1;
                else if (map.UnitsOnMap[j].YPosition > closest.YPosition)
                    newY = map.UnitsOnMap[j].YPosition - 1;
                else
                    newY = map.UnitsOnMap[j].YPosition;
                map.update(map.UnitsOnMap[j], newX, newY);
            }

            newPos = new Vector2(newX, newY);

            if (map.UnitsOnMap[j].Attack)
            {
                for (int i = 0; i < map.UnitsOnMap.Count; i++)
                {
                    if (map.UnitsOnMap[j].Faction != map.UnitsOnMap[i].Faction)
                        map.UnitsOnMap[j].combat(map.UnitsOnMap[i]);
                }
            }

            if (!map.UnitsOnMap[j].Attack)
            {
                for (int i = 0; i < map.UnitsOnMap.Count; i++)
                {
                    if ((map.UnitsOnMap[j].Faction != map.UnitsOnMap[i].Faction) && (map.UnitsOnMap[j].Faction != map.UnitsOnMap[i].Faction))
                        map.UnitsOnMap[j].Attack = true;
                }
            }

            if (map.UnitsOnMap[j].Health < 25)
            {
                newX = map.UnitsOnMap[j].XPosition + 1;
                newY = map.UnitsOnMap[j].YPosition - 1;

                map.update(map.UnitsOnMap[j], newX, newY);
            }
        }
    }
}
