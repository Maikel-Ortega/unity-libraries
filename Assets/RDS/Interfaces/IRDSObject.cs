using UnityEngine;
using System.Collections;
using System;

public interface IRDSObject 
{
    /// <summary>
    /// Occurs before the weights of the table are summed up together
    /// This is the right moment to modifies those weights
    /// </summary>
    event Action<IRDSObject> OnRdsPreResultEvaluation;

    /// <summary>
    /// Occurs when this RDSObject is hit
    /// </summary>
    event Action<IRDSObject> OnRdsHit;

    /// <summary>
    /// Occurs after the result has been calculated, but before the Result method exits
    /// </summary>
    event Action<IRDSObject> OnRdsPostResultEvaluation;

    double rdsWeight    { get; set; }   //Chance for the item to drop
    bool rdsUnique      { get; set; }    //Only drops once for each throw 
    bool rdsAlways      { get; set; }    //Drop always
    bool rdsEnabled     { get; set; }    //If not enabled, won't drop
    /// <summary>
    /// Gets or sets the table this Object belongs to.
    /// Note to inheritors: This property has to be auto-set when an item is added to a table via the AddEntry method.
    /// </summary>
    RDSTable rdsTable { get; set; }

    void RDSPreResultEvaluation();
    void RDSHit();
    void RDSPostResultEvaluation();

    string ToString(int indentationLevel);

}
