namespace Chess.Core.Gamefield;

public enum BoardState
{
    Ongoing,
    WhiteCheckmate,
    BlackCheckmate,
    WhiteStalemate,
    BlackStalemate,
    Draw
}