using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Category", fileName = "Category_")]
public class Category : ScriptableObject, IEquatable<Category>
{
    [SerializeField]
    private string codeName;
    [SerializeField]
    private string displayName;


    public string CodeName => codeName;
    public string DisplayName => displayName;


    #region Operator
    public bool Equals(Category other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(other, this))
            return true;
        if (GetType() != other.GetType())
            return false;

        return codeName == other.CodeName;
    }

    public override int GetHashCode() => (CodeName, DisplayName).GetHashCode();  // (CodeName, DisplayName)의 해시코드값 리턴?

    public override bool Equals(object other) => base.Equals(other);  // scriptableObject형식과 other 형식이 같은지 리턴?
    

    public static bool operator == (Category lhs, string rhs)
    {
        if (lhs is null)
            return ReferenceEquals(rhs, null);
        return lhs.CodeName == rhs || lhs.DisplayName == rhs;
    }

    public static bool operator != (Category lhs, string rhs) => !(lhs == rhs);
    

    #endregion

}
