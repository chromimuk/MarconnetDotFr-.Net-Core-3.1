﻿using System;

namespace MarconnetDotFr.Core.Models.FootyStats
{
    public class SeasonModel
    {
        public string season { get; set; }
        public Division division { get; set; }
        public int? attendance { get; set; }
        public int? ranking { get; set; }
        public CupPerformance coupedefrance { get; set; }
        public CupPerformance coupedelaligue { get; set; }
        public string europe { get; set; }
        public string notes { get; set; }

        #region ComputedProperties
        public int? rankingForChart => CalculateAllDivisionRanking();
        public int? coupeDeFranceForChart => CalculateCupRanking(coupedefrance);
        public int? coupeDeLaLigueForChart => CalculateCupRanking(coupedelaligue);
        public string strDivision => division == Division.D1 ? "D1" : "D2";
        public string strCoupeDeFrance => ToStrCupPerformance(coupedefrance);
        public string strCoupeDeLaLigue => ToStrCupPerformance(coupedelaligue);
        #endregion ComputedProperties

        private int? CalculateAllDivisionRanking()
        {
            // basically reverse the ranking (1 becomes the highest, 20 the lowest) and add 20 if in first division 

            var rank = 21 - ranking;

            if (division == Division.D1)
            {
                rank += 20;
            }

            return rank;
        }

        private int? CalculateCupRanking(CupPerformance cupPerformance)
        {
            int countCupPerformance = Enum.GetNames(typeof(CupPerformance)).Length;

            return cupPerformance switch
            {
                CupPerformance.Winner => countCupPerformance,
                CupPerformance.Final => countCupPerformance - 1,
                CupPerformance.SemiFinals => countCupPerformance - 2,
                CupPerformance.QuarterFinals => countCupPerformance - 3,
                CupPerformance.RoundOfEight => countCupPerformance - 4,
                CupPerformance.RoundOfSixteen => countCupPerformance - 5,
                CupPerformance.RoundOfThirtyTwo => countCupPerformance - 6,
                _ => null,
            };
        }

        private string ToStrCupPerformance(CupPerformance cupPerformance)
        {
            return cupPerformance switch
            {
                CupPerformance.Winner => "Vainqueur",
                CupPerformance.Final => "Finaliste",
                CupPerformance.SemiFinals => "Demi-finaliste",
                CupPerformance.QuarterFinals => "Quart-de-finaliste",
                CupPerformance.RoundOfEight => "8eme de finale",
                CupPerformance.RoundOfSixteen => "16eme de finale",
                CupPerformance.RoundOfThirtyTwo => "32eme de finale",
                _ => null,
            };
        }
    }

    public enum Division
    {
        D1,
        D2
    }

    public enum CupPerformance
    {
        RoundOfThirtyTwo = 32,
        RoundOfSixteen = 16,
        RoundOfEight = 8,
        QuarterFinals = 4,
        SemiFinals = 2,
        Final = 1,
        Winner = 0,
        Unknown = -1
    }
}