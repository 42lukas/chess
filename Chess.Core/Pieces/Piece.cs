namespace Chess.Core.Pieces;

public class Piece
{
    public PieceColor Color { get; }
    public PieceType Type { get; }

    public Piece(PieceColor color, PieceType type)
    {
        this.Color = color;
        this.Type = type;
    }
}