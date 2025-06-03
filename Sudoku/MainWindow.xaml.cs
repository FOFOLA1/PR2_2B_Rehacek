using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace Sudoku;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>

enum GameStage { Config, Game, Finished, Stats }
public enum Difficulty { Informal, Easy, Hard }

public partial class MainWindow : Window
{
    private Difficulty _difficulty;
    private GameStage _stage = GameStage.Config;
    private Button _selectedNum;
    private bool _showErrors = false;
    private Style btnStyle;
    private Style btnStyleSelected;
    private bool _isCommentsEditMode;

    private DispatcherTimer _timer;
    private TimeSpan _elapsedTime;


    public MainWindow()
    {
        InitializeComponent();

        this.KeyDown += Button_KeyDown;
        btnStyle = (Style)FindResource("btnStyle");
        btnStyleSelected = (Style)FindResource("btnStyleSelected");
        _selectedNum = delBtn;
    }

    public string Time
    {
        get { return (string)GetValue(TimeProperty); }
        set { SetValue(TimeProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Time.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TimeProperty =
        DependencyProperty.Register("Time", typeof(string), typeof(MainWindow), new PropertyMetadata("0:00"));


    private void ConfigSceneButtonClick(object sender, RoutedEventArgs e)
    {
        ConfigSceneLoadingLabel.Visibility = Visibility.Visible;
        _difficulty = ((Button)sender).Content switch
        {
            "Neformální" => Difficulty.Informal,
            "Lehký" => Difficulty.Easy,
            "Těžký" => Difficulty.Hard
        };
        PreGenBoard();
        NextStage(GameStage.Game);
        ConfigSceneLoadingLabel.Visibility = Visibility.Hidden;
    }

    private void PreGenBoard()
    {
        Board.Children.Clear();
        Board.ColumnDefinitions.Clear();
        Board.RowDefinitions.Clear();

        buttons.Children.Clear();
        buttons.ColumnDefinitions.Clear();
        buttons.RowDefinitions.Clear();

        int size = 0;
        int boxes_row_count = 0;
        int boxes_col_count = 0;
        int boxes_width = 0;
        int boxes_heigth = 0;

        switch (_difficulty)
        {
            case Difficulty.Easy:
            case Difficulty.Hard:
                size = 9;
                boxes_col_count = 3;
                boxes_row_count = 3;
                boxes_width = 3;
                boxes_heigth = 3;
                break;
            case Difficulty.Informal:
                size = 6;
                boxes_col_count = 3;
                boxes_row_count = 2;
                boxes_width = 3;
                boxes_heigth = 2;
                break;
        }

        for (int i = 0; i < size; i++)
        {
            buttons.ColumnDefinitions.Add(new ColumnDefinition());
            Button btn = new Button();
            btn.Style = btnStyle;
            btn.Content = (i+1).ToString();
            btn.Click += new RoutedEventHandler(NumBtnClick);
            Grid.SetColumn(btn, i);
            buttons.Children.Add(btn);
        }
        _selectedNum = delBtn;
        _selectedNum.Style = btnStyleSelected;

        for (int i = 0; i < boxes_row_count; i++)
        {
            Board.ColumnDefinitions.Add(new ColumnDefinition());
        }

        for (int i = 0; i < boxes_col_count; i++)
        {
            Board.RowDefinitions.Add(new RowDefinition());
        }

        for (int i = 0; i < boxes_col_count; i++)
        {
            for (int j = 0; j < boxes_row_count; j++)
            {
                Border bd = new Border();
                Grid.SetRow(bd, i);
                Grid.SetColumn(bd, j);
                bd.BorderThickness = new Thickness(1);
                bd.BorderBrush = new SolidColorBrush(Colors.Black);


                Grid gr = new Grid();
                for (int k = 0; k < boxes_width; k++)
                {
                    gr.ColumnDefinitions.Add(new ColumnDefinition());
                }
                for (int k = 0; k < boxes_heigth; k++)
                {
                    gr.RowDefinitions.Add(new RowDefinition());
                }
                bd.Child = gr;
                Board.Children.Add(bd);
            }
        }


        List<List<int>> sudoku = SudokuGen.GenerateSudoku(_difficulty);


        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Num num = new Num();
                num.MouseDown += NumMouseDown;
                Grid.SetRow(num, i % boxes_heigth);
                Grid.SetColumn(num, j % boxes_width);
                //Board.Children.Add(num);
                ((Grid)((Border)Board.Children[(i / boxes_row_count) * boxes_row_count + j / boxes_col_count]).Child).Children.Add(num);
                if (sudoku[i][j] == 0)
                {
                    num.Symbol = "";
                }
                else
                {
                    num.Symbol = sudoku[i][j].ToString();
                    num.NumType = Type.Unchangable;
                }
                //num.Symbol = sudoku[k][col] == 0 ? "" : sudoku[k][col].ToString();

            }
        }



        //switch (_difficulty)
        //{
        //    case Difficulty.Easy:
        //        for (int k = 0; k < 9; k++)
        //        {
        //            Board.ColumnDefinitions.Add(new ColumnDefinition());
        //            Board.RowDefinitions.Add(new RowDefinition());
        //        }


        //        break;
        //    case Difficulty.Hard:
        //        for (int k = 0; k < 3; k++)
        //        {
        //            Board.ColumnDefinitions.Add(new ColumnDefinition());
        //            Board.RowDefinitions.Add(new RowDefinition());
        //        }

        //        for (int k = 0; k < 3; k++)
        //        {
        //            for (int col = 0; col < 3; col++)
        //            {
        //                Border bd = new Border();
        //                Grid.SetRow(bd, k);
        //                Grid.SetColumn(bd, col);
        //                bd.BorderThickness = new Thickness(1);
        //                bd.BorderBrush = new SolidColorBrush(Colors.Black);
                        

        //                Grid gr = new Grid();
        //                for (int k = 0; k < 3; k++)
        //                {
        //                    gr.ColumnDefinitions.Add(new ColumnDefinition());
        //                    gr.RowDefinitions.Add(new RowDefinition());
        //                }
        //                bd.Child = gr;
        //                Board.Children.Add(bd);
        //            }
        //        }


        //        List<List<int>> sudoku = SudokuGen.GenerateSudoku(_difficulty);

        //        for (int k = 0; k < 9; k++)
        //        {
        //            for (int col = 0; col < 9; col++)
        //            {
        //                Num num = new Num();
        //                //num.MouseDown += Num_MouseDown;
        //                Grid.SetRow(num, k%3);
        //                Grid.SetColumn(num, col%3);
        //                //Board.Children.Add(num);
        //                ((Grid)((Border)Board.Children[(k / 3) * 3 + col / 3]).Child).Children.Add(num);
        //                if (sudoku[k][col] == 0)
        //                {
        //                    num.Symbol = "";
        //                } else
        //                {
        //                    num.Symbol = sudoku[k][col].ToString();
        //                    num.NumType = Type.Unchangable;
        //                }
        //                //num.Symbol = sudoku[k][col] == 0 ? "" : sudoku[k][col].ToString();
                        
        //            }
        //        }

        //        break;
        //    case Difficulty.Informal:

        //        break;
        //}
        
        

        
    }

    private void renderFinishStage()
    {
        TimeSpan lastRecord = AppData.Get(_difficulty);
        FinishMsg.Text = $"Donokčil jsi obtížnost {_difficulty switch
        {
            Difficulty.Hard => "Těžká",
            Difficulty.Easy => "Lehká",
            Difficulty.Informal => "Neformální"
        }} v čase {Time}{(
            lastRecord == TimeSpan.Zero ? ", který je tvým prvním záznamem v této obtížnosti!" :
            _elapsedTime < lastRecord ? ", a tím jsi pokořil/a svůj dosavadní rekord!" : "."
        )}";
    }

