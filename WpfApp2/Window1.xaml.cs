using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            //MessageBox.Show($"SplitEject: {planet.x}, {planet.y} ", "Debug Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
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
            gameTimer.Interval = TimeSpan.FromMilliseconds(500); // Adjust as needed
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
                    check_ejection = 0;
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
            _viewModel.ResetState();
            //this.DataContext = _viewModel;
        }
        

        /*private void OnWinDieChanged(int newWinValue, int newDieValue)
        {
            win = newWinValue;
            die = newDieValue;
        }*/
    }
  
}

