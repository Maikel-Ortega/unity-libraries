using UnityEngine;
using System.Collections;

//TODO: Descomentar rdsTable cuando este implementado

public class RDSObject : IRDSObject 
{
    private double mWeight;
    private bool mUnique;
    private bool mAlways;
    private bool mEnabled;

    #region constructors

    public RDSObject():this(0)
    {
        
    }

    public RDSObject(double _weight):this(_weight, false, false, true)
    {
        
    }

    public RDSObject(double _weight, bool _unique, bool _always, bool _enabled)
    {
        rdsWeight = _weight;
        rdsUnique = _unique;
        rdsAlways = _always;
        rdsEnabled = _enabled;
        //rdsTable = null;
    }

    #endregion

    #region RDSObject events

    public event System.Action<IRDSObject> OnRdsPreResultEvaluation;
    public event System.Action<IRDSObject> OnRdsHit;
    public event System.Action<IRDSObject> OnRdsPostResultEvaluation;

    /// <summary>
    /// Calls the preResult event
    /// </summary>
    public void RDSPreResultEvaluation()
    {
        if(OnRdsPreResultEvaluation != null)
        {
            OnRdsPreResultEvaluation(this);
        }
    }

    /// <summary>
    /// Calls the rdsHit event
    /// </summary>
    public void RDSHit()
    {
        if(OnRdsHit != null)
        {
            OnRdsHit(this);
        }
    }

    /// <summary>
    /// Calls the postResult event
    /// </summary>
    public void RDSPostResultEvaluation()
    {
        if(OnRdsPostResultEvaluation != null)
        {
            OnRdsPostResultEvaluation(this);
        }
    }

    #endregion

    #region RDSObject members


    /// <summary>
    /// Gets or sets the rds weight.
    /// </summary>
    /// <value>The rds weight.</value>
    public double rdsWeight
    {
        get
        {
            return this.mWeight;
        }
        set
        {
            this.mWeight = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="RDSObject"/> rds unique.
    /// </summary>
    /// <value><c>true</c> if rds unique; otherwise, <c>false</c>.</value>
    public bool rdsUnique
    {
        get
        {
            return this.mUnique;
        }
        set
        {
            this.mUnique = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="RDSObject"/> rds always.
    /// </summary>
    /// <value><c>true</c> if rds always; otherwise, <c>false</c>.</value>
    public bool rdsAlways
    {
        get
        {
            return this.mAlways;
        }
        set
        {
            this.mAlways = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="RDSObject"/> rds enabled.
    /// </summary>
    /// <value><c>true</c> if rds enabled; otherwise, <c>false</c>.</value>
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

    //public RDSTable rdsTable { get; set; }

    #endregion

    #region TOSTRING
    /// <summary>
    /// Returns a <see cref="System.String"/> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String"/> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        return ToString(0);
    }

    /// <summary>
    /// Returns a <see cref="System.String"/> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String"/> that represents this instance.
    /// </returns>
    public string ToString(int indentationlevel)
    {
        string indent = "".PadRight(4 * indentationlevel, ' ');

        return string.Format(indent + "(RDSObject){0} Prob:{1},UAE:{2}{3}{4}",
            this.GetType().Name, rdsWeight,
            (rdsUnique ? "1" : "0"), (rdsAlways ? "1" : "0"), (rdsEnabled ? "1" : "0"));
    }
    #endregion
    
}
