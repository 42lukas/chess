using Chess.Core.Pieces;
using Chess.Core.Gamefield;
using System.Dynamic;

namespace Chess.Core.Rules;

public class MoveValidator
{
    private readonly MoveGenerator _moveGenerator;
    private readonly CheckDetector _checkDetector;
    private readonly MoveExecutor _moveExecutor = new MoveExecutor();

    public MoveValidator(MoveGenerator moveGenerator, CheckDetector checkDetector)
    {
        _moveGenerator = moveGenerator;
        _checkDetector = checkDetector;
    }

    public List<Move> GetLegalMoves(Board board, int row, int col)
    {
        // filter out moves where the King is in check afterwards
        List<Move> moves = new List<Move>();
        Piece? piece = board.GetPieceAt(row, col);
        if (piece == null)
        {
            return moves;
        }

        moves = _moveGenerator.GenerateMoves(board, row, col);

        for (int i = moves.Count - 1; i >= 0; i--)
        {
            var move = moves[i];
            MoveUndoInfo undo = _moveExecutor.MakeMove(board, move);

            if (_checkDetector.IsKingInCheck(board, move))
            {
                moves.RemoveAt(i);
            }

            _moveExecutor.UndoMove(board, undo);
        }


        return moves;
    }

    public bool IsValidMove(Board board, Move move)
    {
        Piece? piece = board.GetPieceAt(move.FromRow, move.FromCol);

        if (piece == null)
        {
            return false;
        }

        if (piece.Color != board.CurrentTurn)
        {
            return false;
        }

        List<Move> legalMoves = GetLegalMoves(board, move.FromRow, move.FromCol);

        return legalMoves.Any(m =>
            m.FromRow == move.FromRow &&
            m.FromCol == move.FromCol &&
            m.ToRow == move.ToRow &&
            m.ToCol == move.ToCol);
    }

    public bool HasAnyLegalMoves(Board board)
    {
        List<(Piece piece, int row, int col)> pieces = board.GetAllPiecesFromColor(board.CurrentTurn);
        foreach ((Piece piece, int row, int col) piece in pieces)
        {
            List<Move> legalMoves = GetLegalMoves(board, piece.row, piece.col);
            if (legalMoves.Count > 0)
            {
                return true;
            }
        }
        return false;
    }
}