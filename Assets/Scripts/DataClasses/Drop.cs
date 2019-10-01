using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Drop class that inherits from item and adds a NumberOfItems value to track how many the user has received
public class Drop : Item
{
    //  Properties
    public Drop()
    {

    }
    public Drop(string _name, int _price)
    {
        Name = _name;
        Price = _price;
        m_NumberOfItems = -1;
    }
    public Drop(string _name, int _price, int _num)
    {
        Name = _name;
        Price = _price;
        m_NumberOfItems = _num;
    }
    public int NumberOfItems { get { return m_NumberOfItems; } set { m_NumberOfItems = value; } }


    private int m_NumberOfItems;


    //  Override used to generate the text for each drop within the UI drop list
    public override string ToString()
    {
        return (this.Name + "\n") +
                ("Quantity:  " + m_NumberOfItems + "\n") +
                ("Total Value:  " + (m_NumberOfItems * this.Price).ToString("#,#") + " gp");
    }
}
