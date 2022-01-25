﻿namespace StartPagePlus.Extensions.RecentItems
{
    using System;
    using System.IO;

    using StartPagePlus.UI.Models;
    using StartPagePlus.UI.ViewModels;

    using static StartPagePlus.UI.Enums.PeriodTypes;
    using static StartPagePlus.UI.Enums.RecentItemTypes;

    public static class RecentItemExtensions
    {
        public static RecentItemViewModel ToViewModel(this RecentItem result, DateTime today, bool showExtension)
        {
            var path = result.Value.LocalProperties.FullPath;
            var date = result.Value.LastAccessed.Date;
            var name = showExtension
                ? Path.GetFileName(path)
                : Path.GetFileNameWithoutExtension(path);
            var pinned = result.Value.IsFavorite;
            var folder = Path.GetDirectoryName(path);
            var period = CalculatePeriodType(pinned, today, date);
            var type = path.CalculateRecentItemType();
            var moniker = type.ToImageMoniker();

            return new RecentItemViewModel
            {
                Name = name,
                Description = folder,
                Date = date,
                Path = path,
                Pinned = pinned,
                PeriodType = period,
                ItemType = type,
                Moniker = moniker
            };
        }
    }
}