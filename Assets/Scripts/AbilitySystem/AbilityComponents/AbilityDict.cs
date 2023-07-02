using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton read-only dictionary to manage Ability data
/// </summary>
public class AbilityDict
{
    //  Properties & fields

    public static AbilityDict Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = new AbilityDict();

            return m_Instance;
        }
    }

    private static AbilityDict m_Instance = null;
    private static readonly Dictionary<string, Ability> m_Dict;

    //  Constructor

    static AbilityDict()
    {
        m_Dict = new AbilityLoader().LoadAbilities();
    }

    //  Methods

    /*  TODO
        As this will just be used to filter the total list of abilities into what is currently being displayed
        I will probably change this to return either ids or facades of the abilities and then feed
        that data into the functionality that computes damage values so that I can really enforce the immutability of this object
    */
    public List<Ability> GetFilteredAbilities(ICriteria<Ability> filterCriteria)
    {
        return filterCriteria.FilterByCriteria(m_Dict.Values);
    }
}
