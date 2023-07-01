using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSetup : MonoBehaviour
{
    public const string standardStart = "bbbbb/bbbbbb/2bbb////2www/wwwwww/wwwww";

    [Header("Board Settings")]
    public int size = 9;

    List<Marble> piecesInPlay;
    // Start is called before the first frame update
    void Start()
    {
        Marble[][] gameBoard = GenerateBoard();
        PlacePieces(gameBoard, standardStart);
        PrintBoard(gameBoard);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Marble[][] GenerateBoard()
    {
        Marble[][] gameBoard = new Marble[size][];
        for (int row = 0; row < size; row++)
        {
            int maxCol = size - Mathf.Abs(row - ((size - 1) / 2));
            gameBoard[row] = new Marble[maxCol];
        }
        return gameBoard;
    }

    List<Marble> PlacePieces(Marble[][] gameBoard, string startingPositions)
    {
        List<Marble> pieces = new List<Marble>();
        int row = 0;
        int col = 0;
        foreach (char c in startingPositions.ToCharArray())
        {
            if (c == '/')
            {
                row++;
                col = 0;
                continue;
            }

            //Safety checks to make sure we aren't going over the board
            if (row >= gameBoard.Length)
            {
                throw new System.IndexOutOfRangeException(row + ":" + col + " are not valid for board size");
            }
            if (col >= gameBoard[row].Length)
            {
                throw new System.IndexOutOfRangeException(row + ":" + col + " are not valid for board size");
            }

            int spaces;
            if (int.TryParse(c.ToString(), out spaces))
            {
                col += spaces;
                Debug.Log(spaces);
                continue;
            }
            if (c == 'w' || c == 'b')
            {
                MarbleColor color;

                if (c == 'w')
                {
                    color = MarbleColor.white;
                }
                else
                {
                    color = MarbleColor.black;
                }

                Marble generated = new Marble(color, new Vector2(col, row));
                gameBoard[row][col] = generated;
                pieces.Add(generated);
                col++;
            }
        }
        Debug.Log("pieces length" + pieces.Count);
        return pieces;
    }

    void PrintBoard(Marble[][] board)
    {
        string message = "";
        for (int row = 0; row < board.Length; row++)
        {
            for (int i = 0; i < board.Length - board[row].Length; i++)
            {
                message += ' ';
            }

            for (int col = 0; col < board[row].Length; col++)
            {
                if (board[row][col] == null)
                {
                    message += "o ";
                }
                else if (board[row][col].mColor == MarbleColor.white)
                {
                    message += "w ";
                }
                else if (board[row][col].mColor == MarbleColor.black)
                {
                    message += "b ";
                }
            }
            message += '\n';
        }
        print(message);
    }

    
}
