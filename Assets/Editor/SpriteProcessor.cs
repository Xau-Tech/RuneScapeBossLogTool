using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//  Automatically convert any imported textures in the Resources/RareItems path to sprites
public class SpriteProcessor : AssetPostprocessor
{
    void OnPostprocessTexture(Texture2D texture)
    {
        string lowerCaseAssetPath = assetPath.ToLower();
        bool isInSpritesDirectory = lowerCaseAssetPath.IndexOf("/resources/rareitems/") != -1;

        if (isInSpritesDirectory)
        {
            TextureImporter textureImporter = (TextureImporter)assetImporter;
            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.alphaIsTransparency = true;
            textureImporter.alphaSource = TextureImporterAlphaSource.FromInput;
            texture.Apply();
        }
    }
}
