using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SeaScooter", menuName = "Upgrades/SeaScooter", order = 0)]
public class SeaScooter : Upgrade
{
    /*  public SeaScooter(Upgrade upgrade)
      {
          tier = upgrade.tier;
          bottleName = upgrade.scooterName;
          capacity = upgrade.speed;
          mesh = upgrade.mesh;
      }
  */
    public int tier;
    public string scooterName;
    public float speed;
    public Mesh mesh;
}
