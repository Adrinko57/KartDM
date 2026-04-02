using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

public static class Timer
{
    private static readonly Stopwatch stopwatch = new();
    private static List<long> steps = new();

    public static bool IsRunning
    {
        get => stopwatch.IsRunning;
    }

    public static double ElapsedSeconds
    {
        get => stopwatch.ElapsedMilliseconds * 0.001f;
    }

    public static int StepsCount
    {
        get => steps.Count;
    }

    public static double GetStepElapsedSeconds(int index)
    {
        return steps[index] * 0.001f;
    }

    /// <summary>
    /// Reset the timer and remove any steps.
    /// </summary>
    public static void Reset()
    {
        stopwatch.Reset();
        steps.Clear();
    }

    public static void Start()
    {
        stopwatch.Start();
    }

    public static void Stop()
    {
        stopwatch.Stop();
    }

    public static void Step()
    {
        steps.Add(stopwatch.ElapsedMilliseconds);
    }

    public static void Save()
    {
        using (StreamWriter writer = new StreamWriter("score.txt"))
        {
            foreach (long step in steps)
            {
                writer.WriteLine(step);
            }
        }
    }

    public static void Load()
    {
        if (File.Exists("score.txt"))
        {
            steps.Clear();

            string[] lines = File.ReadAllLines("score.txt");

            foreach (string line in lines)
            {
                if (long.TryParse(line, out long value))
                {
                    steps.Add(value);
                }
            }
        }
    }
}
