using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    public static float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
        {
            return 1f;
        }
        else if (dir < 0f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }
}

public class EnemyHitComparer : IEqualityComparer<RaycastHit>
{
    public bool Equals(RaycastHit hit1, RaycastHit hit2)
    {
        //compare gameobjects name
        if (hit1.collider.gameObject.name == hit2.collider.gameObject.name)
        {
            return true;
        }
        return false;
    }

    public int GetHashCode(RaycastHit obj)
    {
        return obj.collider.gameObject.name.GetHashCode();
    }
}
