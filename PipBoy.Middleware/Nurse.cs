// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.Middleware
{
    using System.Reactive.Linq;
    using PipBoy.Protocol.ViewModels;
    using ReactiveUI;

    public class Nurse : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<int> hpDefecit;
        private readonly ClientViewModel model;

        public Nurse(ClientViewModel model)
        {
            this.model = model;

            var currHp = this.model.WhenAnyValue(x => x.GameInfo.PlayerInfo.CurrHP);
            var maxHp = this.model.WhenAnyValue(x => x.GameInfo.PlayerInfo.MaxHP);
            var hpDeficit = Observable.CombineLatest(maxHp, currHp, (max, curr) => max - curr);
            hpDeficit.ToProperty(this, x => x.HPDefecit, out this.hpDefecit);
        }

        public int HPDefecit => this.hpDefecit.Value;
    }
}
