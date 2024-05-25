using System.Diagnostics.Metrics;

namespace Persistence.ConstStrings;

public static class DiagnosticsConfig
{
    public const string ServiceName = "Ez.Framework";

    public static Meter Meter = new(ServiceName);
}
