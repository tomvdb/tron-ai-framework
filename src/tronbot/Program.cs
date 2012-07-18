using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace tronbot
{
  class Program
  {

    enum boardState
    {
      Clear = 0,
      You = 1,
      YourWall = 2,
      Opponent = 3,
      OpponentWall = 4
    }

    // ugh ... global variables
    static byte[] You = new byte[2];
    static byte[] Opponent = new byte[2];


    // *********************************************************************************************************************
    // draw current board
    static void drawBoard(boardState[,] gameBoard)
    {
      Console.WriteLine("1 = You");
      Console.WriteLine("2 = Opponent");
      Console.WriteLine("Y = YourWall");
      Console.WriteLine("O = OpponentWall");
      Console.WriteLine("");

      Console.WriteLine("################################");
      for (int y = 0; y < 30; y++)
      {
        string row = "#";

        for (int x = 0; x < 30; x++)
        {
          switch (gameBoard[x, y])
          {
            case boardState.Clear: row += " "; break;
            case boardState.You: row += "1"; break;
            case boardState.Opponent: row += "2"; break;
            case boardState.YourWall: row += "Y"; break;
            case boardState.OpponentWall: row += "O"; break;
          }
        }
        Console.WriteLine(row + "#");
      }

      Console.WriteLine("################################");
    }
    // *********************************************************************************************************************
    static boardState[,] loadGameBoard(string gameStateFile)
    {
      boardState[,] gameBoard = new boardState[30, 30];

      TextReader gameStateInput = new StreamReader(gameStateFile);

      while (gameStateInput.Peek() > 0)
      {
        // read data
        string data = gameStateInput.ReadLine();

        // split data
        string[] values = data.Split(' ');

        byte x = byte.Parse(values[0]);
        byte y = byte.Parse(values[1]);

        switch (values[2].ToUpper())
        {
          case "CLEAR": gameBoard[x, y] = boardState.Clear; break;
          case "YOU":
            You[0] = x;
            You[1] = y;
            gameBoard[x, y] = boardState.You; 
            break;
          case "OPPONENT":
            Opponent[0] = x;
            Opponent[1] = y;
            gameBoard[x, y] = boardState.Opponent; 
            break;
          case "YOURWALL": gameBoard[x, y] = boardState.YourWall; break;
          case "OPPONENTWALL": gameBoard[x, y] = boardState.OpponentWall; break;
        }
      }

      gameStateInput.Close();

      return gameBoard;
    }
    // *********************************************************************************************************************
    static void saveGameBoard(boardState[,] gameBoard, string gameStateFile)
    {
      // write game.state
      TextWriter gameStateOutput = new StreamWriter(gameStateFile);

      for (int x = 0; x < 30; x++)
        for (int y = 0; y < 30; y++)
        {
          boardState state = gameBoard[x, y];
          string dt = x.ToString() + " " + y.ToString() + " " + state.ToString();
          gameStateOutput.WriteLine(dt);
        }

      gameStateOutput.Close();
    }
    // *********************************************************************************************************************

    static void Main(string[] args)
    {

      if (args.Count() != 1)
      {
        Console.WriteLine("No Game State file specified");
        return;
      }

      string gameStateFile = args[0];

      if ( !File.Exists( gameStateFile ) )
      {
        Console.WriteLine("Specified Game State file doesn't exist" );
        return;
      }

      // load game.state
      boardState[,] gameBoard = loadGameBoard(gameStateFile);

      // uncomment the following lines if you want to see the board (before)
      // Console.WriteLine("********* Input: *************");
      // drawBoard(gameBoard);

      // ************************* ai actions *****************************************************************************

      // do you AI processing here and move 

      // run fullsteam ahead - don't really do this!!

      // clear current pos (not sure if this is required ?)
      gameBoard[You[0], You[1]] = boardState.Clear;

      if (You[1] < 29)
        You[1]++;
      else
        You[1] = 0;

      gameBoard[You[0], You[1]] = boardState.You;

      // ******************************************************************************************************************

      // uncomment the following lines if you want to see the board (after)
      // Console.WriteLine("");
      // Console.WriteLine("********* Output: *************");
      // drawBoard(gameBoard);
      
      saveGameBoard(gameBoard, gameStateFile);
      Console.WriteLine("Done");
    }
  }
}
