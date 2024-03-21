using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Create StatusData")]
public class PlayerStatus : ScriptableObject
{
    public float gravitationalAcceleration;    // 重力加速度の速さを指定
    public float maxSpeed;                   // 移動速度の速さを指定
    
}
