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

    private void InternalSort(List<T> list, int left, int right)
    {
        if (left >= right) return;

        int partition = InternalPartition(list, left, right);

        InternalSort(list, left, partition - 1);
        InternalSort(list, partition + 1, right);
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
    }
}

public class AbilityResultSortOptions
{
    public List<string> SortOptions;

    private readonly List<IComparer<AbilityResult>> m_Comparers = new() { new AbilityResultNameSort(), new AbilityResultMinDamageSort(), new AbilityResultMaxDamageSort(), new AbilityResultMinDpsSort() };

    public AbilityResultSortOptions()
    {
        SortOptions = new() { "Name", "Min Damage", "Max Damage", "Min DPS" };
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
        return x.Min.CompareTo(y.Min);
    }
}

public class AbilityResultMaxDamageSort : IComparer<AbilityResult>
{
    public AbilityResultMaxDamageSort()
    {

    }

    int IComparer<AbilityResult>.Compare(AbilityResult x, AbilityResult y)
    {
        return x.Max.CompareTo(y.Max);
    }
}

public class AbilityResultMinDpsSort : IComparer<AbilityResult>
{
    public AbilityResultMinDpsSort()
    {

    }

    int IComparer<AbilityResult>.Compare(AbilityResult x, AbilityResult y)
    {
        return x.MinDps.CompareTo(y.MinDps);
    }
}