using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IRDSTable : IRDSObject
{
    int rdsCount                        { get; set; }   // How many items shall drop from this table?
    IEnumerable<IRDSObject> rdsContents { get; }        // The contents of the table
    IEnumerable<IRDSObject> GetRdsResult();
}
