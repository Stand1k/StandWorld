using UnityEngine;

namespace StandWorld.MapGenerator
{
	public class MapDisplay : MonoBehaviour
	{

		public Renderer textureRender;

		public void DrawTexture(Texture2D texture)
		{
			textureRender.sharedMaterial.mainTexture = texture;
			textureRender.transform.localScale = new Vector3 (-texture.width, texture.height, 100);
			Quaternion quaternion = Quaternion.Euler(0f,0f,270f);
			textureRender.transform.rotation = quaternion;
		}
	
	}
}
