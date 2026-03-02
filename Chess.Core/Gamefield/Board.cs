using System.IO.Pipelines;
using Chess.Core.Pieces;
using Chess.Core.Rules;

namespace Chess.Core.Gamefield;

public class Board
{
    public Piece?[,] Squares { get; }
    public BoardState State { get; set; } = BoardState.Ongoing;
    public PieceColor CurrentTurn { get; set; } = PieceColor.White;
    private readonly MoveGenerator _moveGenerator = new MoveGenerator();

    public Board()
    {
        this.Squares = new Piece?[8, 8];
        // Initialize the board with pieces in their starting positions
        for (int i = 0; i < 8; i++)
        {
            this.Squares[6, i] = new Pawn(PieceColor.White);
            this.Squares[1, i] = new Pawn(PieceColor.Black);
        }
        this.Squares[7, 7] = new Rook(PieceColor.White);
        this.Squares[7, 0] = new Rook(PieceColor.White);
        this.Squares[0, 7] = new Rook(PieceColor.Black);
        this.Squares[0, 0] = new Rook(PieceColor.Black);
        this.Squares[7, 1] = new Knight(PieceColor.White);
        this.Squares[7, 6] = new Knight(PieceColor.White);
        this.Squares[0, 1] = new Knight(PieceColor.Black);
        this.Squares[0, 6] = new Knight(PieceColor.Black);
        this.Squares[7, 2] = new Bishop(PieceColor.White);
        this.Squares[7, 5] = new Bishop(PieceColor.White);
        this.Squares[0, 2] = new Bishop(PieceColor.Black);
        this.Squares[0, 5] = new Bishop(PieceColor.Black);
        this.Squares[7, 3] = new Queen(PieceColor.White);
        this.Squares[7, 4] = new King(PieceColor.White);
        this.Squares[0, 3] = new Queen(PieceColor.Black);
        this.Squares[0, 4] = new King(PieceColor.Black);
    }

    public bool IsValidMove(Move move)
    {
        Piece? piece = this.GetPieceAt(move.FromRow, move.FromCol);
        if (piece == null)
        {
            // No piece at the source square
            return false;
        }

        if (this.CurrentTurn != piece.Color)
        {
            // It's not the current player's turn
            return false;
        }

        if (piece.Type == PieceType.Pawn)
        {
            List<Move> validMoves = _moveGenerator.GenerateMoves(this, move);
            bool result = validMoves.Any(m => m.FromRow == move.FromRow && m.FromCol == move.FromCol && m.ToRow == move.ToRow && m.ToCol == move.ToCol);
            return result;
        }

        if (piece.Type == PieceType.Rook)
        {
            List<Move> validMoves = _moveGenerator.GenerateMoves(this, move);
            bool result = validMoves.Any(m => m.FromRow == move.FromRow && m.FromCol == move.FromCol && m.ToRow == move.ToRow && m.ToCol == move.ToCol);
            return result;
        }

        if (piece.Type == PieceType.Bishop)
        {
            List<Move> validMoves = _moveGenerator.GenerateMoves(this, move);
            System.Console.WriteLine($"Valid moves for bishop: {string.Join(", ", validMoves.Select(m => $"({m.FromRow}, {m.FromCol}) -> ({m.ToRow}, {m.ToCol})"))}");
            bool result = validMoves.Any(m => m.FromRow == move.FromRow && m.FromCol == move.FromCol && m.ToRow == move.ToRow && m.ToCol == move.ToCol);
            return result;
        }



        // Implement logic to determine if a move from (fromRow, fromCol) to (toRow, toCol) is valid
        return true;
    }

    public void ToggleTurn()
    {
        if (this.CurrentTurn == PieceColor.White)
        {
            this.CurrentTurn = PieceColor.Black;
        }
        else
        {
            this.CurrentTurn = PieceColor.White;
        }
    }

    public bool MovePiece(Move move)
    {
        if (!IsValidMove(move))
        {
            return false;
        }

        Piece? piece = this.GetPieceAt(move.FromRow, move.FromCol);
        if (piece == null)
        {
            return false;
        }

        this.SetPieceAt(move.ToRow, move.ToCol, piece);
        this.RemovePieceAt(move.FromRow, move.FromCol);
        ToggleTurn();
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