    private void renderStats()
    {
        StatsData.Children.Clear();
        //StatsData.RowDefinitions.Clear();
        //StatsData.ColumnDefinitions.Clear();

        for (int i = 0; i < Enum.GetValues(typeof(Difficulty)).Length; i++)
        {
            Difficulty difficulty = (Difficulty) Enum.GetValues(typeof(Difficulty)).GetValue(i);

            Label label = new Label();
            label.Content = difficulty switch
            {
                Difficulty.Informal => "Neformální:",
                Difficulty.Easy => "Lehká:",
                Difficulty.Hard => "Těžká:"
            };
            label.FontSize = 30;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.VerticalContentAlignment = VerticalAlignment.Center;
            label.Margin = new Thickness(40, 0, 40, 0);
            Grid.SetRow(label, i);
            Grid.SetColumn(label, 0);
            StatsData.Children.Add(label);



            label = new Label();
            label.Content = formatTime(AppData.Get(difficulty));
            label.FontSize = 30;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.VerticalContentAlignment = VerticalAlignment.Center;
            label.Margin = new Thickness(40, 0, 40, 0);
            Grid.SetRow(label, i);
            Grid.SetColumn(label, 1);
            StatsData.Children.Add(label);



            //TextBlock textblock = new TextBlock();
            //textblock.Text = $"{difficulty switch
            //{
            //    Difficulty.Hard => "Těžká",
            //    Difficulty.Informal => "Neformální",
            //    Difficulty.Easy => "Lehká"
            //}}:\n    {formatTime(AppData.Get(difficulty))}";
            //textblock.FontSize = 30;
            ////textblock.HorizontalAlignment = HorizontalAlignment.Center;
            //textblock.VerticalAlignment = VerticalAlignment.Center;
            ////textblock.HorizontalContentAlignment = HorizontalAlignment.Center;
            ////textblock.VerticalContentAlignment = VerticalAlignment.Center;
            //textblock.Margin = new Thickness(40,0,40,0);
            ////textblock.Height = 150;
            //Grid.SetRow(textblock, i+1);
            ////textblock.Background = Brushes.Gray;
            //Stats.Children.Add(textblock);
        }
    }

