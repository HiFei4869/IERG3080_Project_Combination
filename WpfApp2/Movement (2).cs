using System;
using System.Collections.Generic;
//using ColorPositionCombination;
using WpfApp2;
using NewEndCollision;
using System.Windows.Input;
using System.Windows;
using System.Diagnostics;
using System.Numerics;

/// Ejection and Orbit
/// Functions can be called: 
/// public static double Mass_To_Radius(double mass); 
/// public static void SplitEject(ref Planet planet, double x_c, double y_c)
namespace PhysicEngine.Movement
{
	public class Movement
	{
        public static double EjectDirection(Planet planet, double x_c, double y_c)
        {
            double deltaX = x_c - planet.x;
            double deltaY = y_c - planet.y;
            double direction = (double)Math.Atan2(deltaY, deltaX);
            return direction;
        }
        static double NthRoot(double A, int N)
        {
            return Math.Pow(A, 1.0 / N);
        }
        public static double Mass_To_Radius(double mass)
        {
            double Radius = 0.0;
            Radius = Math.Sqrt(mass/Math.PI);
            return Radius;
        }
	public static void SplitEject(LinkedList<Planet> planet_list, Planet planet, double x_c, double y_c)
	{
	    Globals global = new Globals();
	    Planet newPlanet = new Planet();
	    newPlanet = planet;

	    double direction = EjectDirection(planet, x_c, y_c);
	    double v_e_x = v_e * Math.Cos(direction);  // x_axis velocity of the ejected mass
	    double v_e_y = v_e * Math.Sin(direction);  // y_axis velocity of the ejected mass
	    double v_x_1 = (planet.mass * planet.v_x - m_e * v_e_x) / (planet.mass - m_e);
	    double v_y_1 = (planet.mass * planet.v_y - m_e * v_e_y) / (planet.mass - m_e);

	    newPlanet.mass -= m_e;
	    newPlanet.radius = Mass_To_Radius(planet.mass);
	    newPlanet.v_x = v_x_1;
	    newPlanet.v_y = v_y_1;
	    planet_list.AddLast(newPlanet);
	    Collision.DeleteNode(planet.x, planet.y, planet.radius, planet_list);
	
	    Planet ejectedPlanet = new Planet();
	    ejectedPlanet.mass = m_e;
	    ejectedPlanet.radius = Mass_To_Radius(m_e);
	    ejectedPlanet.v_x = v_e_x;
	    ejectedPlanet.v_y = v_e_y;
	    ejectedPlanet.x = newPlanet.x + (newPlanet.radius + birth_distance) * Math.Cos(direction);
	    ejectedPlanet.y = newPlanet.y + (newPlanet.radius + birth_distance) * Math.Cos(direction);
	    ejectedPlanet.color = 0;
	    ejectedPlanet.id = 0;
	    Globals.planet_list.AddLast(ejectedPlanet);
	}

        public static double v_e = 2;              // velocity of ejected mass
        public static double m_e = 0.1;              // mass ejected in unit time
        public static double birth_distance = 0.1; // distance from the planet and the new ejected planet;
    }
    public class Orbit
    {
        public static double G = 10;         // gravitational constant
        public static double mass_sun = 50; // mass of sun; can be modified
        // Assume the stable orbit is a circle.
        public static double FindCircleOrbit(Planet planet)
        {
            double radius_orbit_new = 0.0;
            double velocitySquare = Math.Pow(planet.v_x, 2) + Math.Pow(planet.v_y, 2);
            radius_orbit_new = G * mass_sun / velocitySquare;
            return radius_orbit_new;
        }
        // Assume the stable orbit is an oval.
        // The orbit: v^2 = G*M*(2/d-1/a); assume mass_sun >> mass_planet.
        // Assume the sun is at the origin.
        public static double FindEllipticOrbit(Planet planet)
        {
            double distance_from_sun_new = 0.0;
            double velocitySquare = Math.Pow(planet.v_x,2)+Math.Pow(planet.v_y,2);
            double distance_from_sun_old = Math.Sqrt(planet.x * planet.x + planet.y * planet.y);
            double semi_major_axis = G * mass_sun * distance_from_sun_old / (2 * G * mass_sun - distance_from_sun_old * velocitySquare);
            distance_from_sun_new = 2/(velocitySquare/(G*mass_sun)+1/ semi_major_axis);
            return distance_from_sun_new;
        }
    }
}

