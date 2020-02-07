using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class Form1 : Form
    {
        private readonly TextBox[] mTextBoxes = new TextBox[81];
 
        public Form1()
        {
            InitializeComponent();

            #region TextBoxes everywhere
            mTextBoxes[0] = textBox1;
            mTextBoxes[1] = textBox2;
            mTextBoxes[2] = textBox3;
            mTextBoxes[3] = textBox4;
            mTextBoxes[4] = textBox5;
            mTextBoxes[5] = textBox6;
            mTextBoxes[6] = textBox7;
            mTextBoxes[7] = textBox8;
            mTextBoxes[8] = textBox9;
            mTextBoxes[9] = textBox10;
            mTextBoxes[10] = textBox11;
            mTextBoxes[11] = textBox12;
            mTextBoxes[12] = textBox13;
            mTextBoxes[13] = textBox14;
            mTextBoxes[14] = textBox15;
            mTextBoxes[15] = textBox16;
            mTextBoxes[16] = textBox17;
            mTextBoxes[17] = textBox18;
            mTextBoxes[18] = textBox19;
            mTextBoxes[19] = textBox20;
            mTextBoxes[20] = textBox21;
            mTextBoxes[21] = textBox22;
            mTextBoxes[22] = textBox23;
            mTextBoxes[23] = textBox24;
            mTextBoxes[24] = textBox25;
            mTextBoxes[25] = textBox26;
            mTextBoxes[26] = textBox27;
            mTextBoxes[27] = textBox28;
            mTextBoxes[28] = textBox29;
            mTextBoxes[29] = textBox30;
            mTextBoxes[30] = textBox31;
            mTextBoxes[31] = textBox32;
            mTextBoxes[32] = textBox33;
            mTextBoxes[33] = textBox34;
            mTextBoxes[34] = textBox35;
            mTextBoxes[35] = textBox36;
            mTextBoxes[36] = textBox37;
            mTextBoxes[37] = textBox38;
            mTextBoxes[38] = textBox39;
            mTextBoxes[39] = textBox40;
            mTextBoxes[40] = textBox41;
            mTextBoxes[41] = textBox42;
            mTextBoxes[42] = textBox43;
            mTextBoxes[43] = textBox44;
            mTextBoxes[44] = textBox45;
            mTextBoxes[45] = textBox46;
            mTextBoxes[46] = textBox47;
            mTextBoxes[47] = textBox48;
            mTextBoxes[48] = textBox49;
            mTextBoxes[49] = textBox50;
            mTextBoxes[50] = textBox51;
            mTextBoxes[51] = textBox52;
            mTextBoxes[52] = textBox53;
            mTextBoxes[53] = textBox54;
            mTextBoxes[54] = textBox55;
            mTextBoxes[55] = textBox56;
            mTextBoxes[56] = textBox57;
            mTextBoxes[57] = textBox58;
            mTextBoxes[58] = textBox59;
            mTextBoxes[59] = textBox60;
            mTextBoxes[60] = textBox61;
            mTextBoxes[61] = textBox62;
            mTextBoxes[62] = textBox63;
            mTextBoxes[63] = textBox64;
            mTextBoxes[64] = textBox65;
            mTextBoxes[65] = textBox66;
            mTextBoxes[66] = textBox67;
            mTextBoxes[67] = textBox68;
            mTextBoxes[68] = textBox69;
            mTextBoxes[69] = textBox70;
            mTextBoxes[70] = textBox71;
            mTextBoxes[71] = textBox72;
            mTextBoxes[72] = textBox73;
            mTextBoxes[73] = textBox74;
            mTextBoxes[74] = textBox75;
            mTextBoxes[75] = textBox76;
            mTextBoxes[76] = textBox77;
            mTextBoxes[77] = textBox78;
            mTextBoxes[78] = textBox79;
            mTextBoxes[79] = textBox80;
            mTextBoxes[80] = textBox81;
            #endregion
        }

        private void Form1_Load(object sender, EventArgs e)
        {
#if DEBUG
            AllocConsole();
#endif
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void button1_Click(object sender, EventArgs e)
        {
            DoSolve();
        }

        private class TextBoxCellWatcher : Game.Solver.IGameWatcher
        {
            private readonly TextBox[] mTextBoxes;

            public TextBoxCellWatcher(TextBox[] textBoxes)
            {
                mTextBoxes = textBoxes;
            }

            public void HandleMove(Game.Solver.Move move)
            {
                int idx = Game.Game.GetPositionIndex(move.AffectedCell.Position);
                mTextBoxes[idx].Text = move.Value.ToString();
                //MessageBox.Show("Check me!");
                Application.DoEvents();
            }
        }

        private void DoSolve()
        {
            Game.Game game = new Game.Game();
            SetupGame(game);
            Solve(game);
        }

        private void SetupGame(Game.Game game)
        {
            for (int i = 0; i < mTextBoxes.Length; ++i)
            {
                if (mTextBoxes[i].Text.Length > 0)
                {
                    game.SetFixedValue(Game.Game.GetIndexPosition(i), int.Parse(mTextBoxes[i].Text));
                }
            }
        }

        private void Solve(Game.Game game)
        {
            var solver = new Game.Solver.SolverBase(game);
            solver.AddGameWatcher(new TextBoxCellWatcher(mTextBoxes));
            bool success = solver.Run();
            string text;
            if (success)
            {
                text = "Game solved successfully";
            }
            else
            {
                text = "Aww";
            }
            MessageBox.Show(text);
        }
    }
}
