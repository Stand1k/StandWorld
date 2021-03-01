using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Helpers;
using StandWorld.World;
using UnityEngine;

namespace StandWorld.Visuals
{
   public class RegionGroundRenderer : RegionRenderer 
   {
		public RegionGroundRenderer(MapRegion region, Layer layer) : base(region, layer) {}

		public override void BuildMeshes() 
		{
			List<int> neighboursGraphicsList = new List<int>();
			int[] neighboursGraphics = new int[8];
			Color32[] colors = new Color32[9];

			foreach (Vector2Int position in region.regionRect)
			{
				neighboursGraphicsList.Clear();
				Tilable tilable = region.map[position].GetTilable(layer); // Ground

				MeshData currentMesh = GetMesh(tilable.mainGraphic.uId, false, (MeshFlags.Base | MeshFlags.Color));
				int vIndex = currentMesh.vertices.Count;
				float z = tilable.mainGraphic.drawPriority;

				currentMesh.vertices.Add(new Vector3(position.x, position.y, z));
				currentMesh.vertices.Add(new Vector3(position.x, position.y + 1, z));
				currentMesh.vertices.Add(new Vector3(position.x+1, position.y + 1, z));
				currentMesh.vertices.Add(new Vector3(position.x+1, position.y, z));
				currentMesh.colors.Add(Color.white);
				currentMesh.colors.Add(Color.white);
				currentMesh.colors.Add(Color.white);
				currentMesh.colors.Add(Color.white);
				currentMesh.AddTriangle(vIndex, 0, 1, 2);
				currentMesh.AddTriangle(vIndex, 0, 2, 3);

				for (int i = 0; i < DirectionUtils.neighbours.Length; i++) 
				{
					Tile neighbourTile = region.map[position + DirectionUtils.neighbours[i]];

					if (neighbourTile != null)
					{
						Tilable neighbourGround = neighbourTile.GetTilable(layer); // Сусідній ground
						neighboursGraphics[i] = neighbourGround.mainGraphic.uId;
						
						if (
							!neighboursGraphicsList.Contains(neighbourGround.mainGraphic.uId) &&
							neighbourGround.mainGraphic.uId != tilable.mainGraphic.uId &&
							neighbourGround.def.groundDef.maxHeight >= tilable.def.groundDef.maxHeight
						) 
						{
							neighboursGraphicsList.Add(neighbourGround.mainGraphic.uId);
						}
					} 
					else 
					{
						neighboursGraphics[i] = tilable.mainGraphic.uId;
					}
				}

				foreach (int graphicUID in neighboursGraphicsList)
				{
					currentMesh = GetMesh(graphicUID, false, (MeshFlags.Base | MeshFlags.Color));
					vIndex = currentMesh.vertices.Count;
					z = GraphicInstance.instances[graphicUID].drawPriority;
                    
                    currentMesh.vertices.Add(new Vector3(position.x + 0.5f, position.y, z));			// 0
                    currentMesh.vertices.Add(new Vector3(position.x, position.y, z));					// 1
                    currentMesh.vertices.Add(new Vector3(position.x, position.y + 0.5f, z));			// 2
                    currentMesh.vertices.Add(new Vector3(position.x, position.y + 1, z));				// 3
                    currentMesh.vertices.Add(new Vector3(position.x + 0.5f, position.y + 1, z));	// 4
                    currentMesh.vertices.Add(new Vector3(position.x + 1, position.y + 1, z));		// 5
                    currentMesh.vertices.Add(new Vector3(position.x + 1, position.y + 0.5f, z));	// 6
                    currentMesh.vertices.Add(new Vector3(position.x + 1, position.y, z));				// 7
                    currentMesh.vertices.Add(new Vector3(position.x + 0.5f, position.y + 0.5f, z));	// 8

                    for (int i = 0; i < colors.Length; i++)
                    {
                    	colors[i] = Color.clear;
                    }

                    for (int i = 0; i < 8; i++)
                    {
                    	if (i % 2 != 0) {
                    		if (graphicUID == neighboursGraphics[i])
                            {
                    			colors[i] = Color.white;
                    		}
                    	} 
                        else 
                        {
                    		if (graphicUID == neighboursGraphics[i])
                            {
                              switch (i)
                                {
                                    case 0: // Південний
                                        colors[1] = Color.white;
                                        colors[0] = Color.white;
                                        colors[7] = Color.white;
                                        break;
                                    case 2:  // Західний
                                        colors[1] = Color.white;
                                        colors[2] = Color.white;
                                        colors[3] = Color.white;
                                        break;
                                    case 4: // Північний
                                        colors[3] = Color.white;
                                        colors[4] = Color.white;
                                        colors[5] = Color.white;
                                        break;
                                    case 6: // Східний
                                        colors[5] = Color.white;
                                        colors[6] = Color.white;
                                        colors[7] = Color.white;
                                        break;
                                }
                    		}
                    	}
                    }

                    currentMesh.colors.AddRange(colors);
                    currentMesh.AddTriangle(vIndex, 0, 8, 6);
                    currentMesh.AddTriangle(vIndex, 0, 6, 7);
                    currentMesh.AddTriangle(vIndex, 1, 8, 0);
                    currentMesh.AddTriangle(vIndex, 1, 2, 8);
                    currentMesh.AddTriangle(vIndex, 2, 4, 8);
                    currentMesh.AddTriangle(vIndex, 2, 3, 4);
                    currentMesh.AddTriangle(vIndex, 8, 5, 6);
                    currentMesh.AddTriangle(vIndex, 8, 4, 5);
				}
			}

			foreach (MeshData meshData in meshes.Values)
			{
				meshData.Build();
			}
		}
	}
}
