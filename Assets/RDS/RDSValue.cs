using UnityEngine;
using System.Collections;

public class RDSValue<T> : RDSObject, IRDSValue<T>
{
    public virtual T rdsValue
    {
        get
        {
            return default(T);
        }
    }    
}
