using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Create StatusData")]
public class PlayerStatus : ScriptableObject
{
    public float gravitationalAcceleration;    // 重力加速度の大きさを指定
    public bool isIertia;                      // 慣性かどうか
    public float maxSpeed;                     // 移動速度の大きさを指定
    
}
