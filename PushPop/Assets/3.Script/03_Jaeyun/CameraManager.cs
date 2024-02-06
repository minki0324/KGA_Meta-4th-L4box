using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
	public Image captureImage;
	public Texture2D captureTexture; // camera capture texture

	public void CameraOpen() // button method
    {
		if (NativeCamera.IsCameraBusy()) return;
		TakePicture();
	}

	private void TakePicture()
    {
		string _filePath = Application.persistentDataPath + "/Profile";
		if (!File.Exists(_filePath))
		{
			Directory.CreateDirectory(_filePath);
		}

		NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
		{
			Debug.Log("Image path: " + _filePath);
			if (path != null)
			{
				// Create a Texture2D from the captured image
				Texture2D texture = NativeCamera.LoadImageAtPath(path, 2048);
				if (texture == null)
				{
					Debug.Log("Couldn't load texture from " + path);
					return;
				}

				// camera load
				GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
				quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
				quad.transform.forward = Camera.main.transform.forward;
				quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);

				Material material = quad.GetComponent<Renderer>().material;
				if (!material.shader.isSupported) material.shader = Shader.Find("Legacy Shaders/Diffuse");

				material.mainTexture = texture;
				captureTexture = texture;

				Rect rect = new Rect(0, 0, captureTexture.width, captureTexture.height);
				captureImage.sprite = Sprite.Create(captureTexture, rect, new Vector2(0.5f, 0.5f));

				Texture2D readableTexture = GetReadableTexture(texture);
				Texture2D snap = new Texture2D(readableTexture.width, readableTexture.height);
				snap.SetPixels(readableTexture.GetPixels());
				snap.Apply();

				// File.WriteAllBytes($"{GameManager.instance.UID}_{GameManager.instance.Profile_Index}.png", snap.EncodeToPNG());
				File.WriteAllBytes($"{_filePath}/00_00.png", snap.EncodeToPNG());

				Destroy(quad, 5f);
			}
		}, 2048, true, NativeCamera.PreferredCamera.Front);
    }

	private Texture2D GetReadableTexture(Texture2D source)
    {
		RenderTexture renderTexture = RenderTexture.GetTemporary(
				source.width,
				source.height,
				0,
				RenderTextureFormat.Default,
				RenderTextureReadWrite.Linear
			);

		Graphics.Blit(source, renderTexture);
		RenderTexture previous = RenderTexture.active;
		RenderTexture.active = renderTexture;
		Texture2D readableTexture = new Texture2D(source.width, source.height);

		readableTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
		readableTexture.Apply();
		RenderTexture.active = previous;
		RenderTexture.ReleaseTemporary(renderTexture);
		return readableTexture;
    }
}
