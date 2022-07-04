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
        public Line?[] Search(Field field, Player player, Position point)
        {
            var line = new Line?[8];
            int index = 0;


            int x = 0;

            if (point.X + 1 < field.Size.X && !(field.Cells[point.X + 1, point.Y].Chip is null) &&
                field.Cells[point.X + 1, point.Y].Chip?.Player != player)
                for (x = point.X + 2; x < field.Size.X && !(field.Cells[x, point.Y].Chip is null) ; x++)
                    if (field.Cells[x, point.Y].Chip.Player == player)
                    {
                        line[index] = new Line { Start = point, End = new Position { X = x, Y = point.Y } };
                        index++;
                        break;
                    }

            if (point.X - 1 > 0 && !(field.Cells[point.X - 1, point.Y].Chip is null) &&
                field.Cells[point.X - 1, point.Y].Chip?.Player != player)
                for (x = point.X - 1; x > 0 && !(field.Cells[x, point.Y].Chip is null) ; x--)
                    if (field.Cells[x, point.Y].Chip.Player == player && x != point.X - 1)
                    {
                        line[index] = new Line { Start = point, End = new Position { X = x, Y = point.Y } };
                        index++;
                        break;
                    }

            int y = 0;

            if (point.Y + 1 < field.Size.Y && !(field.Cells[point.X, point.Y + 1].Chip is null) &&
                field.Cells[point.X, point.Y + 1].Chip?.Player != player)
                for (y = point.Y + 1; y < field.Size.Y && !(field.Cells[point.X, y].Chip is null) ; y++)
                    if (field.Cells[point.X, y].Chip.Player == player && y != point.Y + 1)
                    {
                        line[index] = new Line { Start = point, End = new Position { X = point.X, Y = y } };
                        index++;
                        break;
                    }

            if (point.Y - 1 > 0 && !(field.Cells[point.X, point.Y - 1].Chip is null) &&
                field.Cells[point.X, point.Y - 1].Chip?.Player != player)
                for (y = point.Y - 1; y > 0 && !(field.Cells[point.X, y].Chip is null) ; y--)
                    if (field.Cells[point.X, y].Chip.Player == player && y != point.Y - 1)
                    {
                        line[index] = new Line { Start = point, End = new Position { X = point.X, Y = y } };
                        index++;
                        break;
                    }

            int delta = 0;

            if (point.Y + 1 < field.Size.Y && point.X + 1 < field.Size.X &&
                !(field.Cells[point.X + 1, point.Y + 1].Chip is null) &&
                field.Cells[point.X + 1, point.Y + 1].Chip?.Player != player)
                for (delta = 2; point.Y + delta < field.Size.Y && point.X + delta < field.Size.X &&
                    !(field.Cells[point.X + delta, point.Y + delta].Chip is null) ; delta++)
                    if (field.Cells[point.X + delta, point.Y + delta].Chip.Player == player && delta != 1)
                    {
                        line[index] = new Line { Start = point, End = new Position { X = point.X + delta, Y = point.Y + delta } };
                        index++;
                        break;
                    }

            if (point.Y - 1 > 0 && point.X - 1 > 0 &&
                !(field.Cells[point.X - 1, point.Y - 1].Chip is null) &&
                field.Cells[point.X - 1, point.Y - 1].Chip?.Player != player)

                for (delta = -2; point.Y + delta > 0 && point.X + delta > 0 &&
                !(field.Cells[point.X + delta, point.Y + delta].Chip is null) ; delta--)
                    if (field.Cells[point.X + delta, point.Y + delta].Chip.Player == player && delta != -1)
                    {
                        line[index] = new Line { Start = point, End = new Position { X = point.X + delta, Y = point.Y + delta } };
                        index++;
                        break;
                    }



            if (point.Y - 1 > 0 && point.X + 1 < field.Size.X &&
                !(field.Cells[point.X + 1, point.Y - 1].Chip is null) &&
                field.Cells[point.X + 1, point.Y - 1].Chip?.Player != player)

                for (delta = 2; point.Y - delta > 0 && point.X + delta < field.Size.X &&
                !(field.Cells[point.X + delta, point.Y - delta].Chip is null) ; delta++)
                    if (field.Cells[point.X + delta, point.Y - delta].Chip.Player == player && delta != 1)
                    {
                        line[index] = new Line { Start = point, End = new Position { X = point.X + delta, Y = point.Y - delta } };
                        index++;
                        break;
                    }

            if (point.Y + 1 < field.Size.Y && point.X - 1 > 0 &&
                !(field.Cells[point.X - 1, point.Y + 1].Chip is null) &&
                field.Cells[point.X - 1, point.Y + 1].Chip?.Player != player)

                for (delta = -2; point.Y - delta < field.Size.Y && point.X + delta > 0 &&
                !(field.Cells[point.X + delta, point.Y - delta].Chip is null); delta--)
                    if (field.Cells[point.X + delta, point.Y - delta].Chip.Player == player && delta != -1)
                    {
                        line[index] = new Line { Start = point, End = new Position { X = point.X + delta, Y = point.Y - delta } };
                        index++;
                        break;
                    }

            return index > 0 ? line : null;
        } 
    }

    //Новый LineFinder, пока оставлю старый.
    //public class LineFinder : ILineFinder
    //{
    //    public Line? CheckDirection(Field field, Player player, Position point, int dx, int dy)
    //    {
    //        var nullChip = new Chip(player, null);

    //        int x = point.X + dx;
    //        int y = point.Y + dy;
    //        var size = field.Size;
    //        if (x >= 0 && x < size.X && y >= 0 && y < size.Y &&
    //           (field[x, y].Chip ?? nullChip).Player != player)
    //        {
    //            x += dx; y += dy;
    //            for (; x >= 0 && x < size.X && y >= 0 && y < size.Y &&
    //                field[x, y].Chip is object;)
    //            {
    //                if (field[x, y].Chip.Player == player)
    //                    return new Line { Start = point, End = new Position(x, y) };
    //                x += dx; y += dy;
    //            }
    //        }
    //        return null;
    //    }

    //    public Line?[] Search(Field field, Player player, Position point)
    //    {
    //        var line = new Line?[8];
    //        int index = 0;

    //        (int, int)[] directions = new (int, int)[]
    //        {
    //            (1, 0), (-1, 0),
    //            (0, 1), (0, -1),
    //            (1, 1), (-1, -1),
    //            (1, -1), (-1, 1)
    //        };

    //        foreach (var dir in directions)
    //        {
    //            var lineq = CheckDirection(field, player, point, dir.Item1, dir.Item2);
    //            if (lineq is object)
    //                line[index++] = lineq;
    //        }

    //        return index > 0 ? line : null;
    //    }
    //}
}
