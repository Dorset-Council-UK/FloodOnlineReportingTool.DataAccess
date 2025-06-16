namespace FloodOnlineReportingTool.DataAccess.Models;

public static class InvestigationsDtoExtensions
{
    /// <summary>
    ///     <para>Converts an investigation DTO to an investigation entity.</para>
    ///     <para>A new investigation ID is generated.</para>
    /// </summary>
    public static Investigation ToInvestigation(this InvestigationDto dto, bool isInternal)
    {
        return ToInvestigation(dto, Guid.CreateVersion7(), isInternal);
    }

    /// <summary>
    ///     <para>Converts an investigation DTO to an investigation entity.</para>
    ///     <para>The new investigation ID must be passsed in to this method.</para>
    /// </summary>
    public static Investigation ToInvestigation(this InvestigationDto dto, Guid investigationId, bool isInternal)
    {
        var investigation = new Investigation
        {
            Id = investigationId,

            // Water speed
            BeginId = dto.BeginId.Value,
            WaterSpeedId = dto.WaterSpeedId.Value,
            AppearanceId = dto.AppearanceId.Value,
            MoreAppearanceDetails = dto.MoreAppearanceDetails,

            // Water destination
            Destinations = [.. dto.Destinations.Select(floodProblemId => new InvestigationDestination(investigationId, floodProblemId))],

            // Damaged vehicles
            WereVehiclesDamagedId = dto.WereVehiclesDamagedId.Value,
            NumberOfVehiclesDamaged = dto.NumberOfVehiclesDamaged,

            // Internal (handled below)

            // Peak depth (handled below)
            IsPeakDepthKnownId = dto.IsPeakDepthKnownId.Value,

            // Community impact
            CommunityImpacts = [.. dto.CommunityImpacts.Select(floodImpactId => new InvestigationCommunityImpact(investigationId, floodImpactId))],

            // Blockages
            HasKnownProblems = dto.HasKnownProblems == true,
            KnownProblemDetails = dto.KnownProblemDetails,

            // Actions taken
            ActionsTaken = [.. dto.ActionsTaken.Select(floodMitigationId => new InvestigationActionsTaken(investigationId, floodMitigationId))],
            OtherAction = dto.OtherAction,

            // Help received
            HelpReceived = [.. dto.HelpReceived.Select(floodMitigationId => new InvestigationHelpReceived(investigationId, floodMitigationId))],

            // Before the flooding - Warnings
            FloodlineId = dto.FloodlineId.Value,
            WarningReceivedId = dto.WarningReceivedId.Value,

            // Warning sources
            WarningSources = [.. dto.WarningSources.Select(floodMitigationId => new InvestigationWarningSource(investigationId, floodMitigationId))],
            WarningSourceOther = dto.WarningSourceOther,

            // Floodline warnings (handled below)

            // History
            HistoryOfFloodingId = dto.HistoryOfFloodingId.Value,
            HistoryOfFloodingDetails = dto.HistoryOfFloodingDetails,
        };


        // Internal
        if (isInternal)
        {
            investigation = investigation with
            {
                // Internal - How it entered - Water entry
                Entries = [.. dto.Entries.Select(floodProblemId => new InvestigationEntry(investigationId, floodProblemId))],
                WaterEnteredOther = dto.WaterEnteredOther,

                // Internal - When it entered
                WhenWaterEnteredKnownId = dto.WhenWaterEnteredKnownId.Value,
                FloodInternalUtc = dto.FloodInternalUtc,
            };
        }

        // Peak depth
        if (dto.IsPeakDepthKnownId == RecordStatusIds.Yes)
        {
            investigation = investigation with
            {
                PeakInsideCentimetres = dto.PeakInsideCentimetres.Value,
                PeakOutsideCentimetres = dto.PeakOutsideCentimetres.Value,
            };
        }

        // Floodline warnings
        if (dto.WarningSources.Contains(FloodMitigationIds.FloodlineWarning))
        {
            investigation = investigation with
            {
                WarningTimelyId = dto.WarningTimelyId,
                WarningAppropriateId = dto.WarningAppropriateId,
            };
        }

        return investigation;
    }
}
