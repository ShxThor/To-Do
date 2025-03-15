namespace To_Do.Classes;

public class DeadlineToDo : ToDo
{
    public DeadlineToDo(bool finished,bool important ,string text, DateTime date, TimeOnly time)
        : base(finished,important ,text)
    {
        Date = date;
        Time = time;
    }
    public DateTime Date { get; set; }
    public TimeOnly Time { get; set; }

    public override string ConvertToString(int number)
    {
        Console.ForegroundColor = GetColor();
        string deadline = FormatDeadlineToString();
        string line = $"Nr.{number} | {Text.PadRight(75)}| Due: {deadline.PadRight(17)} |";
        return line;
    }
    public override ConsoleColor GetColor()
    {
        ConsoleColor color;
        if (Finished)
        {
            color = ConsoleColor.DarkGreen;
        }
        else if (Date <= DateTime.Now)
        {
            if (Time > TimeOnly.Parse(DateTime.Now.ToShortTimeString()) && DateTime.Now.ToShortDateString() == Date.ToShortDateString())
            {
                color = ConsoleColor.DarkMagenta;
            }
            else
            {
                color = ConsoleColor.DarkRed;
            }
        }
        else if (Important)
        {
            color = ConsoleColor.DarkYellow;
        }
        else
        {
            color = ConsoleColor.Black;
        }

        return color;
    }
    public string FormatDeadlineToString()
    {
        string day = Date.Day.ToString();
        string month = Date.Month.ToString();
        string year = Date.Year.ToString();
        string time = Time.ToShortTimeString();

        string date = $"{day}.{month}.{year}  {time}";
        return date;
    }
}