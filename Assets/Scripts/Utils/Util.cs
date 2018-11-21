using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Util {

    public static void SaveSpriteToFile(Sprite sprite, string fileName)
    {        
        Texture2D savetex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.ARGB32, false);
        savetex.SetPixels(0, 0, (int)sprite.rect.width, (int)sprite.rect.height, sprite.texture.GetPixels((int)sprite.rect.x, (int)sprite.rect.y, (int)sprite.rect.width, (int)sprite.rect.height));              
        savetex.Apply();
        byte[] bytes = savetex.EncodeToPNG();     
        
        File.WriteAllBytes(Application.dataPath + "/ImageFont/" + fileName, bytes);
    }
    public static Texture2D LoadPNG(string filePath)
    {        
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {            
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2, TextureFormat.BGRA32, false);
            tex.LoadImage(fileData); 
        }
        return tex;
    }
    public static GameObject FindChildObject(GameObject go, string name)
    {
        foreach (Transform tr in go.transform)
        {
            if (tr.name.Equals(name))
            {
                return tr.gameObject;
            }
            else
            {
                GameObject find = FindChildObject(tr.gameObject, name);
                if (find != null)
                {
                    return find;
                }
            }
        }

        return null;
    }
    public static void InitTransform(Transform trans)
    {
        trans.localPosition = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = Vector3.one;
    }
    public static T ToEnum<T>(string str)
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        foreach (T t in A)
        {
            if (t.ToString() == str)
                return t;
        }
        return default(T);
    }
    public static int Rand(int min, int max)
    {
        return Random.Range(min, max + 1);
    }

    public static float Rand(float min, float max)
    {
        return Random.Range(min, max);
    }
    public static int GetPriority(int[] priorities)
    {
        int sum = 0;
        for (int i = 0; i < priorities.Length; ++i)
        {
            sum += priorities[i];
        }

        if (sum <= 0)
            return 0;

        int num = Rand(1, sum);

        sum = 0;
        for (int i = 0; i < priorities.Length; ++i)
        {
            int start = sum;
            sum += priorities[i];
            if (start < num && num <= sum)
            {
                return i;
            }
        }

        return 0;
    }
}
