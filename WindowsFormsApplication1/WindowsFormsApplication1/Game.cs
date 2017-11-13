using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
	class Game
	{
		Grid matrix;
		int reminder;
		public int win = 1;
		public bool finished;

		public Game(int rows, int columns, int mines)
		{
			matrix = new Grid(rows, columns, mines);
			matrix.initialize();

			reminder = rows * columns - mines;
			finished = false;
		}

		public List<Tuple<int, int, int>> cellClicked(int row, int column)
		{
			List < Tuple < int, int, int>> ret =  matrix.view(row, column);

			for (int i = 0; i < ret.Count; ++i)
			{
				if (ret[i].Item3 == -1)
				{
					win = 0;
					finished = true;
				}
				else
				{
					if (--reminder == 0)
					{
						win = 100;
						finished = true;
					}
				}
			}

			return ret;
		}
	}
}
