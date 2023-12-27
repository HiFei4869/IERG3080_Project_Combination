using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

//using ColorPositionCombination;
using Model;

using NewEndCollision;
using PhysicEngine.Movement;


namespace WpfApp2
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    /// 
    public class Color
    {
        public static int ChangeColor(Planet player, Planet planet1)
        {
            if (player.radius >= planet1.radius)
            {
                //Debug.WriteLine($"0");
                return 0;
            }
            else
            {
                //Debug.WriteLine($"1");
                return 1;
            }
        }
    }

    public class Position
    {
        /*public static void ChangePosition(Planet planet)
        {
            //Debug.WriteLine($"before:{planet.x}, {planet.y}");
            double angle = Math.Atan((planet.y - 480) / (planet.x - 270));
            planet.x += 0.02 * planet.v_x;
            planet.y += 0.02 * planet.v_y; //Debug.WriteLine($"{planet.v_x}, {planet.v_y}");
            planet.v_y -= 10 * Math.Sin(angle);
            planet.v_x -= 10 * Math.Cos(angle);
            //Debug.WriteLine($"after:{planet.x}, {planet.y}");
        }*/

        /*public static void ChangePosition(LinkedList<Planet> myLinkedList)
        {
            LinkedListNode<Planet> planetNode = myLinkedList.First;
            while (planetNode != null)
            {
                Planet planet = planetNode.Value;

                double angle = Math.Atan((planet.y - 480) / (planet.x - 270));
                planet.x += 0.02 * planet.v_x;
                planet.y += 0.02 * planet.v_y; //Debug.WriteLine($"{planet.v_x}, {planet.v_y}");
                planet.v_y -= 10 * Math.Sin(angle);
                planet.v_x -= 10 * Math.Cos(angle);

                planetNode = planetNode.Next;
            }
        }*/
    }

    public partial class Window1 : Window
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

        private GameViewModel _viewModel;
        private MouseProcessor mouseProcessor;

        private int win = 0;
        private int die = 0;
        private int check_ejection = 0;
        private double x_c, y_c;

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // 获取鼠标位置
            Point position = e.GetPosition(this);
            check_ejection = 1;
            x_c = position.X;
            y_c = position.Y; //MessageBox.Show($"111", "gameover", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private DispatcherTimer gameTimer;

        public Window1()
        {
            Start.start();
            InitializeComponent();
            _viewModel = new GameViewModel();
            this.DataContext = _viewModel;
            InitializeGame();
        }

        private void InitializeGame()
        {
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(20); // Adjust as needed
            gameTimer.Tick += GameLoop;
            gameTimer.Start();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            DateTime dateTime = new DateTime();
            int lastTime = dateTime.Millisecond;

            //while (win == 0 & die == 0)
            //{
                mouseProcessor = new MouseProcessor();
                this.MouseLeftButtonDown += OnMouseLeftButtonDown;

                //int die = 0, win = 0;
                double[] orbit = new double[3];
                List<Planet> copyList = new List<Planet>(Globals.planet_list);
                List<Planet> copyList1 = new List<Planet>(Globals.planet_list);
                foreach (Planet check1 in copyList)                      //check collision
                {
                    foreach (Planet check2 in copyList1)
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

                ////change color

                Planet player = findPlayer();
                /*foreach (Planet planet in Globals.planet_list)                //change color
                {

                    if (planet.id != 1 && planet.id != 2)
                    {
                        Color.ChangeColor(player, planet);
                    }
                }*/

                for (int i = 0; i < Globals.planet_list.Count; i++)
                {
                    Planet planet = Globals.planet_list.ElementAt(i);

                    if (planet.id != 1 && planet.id != 2)
                    {
                        if (player.radius > planet.radius)
                        {
                            planet.color = 0;

                            Planet newPlanet = new Planet();
                            newPlanet.id = planet.id;
                            newPlanet.x = planet.x;
                            newPlanet.y = planet.y;
                            newPlanet.mass = planet.mass;
                            newPlanet.radius = planet.radius;
                            newPlanet.v_x = planet.v_x;
                            newPlanet.v_y = planet.v_y;
                            newPlanet.color = planet.color;
                            Globals.planet_list_color.AddLast(newPlanet);
                        }
                        else
                        {
                            planet.color = 1;

                            Planet newPlanet = new Planet();
                            newPlanet.id = planet.id;
                            newPlanet.x = planet.x;
                            newPlanet.y = planet.y;
                            newPlanet.mass = planet.mass;
                            newPlanet.radius = planet.radius;
                            newPlanet.v_x = planet.v_x;
                            newPlanet.v_y = planet.v_y;
                            newPlanet.color = planet.color;
                            Globals.planet_list_color.AddLast(newPlanet);
                        }
                    }

                    else
                    {
                        Globals.planet_list_color.AddLast(planet);
                    }
                }

                Globals.planet_list.Clear();

                foreach (var newplanet in Globals.planet_list_color)
                {
                    Globals.planet_list.AddLast(new Planet
                    {
                        id = newplanet.id,
                        x = newplanet.x,
                        y = newplanet.y,
                        mass = newplanet.mass,
                        radius = newplanet.radius,
                        v_x = newplanet.v_x,
                        v_y = newplanet.v_y,
                        color = newplanet.color
                    });
                }

                Globals.planet_list_color.Clear();

                ////eject objects

                if (check_ejection == 1)
                {                    
                    Movement.SplitEject(Globals.planet_list, player, x_c, y_c);
                    check_ejection = 0;
                }

                if(player.mass <= 0)
                {
                    die = 1;
                }

                //Position.ChangePosition(Globals.planet_list);

            /*foreach (Planet planet in Globals.planet_list)           //change position
            {
                Position.ChangePosition(planet);
            }*/
            
            ////change position

            //private const double G = 6.674e-11;

            for (int i = 0; i < Globals.planet_list.Count; i++)
            {
                Planet planet = Globals.planet_list.ElementAt(i);

                if(planet.id != 2)
                {                  
                    double r = Math.Sqrt(Math.Pow(planet.x - 480, 2) + Math.Pow(planet.y - 270, 2));
                    double a = r;
                    double v = Math.Sqrt(10 * 100000 * (2 / r - 1 / a));
                    planet.v_x = v * (planet.y - 270) / r;
                    planet.v_y = -v * (planet.x - 480) / r;
                    planet.x += 0.02 * planet.v_x;
                    planet.y += 0.02 * planet.v_y;

                    /*double angle = Math.Atan((planet.y - 270) / (planet.x - 480));
                    planet.x += 0.02 * planet.v_x;
                    planet.y += 0.02 * planet.v_y;
                    planet.v_y -= 10 * Math.Sin(angle);
                    planet.v_x -= 10 * Math.Cos(angle);*/

                    Planet newPlanet = new Planet();
                    newPlanet.id = planet.id;
                    newPlanet.x = planet.x;
                    newPlanet.y = planet.y;
                    newPlanet.mass = planet.mass;
                    newPlanet.radius = planet.radius;
                    newPlanet.v_x = planet.v_x;
                    newPlanet.v_y = planet.v_y;
                    newPlanet.color = planet.color;
                    Globals.planet_list_new.AddLast(newPlanet);
                    //Debug.WriteLine($"1111Planet {planet.id}: x = {planet.x}, y = {planet.y}");
                }

                else
                {
                    Globals.planet_list_new.AddLast(planet);
                }
            }

            Globals.planet_list.Clear();

            foreach (var newplanet in Globals.planet_list_new)
            {
                Globals.planet_list.AddLast(new Planet
                {
                    id = newplanet.id,
                    x = newplanet.x,
                    y = newplanet.y,
                    mass = newplanet.mass,
                    radius = newplanet.radius,
                    v_x = newplanet.v_x,
                    v_y = newplanet.v_y,
                    color = newplanet.color
                });
            }

            Globals.planet_list_new.Clear();

            Debug.WriteLine($"ini");
                foreach (Planet planet in Globals.planet_list)
                {
                    Debug.WriteLine($"player:{planet.x}, {planet.y}"); ;
                }

                if (TouchBoundary.touchBoundary(Globals.planet_list) == 1) //Check die
                {
                    die = 1;
                }

                if (WinCheck.winCheck(Globals.planet_list) == 1)                //Check win
                {
                    win = 1;
                }

                /*while (dateTime.Millisecond - lastTime < 20)
                {
                    if (dateTime.Millisecond - lastTime < 0)
                    {
                        lastTime -= 999;
                    }
                }


                lastTime = dateTime.Millisecond;*/

                /*Stopwatch stopwatch = Stopwatch.StartNew();
                Thread.Sleep(5);
                stopwatch.Stop();*/

                

                //win = 1;

                /*while (win == 0 & die == 0)
                {
                    mouseProcessor = new MouseProcessor();
                    this.MouseLeftButtonDown += OnMouseLeftButtonDown;
                    //上面这个函数是用来记录鼠标点击的位置的

                    //Combination combination = new Combination();
                    //Combination.Mainprogram();

                    //我让这个循环在这个地方睡5毫秒来实现我们代码每隔一段时间生成一些结果蛤
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    Thread.Sleep(5);
                    stopwatch.Stop();

                    Combination.WinDieChanged += OnWinDieChanged;

                    //win = 1;
                    //gamestatus = 2;
                    //假设是2游戏成功了哈，我只是方便测试一下，没有问题就行了
                    //GameCanvas.Children.Clear();//你们测试的时候可以注释掉这一行让其不能删掉所有画布上的东西
                }*/
                if (win == 1)
                {
                    gameTimer.Stop();
                    MessageBox.Show($"You Win!", "gameover", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (die == 1)
                {
                    gameTimer.Stop();
                    MessageBox.Show($"You Lose!", "gameover", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            //}
            //_viewModel = new GameViewModel();
            //GameCanvas.Children.Clear();
            _viewModel.ResetState();
            Debug.WriteLine($"1");
            //this.DataContext = _viewModel;
        }
        

        /*private void OnWinDieChanged(int newWinValue, int newDieValue)
        {
            win = newWinValue;
            die = newDieValue;
        }*/
    }
  
}

