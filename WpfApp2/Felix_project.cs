//using ColorPositionCombination;
using WpfApp2;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Diagnostics;
using System.Windows.Controls;

namespace NewEndCollision
{
    public class Globals
    {
        public static LinkedList<Planet> planet_list = new LinkedList<Planet>();
        public static LinkedList<Planet> planet_list_new = new LinkedList<Planet>();
        public static Planet player = new Planet();
        public static Planet sun = new Planet();

    }

    public struct Planet
    {
        public int id;          //0: normal planet, 1: player, 2: sun
        public double x;
        public double y;
        public double mass;
        public double radius; // radius ^2 ~ mass
        public double v_x;
        public double v_y;
        public int color;     // 0: blue, 1: red, 2: color of player, 3: color of sun
    }

    public class Collision
    {
        public static void DeleteNode(double x, double y, double radius, LinkedList<Planet> myLinkedList)
        {
            LinkedListNode<Planet> node = myLinkedList.First;
            while(node!=null)
            {

                if (node.Value.x == x && node.Value.y == y && node.Value.radius == radius)
                {
                    myLinkedList.Remove(node);
                    break;
                }
                node = node.Next;
            }
        }                                                   //delete node


        public static int collision(Planet planet1, Planet planet2, LinkedList<Planet> myLinkedList)
        {
            if (Start.distance(planet1.x, planet1.y, planet2.x, planet2.y)< planet1.radius+ planet2.radius)
            {
                if (planet1.radius >= planet2.radius)
                {
                    planet1.radius = Math.Sqrt(planet1.radius * planet1.radius + planet2.radius * planet2.radius);
                    planet1.v_x = planet1.v_x + planet2.v_x * planet2.mass / planet1.mass;
                    planet1.v_y = planet1.v_y + planet2.v_y * planet2.mass / planet1.mass;
                    planet1.mass = planet1.radius * planet1.radius;
                    int id = planet2.id;
                    DeleteNode(planet2.x, planet2.y, planet2.radius, myLinkedList);
                    if (id == 1)
                    {
                        return 1;
                    }
                    return 0;
                }
                else
                {
                    planet2.radius = Math.Sqrt(planet1.radius * planet1.radius + planet2.radius * planet2.radius);
                    planet2.v_x = planet2.v_x + planet1.v_x * planet1.mass / planet2.mass;
                    planet2.v_y = planet2.v_y + planet1.v_y * planet1.mass / planet2.mass;
                    planet2.mass = planet1.radius * planet1.radius;
                    int id = planet1.id;
                    DeleteNode(planet1.x, planet1.y, planet1.radius, myLinkedList);
                    if (id == 1)
                    {
                        return 1;
                    }
                    return 0;
                }
            }
            return 0;
        }                                           //check if collision, return 1 if player die, else return 0
    }

    public class Start
    {
        public static double initial_velocity(double radius)
        {
            return Math.Sqrt(Math.Abs(10 * radius));
        }

        public static double initial_angle(double x, double y)
        {
            return Math.Atan(y / x);
        }

        public static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public static double distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }

        public static void start()
        {
            Globals.sun.x = 480.0;
            Globals.sun.y = 270.0;
            Globals.sun.mass = 100000;
            Globals.sun.radius = 5;
            Globals.sun.v_x = 0;
            Globals.sun.v_y = 0;
            Globals.sun.color = 3;
            Globals.sun.id = 2;

            Globals.player.x = 600;
            Globals.player.y = 100;
            Globals.player.mass = 6.25;
            Globals.player.radius = 2.5;
            Globals.player.v_x = initial_velocity(Globals.player.radius) * Math.Cos(initial_angle(Globals.player.x, Globals.player.y));
            Globals.player.v_y = initial_velocity(Globals.player.radius) * Math.Sin(initial_angle(Globals.player.x, Globals.player.y));
            Globals.player.color = 2;
            Globals.player.id = 1; 

            Globals.planet_list= new LinkedList<Planet>();
            Globals.planet_list.AddLast(Globals.sun);
            Globals.planet_list.AddLast(Globals.player);

            for (int i = 0; i <= 100; i++)
            {
                Planet planet = new Planet();
                planet.radius = GetRandomNumber(0.1, 2.5);
                bool test = true;
                while (test)
                {
                    planet.x = GetRandomNumber(0, 960);
                    planet.y = GetRandomNumber(0, 540);
                    if (distance(planet.x, planet.y, 480, 270) > (Globals.sun.radius + planet.radius) && distance(planet.x, planet.y, 480, 270) < 270)
                    {
                        test = false;
                    }
                }
                planet.mass = planet.radius * planet.radius;
                planet.v_x = initial_velocity(planet.radius) * Math.Cos(initial_angle(planet.x, planet.y));
                planet.v_y = initial_velocity(planet.radius) * Math.Sin(initial_angle(planet.x, planet.y));
                planet.color = 0;
                planet.id = 0;
                Globals.planet_list.AddLast(planet);
            }                   //small planet

            
            for (int i = 0; i <= 100; i++)
            {
                Planet planet = new Planet();
                planet.radius = GetRandomNumber(2.5, 5);
                bool test = true;
                while (test)
                {
                    planet.x = GetRandomNumber(0, 960);
                    planet.y = GetRandomNumber(0, 540);
                    if (distance(planet.x, planet.y, 480, 270) > (Globals.sun.radius + planet.radius) && distance(planet.x, planet.y, 480, 270) < 270)
                    {
                        test = false;
                    }
                }
                planet.mass = planet.radius * planet.radius;
                planet.v_x = initial_velocity(planet.radius) * Math.Cos(initial_angle(planet.x, planet.y));
                planet.v_y = initial_velocity(planet.radius) * Math.Sin(initial_angle(planet.x, planet.y));
                planet.color = 1;
                planet.id = 0;
                Globals.planet_list.AddLast(planet);
            }                  //large planet
        }                                       //intialize planets


    }
    public class TouchBoundary
    {
        public static int touchBoundary(LinkedList<Planet> myLinkedList)
        {
            LinkedListNode<Planet> node = myLinkedList.First;
            while (node != null)
            {
                if (node.Value.id == 1)
                {
                    if (node.Value.x + node.Value.radius > 960 || node.Value.x - node.Value.radius < 0 || node.Value.y + node.Value.radius > 540 || node.Value.y - node.Value.radius < 0)
                    {
                        return 1;
                    }
                    return 0;
                }
                node = node.Next;
            }
            return 0;
        }           //return 1 if touch boundary
    }

    public class WinCheck
    {
        public static int winCheck(LinkedList<Planet> myLinkedList)
        {
            LinkedListNode<Planet> node = myLinkedList.First;
            while (node != null)
            {
                if (node.Value.id == 1)
                {
                    if (node.Value.radius > 8)
                    {
                        return 1;
                    }
                    return 0;
                }
                node = node.Next;
            }
            return 0;
        }           //return 1 if win
    }

}
