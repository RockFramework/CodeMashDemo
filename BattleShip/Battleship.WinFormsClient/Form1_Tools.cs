using System.Drawing;
using System.Windows.Forms;
using Battleship.Model;

namespace Battleship.WinFormsClient
{
    public partial class Form1
    {
        private const int _rowCount = 10;
        private const int _columnCount = 10;

        private void InitializeCells()
        {
            _buttonCells = new Button[_rowCount, _columnCount];
            battleShipPanel.SuspendLayout();
            SuspendLayout();
            templateCell.Visible = false;

            for (var column = 0; column < _columnCount; column++)
            {
                var cell = new Button();
                cell.Location = new System.Drawing.Point(templateCell.Left + templateCell.Width * (column + 1), templateCell.Height);
                cell.Name = "col";
                cell.Enabled = false;
                cell.Size = new Size(templateCell.Width, templateCell.Height);
                cell.TabIndex = 0;
                cell.Text = (column+1).ToString();
                battleShipPanel.Controls.Add(cell);
            }

            for (var row = 0; row < _rowCount; row++)
            {
                {
                    var cell = new Button();
                    cell.Location = new System.Drawing.Point(templateCell.Left, templateCell.Height + templateCell.Height * (row + 1));
                    cell.Name = "row";
                    cell.Enabled = false;
                    cell.Size = new Size(templateCell.Width, templateCell.Height);
                    cell.TabIndex = 0;
                    cell.Text = ((char)((int)'A' + row)).ToString();
                    battleShipPanel.Controls.Add(cell);
                }

                for (var column = 0; column < _columnCount; column++)
                {
                    var cell = _buttonCells[row, column] = new Button();
                    cell.Location = new System.Drawing.Point(templateCell.Left + templateCell.Width * (column + 1), templateCell.Height + templateCell.Height * (row + 1));
                    cell.Name = "cell";
                    cell.Size = new Size(templateCell.Width, templateCell.Height);
                    cell.TabIndex = 0;
                    cell.Text = "-";
                    cell.Click += cell_Click;

                    battleShipPanel.Controls.Add(cell);
                }
            }

            battleShipPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void BindUIToBoard()
        {
            for (int row = 0; row < _rowCount; row++)
            {
                for (int column = 0; column < _columnCount; column++)
                {
                    var guess = new Cell(row, column);
                    _buttonCells[row, column].Tag = guess;
                }
            }
        }

        private void ClearCells()
        {
            for (int row = 0; row < _rowCount; row++)
            {
                for (int column = 0; column < _columnCount; column++)
                {
                    _buttonCells[row, column].Text = "-";
                    _buttonCells[row, column].BackColor = Color.LightGray;
                }
            }
        }
    }
}
