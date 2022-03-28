using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaReversy.Game
{
    public interface ILineFinder
    {
        Line?[] Search(Field field, Player player, Position point);
    }

    public class LineFinder : ILineFinder
    {
        public Line?[] Search(Field field, Player player, Position point)
        {
            var line = new Line?[8];
            int index = 0;


            int x = 0;

            if (point.X + 1 < field.Size.X && field.Cells[point.X + 1, point.Y].Chip is not null && 
                field.Cells[point.X + 1, point.Y].Chip?.Player != player)
                for (x = point.X + 2; x < field.Size.X && field.Cells[x, point.Y].Chip is not null; x++)
                    if (field.Cells[x, point.Y].Chip.Player == player)
                    {
                        line[index] = new Line { Point1 = point, Point2 = new Position { X = x, Y = point.Y } };
                        index++;
                        break;
                    }

            if (point.X - 1 > 0 && field.Cells[point.X - 1, point.Y].Chip is not null && 
                field.Cells[point.X - 1, point.Y].Chip?.Player != player)
                for (x = point.X - 1; x > 0 && field.Cells[x, point.Y].Chip is not null; x--)
                    if (field.Cells[x, point.Y].Chip.Player == player && x != point.X - 1)
                    {
                        line[index] = new Line { Point1 = point, Point2 = new Position { X = x, Y = point.Y } };
                        index++;
                        break;
                    }

            int y = 0;

            if (point.Y + 1 < field.Size.Y && field.Cells[point.X, point.Y + 1].Chip is not null && 
                field.Cells[point.X, point.Y + 1].Chip?.Player != player)
                for (y = point.Y + 1; y < field.Size.Y && field.Cells[point.X, y].Chip is not null; y++)
                    if (field.Cells[point.X, y].Chip.Player == player && y != point.Y + 1)
                    {
                        line[index] = new Line { Point1 = point, Point2 = new Position { X = point.X, Y = y } };
                        index++;
                        break;
                    }

            if (point.Y - 1 > 0 && field.Cells[point.X, point.Y - 1].Chip is not null &&
                field.Cells[point.X, point.Y - 1].Chip?.Player != player)
                for (y = point.Y - 1; y > 0 && field.Cells[point.X, y].Chip is not null; y--)
                    if (field.Cells[point.X, y].Chip.Player == player && y != point.Y - 1)
                    {
                        line[index] = new Line { Point1 = point, Point2 = new Position { X = point.X, Y = y } };
                        index++;
                        break;
                    }

            int delta = 0;

            if (point.Y + 1 < field.Size.Y && point.X + 1 < field.Size.X &&
                field.Cells[point.X + 1, point.Y + 1].Chip is not null &&
                field.Cells[point.X + 1, point.Y + 1].Chip?.Player != player)
                for (delta = 2; point.Y + delta < field.Size.Y && point.X + delta < field.Size.X &&
                    field.Cells[point.X + delta, point.Y + delta].Chip is not null; delta++)
                    if (field.Cells[point.X + delta, point.Y + delta].Chip.Player == player && delta != 1)
                    {
                        line[index] = new Line { Point1 = point, Point2 = new Position { X = point.X + delta, Y = point.Y + delta } };
                        index++;
                        break;
                    }

            if (point.Y - 1 > 0 && point.X - 1 > 0 &&
                field.Cells[point.X - 1, point.Y - 1].Chip is not null &&
                field.Cells[point.X - 1, point.Y - 1].Chip?.Player != player)

                for (delta = -2; point.Y + delta > 0 && point.X + delta > 0 &&
                field.Cells[point.X + delta, point.Y + delta].Chip is not null; delta--)
                    if (field.Cells[point.X + delta, point.Y + delta].Chip.Player == player && delta != -1)
                    {
                        line[index] = new Line { Point1 = point, Point2 = new Position { X = point.X + delta, Y = point.Y + delta } };
                        index++;
                        break;
                    }



            if (point.Y - 1 > 0 && point.X + 1 < field.Size.X &&
                field.Cells[point.X + 1, point.Y - 1].Chip is not null &&
                field.Cells[point.X + 1, point.Y - 1].Chip?.Player != player)

                for (delta = 2; point.Y - delta > 0 && point.X + delta < field.Size.X &&
                field.Cells[point.X + delta, point.Y - delta].Chip is not null; delta++)
                    if (field.Cells[point.X + delta, point.Y - delta].Chip.Player == player && delta != 1)
                    {
                        line[index] = new Line { Point1 = point, Point2 = new Position { X = point.X + delta, Y = point.Y - delta } };
                        index++;
                        break;
                    }

            if (point.Y + 1 < field.Size.Y && point.X - 1 > 0 && 
                field.Cells[point.X - 1, point.Y + 1].Chip is not null &&
                field.Cells[point.X - 1, point.Y + 1].Chip?.Player != player)

                for (delta = -2; point.Y - delta < field.Size.Y && point.X + delta > 0 &&
                field.Cells[point.X + delta, point.Y - delta].Chip is not null; delta--)
                    if (field.Cells[point.X + delta, point.Y - delta].Chip.Player == player && delta != -1)
                    {
                        line[index] = new Line { Point1 = point, Point2 = new Position { X = point.X + delta, Y = point.Y - delta } };
                        index++;
                        break;
                    }

            return index > 0 ? line : null;
        }
    }
}
