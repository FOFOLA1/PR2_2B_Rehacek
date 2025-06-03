using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for Num.xaml
    /// </summary>
    public enum Type
    {
        Changable,
        Unchangable,
        Incorrect
    }

    public partial class Num : UserControl
    {
        private static int nextID = 0;
        public int ID;
        private List<int> commentNums = new List<int>();

        public string Symbol
        {
            get { return (string)GetValue(SymbolProperty); }
            set { SetValue(SymbolProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Symbol.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SymbolProperty =
            DependencyProperty.Register("Symbol", typeof(string), typeof(Num), new PropertyMetadata(""));

        public Type NumType
        {
            get { return (Type)GetValue(NumTypeProperty); }
            set {
                SetValue(NumTypeProperty, value);
                border.Background = value switch
                {
                    Type.Unchangable => new SolidColorBrush(Colors.LightGray),
                    Type.Changable => new SolidColorBrush(Colors.AliceBlue),
                    Type.Incorrect => new SolidColorBrush(Colors.OrangeRed)
                };
            }
        }

        // Using a DependencyProperty as the backing store for NumType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumTypeProperty =
            DependencyProperty.Register("NumType", typeof(Type), typeof(Num), new PropertyMetadata(Type.Changable));





        public string CommentsString
        {
            get { return (string)GetValue(CommentsStringProperty); }
            private set { SetValue(CommentsStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommentsString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommentsStringProperty =
            DependencyProperty.Register("CommentsString", typeof(string), typeof(Num), new PropertyMetadata(""));





        public Num()
        {
            ID = nextID++;
            InitializeComponent();
        }

        public void ChangeContent(string Content)
        {
            if (NumType == Type.Changable || NumType == Type.Incorrect)
            {
                Symbol = Content;
            }
        }

        public void SwitchComment(int comment)
        {
            if (commentNums.Contains(comment))
                commentNums.Remove(comment);

            else if (commentNums.Count < 3)
            {
                commentNums.Add(comment);
                commentNums.Sort();
            }
            rewriteComments();
        }

        public void RemoveAllComments()
        {
            commentNums = new List<int>();
            rewriteComments();
        }

        private void rewriteComments()
        {
            CommentsString = commentNums.Count > 0 ? string.Join("  ", commentNums) : "";
        }
    }
}
