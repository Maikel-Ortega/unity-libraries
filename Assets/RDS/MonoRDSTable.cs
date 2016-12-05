using UnityEngine;
using System.Collections;

public class MonoRDSTable : MonoBehaviour, IRDSTable
{
    RDSTable mTable;
	
    #region monoRDSTable 
    public void Awake()
    {
        
    }



    #endregion


    #region IRDSObject implementation
    public event System.Action<IRDSObject> OnRdsPreResultEvaluation;
    public event System.Action<IRDSObject> OnRdsHit;
    public event System.Action<IRDSObject> OnRdsPostResultEvaluation;

    public void RDSPreResultEvaluation()
    {

    }

    public void RDSHit()
    {

    }

    public void RDSPostResultEvaluation()
    {

    }

    public string ToString(int indentationLevel)
    {
        return mTable.ToString();
    }

    public double rdsWeight
    {
        get
        {
            return this.mTable.rdsWeight;
        }
        set
        {
            this.mTable.rdsWeight = value;
        }
    }

    public bool rdsUnique
    {
        get
        {
            return this.mTable.rdsUnique;
        }
        set
        {
            this.mTable.rdsUnique = value;
        }
    }

    public bool rdsAlways
    {
        get
        {
            return this.mTable.rdsAlways;
        }
        set
        {
            this.mTable.rdsAlways = value;
        }
    }
    public bool rdsEnabled
    {
        get
        {
            return this.mTable.rdsEnabled;
        }
        set
        {
            this.mTable.rdsEnabled = value;
        }
    }

    //Table that OWNS this object, not the table contained
    public RDSTable rdsTable
    {
        get
        {
            return this.mTable.rdsTable;
        }
        set
        {
            this.mTable.rdsTable = value;
        }
    }


    #endregion
    #region IRDSTable implementation
    public System.Collections.Generic.IEnumerable<IRDSObject> GetRdsResult()
    {
        return this.mTable.GetRdsResult();
    }

    public int rdsCount
    {
        get
        {
            return this.mTable.rdsCount;
        }
        set
        {
            this.mTable.rdsCount = value;
        }
    }

    public System.Collections.Generic.IEnumerable<IRDSObject> rdsContents
    {
        get
        {
            return this.mTable.rdsContents;
        }
    }
    #endregion
}
