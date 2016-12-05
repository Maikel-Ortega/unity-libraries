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

    public double rdsWeight{ get; set;}
    public bool rdsUnique{ get; set;}
    public bool rdsAlways{ get; set;}
    public bool rdsEnabled{ get; set;}
    public RDSTable rdsTable { get; set;}

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

    /// <summary>
    /// Adds to result.
    /// </summary>
    /// <param name="resultList">Result list.</param>
    /// <param name="newObj">New object.</param>
    private void AddToResult(List<IRDSObject> resultList, IRDSObject newObj)
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
                    resultList.AddRange( ((IRDSTable)newObj).GetRdsResult() );
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
    public virtual IEnumerable<IRDSObject> GetRdsResult()
    {
        
        List<IRDSObject> r = new List<IRDSObject>();
        uniqueDrops = new List<IRDSObject>();

        // We call this event just before we add each object to result
        // In this moment they can disable themselves, or change their weight
        foreach(IRDSObject o in mContents)
        {
            o.RDSPreResultEvaluation();
        }

        //Now we add all objects that are set as Always
        //Count will be ignored for this objects!
        foreach(IRDSObject o in mContents.Where( e=> e.rdsAlways && e.rdsEnabled))
        {
            AddToResult(r, o);
        }

        int alwaysCount = mContents.Count(e=> e.rdsAlways && e.rdsEnabled);
        int realDropCount = rdsCount - alwaysCount;

        //We only continue if we didnt fill our count with always-type of items
        if(realDropCount > 0)
        {
            for(int dropCount = 0; dropCount < realDropCount; dropCount++)
            {
                IEnumerable<IRDSObject> dropables = mContents.Where(e => e.rdsEnabled && ! e.rdsAlways);

                double hitvalue = RDSRandom.GetDoubleValue(dropables.Sum(e => e.rdsWeight));

                double curValue = 0;
                foreach (IRDSObject o in dropables)
                {
                    curValue += o.rdsWeight;
                    if( hitvalue < curValue )
                    {
                        //This is the one that got chosen!
                        AddToResult(r, o);
                        break;
                    }
                }
            }
        }

        foreach( IRDSObject o in r)
        {
            o.RDSPostResultEvaluation();
        }

        return r;
    }

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
    /// <param name="indentationlevel">The indentationlevel. 4 blanks at the beginning of each line for each level.</param>
    /// <returns>
    /// A <see cref="System.String"/> that represents this instance.
    /// </returns>
    public string ToString(int indentationlevel)
    {
        string indent = "".PadRight(4 * indentationlevel, ' ');

        string sb = string.Format(indent + "(RDSTable){0} Entries:{1},Prob:{2},UAE:{3}{4}{5}{6}", 
            this.GetType().Name, mContents.Count, rdsWeight,
            (rdsUnique ? "1" : "0"), (rdsAlways ? "1" : "0"), (rdsEnabled ? "1" : "0"), (mContents.Count > 0 ? "\r\n" : ""));

        foreach (IRDSObject o in mContents)
        {
            sb += ("\r\n"+(indent + o.ToString(indentationlevel + 1)));
        }
        return sb.ToString();
    }
    #endregion
}
















