using UnityEngine;
using System.Collections;


[CreateAssetMenu()]
public class RDSScriptableObject : ScriptableObject, IRDSObject
{
    [System.Serializable]
    public class RDSScriptableProperty
    {
        public RDSScriptableObject rdsObject;
        public double rdsWeight;
        public bool rdsUnique;   
        public bool rdsAlways;     
        public bool rdsEnabled;
    }

    public string name;

    #region IRDSObject implementation
    public event System.Action<IRDSObject> OnRdsPreResultEvaluation;
    public event System.Action<IRDSObject> OnRdsHit;
    public event System.Action<IRDSObject> OnRdsPostResultEvaluation;
    public void RDSPreResultEvaluation()
    {
        throw new System.NotImplementedException();
    }
    public void RDSHit()
    {
        throw new System.NotImplementedException();
    }
    public void RDSPostResultEvaluation()
    {
        throw new System.NotImplementedException();
    }
    public string ToString(int indentationLevel)
    {
        throw new System.NotImplementedException();
    }
    public double rdsWeight
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
    public bool rdsUnique
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
    public bool rdsAlways
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
    public bool rdsEnabled
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
    public RDSTable rdsTable
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
    #endregion
    
}
