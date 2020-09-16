using UnityEngine;
using System.Collections.Generic;
using System.Collections;

class Properties : MonoBehaviour
{
  public static float Speed {get; set;} = 0.1f;
  public static float RotateSpeed {get; protected set;} = 100f;
  public static float JumpSpeed {get; protected set;} = 2.5f;
}
