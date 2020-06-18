using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Minesweeper
{
    public sealed partial class MainPage : Page
    {
        
        SplitView split = new SplitView();
        
        static TextBox MRTextBox = new TextBox();
        static TextBox MCTextBox = new TextBox();
        static TextBox nMTextBox = new TextBox();
        public static int MR = 16;
        public static int MC = 30;
        public static int cellSise;
        int nM = 60;
        private int nMin;  // кол-во найденных мин
        private int nFlag; // кол-во поставленных флагов
        static bool status; // статус игры
        static int rBomb, cBomb;
        static Cell[,] cellArr = new Cell[30, 30];
        
        public MainPage()
        {  
            this.InitializeComponent();
            //Pole.PointerPressed += Pole_PointerPressed; //Событие клик на поле
            for (int i = 0; i < 30; i++)              //Создаём массив клеток
                for (int j = 0; j < 30; j++)
                    cellArr[i, j] = new Cell(i, j);

            //Сосдали статически

           /* Grid.SetRow(Pole, 1);                     //Создаём поле
            MainGrid.Children.Add(Pole);
            Pole.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(Flags ,1);
            MainGrid.Children.Add(Flags);
            Flags.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(Numbers, 1);
            MainGrid.Children.Add(Numbers);
            Numbers.HorizontalAlignment = HorizontalAlignment.Center;
            */
            newGame();                //Новая игра
            /*
            split.PaneBackground = new SolidColorBrush(Windows.UI.Color.FromArgb(200, 50, 171, 249));
            Grid Settings = new Grid();
            Settings.RowDefinitions.Add(new RowDefinition());
            Settings.RowDefinitions.Add(new RowDefinition());
            Settings.RowDefinitions.Add(new RowDefinition());
            Settings.Children.Add(MRTextBox);
            MRTextBox.Height = 20;
            MCTextBox.Height = 20;
            nMTextBox.Height = 20;
            MRTextBox.Text = Convert.ToString(MR);
            MCTextBox.Text = Convert.ToString(MC);
            nMTextBox.Text = Convert.ToString(nM);
            Grid.SetRow(MRTextBox, 0);
            MRTextBox.TextChanged += MRTextBox_TextChanged;
          
            Settings.Children.Add(MCTextBox);
            Grid.SetRow(MCTextBox, 1);
            MCTextBox.TextChanged += MCTextBox_TextChanged;
            Settings.Children.Add(nMTextBox);
            Grid.SetRow(nMTextBox, 2);
            nMTextBox.TextChanged += NMTextBox_TextChanged;
            MainGrid.Children.Add(split);
            Grid.SetRow(split, 1);
            split.Pane = Settings;
            */
        }

        private void NMTextBox_TextChanged(object sender, TextChangedEventArgs e)            //Изменение параметров игры
        {
            if(nMTextBox.Text.Length>0)
            nM = Convert.ToInt16(nMTextBox.Text);
        }

        private void MCTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(MCTextBox.Text.Length > 0)
            MC = Convert.ToInt16(MCTextBox.Text);
        }

        private void MRTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(MRTextBox.Text.Length > 0)
            MR = Convert.ToInt16(MRTextBox.Text); 
        }

        private void Pole_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if(status)
            { 
            double x, y;
            int r, c;
                Windows.UI.Xaml.Input.Pointer ptr = e.Pointer;
                if (ptr.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
                {
                  //  Windows.UI.Input.PointerPoint ptrPt = e.GetCurrentPoint(Pole);
                  //  x = ptrPt.Position.X;
                  //  y = ptrPt.Position.Y;
                    //c = (int)(x / cellSise);
                   // r = (int)(y / cellSise);
                 //   if (ptrPt.Properties.IsLeftButtonPressed)
                 //       open(r,c);
                  /*  if (ptrPt.Properties.IsRightButtonPressed)
                   {   
                        if (cellArr[r, c].isOpen) return;

                        if (cellArr[r, c].haveSettedFlag)             //Убрать флаг
                        {
                            cellArr[r, c].haveSettedFlag = false;
                            if (cellArr[r, c].haveBomb) nMin--;
                            nFlag--;
                          //  Flags.Children.Clear();
                            for (int i = 0; i < MR; i++)
                                for (int j = 0; j < MC; j++)
                                    if (cellArr[i, j].haveSettedFlag) DrawFlag(i,j);
                        }
                        else                                         //Поставить флаг
                        {
                            DrawFlag(r, c);        
                            if (cellArr[r, c].haveBomb) nMin++;
                            cellArr[r, c].haveSettedFlag = true;
                            nFlag++;
                            if (nMin == nM)
                            {
                                WinningLabel.Visibility = Visibility.Visible;
                                status = false;
                            }
                        }
                    }
                    */
                }
            }
        }
        private void Click(object sender) { }
        public void newGame() {  
            status = true; // начало игры
            nFlag = 0;
            nMin = 0;
            //очищаем гриды
            ButtonsGrid.ColumnDefinitions.Clear();
            ButtonsGrid.RowDefinitions.Clear();
            ButtonsGrid.Children.Clear();
            FlagsGrid.ColumnDefinitions.Clear();
            FlagsGrid.RowDefinitions.Clear();
            FlagsGrid.Children.Clear();
            NumbersGrid.ColumnDefinitions.Clear();
            NumbersGrid.RowDefinitions.Clear();
            NumbersGrid.Children.Clear();
            //задаём количество строк и столбцов
            for (int i = 0; i < MR; i++)
            {
                ButtonsGrid.RowDefinitions.Add(new RowDefinition());
                FlagsGrid.RowDefinitions.Add(new RowDefinition());
                NumbersGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < MC; i++)
            {
                ButtonsGrid.ColumnDefinitions.Add(new ColumnDefinition());
                FlagsGrid.ColumnDefinitions.Add(new ColumnDefinition());
                NumbersGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            //Создаем цифры
            for (int i = 0; i < MR; i++)
                for (int j = 0; j < MC; j++)
                {
                    TextBlock tb = new TextBlock();
                    tb.Text = "9";
                    tb.VerticalAlignment = VerticalAlignment.Center;
                    tb.HorizontalAlignment = HorizontalAlignment.Center;     
                    NumbersGrid.Children.Add(tb);
                    Grid.SetRow(tb, i);
                    Grid.SetColumn(tb, j);
                }
            //Создаем кнопки
            for (int i = 0; i < MR; i++)
                for (int j = 0; j < MC; j++)
                {
                    Button btn = new Button();
                    btn.Click += Button_Click_2;
                    btn.Background = new SolidColorBrush(Windows.UI.Colors.Gray);
                    btn.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black);
                    btn.VerticalAlignment = VerticalAlignment.Stretch;
                    btn.HorizontalAlignment = HorizontalAlignment.Stretch;
                    ButtonsGrid.Children.Add(btn);
                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);
                }
            /*
                        for (int i = 0; i < MR; i++)
                            for (int j = 0; j < MC; j++)
                            {
                                DrawRectangle(i, j, new SolidColorBrush(Windows.UI.Color.FromArgb(255, 210, 210, 210)), true, false, false);
                                DrawRectangleBorder(i, j, true);
                            }
            */
            /*
                        Flags.Children.Clear();
                        for (int i = 0; i < MR; i++)
                        {
                            Flags.RowDefinitions[i].Height = new GridLength(cellSise);
                            Pole.RowDefinitions[i].Height = new GridLength(cellSise);
                            Numbers.RowDefinitions[i].Height = new GridLength(cellSise);
                        }
                        for (int j = 0; j < MC; j++)
                        {
                            Flags.ColumnDefinitions[j].Width = new GridLength(cellSise);
                            Pole.ColumnDefinitions[j].Width = new GridLength(cellSise);
                            Numbers.ColumnDefinitions[j].Width = new GridLength(cellSise);
                        }


            */
            
            Random rnd = new Random();
            int n = 0;
            int tempRow;
            int tempCol;
            int k;

            // очистить поле
            for (int i = 0; i < MR; i++)
                for (int j = 0; j < MC; j++)
                {
                    cellArr[i, j].Status = 0;
                    cellArr[i, j].haveBomb = false;
                    cellArr[i, j].haveSettedFlag = false;
                    cellArr[i, j].isOpen = false;
                }

            // расставим мины
            while (n < nM) {
                tempRow = rnd.Next(MR);
                tempCol = rnd.Next(MC);
                if (!cellArr[tempRow, tempCol].haveBomb)
                {
                    cellArr[tempRow, tempCol].haveBomb = true;
                    n++;
                }
            }

            // для каждой клетки вычислим кол-во 
            // мин в соседних клетках
            for (int i = 0; i < MR; i++)
                for (int j = 0; j < MC; j++)
                    if (!cellArr[i, j].haveBomb)
                    {
                        k = 0;
                        if (i != 0)                          //Верхние три клетки
                        {
                            if(j!=0)
                                if (cellArr[i - 1, j - 1].haveBomb) k++;
                            if(j!=MC-1)
                                if (cellArr[i - 1, j + 1].haveBomb) k++;
                            if (cellArr[i - 1, j].haveBomb) k++;                         
                        }

                        if (j != 0)                                  //Боковые две клетки
                            if (cellArr[i, j - 1].haveBomb) k++;
                        if (j != MC - 1)
                            if (cellArr[i, j + 1].haveBomb) k++;

                        if (i != MR - 1)                            //Нижние три клетки
                        {
                            if (j != 0)
                                if (cellArr[i + 1, j - 1].haveBomb) k++;                         
                            if (j != MC - 1)
                                if (cellArr[i + 1, j + 1].haveBomb) k++;
                            if (cellArr[i + 1, j].haveBomb) k++;
                        }
                        cellArr[i, j].Status = k;
                    }     
            nMin = 0;      // нет обнаруженных мин
            nFlag = 0;      // нет поставленных флагов         
      }
        public static void open(int r, int c)
        {
            if (cellArr[r, c].haveBomb)
            {
                //Flags.Children.Clear();
                for (int i = 0; i < MR; i++)
                    for (int j = 0; j < MC; j++)
                    {
                        if (cellArr[i, j].haveBomb && !cellArr[i, j].haveSettedFlag)
                            {
                                DrawRectangle(i, j, new SolidColorBrush(Windows.UI.Color.FromArgb(255, 240, 240, 240)), false, true, false);
                                cellArr[i, j].isOpen = true;
                                DrawBomb(i, j);
                            }
                        if (!cellArr[i, j].haveBomb && cellArr[i, j].haveSettedFlag)
                        {
                            DrawRectangle(i, j, new SolidColorBrush(Windows.UI.Color.FromArgb(255, 240, 240, 240)), false, true, false);
                            DrawBomb(i, j);
                           Windows.UI.Xaml.Shapes.Line ln1 = new Windows.UI.Xaml.Shapes.Line();
                            ln1.Stroke = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0));
                            ln1.X1 = 0.05 * cellSise;
                            ln1.Y1 = 0.05 * cellSise;
                            ln1.X2 = 0.95 * cellSise;
                            ln1.Y2 = 0.95 * cellSise;
                            ln1.StrokeThickness = 4;
                            //Pole.Children.Add(ln1);
                            Grid.SetRow(ln1, i);
                            Grid.SetColumn(ln1, j);
                            Windows.UI.Xaml.Shapes.Line ln2 = new Windows.UI.Xaml.Shapes.Line();
                            ln2.Stroke = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0));
                            ln2.X1 = 0.95 *  cellSise;
                            ln2.Y1 = 0.05 * cellSise;
                            ln2.X2 = 0.05 * cellSise;
                            ln2.Y2 = 0.95 * cellSise;
                            ln2.StrokeThickness = 4;
                            //Pole.Children.Add(ln2);
                            Grid.SetRow(ln2, i);
                            Grid.SetColumn(ln2, j);
                            cellArr[i, j].isOpen = true;
                        }
                        if (cellArr[i, j].haveBomb && cellArr[i, j].haveSettedFlag) { DrawFlag(i,j); }
                    }
                DrawRectangle(r, c, new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0)), false, true, false);
                status = false;
                rBomb = r;
                cBomb = c;       
            }
            else if (cellArr[r, c].Status == 0)
            {
                DrawRectangle(r, c, new SolidColorBrush(Windows.UI.Color.FromArgb(255, 240, 240, 240)),false, true, false);
                cellArr[r, c].isOpen = true;
                if (r != 0)                             //Сбоку 
                    if (!cellArr[r - 1,c].isOpen)
                        open(r - 1, c);
                if (r != MR - 1)
                    if (!cellArr[r + 1, c].isOpen)
                        open(r + 1, c);
                if (c != 0)
                    if (!cellArr[r, c - 1].isOpen)   
                        open(r, c - 1);
                if (c != MC - 1)
                    if (!cellArr[r, c + 1].isOpen) 
                        open(r, c + 1);
                if (r != 0 && c != 0)                   //По диагонали  
                    if (!cellArr[r - 1, c - 1].isOpen)                    
                        open(r - 1, c - 1);
                if (r != MR - 1 && c != 0)
                    if (!cellArr[r + 1, c - 1].isOpen)
                        open(r + 1, c - 1);
                if (r != 0 && c != MC - 1)
                    if (!cellArr[r - 1, c + 1].isOpen)
                        open(r - 1, c + 1);
                if (r != MR - 1 && c != MC - 1)
                    if (!cellArr[r + 1, c + 1].isOpen)
                        open(r + 1, c + 1);
            }
            else if(cellArr[r, c].Status > 0 && cellArr[r, c].Status < 9)
            { 
                DrawRectangle(r,c, new SolidColorBrush(Windows.UI.Color.FromArgb(255, 240, 240, 240)), false, true,false);
                cellArr[r, c].isOpen = true;
                TextBlock tb = new TextBlock();
                tb.Text = Convert.ToString(cellArr[r, c].Status);
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                tb.VerticalAlignment = VerticalAlignment.Center;
               // Numbers.Children.Add(tb);
                Grid.SetColumn(tb, c);
                Grid.SetRow(tb, r);   
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            newGame();        
        }
        public static void DrawRectangle(int r, int c, SolidColorBrush brush, bool isInitialization, bool isOpening, bool isRedrawing)
        {
            Windows.UI.Xaml.Shapes.Rectangle rect = new Windows.UI.Xaml.Shapes.Rectangle();
            rect.Height = cellSise;
            rect.Width = cellSise;
            rect.Stroke = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 120, 120, 120));
            rect.Fill = brush;
            Grid.SetColumn(rect, c);
            Grid.SetRow(rect, r);
           // if (isInitialization)
               // Pole.Children.Add(rect);
           // else
            {
               // Pole.Children.RemoveAt(3 * (c + r * MC));
               // Pole.Children.Insert(3 * (c + r * MC), rect);    
            }
            if (isOpening && !isRedrawing)
            {
                Windows.UI.Xaml.Shapes.Rectangle fake1 = new Windows.UI.Xaml.Shapes.Rectangle();
                Windows.UI.Xaml.Shapes.Rectangle fake2 = new Windows.UI.Xaml.Shapes.Rectangle();
                fake1.Height = fake2.Height = fake1.Width = fake2.Width = 0;
            //    Pole.Children.RemoveAt(3 * (c + r * MC) + 1);
            //    Pole.Children.Insert(3 * (c + r * MC) + 1, fake1);
            //    Pole.Children.RemoveAt(3 * (c + r * MC) + 2);
             //   Pole.Children.Insert(3 * (c + r * MC) + 2, fake2);
            }
            if (isRedrawing)
            {
                Windows.UI.Xaml.Shapes.Rectangle fake1 = new Windows.UI.Xaml.Shapes.Rectangle();
                Windows.UI.Xaml.Shapes.Rectangle fake2 = new Windows.UI.Xaml.Shapes.Rectangle();
                fake1.Height = fake2.Height = fake1.Width = fake2.Width = 0;               
             //   Pole.Children.Insert(3 * (c + r * MC) + 1, fake1);
             //   Pole.Children.Insert(3 * (c + r * MC) + 2, fake2);
            }
        }
        public static void DrawRectangleBorder(int r, int c, bool isInitialization)
        {
            Windows.UI.Xaml.Shapes.Polygon BorderLU = new Windows.UI.Xaml.Shapes.Polygon();
            BorderLU.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
            PointCollection pcBase = new PointCollection();
            pcBase.Add(new Point(cellSise, 0));
            pcBase.Add(new Point(0, 0));
            pcBase.Add(new Point(0, cellSise));
            pcBase.Add(new Point(0.1 * cellSise, 0.9 * cellSise));
            pcBase.Add(new Point(0.1 * cellSise, 0.1 * cellSise));
            pcBase.Add(new Point(0.9 * cellSise, 0.1 * cellSise));
            BorderLU.Points = pcBase;
            Windows.UI.Xaml.Shapes.Polygon BorderRD = new Windows.UI.Xaml.Shapes.Polygon();
            BorderRD.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            PointCollection pcBase2 = new PointCollection();
            pcBase2.Add(new Point(cellSise, 0));
            pcBase2.Add(new Point(cellSise, cellSise));
            pcBase2.Add(new Point(0, cellSise));
            pcBase2.Add(new Point(0.1 * cellSise, 0.9 * cellSise));
            pcBase2.Add(new Point(0.9 * cellSise, 0.9 * cellSise));
            pcBase2.Add(new Point(0.9 * cellSise, 0.1 * cellSise));
            BorderRD.Points = pcBase2;
            if (isInitialization)
            {
            //    Pole.Children.Add(BorderLU);
            //    Pole.Children.Add(BorderRD);
            }
            else
            {
             //   Pole.Children.RemoveAt(3 * (c + r * MC) + 1);
             //   Pole.Children.RemoveAt(3 * (c + r * MC) + 1);
             //   Pole.Children.Insert(3 * (c + r * MC) + 1, BorderLU);
             //   Pole.Children.Insert(3 * (c + r * MC) + 2, BorderRD);
            }
            Grid.SetColumn(BorderLU, c);
            Grid.SetRow(BorderLU, r);
            Grid.SetColumn(BorderRD, c);
            Grid.SetRow(BorderRD, r);

        }
        public static void DrawBomb(int i, int j)//Рисование бомбы
        {
            Windows.UI.Xaml.Shapes.Ellipse elipse = new Windows.UI.Xaml.Shapes.Ellipse();
            elipse.Height = 0.6 * cellSise;
            elipse.Width = 0.6 * cellSise;
            elipse.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            Grid.SetRow(elipse, i);
            Grid.SetColumn(elipse, j);
           // Pole.Children.Add(elipse);
            Windows.UI.Xaml.Shapes.Line ln1 = new Windows.UI.Xaml.Shapes.Line();
            ln1.Stroke = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            ln1.X1 = cellSise / 2;
            ln1.Y1 = 0.1 * cellSise;
            ln1.X2 = cellSise / 2;
            ln1.Y2 = 0.9 * cellSise;
            Grid.SetRow(ln1, i);
            Grid.SetColumn(ln1, j);
          //  Pole.Children.Add(ln1);
            Windows.UI.Xaml.Shapes.Line ln2 = new Windows.UI.Xaml.Shapes.Line();
            ln2.Stroke = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            ln2.X1 = 0.1 * cellSise;
            ln2.Y1 = cellSise / 2;
            ln2.X2 = 0.9 * cellSise;
            ln2.Y2 = cellSise / 2;
            Grid.SetRow(ln2, i);
            Grid.SetColumn(ln2, j);
            //Pole.Children.Add(ln2);
            Windows.UI.Xaml.Shapes.Line ln3 = new Windows.UI.Xaml.Shapes.Line();
            ln3.Stroke = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            ln3.X1 = 0.2 * cellSise;
            ln3.Y1 = 0.2 * cellSise;
            ln3.X2 = 0.8 * cellSise;
            ln3.Y2 = 0.8 * cellSise;
            Grid.SetRow(ln3, i);
            Grid.SetColumn(ln3, j);
          //  Pole.Children.Add(ln3);
            Windows.UI.Xaml.Shapes.Line ln4 = new Windows.UI.Xaml.Shapes.Line();
            ln4.Stroke = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            ln4.X1 = 0.8 * cellSise;
            ln4.Y1 = 0.2 * cellSise;
            ln4.X2 = 0.2 * cellSise;
            ln4.Y2 = 0.8 * cellSise;
            Grid.SetRow(ln4, i);
            Grid.SetColumn(ln4, j);
            //Pole.Children.Add(ln4);
            Windows.UI.Xaml.Shapes.Ellipse blick = new Windows.UI.Xaml.Shapes.Ellipse();
            blick.Height = 0.2 * cellSise;
            blick.Width = 0.2 * cellSise;
            blick.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
            blick.Margin = new Thickness(-0.2 * cellSise, -0.2 * cellSise, 0, 0);
            Grid.SetRow(blick, i);
            Grid.SetColumn(blick, j);
           // Pole.Children.Add(blick);
        }
        public static void DrawFlag(int i, int j)//Рисование флага
        {
            Windows.UI.Xaml.Shapes.Line ln = new Windows.UI.Xaml.Shapes.Line();
            ln.Stroke = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            ln.StrokeThickness = 4;
            ln.X1 = 0.5 * cellSise;
            ln.Y1 = 0.5 * cellSise;
            ln.X2 = 0.5 * cellSise;
            ln.Y2 = 0.7 * cellSise;
          //  Flags.Children.Add(ln);
            Grid.SetRow(ln, i);
            Grid.SetColumn(ln, j);

            Windows.UI.Xaml.Shapes.Polygon flagBase = new Windows.UI.Xaml.Shapes.Polygon();
            flagBase.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            PointCollection pcBase = new PointCollection();
            pcBase.Add(new Point(0.65 * cellSise, 0.7 * cellSise));
            pcBase.Add(new Point(0.35 * cellSise, 0.7 * cellSise));
            pcBase.Add(new Point(0.35 * cellSise, 0.75 * cellSise));
            pcBase.Add(new Point(0.2 * cellSise, 0.75 * cellSise));
            pcBase.Add(new Point(0.2 * cellSise, 0.8 * cellSise));
            pcBase.Add(new Point(0.8 * cellSise, 0.8 * cellSise));
            pcBase.Add(new Point(0.8 * cellSise, 0.75 * cellSise));
            pcBase.Add(new Point(0.65 * cellSise, 0.75 * cellSise));
            flagBase.Points = pcBase;
            flagBase.Stroke = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            //Flags.Children.Add(flagBase);
            Grid.SetColumn(flagBase, j);
            Grid.SetRow(flagBase, i);

            Windows.UI.Xaml.Shapes.Polygon trng = new Windows.UI.Xaml.Shapes.Polygon();
            trng.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0));
            PointCollection pc = new PointCollection();
            pc.Add(new Point(0.5 * cellSise + 1, 0.2 * cellSise));
            pc.Add(new Point(0.45 * cellSise, 0.2 * cellSise));
            pc.Add(new Point(0.2 * cellSise, 0.35 * cellSise));
            pc.Add(new Point(0.45 * cellSise, 0.5 * cellSise));
            pc.Add(new Point(0.5 * cellSise + 1, 0.5 * cellSise));
            trng.Points = pc;
            trng.Stroke = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0));
           // Flags.Children.Add(trng);
            Grid.SetColumn(trng, j);
            Grid.SetRow(trng, i);
        }
        public int[] cellClick(double X, double Y) //Возвращает массив 0 элемент-строка, 1 элемент-столбик 
        {
            int[] arr = new int[2];
            arr[0] = (int)(Y / cellSise);
            arr[1] = (int)(X / cellSise);           
            return arr;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            split.IsPaneOpen = !split.IsPaneOpen;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            /*
            Flags.Children.Clear();
            Pole.Children.Clear();
            cellSise = (int)(page.ActualWidth / MC);
            for (int i = 0; i < MR; i++)
            {
                for (int j = 0; j < MC; j++)
                {
                     if (cellArr[i, j].isOpen)
                     {
                        if(i==rBomb && j == cBomb)
                            DrawRectangle(rBomb, cBomb, new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0)), true, false, false);
                                else
                            DrawRectangle(i, j, new SolidColorBrush(Windows.UI.Color.FromArgb(255, 240, 240, 240)), true, false, true);
                     }
                     else
                     {   
                            DrawRectangle(i, j, new SolidColorBrush(Windows.UI.Color.FromArgb(255, 210, 210, 210)), true, false, false);
                            DrawRectangleBorder(i, j, true);
                     }
                    if (status)
                        if (cellArr[i, j].haveSettedFlag) DrawFlag(i, j);
                    if (!status)
                    {
                        if (cellArr[i, j].haveBomb && !cellArr[i, j].haveSettedFlag)
                        {
                            DrawBomb(i, j);
                        }
                        if (!cellArr[i, j].haveBomb && cellArr[i, j].haveSettedFlag)
                        {
                            DrawRectangle(i, j, new SolidColorBrush(Windows.UI.Color.FromArgb(255, 240, 240, 240)), true, true, false);
                            DrawBomb(i, j);
                            Windows.UI.Xaml.Shapes.Line ln1 = new Windows.UI.Xaml.Shapes.Line();
                            ln1.Stroke = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0));
                            ln1.X1 = 0.05 * cellSise;
                            ln1.Y1 = 0.05 * cellSise;
                            ln1.X2 = 0.95 * cellSise;
                            ln1.Y2 = 0.95 * cellSise;
                            ln1.StrokeThickness = 4;
                            Pole.Children.Add(ln1);
                            Grid.SetRow(ln1, i);
                            Grid.SetColumn(ln1, j);
                            Windows.UI.Xaml.Shapes.Line ln2 = new Windows.UI.Xaml.Shapes.Line();
                            ln2.Stroke = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0));
                            ln2.X1 = 0.95 * cellSise;
                            ln2.Y1 = 0.05 * cellSise;
                            ln2.X2 = 0.05 * cellSise;
                            ln2.Y2 = 0.95 * cellSise;
                            ln2.StrokeThickness = 4;
                            Pole.Children.Add(ln2);
                            Grid.SetRow(ln2, i);
                            Grid.SetColumn(ln2, j);
                        }
                        if (cellArr[i, j].haveBomb && cellArr[i, j].haveSettedFlag) { DrawFlag(i, j); }
                        
                    }
                }
                Flags.RowDefinitions[i].Height = new GridLength(cellSise);
                Pole.RowDefinitions[i].Height = new GridLength(cellSise);
                Numbers.RowDefinitions[i].Height = new GridLength(cellSise);
            }
            for (int j = 0; j < MC; j++)
            {
                Flags.ColumnDefinitions[j].Width = new GridLength(cellSise);
                Pole.ColumnDefinitions[j].Width = new GridLength(cellSise);
                Numbers.ColumnDefinitions[j].Width = new GridLength(cellSise);
            }
            */
        }
    }
}

