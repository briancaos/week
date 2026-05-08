using System.Globalization;

namespace API.Shared
{
  public sealed record Week : IComparable<Week>, IEquatable<Week>
  {
    private const DayOfWeek FirstDayOfWeek = DayOfWeek.Monday;
    private const DayOfWeek LastDayOfWeek = DayOfWeek.Sunday;
    private const DayOfWeek FirstDayOfYear = DayOfWeek.Thursday;
    private static readonly Calendar Calendar = CultureInfo.InvariantCulture.Calendar;

    private Week() { }

    public Week(int year, int week)
    {
      DateTime date = ISOWeek.ToDateTime(year, week, DayOfWeek.Monday);
      FirstDateOfWeek = GetFirstDateOfWeek(date);
      LastDateOfWeek = GetLastDateOfWeek(date);
      DateTime offsetDate = GetYearOffsetDayOfWeek(FirstDateOfWeek);
      WeekNumber = Calendar.GetWeekOfYear(offsetDate, CalendarWeekRule.FirstFourDayWeek, FirstDayOfWeek);
      WeekYear = offsetDate.Year;
    }

    public Week(DateTime date)
    {
      FirstDateOfWeek = GetFirstDateOfWeek(date);
      LastDateOfWeek = GetLastDateOfWeek(date);
      DateTime offsetDate = GetYearOffsetDayOfWeek(FirstDateOfWeek);
      WeekNumber = Calendar.GetWeekOfYear(offsetDate, CalendarWeekRule.FirstFourDayWeek, FirstDayOfWeek);
      WeekYear = offsetDate.Year;
    }

    public static bool operator >=(Week left, Week right) => left.CompareTo(right) >= 0;

    public static bool operator <=(Week left, Week right) => left.CompareTo(right) <= 0;

    public static bool operator >(Week left, Week right) => left.CompareTo(right) > 0;

    public static bool operator <(Week left, Week right) => left.CompareTo(right) < 0;

    public DateTime FirstDateOfWeek { get; private set; }

    public DateTime LastDateOfWeek { get; private set; }

    public int WeekNumber { get; private set; }

    public int WeekYear { get; private set; }

    public int CompareTo(Week? week)
    {
      return week == null ? -1 : string.Compare(ToString(), week.ToString(), StringComparison.Ordinal);
    }

    public bool Equals(Week? week)
    {
      return week is not null && WeekYear == week.WeekYear && WeekNumber == week.WeekNumber;
    }

    /// <summary>
    /// Returns a sortable ISO week string (e.g. "2023-W22").
    /// </summary>
    public override string ToString()
    {
      return string.Format("{0}-W{1:00}", WeekYear, WeekNumber);
    }

    public override int GetHashCode()
    {
      return ToString().GetHashCode();
    }

    private DateTime GetFirstDateOfWeek(DateTime date)
    {
      while (date.DayOfWeek != FirstDayOfWeek && date != DateTime.MinValue.Date)
        date = date.AddDays(-1);
      return date;
    }

    private DateTime GetLastDateOfWeek(DateTime date)
    {
      while (date.DayOfWeek != LastDayOfWeek && date != DateTime.MaxValue.Date)
        date = date.AddDays(1);
      return date;
    }

    private DateTime GetYearOffsetDayOfWeek(DateTime firstDateOfWeek)
    {
      while (firstDateOfWeek.DayOfWeek != FirstDayOfYear)
        firstDateOfWeek = firstDateOfWeek.AddDays(1);
      return firstDateOfWeek;
    }

  }
}
