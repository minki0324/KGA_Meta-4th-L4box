
using UnityEngine;
using UnityEditor;

public class SpriteAssetPostprocessor : AssetPostprocessor
{
    private void OnPostprocessTexture(Texture2D texture)
    {
        TextureImporter textureImporter = (TextureImporter)assetImporter;

        ModifySpriteAlphaGammaToLinear(textureImporter, texture);
    }

    private static void ModifySpriteAlphaGammaToLinear(TextureImporter textureImporter, Texture2D texture)
    {
        if (textureImporter.textureType != TextureImporterType.Sprite ||
            PlayerSettings.colorSpace != ColorSpace.Linear) return;
        if (texture.name.Contains("Linear") == false) return;

        Color[] pixels = texture.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            Color pixel = pixels[i];
            // pixel.a = Mathf.Pow(pixel.a, 2.2f);
            pixel.a = GammaToLinearSpace(pixel.a);
            pixels[i] = pixel;
        }
        texture.SetPixels(pixels);
        texture.Apply();
        Debug.Log(texture.name);
    }

    static float GammaToLinearSpace(float sRGB)
    {
        // Approximate version from http://chilliant.blogspot.com.au/2012/08/srgb-approximations-for-hlsl.html?m=1
        return sRGB * (sRGB * (sRGB * 0.305306011f + 0.682171111f) + 0.012522878f);

        // Precise version, useful for debugging.
        //return half3(GammaToLinearSpaceExact(sRGB.r), GammaToLinearSpaceExact(sRGB.g), GammaToLinearSpaceExact(sRGB.b));
    }
}
