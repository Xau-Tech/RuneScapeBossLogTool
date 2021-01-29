//  Used on UI objects that can't be de-selected by the TabNavigateSelectable system in certain circumstances
public interface IUninterruptable
{
    bool CanDeselect();
}
