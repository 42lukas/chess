namespace Chess.Core.Gamefield;

public class Move
{
    public int FromRow { get; }
    public int FromCol { get; }
    public int ToRow { get; }
    public int ToCol { get; }

    public Move(int fromRow, int fromCol, int toRow, int toCol)
    {
        this.FromRow = fromRow;
        this.FromCol = fromCol;
        this.ToRow = toRow;
        this.ToCol = toCol;
    }
}