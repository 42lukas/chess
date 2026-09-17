using Chess.Core.Pieces;
using Chess.Core.Gamefield;
using System.Dynamic;

namespace Chess.Core.Rules;

public class MoveExecutor
{

    public MoveExecutor()
    {
    }

    public MoveUndoInfo MakeMove(Board board, Move move)
    {
        var piece = board.GetPieceAt(move.FromRow, move.FromCol);
        var capturedPiece = board.GetPieceAt(move.ToRow, move.ToCol);
        board.SetPieceAt(move.ToRow, move.ToCol, piece);
        board.RemovePieceAt(move.FromRow, move.FromCol);

        return new MoveUndoInfo(move, capturedPiece);
    }

    public void UndoMove(Board board, MoveUndoInfo undo)
    {
        Move move = undo.Move;

        var piece = board.GetPieceAt(move.ToRow, move.ToCol);
        board.SetPieceAt(move.FromRow, move.FromCol, piece);
        board.RemovePieceAt(move.ToRow, move.ToCol);
        if (undo.CapturedPiece != null)
        {
            board.SetPieceAt(move.ToRow, move.ToCol, undo.CapturedPiece);
        }
    }
}
