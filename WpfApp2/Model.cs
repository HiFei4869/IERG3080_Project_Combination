using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

///public struct Ball
    /*{ 
    public double X, Y; // 位置
    public double Vx, Vy; // 速度
    public double Mass; // 质量
    public double Radius; // 半径
    public string Color; // 颜色 别用一些很逆天的颜色，用一些普通的颜色蛤
    public Ball(double x, double y, double vx, double vy, double mass, double radius, string color)
    {
        X = x;
        Y = y;
        Vx = vx;
        Vy = vy;
        Mass = mass;
        Radius = radius;
        Color = color;
    }*/
///

namespace Model
{
    /*public class BallInitializer//你们来改写这个程序，让他能够随机生成一系列小球
    {

        private static readonly Random random = new Random();

        public static Ball[] InitializeBalls(int numberOfBalls)
        {
            Ball[] balls = new Ball[numberOfBalls];

            for (int i = 0; i < numberOfBalls; i++)
            {
            
                double x = 100*i; 
                double y = 100*i; 

               
                double vx = 0;
                double vy = 0;

               
                double mass = 0; 
                double radius = 100;
                string color = "Red";

                balls[i] = new Ball(x, y, vx, vy, mass, radius, color);
            }
            return balls;
        }
    }*/
    public class MouseProcessor//这个告诉你们鼠标点击的位置坐标
    {
        public void ProcessMouseClick(Point clickPosition)
        {
            //MessageBox.Show($"Mouse clicked at X: {clickPosition.X}, Y: {clickPosition.Y}","333", MessageBoxButton.OK, MessageBoxImage.Information);
            Console.WriteLine($"Mouse clicked at X: {clickPosition.X}, Y: {clickPosition.Y}");
        }
    }

}
