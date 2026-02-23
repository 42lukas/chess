using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Chess;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        InitializeBoard();
    }

    public void InitializeBoard()
    {
        var board_btns = new Button[64];
        var board_txt_letters = new string[] { "H", "G", "F", "E", "D", "C", "B", "A" };
        var board_txt_numbers = new string[] { "8", "7", "6", "5", "4", "3", "2", "1" };
        var board_txts = new TextBlock[16];
        this.RootGrid.RowDefinitions = new RowDefinitions("100, 100, 100, 100, 100, 100, 100, 100, 100");
        this.RootGrid.ColumnDefinitions = new ColumnDefinitions("100, 100, 100, 100, 100, 100, 100, 100, 100");

        for (int i = 0; i < board_btns.Length; i++)
        {
            board_btns[i] = new Button
            {
                Height = 100,
                Width = 100,
                Name = $"{i}"
            };
            Grid.SetRow(board_btns[i], i / 8);
            Grid.SetColumn(board_btns[i], i % 8);
            board_btns[i].Click += ClickHandler;

            this.RootGrid.Children.Add(board_btns[i]);
        }

        for (int i = 0; i < board_txt_letters.Length; i++)
        {
            board_txts[i] = new TextBlock
            {
                Text = board_txt_letters[i],
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                Foreground = new SolidColorBrush(Colors.White)
            };
            Grid.SetRow(board_txts[i], 8);
            Grid.SetColumn(board_txts[i], i);

            this.RootGrid.Children.Add(board_txts[i]);
        }

        for (int i = 0; i < board_txt_numbers.Length; i++)
        {
            board_txts[i + 8] = new TextBlock
            {
                Text = board_txt_numbers[i],
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                Foreground = new SolidColorBrush(Colors.White)
            };
            Grid.SetRow(board_txts[i + 8], i);
            Grid.SetColumn(board_txts[i + 8], 8);

            this.RootGrid.Children.Add(board_txts[i + 8]);
        }

        this.Content = this.RootGrid;
    }

    public void ClickHandler(object? sender, RoutedEventArgs args)
    {
        if (sender is Button clickedButton && int.TryParse(clickedButton.Name, out int index))
        {
            int row = index / 8;
            int col = index % 8;
            System.Console.WriteLine($"Button at row {row + 1}, column {col + 1} was clicked.");
        }
    }
}