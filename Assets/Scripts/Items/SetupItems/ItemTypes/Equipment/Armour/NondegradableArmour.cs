//  Armour that does not degrade with use
public class NondegradableArmour : Armour
{
    public NondegradableArmour(NondegradeArmourSO armourData) : base(armourData) { }

    //  No degradation means no cost/hr!
    public override ulong GetValue()
    {
        return 0;
    }
}
