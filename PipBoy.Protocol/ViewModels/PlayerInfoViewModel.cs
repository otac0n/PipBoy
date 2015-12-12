// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.Protocol.ViewModels
{
    using System.Collections.Generic;
    using ReactiveUI;

    public class PlayerInfoViewModel : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<int> caps;
        private readonly ObservableAsPropertyHelper<int> currAP;
        private readonly ObservableAsPropertyHelper<int> currentHPGain;
        private readonly ObservableAsPropertyHelper<int> currHP;
        private readonly ObservableAsPropertyHelper<int> currWeight;
        private readonly ObservableAsPropertyHelper<int> dateDay;
        private readonly ObservableAsPropertyHelper<int> dateMonth;
        private readonly ObservableAsPropertyHelper<int> dateYear;
        private readonly ObservableAsPropertyHelper<int> maxAP;
        private readonly ObservableAsPropertyHelper<int> maxHP;
        private readonly ObservableAsPropertyHelper<int> maxWeight;
        private readonly ObservableAsPropertyHelper<int> perkPoints;
        private readonly ObservableAsPropertyHelper<string> playerName;
        private readonly ObservableAsPropertyHelper<int> timeHour;
        private readonly ObservableAsPropertyHelper<int> xpLevel;
        private readonly ObservableAsPropertyHelper<int> xpProgressPct;

        public PlayerInfoViewModel(Box box)
        {
            var properties = box.WhenAny(x => x.Value, x => x.Value as Dictionary<string, Box>);
            properties.ToBoxedProperty(this, x => x.Caps, out this.caps);
            properties.ToBoxedProperty(this, x => x.CurrAP, out this.currAP);
            properties.ToBoxedProperty(this, x => x.CurrentHPGain, out this.currentHPGain);
            properties.ToBoxedProperty(this, x => x.CurrHP, out this.currHP);
            properties.ToBoxedProperty(this, x => x.CurrWeight, out this.currWeight);
            properties.ToBoxedProperty(this, x => x.DateDay, out this.dateDay);
            properties.ToBoxedProperty(this, x => x.DateMonth, out this.dateMonth);
            properties.ToBoxedProperty(this, x => x.DateYear, out this.dateYear);
            properties.ToBoxedProperty(this, x => x.MaxAP, out this.maxAP);
            properties.ToBoxedProperty(this, x => x.MaxHP, out this.maxHP);
            properties.ToBoxedProperty(this, x => x.MaxWeight, out this.maxWeight);
            properties.ToBoxedProperty(this, x => x.PerkPoints, out this.perkPoints);
            properties.ToBoxedProperty(this, x => x.PlayerName, out this.playerName);
            properties.ToBoxedProperty(this, x => x.TimeHour, out this.timeHour);
            properties.ToBoxedProperty(this, x => x.XPLevel, out this.xpLevel);
            properties.ToBoxedProperty(this, x => x.XPProgressPct, out this.xpProgressPct);
        }

        public int Caps => this.caps.Value;

        public int CurrAP => this.currAP.Value;

        public int CurrentHPGain => this.currentHPGain.Value;

        public int CurrHP => this.currHP.Value;

        public int CurrWeight => this.currWeight.Value;

        public int DateDay => this.dateDay.Value;

        public int DateMonth => this.dateMonth.Value;

        public int DateYear => this.dateYear.Value;

        public int MaxAP => this.maxAP.Value;

        public int MaxHP => this.maxHP.Value;

        public int MaxWeight => this.maxWeight.Value;

        public int PerkPoints => this.perkPoints.Value;

        public string PlayerName => this.playerName.Value;

        public int TimeHour => this.timeHour.Value;

        public int XPLevel => this.xpLevel.Value;

        public int XPProgressPct => this.xpProgressPct.Value;
    }
}
