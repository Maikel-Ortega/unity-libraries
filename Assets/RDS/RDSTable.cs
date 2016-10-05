using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RDSTable : IRDSTable 
{
    #region constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="RDSTable"/> class.
    /// </summary>
    public RDSTable()
        : this(null, 1, 1, false, false, true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RDSTable"/> class.
    /// </summary>
    /// <param name="contents">The contents.</param>
    /// <param name="count">The count.</param>
    /// <param name="probability">The probability.</param>
    public RDSTable(IEnumerable<IRDSObject> contents, int count, double weight)
        : this(contents, count, weight, false, false, true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RDSTable"/> class.
    /// </summary>
    /// <param name="contents">Contents.</param>
    /// <param name="count">Count.</param>
    /// <param name="weight">Weight.</param>
    /// <param name="unique">If set to <c>true</c> unique.</param>
    /// <param name="always">If set to <c>true</c> always.</param>
    /// <param name="enabled">If set to <c>true</c> enabled.</param>
    public RDSTable(IEnumerable<IRDSObject> contents, int count, double weight, bool unique, bool always, bool enabled)
    {
        if(contents != null)
        {
            mContents = contents.ToList();
        }

        rdsCount = count;
        rdsWeight = weight;
        rdsUnique = unique;
        rdsAlways = always;
        rdsEnabled = enabled;
    }

    #endregion

    #region events
    
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
    
    #endregion 
    
    #region members

    private List<IRDSObject> mContents = null;

    /// <summary>
    /// Gets or sets the rds weight.
    /// </summary>
    /// <value>The rds weight.</value>
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

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="RDSTable"/> rds unique.
    /// </summary>
    /// <value><c>true</c> if rds unique; otherwise, <c>false</c>.</value>
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

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="RDSTable"/> rds always.
    /// </summary>
    /// <value><c>true</c> if rds always; otherwise, <c>false</c>.</value>
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

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="RDSTable"/> rds enabled.
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

    public int rdsCount  { get; set;}

    public System.Collections.Generic.IEnumerable<IRDSObject> rdsContents 
    {  
        get{ return mContents; } 
    }

    /// <summary>
    /// Clears the contents.
    /// </summary>
    public virtual void ClearContents()
    {
        mContents = new List<IRDSObject>();
    }

    /// <summary>
    /// Adds the given entry to contents collection.
    /// </summary>
    /// <param name="entry">The entry.</param>
    public virtual void AddEntry(IRDSObject entry)
    {
        mContents.Add(entry);
        entry.rdsTable = this;
    }

    /// <summary>
    /// Adds a new entry to the contents collection and allows directly assigning of a probability for it.
    /// Use this signature if (for whatever reason) the base classes constructor does not support all
    /// constructors of RDSObject or if you implemented IRDSObject directly in your class and you need
    /// to (re)apply a new probability at the moment you add it to a RDSTable.
    /// NOTE: The probability given is written back to the given instance "entry".
    /// </summary>
    /// <param name="entry">The entry.</param>
    /// <param name="probability">The probability.</param>
    public virtual void AddEntry(IRDSObject entry, double weight)
    {
        mContents.Add(entry);
        entry.rdsWeight = weight;
        entry.rdsTable = this;
    }

    /// <summary>
    /// Adds a new entry to the contents collection and allows directly assigning of a probability and drop flags for it.
    /// Use this signature if (for whatever reason) the base classes constructor does not support all
    /// constructors of RDSObject or if you implemented IRDSObject directly in your class and you need
    /// to (re)apply a new probability and flags at the moment you add it to a RDSTable.
    /// NOTE: The probability, unique, always and enabled flags given are written back to the given instance "entry".
    /// </summary>
    /// <param name="entry">The entry.</param>
    /// <param name="probability">The probability.</param>
    /// <param name="unique">if set to <c>true</c> this object can only occur once per result.</param>
    /// <param name="always">if set to <c>true</c> [always] this object will appear always in the result.</param>
    /// <param name="enabled">if set to <c>false</c> [enabled] this object will never be part of the result (even if it is set to always=true!).</param>
    public virtual void AddEntry(IRDSObject entry, double weight, bool unique, bool always, bool enabled)
    {
        mContents.Add(entry);
        entry.rdsWeight = weight;
        entry.rdsUnique = unique;
        entry.rdsAlways = always;
        entry.rdsEnabled = enabled;
        entry.rdsTable = this;
    }

    /// <summary>
    /// Removes the given entry from the contents. If it is not part of the contents, an exception occurs.
    /// </summary>
    /// <param name="entry">The entry.</param>
    public virtual void RemoveEntry(IRDSObject entry)
    {
        mContents.Remove(entry);
        entry.rdsTable = null;
    }

    /// <summary>
    /// Removes the entry at the given index position.
    /// If the index is out-of-range of the current contents collection, an exception occurs.
    /// </summary>
    /// <param name="index">The index.</param>
    public virtual void RemoveEntry(int index)
    {
        IRDSObject entry = mContents[index];
        entry.rdsTable = null;
        mContents.RemoveAt(index);
    }

    #endregion

    #region Results

    private List<IRDSObject> uniqueDrops = new List<IRDSObject>();

    private void AddResult(List<IRDSObject> resultList, IRDSObject newObj)
    {
        if (!newObj.rdsUnique || !uniqueDrops.Contains(newObj))
        {
            if(newObj.rdsUnique)
            {
                uniqueDrops.Add(newObj);
            }

            if(!(newObj is RDSNull))
            {
                if( newObj is IRDSTable)
                {
                    resultList.AddRange( ((IRDSTable)newObj).rdsResult );
                }
                else
                {
                    IRDSObject toAdd = newObj;

                    //Si tenemos que crear una nueva referencia nueva, con un metodo factoria
                    //lo hacemos aqui.

                    resultList.Add(toAdd);
                    newObj.RDSHit();
                }
            }
            else
            {
                newObj.RDSHit();
            }
        }
    }

    /// <summary>
    /// Gets the rds result.
    /// </summary>
    /// <value>The rds result.</value>
    public virtual System.Collections.Generic.IEnumerable<IRDSObject> rdsResult
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

   

    #endregion
}
