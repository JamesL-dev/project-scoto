using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePuzzle : MonoBehaviour
{
    [SerializeField] protected bool isActive = false;
    [SerializeField] protected float frequency = 0.5f;
    [SerializeField] protected int delay = 300;
}
