using System.Collections.Generic;
using PipBoy.Protocol;
using ReactiveUI;

namespace PipBoy.ViewModels
{
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
            properties.ToBoxedProperty(this, x => x.Caps, out caps, b => (int)b.Value);
            properties.ToBoxedProperty(this, x => x.CurrAP, out currAP, b => (int)b.Value);
            properties.ToBoxedProperty(this, x => x.CurrentHPGain, out currentHPGain, b => (int)b.Value);
            properties.ToBoxedProperty(this, x => x.CurrHP, out currHP, b => (int)b.Value);
            properties.ToBoxedProperty(this, x => x.CurrWeight, out currWeight, b => (int)b.Value);
            properties.ToBoxedProperty(this, x => x.DateDay, out dateDay, b => (int)b.Value);
            properties.ToBoxedProperty(this, x => x.DateMonth, out dateMonth, b => (int)b.Value);
            properties.ToBoxedProperty(this, x => x.DateYear, out dateYear, b => (int)b.Value);
            properties.ToBoxedProperty(this, x => x.MaxAP, out maxAP, b => (int)b.Value);
            properties.ToBoxedProperty(this, x => x.MaxHP, out maxHP, b => (int)b.Value);
            properties.ToBoxedProperty(this, x => x.MaxWeight, out maxWeight, b => (int)b.Value);
            properties.ToBoxedProperty(this, x => x.PerkPoints, out perkPoints, b => (int)b.Value);
            properties.ToBoxedProperty(this, x => x.PlayerName, out playerName, b => b.Value as string);
            properties.ToBoxedProperty(this, x => x.TimeHour, out timeHour, b => (int)b.Value);
            properties.ToBoxedProperty(this, x => x.XPLevel, out xpLevel, b => (int)b.Value);
            properties.ToBoxedProperty(this, x => x.XPProgressPct, out xpProgressPct, b => (int)b.Value);
        }

        private int Caps => this.caps.Value;

        private int CurrAP => this.currAP.Value;

        private int CurrentHPGain => this.currentHPGain.Value;

        private int CurrHP => this.currHP.Value;

        private int CurrWeight => this.currWeight.Value;

        private int DateDay => this.dateDay.Value;

        private int DateMonth => this.dateMonth.Value;

        private int DateYear => this.dateYear.Value;

        private int MaxAP => this.maxAP.Value;

        private int MaxHP => this.maxHP.Value;

        private int MaxWeight => this.maxWeight.Value;

        private int PerkPoints => this.perkPoints.Value;

        public string PlayerName => this.playerName.Value;

        private int TimeHour => this.timeHour.Value;

        private int XPLevel => this.xpLevel.Value;

        private int XPProgressPct => this.xpProgressPct.Value;
    }
}
