using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using NewEndCollision;
using PhysicEngine.Movement;

//planet_list all the planet with strut Planet
//win, die which is int: 1 means ys, 0 means no

namespace ColorPositionCombination
{
    public class Color
    {
        public static int ChangeColor(Planet player, Planet planet1)
        {
            if (player.radius >= planet1.radius)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }

    public class Position
    {
        public static void ChangePosition(Planet planet)
        {
            double angle = Math.Atan((planet.y - 480) / (planet.x - 270));
            planet.x = 0.02 * planet.v_x;
            planet.y += 0.02 * planet.v_y;
            planet.v_y -= 10 * Math.Sin(angle);
            planet.v_x -= 10 * Math.Cos(angle);
        }
    }

    public class Combination
    {
        public static Planet findPlayer()
        {

            LinkedListNode<Planet> node = Globals.planet_list.First;
            while (true)
            {
                LinkedListNode<Planet> nextNode = node.Next;
                if (node.Value.id == 1)
                {
                    return node.Value;
                }
                if (node == Globals.planet_list.Last)
                {
                    return node.Value;
                }
                node = nextNode;
            }
        }
        public static void Main()
        {
            DateTime dateTime = new DateTime();
            int lastTime = dateTime.Millisecond;
            int check_ejection;
            double x_c, y_c;

            Start.start();                         //Initialize

            while (true)
            {
                int die = 0, win = 0;
                double[] orbit = new double[3];
                foreach (Planet check1 in Globals.planet_list)                      //check collision
                {
                    foreach (Planet check2 in Globals.planet_list)
                    {
                        if (check1.x != check2.x || check1.y != check2.y || check1.radius != check2.radius)
                        {
                            if (Collision.collision(check1, check2, Globals.planet_list) == 1)
                            {
                                die = 1;
                            }
                        }
                    }
                }

                Planet player = findPlayer();
                foreach (Planet planet in Globals.planet_list)                //change color
                {

                    if (planet.id != 1 && planet.id != 2)
                    {
                        Color.ChangeColor(player, planet);
                    }
                }

                if (check_ejection == 1)
                {
                    Movement.SplitEject(player, x_c, y_c);                //eject objects(not done!!!)
                }

                foreach (Planet planet in Globals.planet_list)           //change position
                {
                    Position.ChangePosition(planet);
                }

                if (TouchBoundary.touchBoundary(Globals.planet_list) == 1) //Check die
                {
                    die = 1;
                }

                if (WinCheck.winCheck(Globals.planet_list) == 1)                //Check win
                {
                    win = 1;
                }

                while (dateTime.Millisecond - lastTime < 20)
                {
                    if (dateTime.Millisecond - lastTime < 0)
                    {
                        lastTime -= 999;
                    }
                }


                lastTime = dateTime.Millisecond;
            }
        }
    }

}