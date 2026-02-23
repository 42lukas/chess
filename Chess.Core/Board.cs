using Chess.Core.Pieces;
using Chess;

namespace Chess.Core;

public class Board
{
    public Piece?[,] Squares { get; }

    public Board()
    {
        this.Squares = new Piece?[8, 8];
        // Initialize the board with pieces in their starting positions
        for (int i = 0; i < 8; i++)
        {
            this.Squares[1, i] = new Pawn(PieceColor.White);
            this.Squares[6, i] = new Pawn(PieceColor.Black);
        }
        this.Squares[0, 0] = new Rook(PieceColor.White);
        this.Squares[0, 7] = new Rook(PieceColor.White);
        this.Squares[7, 0] = new Rook(PieceColor.Black);
        this.Squares[7, 7] = new Rook(PieceColor.Black);
        this.Squares[0, 1] = new Knight(PieceColor.White);
        this.Squares[0, 6] = new Knight(PieceColor.White);
        this.Squares[7, 1] = new Knight(PieceColor.Black);
        this.Squares[7, 6] = new Knight(PieceColor.Black);
        this.Squares[0, 2] = new Bishop(PieceColor.White);
        this.Squares[0, 5] = new Bishop(PieceColor.White);
        this.Squares[7, 2] = new Bishop(PieceColor.Black);
        this.Squares[7, 5] = new Bishop(PieceColor.Black);
        this.Squares[0, 3] = new Queen(PieceColor.White);
        this.Squares[0, 4] = new King(PieceColor.White);
        this.Squares[7, 3] = new Queen(PieceColor.Black);
        this.Squares[7, 4] = new King(PieceColor.Black);
    }

    public bool IsValidMove(int fromRow, int fromCol, int toRow, int toCol)
    {
        // Implement logic to determine if a move from (fromRow, fromCol) to (toRow, toCol) is valid
        return true;
    }

    public bool IsCheck(PieceColor color)
    {
        // Implement logic to determine if the king of the specified color is in check
        return false;
    }

    public bool IsCheckmate(PieceColor color)
    {
        // Implement logic to determine if the king of the specified color is in checkmate
        return false;
    }

    public void SetPieceAt(int row, int col, Piece? piece)
    {
        this.Squares[row, col] = piece;
        System.Console.WriteLine($"Set piece at row {row}, column {col} to {piece?.ToString() ?? "null"}");
    }

    public Piece? GetPieceAt(int row, int col)
    {
        return this.Squares[row, col];
    }

    public void RemovePieceAt(int row, int col)
    {
        this.Squares[row, col] = null;
    }
}
