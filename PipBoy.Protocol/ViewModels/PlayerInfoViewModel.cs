// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.Protocol.ViewModels
{
    using ReactiveUI;

    public class PlayerInfoViewModel : BoxedProperties
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
            : base(box)
        {
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
