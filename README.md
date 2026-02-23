# Chess

Ein kleines 2D-Schachprojekt in C# (.NET) mit GUI, entwickelt in VS Code auf macOS.

## Voraussetzungen
- .NET SDK (aktuelle Version)
- VS Code + C# Dev Kit Extension

Prüfen:
```bash
dotnet --version
```

## Projektstruktur
- Chess/ – GUI / App-Projekt
- Chess.Core/ – Schachlogik (Board, Pieces, Moves, Regeln) (falls vorhanden)
- chess.sln – Solution-Datei (falls vorhanden)

## Starten
Im Repo-Root Folder:
```bash
cd Chess
dotnet run
```

## Ziel
- Darstellung eines 8×8 Boards
- Eingabe von Zügen per Klick
- Regelprüfung (legal/illegal)

## Autor
Lukas