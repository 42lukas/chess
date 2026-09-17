using System.Drawing;
using System.IO.Pipelines;
using System.Runtime.ConstrainedExecution;
using Chess.Core.Pieces;
using Chess.Core.Rules;

namespace Chess.Core.Gamefield;

public class Board
{
    public Piece?[,] Squares { get; }
    public BoardState State { get; set; }
    public PieceColor CurrentTurn { get; set; } = PieceColor.White;

    private readonly MoveGenerator _moveGenerator;
    private readonly CheckDetector _checkDetector;
    private readonly MoveValidator _moveValidator;

    public Board(MoveGenerator moveGenerator, CheckDetector checkDetector, MoveValidator moveValidator)
    {
        _moveGenerator = moveGenerator;
        _checkDetector = checkDetector;
        _moveValidator = moveValidator;


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

        State = BoardState.Ongoing;
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
        if (!_moveValidator.IsValidMove(this, move))
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


        if (IsCheck(CurrentTurn))
        {
            if (IsCheckmate(CurrentTurn))
            {
                State = BoardState.Checkmate;
            }
            else
            {
                State = BoardState.Ongoing;
            }
        }
        else
        {
            if (IsStalemate(CurrentTurn))
            {
                State = BoardState.Stalemate;
            }
            else if (IsDraw(CurrentTurn))
            {
                State = BoardState.Draw;
            }
            else
            {
                State = BoardState.Ongoing;
            }
        }
        ToggleTurn();
        return true;
    }

    public bool IsCheck(PieceColor color)
    {
        List<(Piece piece, int row, int col)> pieces = GetAllPiecesFromColor(color);
        foreach ((Piece piece, int row, int col) piece in pieces)
        {
            Check? check = _checkDetector.VerifyCheck(this, piece.row, piece.col);
            if (check != null)
            {
                System.Console.WriteLine($"CHECKKKKKKKKKK {check.kingColor}");
                return true;
            }
        }
        return false;
    }

    public bool IsCheckmate(PieceColor color)
    {
        // Implement logic to determine if the king of the specified color is in checkmate
        return false;
    }

    public bool IsStalemate(PieceColor color)
    {
        // Implement logic to determine if the king of the specified color is in stalemate
        return false;
    }

    public bool IsDraw(PieceColor color)
    {
        // Implement logic to determine if there is a draw
        return false;
    }

    public List<(Piece piece, int row, int col)> GetAllPieces()
    {
        List<(Piece piece, int row, int col)> pieces = new List<(Piece piece, int row, int col)>();
        // row
        for (int r = 0; r < 8; r++)
        {
            // col 
            for (int c = 0; c < 8; c++)
            {
                Piece? piece = GetPieceAt(r, c);
                if (piece != null)
                {
                    pieces.Add((piece, r, c));
                }
            }
        }
        return pieces;
    }

    public List<(Piece piece, int row, int col)> GetAllPiecesFromColor(PieceColor color)
    {
        List<(Piece piece, int row, int col)> pieces = new List<(Piece piece, int row, int col)>();
        // row
        for (int r = 0; r < 8; r++)
        {
            // col 
            for (int c = 0; c < 8; c++)
            {
                Piece? piece = GetPieceAt(r, c);
                if (piece != null && piece.Color == color)
                {
                    pieces.Add((piece, r, c));
                }
            }
        }
        return pieces;
    }

    public void SetPieceAt(int row, int col, Piece? piece)
    {
        this.Squares[row, col] = piece;
        // System.Console.WriteLine($"Set piece at row {row}, column {col} to {piece?.ToString() ?? "null"}");
    }

    public Piece? GetPieceAt(int row, int col)
    {
        return this.Squares[row, col];
    }

    public void RemovePieceAt(int row, int col)
    {
        this.Squares[row, col] = null;
    }

    public PieceColor GetOppositeColor()
    {
        if (CurrentTurn == PieceColor.White)
        {
            return PieceColor.Black;
        }
        else
        {
            return PieceColor.White;
        }
    }
}
