using System.Diagnostics;

public interface IMetricsService
{
    Stopwatch StartTimer();

    void StopTimer(Stopwatch timer, string controllerName, string actionName, string methodName = "POST");
}