﻿namespace FloodOnlineReportingTool.DataAccess.Models;

/// <summary>
/// Some of the flood impact category priorities.
/// Helps ensure consistency.
/// </summary>
public static class FloodImpactPriority
{
    public const string None = null;
    public const string Internal = "Internal";
    public const string External = "External";
    public const string Both = "Both";
    public const string Other = "Other";
}
