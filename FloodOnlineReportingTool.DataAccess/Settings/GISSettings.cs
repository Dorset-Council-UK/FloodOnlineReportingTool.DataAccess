﻿namespace FloodOnlineReportingTool.DataAccess.Settings;

public record GISSettings
{
    public const string SectionName = "GIS";

    public required string ApiKey { get; init; }
    public required string PathBase { get; init; }
    public required Uri AddressSearchUrl { get; init; }
    public required Uri NearestAddressesUrl { get; init; }
    public required int AccessTokenIssueDurationMonths { get; init; } = 6;
}
