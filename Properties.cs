using UnityEngine;
using System.Collections.Generic;
using System.Collections;

class Properties : MonoBehaviour
{
  public static float Speed {get; protected set;} = 0.1f; // this Speed value for move (can't set value in other class)
  public static float RotateSpeed {get; protected set;} = 10f; // Rotatespeed value for Rotate (can't set value in other class)
}