    private void NextStage(GameStage next)
    {
        switch (_stage)
        {
            case GameStage.Config:
                if (next == GameStage.Game)
                {
                    ConfigScene.Visibility = Visibility.Hidden;
                    GameScene.Visibility = Visibility.Visible;
                    _elapsedTime = TimeSpan.Zero;

                    _timer = new DispatcherTimer();
                    _timer.Interval = TimeSpan.FromSeconds(1);
                    _timer.Tick += Timer_Tick;
                    _timer.Start();
                } else if (next == GameStage.Stats)
                {
                    renderStats();
                    ConfigScene.Visibility = Visibility.Hidden;
                    Stats.Visibility = Visibility.Visible;
                }
                    break;
            case GameStage.Game:
                _timer.Stop();
                renderFinishStage();
                AppData.CheckThenEdit(_difficulty, _elapsedTime);

                GameScene.Visibility = Visibility.Hidden;
                EndScene.Visibility = Visibility.Visible;
                break;
            case GameStage.Finished:
                _selectedNum = delBtn;
                _showErrors = false;
                _isCommentsEditMode = false;
                _showErrors = false;
                errors_button.Style = btnStyle;
                comments_button.Style = btnStyle;

                EndScene.Visibility = Visibility.Hidden;
                ConfigScene.Visibility = Visibility.Visible;
                break;
            case GameStage.Stats:
                Stats.Visibility = Visibility.Hidden;
                ConfigScene.Visibility = Visibility.Visible;
                break;
        }
        _stage = next;
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        _elapsedTime = _elapsedTime.Add(TimeSpan.FromSeconds(1));

        Time = formatTime(_elapsedTime);
    }

    private string formatTime(TimeSpan time)
    {
        if (time.Hours > 0)
        {
            return string.Format("{0}:{1:D2}:{2:D2}",
                                        time.Hours,
                                        time.Minutes,
                                        time.Seconds);
        }
        else if (time.Minutes >= 10)
        {
            return string.Format("{0:D2}:{1:D2}",
                                        time.Minutes,
                                        time.Seconds);
        }
        else
        {
            return string.Format("{0}:{1:D2}",
                                        time.Minutes,
                                        time.Seconds);
        }
    }

    private void NumBtnClick(object sender, RoutedEventArgs e)
    {
        SwitchBtns((Button)sender);
    }

    private void NumMouseDown(object sender, MouseButtonEventArgs e)
    {
        Num num = (Num)sender;
        if (_isCommentsEditMode)
        {
            switch (_selectedNum.Content)
            {
                case "Smazat":
                    num.RemoveAllComments();
                    break;
                default:
                    int temp = int.Parse(_selectedNum.Content.ToString()!);
                    num.SwitchComment(temp);
                    break;
            } 
        } else
        {
            num.ChangeContent(_selectedNum.Content switch
            {
                "Smazat" => "",
                _ => (string)_selectedNum.Content
            });
            if (CheckErrors())
            {
                NextStage(GameStage.Finished);
            }
        }
    }

    private void errors_button_Click(object sender = null, RoutedEventArgs e = null)
    {
        _showErrors = !_showErrors;
        errors_button.Style = _showErrors ? btnStyleSelected : btnStyle;
        CheckErrors();
    }


