using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;

public static class Extensions
{
    public static Vector3 Random(this Vector3 v, bool doubleSided = false)
    {
        if (doubleSided)
            return new Vector3(UnityEngine.Random.Range(-v.x, v.x), UnityEngine.Random.Range(-v.y, v.y), UnityEngine.Random.Range(-v.z, v.z));
        else
            return new Vector3(UnityEngine.Random.value * v.x, UnityEngine.Random.value * v.y, UnityEngine.Random.value * v.z);
    }

    public static Enum Random(this Type t, params int[] excluding)
    {
        return Enum.GetValues(t)          // get values from Type provided            
            .OfType<Enum>()               // casts to Enum
            .Where(v => !excluding.Contains(Convert.ToInt32(v)))
            .OrderBy(e => UnityEngine.Random.value) // mess with order of results
            .FirstOrDefault();            // take first item in result
    }
    public static T Random<T>(this List<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static T Random<T>(this List<T> list, Func<T, float> weight)
    {   
        var winner = UnityEngine.Random.Range(0f, list.Sum(weight));
        foreach(var item in list)
        {
            winner -= weight(item);
            if (winner <= 0)
                return item;
        }
        throw new UnityException("Random function is borked. Whoops.");
    }

    public static T Random<T>(this IEnumerable<T> list)
    {
        return list.ElementAt(UnityEngine.Random.Range(0, list.Count()));
    }

    /// <summary>
    /// Moves an entire game object to a new layer
    /// </summary>
    /// <param name="root"></param>
    /// <param name="layer"></param>
    public static void MoveToLayer(this Transform root, int layer, params string[] excludedSubObjects)
    {
        if (!excludedSubObjects.Contains(root.name))
        {
            root.gameObject.layer = layer;
            foreach (Transform child in root)
                MoveToLayer(child, layer, excludedSubObjects);
        }
    }

    /// <summary>
    /// Extension method to check if a layer is in a layermask
    /// </summary>
    /// <param name="mask"></param>
    /// <param name="layer"></param>
    /// <returns></returns>
    public static bool Contains(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    //Breadth-first search
    public static Transform FindDeepChild(this Transform aParent, string aName)
    {
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(aParent);
        while (queue.Count > 0)
        {
            var c = queue.Dequeue();
            if (c.name == aName)
                return c;
            foreach (Transform t in c)
                queue.Enqueue(t);
        }
        return null;
    }

    public static T GetCopyOf<T>(this Component comp, T other) where T : Component
    {
        Type type = comp.GetType();
        if (type != other.GetType()) return null; // type mis-match
        BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
        PropertyInfo[] pinfos = type.GetProperties(flags);
        foreach (var pinfo in pinfos)
        {
            if (pinfo.CanWrite)
            {
                try
                {
                    pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                }
                catch { } // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anything specific.
            }
        }
        FieldInfo[] finfos = type.GetFields(flags);
        foreach (var finfo in finfos)
        {
            finfo.SetValue(comp, finfo.GetValue(other));
        }
        return comp as T;
    }

    public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component
    {
        return go.AddComponent<T>().GetCopyOf(toAdd) as T;
    }

    public static bool HasParameter(this Animator animator, string paramName)
    {
        for (int i = 0; i < animator.parameterCount; i++)
        {
            if (animator.parameters[i].name == paramName) return true;
        }
        return false;
    }

}