﻿using System.Collections.Generic;
using System.Threading.Tasks;
using LastFM.AspNetCore.Stats.Entities;
using MarconnetDotFr.Core.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MarconnetDotFr.Pages
{
    public class LastFMStatsModel : PageModel
    {
        private readonly ILastFMStatsService _lastFMStatsService;

        public Task<LastFMUser> LastFMUser { get; set; }
        public Task<IEnumerable<Album>> TopAlbums { get; set; }
        public Task<IEnumerable<Track>> TopTracks { get; set; }
        public Task<IEnumerable<Artist>> TopArtists { get; set; }
        public Task<IEnumerable<Track>> RecentTracks { get; set; }

        public LastFMStatsModel(ILastFMStatsService lastFMStatsService)
        {
            _lastFMStatsService = lastFMStatsService;
        }

        public void OnGet(string username = "chromimuk")
        {
            LastFMUser = _lastFMStatsService.GetUserInfo(username);
            TopAlbums = _lastFMStatsService.GetTopAlbums(username);
            TopTracks = _lastFMStatsService.GetTopTracks(username);
            TopArtists = _lastFMStatsService.GetTopArtists(username);
            RecentTracks = _lastFMStatsService.GetRecentTracks(username);
        }
    }
}