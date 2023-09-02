using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSort<T>
{
    private readonly IComparer<T> m_Comparer;

    public QuickSort(IComparer<T> comparer)
    {
        m_Comparer = comparer;
    }

    public void Sort(List<T> list)
    {
        InternalSort(list, 0, list.Count - 1);
    }

    private void InternalSort(List<T> list, int low, int high)
    {
        if(low >= 0 && high >= 0 && low < high)
        {
            int partition = InternalPartition(list, low, high);
            InternalSort(list, low, partition);
            InternalSort(list, partition + 1, high);
        }
    }

    private int InternalPartition(List<T> list, int low, int high)
    {
        //Debug.Log($"Low: {low}, High: {high}, Pivot: {pivot}");
        int pivot = DeterminePivot(list, low, high);
        T pivotObj = list[pivot];

        int i = low - 1;
        int j = high + 1;

        while (true)
        {
            do
            {
                ++i;
            } while (m_Comparer.Compare(list[i], pivotObj) < 0);

            do
            {
                --j;
            } while (m_Comparer.Compare(list[j], pivotObj) > 0);

            if (i >= j) return j;

            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    private int DeterminePivot(List<T> list, int low, int high)
    {
        T lowObj = list[low];
        T highObj = list[high];
        int midPoint = Mathf.FloorToInt((high - low) / 2.0f) + low;
        T midObj = list[midPoint];
        
        if ((m_Comparer.Compare(lowObj, highObj) > 0) ^ (m_Comparer.Compare(lowObj, midObj) > 0))
            return low;
        else if ((m_Comparer.Compare(highObj, lowObj) < 0) ^ (m_Comparer.Compare(highObj, midObj) < 0))
            return high;
        else
            return midPoint;
    }

    /*private void InternalSort(List<T> list, int left, int right)
    {
        if (left >= right) return;

        int pivot = InternalPartition(list, left, right);
        
        InternalSort(list, left, pivot - 1);
        InternalSort(list, pivot + 1, right);
    }

    private int InternalPartition(List<T> list, int left, int right)
    {
        T partition = list[right];

        int swapIndex = left;
        for (int i = left; i < right; ++i)
        {
            T item = list[i];
            if (m_Comparer.Compare(item, partition) <= 0)
            {
                list[i] = list[swapIndex];
                list[swapIndex] = item;
                ++swapIndex;
            }
        }

        list[right] = list[swapIndex];
        list[swapIndex] = partition;

        return swapIndex;
    }*/
}

public class AbilityResultSortOptions
{
    public List<string> SortOptions;

    private readonly List<IComparer<AbilityResult>> m_Comparers = new() { new AbilityResultNameSort(), new AbilityResultMinDamageSort(), new AbilityResultMaxDamageSort(), new AbilityResultMinDpsSort(), new AbilityResultMaxDpsSort() };

    public AbilityResultSortOptions()
    {
        SortOptions = new() { "Name", "Min Damage", "Max Damage", "Min DPS", "Max DPS" };
    }

    public IComparer<AbilityResult> GetComparer(int value)
    {
        return m_Comparers[value];
    }
}

public class AbilityResultNameSort : IComparer<AbilityResult>
{
    public AbilityResultNameSort()
    {

    }

    int IComparer<AbilityResult>.Compare(AbilityResult x, AbilityResult y)
    {
        return x.Name.CompareTo(y.Name);
    }
}

public class AbilityResultMinDamageSort : IComparer<AbilityResult>
{
    public AbilityResultMinDamageSort()
    {

    }

    int IComparer<AbilityResult>.Compare(AbilityResult x, AbilityResult y)
    {
        return y.Min.CompareTo(x.Min);
    }
}

public class AbilityResultMaxDamageSort : IComparer<AbilityResult>
{
    public AbilityResultMaxDamageSort()
    {

    }

    int IComparer<AbilityResult>.Compare(AbilityResult x, AbilityResult y)
    {
        return y.Max.CompareTo(x.Max);
    }
}

public class AbilityResultMinDpsSort : IComparer<AbilityResult>
{
    public AbilityResultMinDpsSort()
    {

    }

    int IComparer<AbilityResult>.Compare(AbilityResult x, AbilityResult y)
    {
        return y.MinDps.CompareTo(x.MinDps);
    }
}

public class AbilityResultMaxDpsSort : IComparer<AbilityResult>
{
    public AbilityResultMaxDpsSort()
    {

    }

    int IComparer<AbilityResult>.Compare(AbilityResult x, AbilityResult y)
    {
        return y.MaxDps.CompareTo(x.MaxDps);
    }
}