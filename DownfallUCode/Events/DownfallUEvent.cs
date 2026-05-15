using BaseLib.Abstracts;
using BaseLib.Extensions;
using DownfallU.DownfallUCode.Extensions;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;

namespace DownfallU.DownfallUCode.Events;

public abstract class DownfallUEvent : CustomEventModel
{
    public abstract string CharacterId { get; }
    public override string CustomInitialPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".EventImagePath(CharacterId);
    protected EventOption OptionLocked(Func<Task> onChosen, string pageKey = "INITIAL", params IHoverTip[] tips)
    {
        return new EventOption(this, null, $"{Id.Entry}.pages.{pageKey}.options.{StringHelper.Slugify(onChosen.Method.Name)}", tips);
    }
}