using System;
using System.Drawing;
using System.Windows.Forms;
using Battleship.Model;

namespace Battleship.WinFormsClient
{
    public partial class Form1 : Form
    {
        private Button[,] _buttonCells;
        private string _gameState;
        private int _currentGameId;

        public Form1()
        {
            InitializeComponent();
            InitializeCells(); // Create user interface buttons.
            BindUIToBoard();
            ClearCells();
            battleShipPanel.Enabled = false;
        }

        private void new_Click(object sender, EventArgs e)
        {
            _currentGameId = CreateNewGame();
            _gameState = null;
            ClearCells();
            battleShipPanel.Enabled = true;
        }

        private void cell_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var cell = (Cell)button.Tag;
            
            battleShipPanel.Enabled = false;
            
            var guessResult = GetGuessResponse(cell, _currentGameId, _gameState);  // Sink, mis, ....
            
            battleShipPanel.Enabled = true;

            // Update the board object with the cell state
            UpdateButton(button, guessResult.Hit);
            _gameState = guessResult.GameState;

            if (guessResult.SunkShip.HasValue)
            {
                MessageBox.Show(string.Format("You sunk my {0}!", guessResult.SunkShip));
            }
            else
            {
                label2.Text = guessResult.Hit ? "Hit!" : "Miss!";
                timer1.Enabled = true;
            }

            if (guessResult.GameOver)
            {
                battleShipPanel.Enabled = false;
                MessageBox.Show("Game Over!");
            }
        }

        private void UpdateButton(Button button, bool hit)
        {
            // Our goal here is update the botton to reflect the cellState.

            button.Text = hit ? "X" : "O";
            button.BackColor = hit ? Color.Red : Color.LightSkyBlue;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            label2.Text = "";
        }






        // client, webapi, windows service solutions, and a project for each
        // Model project...

        //Add New Game
        //  not clickable until start
        // only click when can
        // simpler clicker:  (input, Output)

        // SQL lite x 2

        // Regular excercise...
        //  for logging, web api, etc.

        /*
         * Display that hit, vs sunk, another color
         * 
         * 
         */

    }
}
