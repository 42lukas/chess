using Chess.Core.Pieces;
using Chess.Core.Gamefield;
using System.Dynamic;

namespace Chess.Core.Rules;

public class CheckDetector
{
    private readonly MoveGenerator _moveGenerator;

    public CheckDetector(MoveGenerator moveGenerator)
    {
        _moveGenerator = moveGenerator;
    }

    public Check? VerifyCheck(Board board, int fromRow, int fromCol)
    {
        List<Move> possibleMoves = _moveGenerator.GenerateMoves(board, fromRow, fromCol);
        foreach (Move move in possibleMoves)
        {
            var attPiece = board.GetPieceAt(move.FromRow, move.FromCol);
            var piece = board.GetPieceAt(move.ToRow, move.ToCol);
            if (attPiece == null)
            {
                return null;
            }
            if (piece == null)
            {
                continue;
            }

            if (piece.Type == PieceType.King)
            {
                return new Check(move.ToRow, move.ToCol, piece.Color, attPiece);
            }
        }
        return null;
    }

    public bool IsKingInCheck(Board board, Move move)
    {
        PieceColor EnemyColor = board.GetOppositeColor();
        List<(Piece piece, int row, int col)> pieces = board.GetAllPiecesFromColor(EnemyColor);
        foreach ((Piece piece, int row, int col) piece in pieces)
        {
            Check? check = VerifyCheck(board, piece.row, piece.col);
            if (check != null && check.kingColor != EnemyColor)
            {
                return true;
            }
        }
        return false;
    }
}
