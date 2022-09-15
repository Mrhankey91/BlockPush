using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Utils
{
    public struct ChanceInput
    {
        public float chance;
        public int value;

        public ChanceInput(float chance, int value)
        {
            this.chance = chance;
            this.value = value;
        }
    }

    public static int GetChanceValue(List<ChanceInput> input)
    {
        float random = UnityEngine.Random.Range(0f, 100f);
        float temp = 0f;
        foreach (ChanceInput ci in input)
        {
            temp += ci.chance;
            if (random <= temp)
            {
                return ci.value;
            }
        }
        return 0;
    }

    public static void SetLayer(this GameObject gameObject, string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
        foreach(Transform tf in transforms)
        {
            tf.gameObject.layer = layer;
        }
    }

    public class TArray
    {
        public string id;
    }

    public static Dictionary<string, T> ArrayToDictionary<T>(this T[] array) where T: TArray
    {
        Dictionary<string, T> dict = new Dictionary<string, T>();

        if (array == null)
            return dict;

        for(int i = 0; i < array.Length; ++i)
        {
            dict.Add(array[i].id, array[i]);
        }
        return dict;
    }
}
