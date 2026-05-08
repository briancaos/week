# Week Value Object (ISO-8601)

This project provides an immutable `Week` value object for working with ISO‑8601 week numbers in .NET.
It encapsulates week-based date calculations and offers correct comparison, equality, and formatting behavior.

The implementation follows ISO‑8601 rules:

- Weeks start on Monday
- Week 1 is the first week with at least four days in the new year

## Target Framework

.NET 6 or later
Requires System.Globalization

## Features

- ISO‑8601 compliant week calculations
- Create a week from:
  - a `DateTime`
  - a combination of `year` and `week number`
- Comparable and sortable (`IComparable<Week>`)
- Value-based equality (`IEquatable<Week>`)
- Stable, sortable string representation (`YYYY-Www`)
- Immutable design

---

## Week Record Overview

```csharp
public sealed record Week :
    IComparable<Week>,
    IEquatable<Week>
```

## Public Properties

- WeekYear
  The ISO week‑year (may differ from the calendar year)
- WeekNumber
  The ISO week number (1–53)
- FirstDateOfWeek
  The Monday of the week
- LastDateOfWeek
  The Sunday of the week

## Usage Examples
Create a week from a DateTime

```
var week = new Week(DateTime.UtcNow);

Console.WriteLine(week.WeekYear);
Console.WriteLine(week.WeekNumber);
Console.WriteLine(week); // e.g. "2026-W19"
```

Create a week from year and week number

```
var week = new Week(2026, 19);

Console.WriteLine(week.FirstDateOfWeek);
Console.WriteLine(week.LastDateOfWeek);
```

## Comparison and ordering

```

var w1 = new Week(2026, 10);
var w2 = new Week(2026, 20);

if (w1 < w2)
{
    Console.WriteLine("Week 10 is before Week 20");
}
```

Supported operators:

- <
- <=
- >
- >=
  
Equality (value object behavior)

```

var a = new Week(2025, 52);
var b = new Week(new DateTime(2025, 12, 28));

bool equals = a.Equals(b); // true
```

## String Representation

Calling ToString() returns a sortable ISO‑style string:
- YYYY-Www

```
new Week(2023, 2).ToString(); // "2023-W02"
```

This format:
- Sorts lexicographically
- Is safe for logs, APIs, keys, and storage
- Matches ISO‑8601 conventions

## Implementation Notes

- Uses System.Globalization.ISOWeek
- Uses the invariant culture calendar
- Calculates week‑year using the ISO Thursday rule
- Instance state is immutable after construction
- Independent of system locale

## Thread Safety

The Week record is thread‑safe due to its immutable design.

