using UnityEngine;
using System.Collections;
using System.Collections.Generic;



[CreateAssetMenu()]
public class RDSScriptableTable : RDSScriptableObject, IRDSTable 
{ 
  

    public List<RDSScriptableProperty> contents;

    #region IRDSTable implementation

    public IEnumerable<IRDSObject> GetRdsResult()
    {
        throw new System.NotImplementedException();
    }

    public int rdsCount
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public IEnumerable<IRDSObject> rdsContents
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    #endregion
}
