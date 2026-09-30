using System.Drawing;
using Chess.Core.Pieces;
using Chess.Core.Gamefield;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace Chess.Core.Gamefield;

public class Check
{
    public int posKingRow { get; }
    public int posKingCol { get; }
    public PieceColor kingColor { get; }
    public Piece attPiece { get; }

    public Check(int posKingRow, int posKingCol, PieceColor kingColor, Piece attPiece)
    {
        this.posKingRow = posKingRow;
        this.posKingCol = posKingCol;
        this.kingColor = kingColor;
        this.attPiece = attPiece;
    }
}

// ToDo: nun Event einfügen mit subscribern