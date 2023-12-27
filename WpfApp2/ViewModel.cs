using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
//using System.Windows.Media;
using NewEndCollision;
using System.Windows;

public class Ball : INotifyPropertyChanged
{
    private double _x;
    private double _y;
    private double _radius;
    private string _color;

    public double BallX
    {
        get => _x;
        set { _x = value; OnPropertyChanged(nameof(BallX)); }
    }

    public double BallY
    {
        get => _y;
        set { _y = value; OnPropertyChanged(nameof(BallY)); }
    }

    public double BallRadius
    {
        get => _radius;
        set { _radius = value; OnPropertyChanged(nameof(BallRadius)); }
    }

    public string BallColor
    {
        get => _color;
        set { _color = value; OnPropertyChanged(nameof(BallColor)); }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //Debug.WriteLine($"PropertyChanged: {propertyName}");
        //MessageBox.Show($"PropertyChanged: {propertyName}", "Debug Information", MessageBoxButton.OK, MessageBoxImage.Information);
    }

}

public class GameViewModel : INotifyPropertyChanged
{
  

    /*private double _orbitCenterX;
    private double _orbitCenterY;
    private double _orbitWidth;
    private double _orbitHeight;
    private string _orbitColor;
    private double _orbitRotationAngle;

    
    public double OrbitRotationAngle
    {
        get => _orbitRotationAngle;
        set
        {
            _orbitRotationAngle = value;
            OnPropertyChanged(nameof(OrbitRotationAngle));
        }
    }
    // 添加轨道属性
    public double OrbitCenterX
    {
        get => _orbitCenterX;
        set
        {
            _orbitCenterX = value;
            OnPropertyChanged(nameof(OrbitCenterX));
        }
    }

    public double OrbitCenterY
    {
        get => _orbitCenterY;
        set
        {
            _orbitCenterY = value;
            OnPropertyChanged(nameof(OrbitCenterY));
        }
    }

    public double OrbitWidth
    {
        get => _orbitWidth;
        set
        {
            _orbitWidth = value;
            OnPropertyChanged(nameof(OrbitWidth));
        }
    }

    public double OrbitHeight
    {
        get => _orbitHeight;
        set
        {
            _orbitHeight = value;
            OnPropertyChanged(nameof(OrbitHeight));
        }
    }

    public string OrbitColor
    {
        get => _orbitColor;
        set
        {
            _orbitColor = value;
            OnPropertyChanged(nameof(OrbitColor));
        }
    }*/


    private ObservableCollection<Ball> _balls = new ObservableCollection<Ball>();
    public ObservableCollection<Ball> Balls
    {
        get
        {
            //MessageBox.Show($"Balls getter accessed.", "gameover", MessageBoxButton.OK, MessageBoxImage.Information);
            return _balls; 
        }
        set
        {
            _balls = value;
            OnPropertyChanged(nameof(Balls));
        }
    }

    public GameViewModel()
    {
        //Start.start();

        foreach (Planet planet in Globals.planet_list)
        {
            Debug.WriteLine($"player:{planet.x}, {planet.y}");
            if (planet.color == 0)
                AddBall(planet.x, planet.y, planet.radius, "blue");
            else if (planet.color == 1)
                AddBall(planet.x, planet.y, planet.radius, "red");
            else if (planet.color == 2)
                AddBall(planet.x, planet.y, planet.radius, "green");
            else if (planet.color == 3)
                AddBall(planet.x, planet.y, planet.radius, "orange");
        }

        OnPropertyChanged(nameof(Balls));

        //下面的你们看用啥方式来随机生成所需的椭圆轨道
        /*OrbitCenterX = 400;
        OrbitCenterY = 200;
        OrbitWidth = 200;//是长轴不是半长轴
        OrbitHeight = 100;//是短轴不是半短轴
        OrbitColor = "White"; // 可以使用颜色名称或十六进制颜色代码
        OrbitRotationAngle = 0; // 默认无旋转，如果旋转的话，默认是按照椭圆的中心点旋转，使用度degree作为单位*/
    }

    public void ResetState()
    {
        Debug.WriteLine($"before");
        foreach (Planet planet in Globals.planet_list)
        {
            Debug.WriteLine($"player:{planet.x}, {planet.y}");;
        }
        Debug.WriteLine($"after");
        //MessageBox.Show($"aaaaaaa", "Debug Information", MessageBoxButton.OK, MessageBoxImage.Information);
        Balls.Clear();
        foreach (Planet planet in Globals.planet_list)
        {
            Debug.WriteLine($"player:{planet.x}, {planet.y}");
            if (planet.color == 0)
                AddBall(planet.x, planet.y, planet.radius, "blue");
            else if (planet.color == 1)
                AddBall(planet.x, planet.y, planet.radius, "red");
            else if (planet.color == 2)
                AddBall(planet.x, planet.y, planet.radius, "green");
            else if (planet.color == 3)
                AddBall(planet.x, planet.y, planet.radius, "orange");
        }

        Balls = new ObservableCollection<Ball>(Balls); // Reassign the collection to trigger update
        OnPropertyChanged(nameof(Balls));
    }

    public void AddBall(double x, double y, double radius, string color)
    {
        Balls.Add(new Ball { BallX = x, BallY = y, BallRadius = radius, BallColor = color });
    }
    

    public void RemoveBall(double x, double y, double radius, string color)
    {
        var ballToRemove = Balls.FirstOrDefault(ball => ball.BallX == x && ball.BallY == y && ball.BallRadius == radius && ball.BallColor == color);
        if (ballToRemove != null)
        {
            Balls.Remove(ballToRemove);
        }
    }


    //在这个地方你们引入一些奇奇怪怪的方法来更新小球的变化，
    //记住第一个小球是默认的玩家小球，你们还要用随机数来增加一些额外的到处动的小球

    //上面这个更改方式会传递到UI中，会立刻改变，你们别引入一些奇奇怪怪的改变方式蛤到时候你们改的时候用循环跑哈

    // 其他的ViewModel方法
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //Debug.WriteLine($"PropertyChanged: {propertyName}");
        //MessageBox.Show($"111PropertyChanged: {propertyName}", "Debug Information", MessageBoxButton.OK, MessageBoxImage.Information);
    }


    //你们通过调用下面这个函数来实现椭圆轨道的绘制
    /*public void UpdateOrbit(double centerX, double centerY, double width, double height, string color, double rotationangle)
    {
        OrbitCenterX = centerX;
        OrbitCenterY = centerY;
        OrbitWidth = width;
        OrbitHeight = height;
        OrbitColor = color;
        OrbitRotationAngle=rotationangle;
        OnPropertyChanged(nameof(OrbitCenterX));
        OnPropertyChanged(nameof(OrbitCenterY));
        OnPropertyChanged(nameof(OrbitWidth));
        OnPropertyChanged(nameof(OrbitHeight));
        OnPropertyChanged(nameof(OrbitColor));
        OnPropertyChanged(nameof(OrbitRotationAngle));
    }*/

}

