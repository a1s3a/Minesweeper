using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
	public partial class Form1 : Form
	{
		Stopwatch time;
		Game g;
		int[,] toView;
		int rows, columns;

		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			int mines;

			try
			{
				rows = int.Parse(textBox1.Text.ToString());
				columns = int.Parse(textBox2.Text.ToString());
				mines = int.Parse(textBox3.Text.ToString());
				if (mines >= rows * columns)
				{
					MessageBox.Show("Too many mines");
					return;
				}
				toView = new int[rows, columns];
				for (int i = 0; i < rows; ++i)
				{
					for (int j = 0; j < columns; ++j)
					{
						toView[i, j] = 100;
					}
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
				return;
			}

			listBox1.Items.Clear();

			g = new Game(rows, columns, mines);
			time = Stopwatch.StartNew();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			//FileStream fs = new FileStream("Data Set.txt", FileMode)
			//listBox1.Items.Clear();

		}

		private void button2_Click(object sender, EventArgs e)
		{
			int row = 0;
			int column = 0;

			try
			{
				row = int.Parse(textBox6.Text.ToString()) - 1;
				column = int.Parse(textBox7.Text.ToString()) - 1;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}

			List<Tuple<int, int, int>> update = g.cellClicked(row, column);

			if (g.finished)
			{
				time.Stop();
			}

			for (int i = 0; i < update.Count; ++i)
			{
				toView[update[i].Item1, update[i].Item2] = update[i].Item3;
			}

			listBox1.Items.Clear();

			for (int i = 0; i < rows; ++i)
			{
				string str = "";
				for (int j = 0; j < columns; ++j)
				{
					if (toView[i, j] == 100) str += '_';
					else if (toView[i, j] == -1) str += 'X';
					else str += toView[i, j].ToString();
				}
				listBox1.Items.Add(str);
			}

			if (g.finished)
			{
				int totalTime = (int)time.ElapsedMilliseconds / 1000;
				textBox4.Text = Math.Max((24 * 60 * 60 - totalTime) * g.win, 0).ToString();
				textBox5.Text = (totalTime).ToString();
			}
		}
	}
}
