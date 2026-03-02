using Chess.Core.Pieces;
using Chess.Core.Gamefield;

namespace Chess.Core.Rules;

public class MoveGenerator
{
    public List<Move> GenerateMoves(Board board, Move move)
    {
        var piece = board.GetPieceAt(move.FromRow, move.FromCol);
        if (piece == null)
        {
            return new List<Move>();
        }

        switch (piece.Type)
        {
            case PieceType.Pawn:
                return GeneratePawnMoves(board, move.FromRow, move.FromCol);
            case PieceType.Rook:
                return GenerateRookMoves(board, move.FromRow, move.FromCol);
            case PieceType.Knight:
                return GenerateKnightMoves(board, move.FromRow, move.FromCol);
            case PieceType.Bishop:
                return GenerateBishopMoves(board, move.FromRow, move.FromCol);
            case PieceType.Queen:
                return GenerateQueenMoves(board, move.FromRow, move.FromCol);
            case PieceType.King:
                return GenerateKingMoves(board, move.FromRow, move.FromCol);
            default:
                throw new InvalidOperationException("Unknown piece type");
        }
    }

    private List<Move> GeneratePawnMoves(Board board, int row, int col)
    {
        List<Move> moves = new List<Move>();
        Piece? piece = board.GetPieceAt(row, col);
        if (piece == null || piece.Type != PieceType.Pawn)
        {
            return moves;
        }

        if (row == 6 && piece.Color == PieceColor.White)
        {
            // Initial double move for white pawn
            if (board.GetPieceAt(row - 1, col) == null)
            {
                moves.Add(new Move(row, col, row - 1, col));
                if (board.GetPieceAt(row - 2, col) == null)
                {
                    moves.Add(new Move(row, col, row - 2, col));
                }
            }
        }
        else if (row == 1 && piece.Color == PieceColor.Black)
        {
            // Initial double move for black pawn
            if (board.GetPieceAt(row + 1, col) == null)
            {
                moves.Add(new Move(row, col, row + 1, col));
                if (board.GetPieceAt(row + 2, col) == null)
                {
                    moves.Add(new Move(row, col, row + 2, col));
                }
            }
        }
        else
        {
            // Normal move for pawns
            int direction = piece.Color == PieceColor.White ? -1 : 1;
            if (board.GetPieceAt(row + direction, col) == null)
            {
                moves.Add(new Move(row, col, row + direction, col));
            }
        }

        // attack moves for pawns
        int attackDirection = piece.Color == PieceColor.White ? -1 : 1;
        if (col > 0 && board.GetPieceAt(row + attackDirection, col - 1) != null && board.GetPieceAt(row + attackDirection, col - 1)?.Color != piece.Color)
        {
            moves.Add(new Move(row, col, row + attackDirection, col - 1));
        }
        if (col < 7 && board.GetPieceAt(row + attackDirection, col + 1) != null && board.GetPieceAt(row + attackDirection, col + 1)?.Color != piece.Color)
        {
            moves.Add(new Move(row, col, row + attackDirection, col + 1));
        }

        return moves;
    }

    private List<Move> GenerateRookMoves(Board board, int row, int col)
    {
        List<Move> moves = new List<Move>();
        Piece? piece = board.GetPieceAt(row, col);
        if (piece == null || piece.Type != PieceType.Rook)
        {
            return moves;
        }

        // Generate moves in all 4 directions (up, down, left, right)
        int[] directions = { -1, 0, 1, 0 }; // up, right, down, left
        for (int d = 0; d < 4; d++)
        {
            int newRow = row + directions[d];
            int newCol = col + directions[(d + 1) % 4];
            while (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
            {
                Piece? targetPiece = board.GetPieceAt(newRow, newCol);
                if (targetPiece == null)
                {
                    moves.Add(new Move(row, col, newRow, newCol));
                }
                else
                {
                    if (targetPiece.Color != piece.Color)
                    {
                        moves.Add(new Move(row, col, newRow, newCol));
                    }
                    break;
                }
                newRow += directions[d];
                newCol += directions[(d + 1) % 4];
            }
        }

        return moves;
    }

    private List<Move> GenerateKnightMoves(Board board, int row, int col)
    {
        // Implement knight move generation logic
        return new List<Move>();
    }

    private List<Move> GenerateBishopMoves(Board board, int row, int col)
    {
        List<Move> moves = new List<Move>();
        Piece? piece = board.GetPieceAt(row, col);
        if (piece == null || piece.Type != PieceType.Bishop)
        {
            return moves;
        }

        // Generate moves in all 4 diagonal directions (left-up, left-down, right-up, right-down)
        (int, int)[] directions = { (-1, -1), (1, -1), (-1, 1), (1, 1) }; // (d-row, d-col), ...
        for (int d = 0; d < 4; d++)
        {
            int newRow = row + directions[d].Item1;
            int newCol = col + directions[(d + 1) % 4].Item2;
            while (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
            {
                Piece? targetPiece = board.GetPieceAt(newRow, newCol);
                if (targetPiece == null)
                {
                    moves.Add(new Move(row, col, newRow, newCol));
                }
                else
                {
                    if (targetPiece.Color != piece.Color)
                    {
                        moves.Add(new Move(row, col, newRow, newCol));
                    }
                    break;
                }
                newRow += directions[d].Item1;
                newCol += directions[(d + 1) % 4].Item2;
            }
        }

        return moves;
    }

    private List<Move> GenerateQueenMoves(Board board, int row, int col)
    {
        // Implement queen move generation logic
        return new List<Move>();
    }

    private List<Move> GenerateKingMoves(Board board, int row, int col)
    {
        // Implement king move generation logic
        return new List<Move>();
    }
}