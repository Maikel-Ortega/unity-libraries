using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class RDSScriptableProperty
{
    public float rdsWeight;
    public bool rdsUnique;
    public bool rdsAlways;
    public bool rdsEnabled;
    public RDSScriptableObject rdsObject;
    public event System.Action<RDSScriptableProperty> OnRdsPreResultEvaluation;
    public event System.Action<RDSScriptableProperty> OnRdsHit;
    public event System.Action<RDSScriptableProperty> OnRdsPostResultEvaluation;

    public void RDSPreResultEvaluation(){if(OnRdsPreResultEvaluation != null) {OnRdsPreResultEvaluation(this);}}
    public void RDSHit(){if(OnRdsHit != null) {OnRdsHit(this);}}
    public void RDSPostResultEvaluation(){if(OnRdsPostResultEvaluation != null) {OnRdsPostResultEvaluation(this);}}

}

[CreateAssetMenu()]
public class RDSScriptableTable : RDSScriptableObject 
{
   

    public List<RDSScriptableProperty> mContents;
    public int rdsCount; //How many objects drop


    #region results
    private List<RDSScriptableProperty> uniqueDrops = new List<RDSScriptableProperty>();
    /// <summary>
    /// Adds to result.
    /// </summary>
    /// <param name="resultList">Result list.</param>
    /// <param name="newObj">New object.</param>
    private void AddToResult(List<RDSScriptableProperty> resultList, RDSScriptableProperty newObj)
    {
        if (!newObj.rdsUnique || !uniqueDrops.Contains(newObj))
        {
            if(newObj.rdsUnique)
            {
                uniqueDrops.Add(newObj);
            }

            if(!(newObj is RDSNull))
            {
                if( newObj.rdsObject is RDSScriptableTable)
                {
                    resultList.AddRange( ((RDSScriptableTable)newObj.rdsObject).GetRdsResult() );
                }
                else
                {
                    RDSScriptableProperty toAdd = newObj;
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

    public virtual List<RDSScriptableProperty> GetRdsResult()
    {

        List<RDSScriptableProperty> r = new List<RDSScriptableProperty>();
        uniqueDrops = new List<RDSScriptableProperty>();

        // We call this event just before we add each object to result
        // In this moment they can disable themselves, or change their weight
        foreach(RDSScriptableProperty o in mContents)
        {
            o.RDSPreResultEvaluation();
        }

        //Now we add all objects that are set as Always
        //Count will be ignored for this objects!
        foreach(RDSScriptableProperty o in mContents.Where( e=> e.rdsAlways && e.rdsEnabled))
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
                IEnumerable<RDSScriptableProperty> dropables = mContents.Where(e => e.rdsEnabled && ! e.rdsAlways);

                double hitvalue = RDSRandom.GetDoubleValue(dropables.Sum(e => e.rdsWeight));

                double curValue = 0;
                foreach (RDSScriptableProperty o in dropables)
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

        foreach( RDSScriptableProperty o in r)
        {
            o.RDSPostResultEvaluation();
        }

        return r;
    }

    #endregion
}
