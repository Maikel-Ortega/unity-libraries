using UnityEngine;
using System.Collections;

public interface IRDSValue<T> : IRDSObject
{
    T rdsValue { get; }
}
