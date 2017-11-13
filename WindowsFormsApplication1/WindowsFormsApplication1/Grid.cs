using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
	class Grid
	{
		static Random myRand = new Random();
		int[,] matrix;
		bool[,] viewd;
		List<Tuple<int, int, int>> toView;
		int rows;
		int columns;
		int mines;

		public Grid(int rows, int columns, int mines)
		{
			this.rows = rows;
			this.columns = columns;
			this.mines = mines;
			this.matrix = new int[rows, columns];
			this.viewd = new bool[rows, columns];
			this.toView = new List<Tuple<int, int, int>>();
		}

		public void initialize()
		{
			HashSet<Tuple<int, int>> positions = new HashSet<Tuple<int, int>>();
			for (int i = 0; i < mines; ++i)
			{
				int x = myRand.Next() % rows;
				int y = myRand.Next() % columns;

				if (positions.Contains(new Tuple<int, int>(x, y)) == true)
				{
					--i;
					continue;
				}

				positions.Add(new Tuple<int, int>(x, y));
				matrix[x, y] = ~0;
			}

			for (int i = 0; i < rows; ++i)
			{
				for (int j = 0; j < columns; ++j)
				{
					if (matrix[i, j] == ~0) continue;

					for (int dx = ~0; dx <= 1; ++dx)
					{
						for (int dy = ~0; dy <= 1; ++dy)
						{
							if (dx == 0 && dy == 0) continue;

							int x = i + dx;
							int y = j + dy;

							if (x == ~0 || x == rows) continue;
							if (y == ~0 || y == columns) continue;

							if (matrix[x, y] == ~0) ++matrix[i, j];
						}
					}
				}
			}
		}

		void dfs(int row, int column)
		{
			for (int dx = ~0; dx <= 1; ++dx)
			{
				for (int dy = ~0; dy <= 1; ++dy)
				{
					if (dx == 0 && dy == 0) continue;

					int x = row + dx;
					int y = column + dy;

					if (x == ~0 || x == rows) continue;
					if (y == ~0 || y == columns) continue;
					if (matrix[x, y] != 0) continue;
					if (viewd[x, y]) continue;

					viewd[x, y] = true;
					toView.Add(new Tuple<int, int, int>(x, y, 0));
					dfs(x, y);
				}
			}
		}

		public List<Tuple<int, int, int>> view (int row, int column)
		{
			toView.Clear();
			if (viewd[row, column]) return toView;

			viewd[row, column] = true;
			toView.Add(new Tuple<int, int, int>(row, column, matrix[row, column]));
			if (matrix[row, column] != 0) return toView;

			dfs(row, column);

			return toView;
		}
	}
}
