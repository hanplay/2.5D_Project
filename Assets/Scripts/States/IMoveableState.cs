﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveableState 
{
    void MoveTo(Vector3 destination);
    void ChaseTarget(Unit targetUnit);

}
