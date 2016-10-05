using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IRDSTable : IRDSObject
{
    int rdsCount                        { get; set; }
    IEnumerable<IRDSObject> rdsContents { get; }
    IEnumerable<IRDSObject> GetRdsResult();
}