    private bool CheckErrors()
    {
        bool isWin = true;

        foreach (Border box in Board.Children)
        {
            foreach (Num cell in ((Grid)box.Child).Children)
            {
                if (cell.NumType == Type.Incorrect)
                    cell.NumType = Type.Changable;
            }
        }

        int size = 0;
        int boxes_row_count = 0;
        int boxes_col_count = 0;
        int boxes_width = 0;
        int boxes_heigth = 0;

        switch (_difficulty)
        {
            case Difficulty.Easy:
            case Difficulty.Hard:
                size = 9;
                boxes_col_count = 3;
                boxes_row_count = 3;
                boxes_width = 3;
                boxes_heigth = 3;
                break;
            case Difficulty.Informal:
                size = 6;
                boxes_col_count = 3;
                boxes_row_count = 2;
                boxes_width = 3;
                boxes_heigth = 2;
                break;
        }

        for (int i = 0; i < boxes_col_count * boxes_row_count; i++)
        {
            Grid box = (Grid)((Border)Board.Children[i]).Child;
            List<Num> prew = new List<Num>();

            for (int j = 0; j < box.Children.Count; j++)
            {
                if (((Num)box.Children[j]).Symbol == "")
                {
                    isWin = false;
                    continue;
                }
                for (int k = 0; k < prew.Count; k++)
                {
                    if (j != k && ((Num)box.Children[j]).Symbol == prew[k].Symbol)
                    {
                        if (_showErrors)
                        {
                            if (((Num)box.Children[j]).NumType != Type.Unchangable)
                                ((Num)box.Children[j]).NumType = Type.Incorrect;
                            if (prew[k].NumType != Type.Unchangable)
                                prew[k].NumType = Type.Incorrect;
                        }
                        isWin = false;
                    }
                }
                //num.Content;
                prew.Add((Num)box.Children[j]);

            }
        }

        for (int row = 0; row < size; row++)
        {
            List<Num> prew = new List<Num>();
            for (int col = 0; col < size; col++)
            {

                //int box_row = row / boxes_heigth;
                //int box_col = col / boxes_width;
                //int box_index = box_row * boxes_row_count + box_col;

                //int row_in_box = row % boxes_heigth;
                //int col_in_box = col % boxes_width;
                //int index_in_box = row_in_box * boxes_width + col_in_box;
                Num current = (Num)((Grid)((Border)Board.Children[row / boxes_heigth * boxes_row_count + col / boxes_width]).Child).Children[row % boxes_heigth * boxes_width + col % boxes_width];
                //Num current = (Num)((Grid)((Border)Board.Children[0]).Child).Children[0];

                //current.Symbol = (row * size + col).ToString();
                if (current.Symbol == "")
                {
                    isWin = false;
                    continue;
                }
                for (int k = 0; k < prew.Count; k++)
                {
                    if (current.Symbol == prew[k].Symbol)
                    {
                        if (_showErrors)
                        {
                            if (current.NumType != Type.Unchangable)
                                current.NumType = Type.Incorrect;
                            if (prew[k].NumType != Type.Unchangable)
                                prew[k].NumType = Type.Incorrect;
                        }
                        isWin = false;
                    }
                }
                prew.Add(current);
            }
        }

        for (int row = 0; row < size; row++)
        {
            List<Num> prew2 = new List<Num>();
            for (int col = 0; col < size; col++)
            {
                Num current = (Num)((Grid)((Border)Board.Children[col / boxes_heigth * boxes_row_count + row / boxes_width]).Child).Children[col % boxes_heigth * boxes_width + row % boxes_width];
                //current.Symbol = (row * size + col).ToString();
                if (current.Symbol == "")
                {
                    isWin = false;
                    continue;
                }
                for (int k = 0; k < prew2.Count; k++)
                {
                    if (current.Symbol == prew2[k].Symbol)
                    {
                        if (_showErrors)
                        {
                            if (current.NumType != Type.Unchangable)
                                current.NumType = Type.Incorrect;
                            if (prew2[k].NumType != Type.Unchangable)
                                prew2[k].NumType = Type.Incorrect;
                        }
                        isWin = false;
                    }
                }
                prew2.Add(current);
            }
        }
        return isWin;
    }

    private void Button_KeyDown(object sender, KeyEventArgs e)
    {
        int number_index = -2;

        if (e.Key >= Key.D0 && e.Key <= Key.D9)
        {
            number_index = e.Key - Key.D0 - 1;
        } else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
        {
            number_index = e.Key - Key.NumPad0 - 1;
        }


        if (number_index == -1 || e.Key == Key.Back)
        {
            SwitchBtns(delBtn);
        }
        else if (number_index > -1 && buttons.Children.Count > number_index)
        {
            SwitchBtns((Button)buttons.Children[number_index]);
        } else if (e.Key == Key.P || e.Key == Key.N || e.Key == Key.OemOpenBrackets || e.Key == Key.Divide)
        {
            comments_Click();
        } else if (e.Key == Key.E || e.Key == Key.C)
        {
            errors_button_Click();
        }

        
    }

    private void SwitchBtns(Button newBtn)
    {
        _selectedNum.Style = btnStyle;
        newBtn.Style = btnStyleSelected;


        _selectedNum = newBtn;
    }

    private void comments_Click(object sender = null, RoutedEventArgs e = null)
    {
        _isCommentsEditMode = !_isCommentsEditMode;
        comments_button.Style = _isCommentsEditMode ? btnStyleSelected : btnStyle;
    }

    private void EndScene_BackButton_Click(object sender, RoutedEventArgs e)
    {
        NextStage(GameStage.Config);
    }

    private void Stats_Button_Click(object sender, RoutedEventArgs e)
    {
        NextStage(GameStage.Stats);
    }

    private void StatsBackButtonClick(object sender, RoutedEventArgs e)
    {
        NextStage(GameStage.Config);
    }
}


