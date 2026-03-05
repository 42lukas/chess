using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Chess.Core.Gamefield;
using Chess.Core.Pieces;
using Chess.Core.Rules;

namespace Chess;

public partial class MainWindow : Window
{
    public Button[] board_btns = new Button[64];
    private Bitmap? w_pawn_bit, b_pawn_bit, w_rook_bit, b_rook_bit, w_knight_bit, b_knight_bit, w_bishop_bit, b_bishop_bit, w_queen_bit, b_queen_bit, w_king_bit, b_king_bit;
    public Board board = new Board();
    private int selectedRow = -1;
    private int selectedCol = -1;

    public MainWindow()
    {
        InitializeComponent();
        InitializeBoard();
        LoadPieceImages();
        RenderBoard();
    }

    public void InitializeBoard()
    {
        var board_txts = new TextBlock[16];
        var board_txt_letters = new string[] { "H", "G", "F", "E", "D", "C", "B", "A" };
        var board_txt_numbers = new string[] { "8", "7", "6", "5", "4", "3", "2", "1" };
        this.RootGrid.RowDefinitions = new RowDefinitions("100, 100, 100, 100, 100, 100, 100, 100, 100");
        this.RootGrid.ColumnDefinitions = new ColumnDefinitions("100, 100, 100, 100, 100, 100, 100, 100, 100");

        for (int i = 0; i < board_btns.Length; i++)
        {
            board_btns[i] = new Button
            {
                Height = 100,
                Width = 100,
                Name = $"{i}",
                Background = (i / 8 + i % 8) % 2 == 0 ? new SolidColorBrush(Color.Parse("#F0D9B5")) : new SolidColorBrush(Color.Parse("#B58863"))
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

    public void LoadPieceImages()
    {
        w_pawn_bit = new Bitmap($"Assets/w_pawn.png");
        b_pawn_bit = new Bitmap($"Assets/b_pawn.png");
        w_rook_bit = new Bitmap($"Assets/w_rook.png");
        b_rook_bit = new Bitmap($"Assets/b_rook.png");
        w_knight_bit = new Bitmap($"Assets/w_knight.png");
        b_knight_bit = new Bitmap($"Assets/b_knight.png");
        w_bishop_bit = new Bitmap($"Assets/w_bishop.png");
        b_bishop_bit = new Bitmap($"Assets/b_bishop.png");
        w_queen_bit = new Bitmap($"Assets/w_queen.png");
        b_queen_bit = new Bitmap($"Assets/b_queen.png");
        w_king_bit = new Bitmap($"Assets/w_king.png");
        b_king_bit = new Bitmap($"Assets/b_king.png");
    }

    public void RenderBoard()
    {
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                var piece = board.GetPieceAt(row, col);
                if (piece != null)
                {
                    System.Console.WriteLine($"Piece at row {row + 1}, column {col + 1}: {piece}");
                    switch (piece)
                    {
                        case Pawn p when p.Color == PieceColor.White:
                            board_btns[row * 8 + col].Content = new Image { Source = w_pawn_bit, Stretch = Stretch.Uniform };
                            break;
                        case Pawn p when p.Color == PieceColor.Black:
                            board_btns[row * 8 + col].Content = new Image { Source = b_pawn_bit, Stretch = Stretch.Uniform };
                            break;
                        case Rook r when r.Color == PieceColor.White:
                            board_btns[row * 8 + col].Content = new Image { Source = w_rook_bit, Stretch = Stretch.Uniform };
                            break;
                        case Rook r when r.Color == PieceColor.Black:
                            board_btns[row * 8 + col].Content = new Image { Source = b_rook_bit, Stretch = Stretch.Uniform };
                            break;
                        case Knight k when k.Color == PieceColor.White:
                            board_btns[row * 8 + col].Content = new Image { Source = w_knight_bit, Stretch = Stretch.Uniform };
                            break;
                        case Knight k when k.Color == PieceColor.Black:
                            board_btns[row * 8 + col].Content = new Image { Source = b_knight_bit, Stretch = Stretch.Uniform };
                            break;
                        case Bishop b when b.Color == PieceColor.White:
                            board_btns[row * 8 + col].Content = new Image { Source = w_bishop_bit, Stretch = Stretch.Uniform };
                            break;
                        case Bishop b when b.Color == PieceColor.Black:
                            board_btns[row * 8 + col].Content = new Image { Source = b_bishop_bit, Stretch = Stretch.Uniform };
                            break;
                        case Queen q when q.Color == PieceColor.White:
                            board_btns[row * 8 + col].Content = new Image { Source = w_queen_bit, Stretch = Stretch.Uniform };
                            break;
                        case Queen q when q.Color == PieceColor.Black:
                            board_btns[row * 8 + col].Content = new Image { Source = b_queen_bit, Stretch = Stretch.Uniform };
                            break;
                        case King k when k.Color == PieceColor.White:
                            board_btns[row * 8 + col].Content = new Image { Source = w_king_bit, Stretch = Stretch.Uniform };
                            break;
                        case King k when k.Color == PieceColor.Black:
                            board_btns[row * 8 + col].Content = new Image { Source = b_king_bit, Stretch = Stretch.Uniform };
                            break;
                        default:
                            board_btns[row * 8 + col].Content = piece.ToString();
                            break;
                    }
                }
                else
                {
                    board_btns[row * 8 + col].Content = null;
                }
            }
        }
    }

    public void UpdateBoard()
    {
        RemoveHighlightSqure();
        RenderBoard();
    }

    private void HighlighSquares(List<Move> moves)
    {
        RemoveHighlightSqure();
        for (int i = 0; i < moves.Count; i++)
        {
            int buttonIndex = moves[i].ToRow * 8 + moves[i].ToCol;
            board_btns[buttonIndex].Background = new SolidColorBrush(Color.Parse("#00ff00"));
        }
    }

    private void RemoveHighlightSqure()
    {
        for (int i = 0; i < board_btns.Length; i++)
        {
            board_btns[i].Background = (i / 8 + i % 8) % 2 == 0 ? new SolidColorBrush(Color.Parse("#F0D9B5")) : new SolidColorBrush(Color.Parse("#B58863"));
        }
    }

    public void ClickHandler(object? sender, RoutedEventArgs args)
    {
        if (sender is Button clickedButton && int.TryParse(clickedButton.Name, out int index))
        {
            int row = index / 8;
            int col = index % 8;
            Console.WriteLine($"Button at row {row + 1}, column {col + 1} was clicked.");

            Piece? pieceAtPos = board.GetPieceAt(row, col);
            if (selectedCol == -1 && selectedRow == -1 && (pieceAtPos == null || board.CurrentTurn != pieceAtPos.Color))
            {
                // If no piece is at the clicked square, do nothing
                return;
            }

            if (selectedCol >= 0 && selectedRow >= 0)
            {
                if ((pieceAtPos != null && pieceAtPos.Color != board.CurrentTurn) || pieceAtPos == null)
                {
                    MoveSelectedPiece(row, col);
                    return;
                }
                SelectPiece(row, col);
            }
            else
            {
                SelectPiece(row, col);
            }
        }
    }

    private void SelectPiece(int row, int col)
    {
        selectedRow = row;
        selectedCol = col;
        Console.WriteLine(selectedRow);
        if (selectedCol >= 0 && selectedRow >= 0)
        {
            List<Move> moves = board.PreGenerateMoves(selectedRow, selectedCol);
            HighlighSquares(moves);
        }
        else
        {
            Console.WriteLine("selectedRow und selectedCol sind kleiner als 0. Wähle ein Feld aus!");
        }
        return;
    }

    private void MoveSelectedPiece(int toRow, int toCol)
    {
        if (selectedRow == toRow && selectedCol == toCol)
        {
            return;
        }

        Move move = new Move(selectedRow, selectedCol, toRow, toCol);
        if (board.MovePiece(move))
        {
            UpdateBoard();
        }
        else
        {
            // If the move was not successful, do nothing
            selectedRow = -1;
            selectedCol = -1;
            return;
        }
    }
}