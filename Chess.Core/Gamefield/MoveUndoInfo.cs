using Chess.Core.Pieces;

namespace Chess.Core.Gamefield;

public class MoveUndoInfo
{
    public Move Move { get; }
    public Piece? CapturedPiece { get; }

    public MoveUndoInfo(Move move, Piece? capturedPiece)
    {
        Move = move;
        CapturedPiece = capturedPiece;
    }
}