using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversyEngine
{
    public interface ILineFinder
    {
        Line?[] Search(Field field, Player player, Position point);
    }

    public class LineFinder : ILineFinder
    {
        private Line? CheckDirection(Field field, Player player, Position point, int dx, int dy)
        {
            var nullChip = new Chip(player, "#000");

            int x = point.X + dx;
            int y = point.Y + dy;
            var size = field.Size;
            if (x >= 0 && x < size.X && y >= 0 && y < size.Y &&
               (field[x, y].Chip ?? nullChip).Player != player)
            {
                x += dx; y += dy;
                for (; x >= 0 && x < size.X && y >= 0 && y < size.Y &&
                    field[x, y].Chip is object;)
                {
                    if (field[x, y].Chip.Player == player)
                        return new Line { Start = point, End = new Position(x, y) };
                    x += dx; y += dy;
                }
            }
            return null;
        }

        public Line?[] Search(Field field, Player player, Position point)
        {
            var line = new Line?[8];
            int index = 0;

            (int, int)[] directions = new (int, int)[]
            {
                (1, 0), (-1, 0),
                (0, 1), (0, -1),
                (1, 1), (-1, -1),
                (1, -1), (-1, 1)
            };

            foreach (var dir in directions)
            {
                var lineq = CheckDirection(field, player, point, dir.Item1, dir.Item2);
                if (lineq is object)
                    line[index++] = lineq;
            }

            return index > 0 ? line : null;
        }
    }
}
