using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MaterialFromTexture
{
    [MenuItem("Tools/Create Material(s) from Texture(s)")]
    static void CreateMaterialFromTexture()
    {
        if (Selection.objects.Length > 0)
        {
            Object[] textures = Selection.objects;
            Shader shader = Shader.Find("DT_Shaders/Toon_DoubleSided");
            foreach (Object obj in textures)
            {
                if (obj is Texture)
                {
                    Texture texture = obj as Texture;
                    Material material = new Material(shader);
                    material.SetTexture("_DiffuseMap", texture);
                    material.SetColor("_DiffuseAmbient", new Color32(255, 255, 255, 255));
                    material.SetFloat("_OutlineWidth", 0.007f);
                    material.SetColor("_OutlineColor", new Color32(0, 0, 0, 255));
                    material.SetColor("Color", new Color32(0, 0, 0, 0));
                    AssetDatabase.CreateAsset(material, "Assets/Materials/Equipamentos/" + texture.name + "_MAT.mat");
                }
            } 
        }
        else
        {
            Debug.Log("Selecione um item ao menos!");
        }
    }    
}
