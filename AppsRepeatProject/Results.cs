using System;

public class Results
{
    public DateTime Timestamp { get; set; }
    public string PlayerOneName { get; set; }
    public int PlayerOneScore { get; set; }
    public string PlayerTwoName { get; set; }
    public int PlayerTwoScore { get; set; }

    public string Summary => $"{Timestamp}: {PlayerOneName} ({PlayerOneScore}) vs {PlayerTwoName} ({PlayerTwoScore})";
}
