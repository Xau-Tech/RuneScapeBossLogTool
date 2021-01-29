//  Should the boss selected be kept constant across all tabs
public class BossSyncOption : GenericOption
{
    private readonly static string[] choices = { "false", "true" };
    private readonly static string name = "BossSync";

    public BossSyncOption(IDisplayOption displayInterface) : base(displayInterface)
    {
        SetName(in name);
        SetChoices(in choices);
        SetValue(in choices[0]);    //  false
        SetOptionType(OptionData.OptionTypes.Toggle);
    }

    public static string Name()
    {
        return name;
    }

    public static OptionData.OptionTypes OptionType()
    {
        return OptionData.OptionTypes.Toggle;
    }

    public override void Apply() { }

    public override void DisplayChoice()
    {
        displayInterface.DisplayChoice(GetValue());
    }

    public override void PopulateChoices() { }
}
