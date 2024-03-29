﻿using StandWorld.Definitions;
using StandWorld.Helpers;
using StandWorld.Visuals;
using UnityEngine;

namespace StandWorld.Characters
{
    public class HumanSkin
    {
        public HumanSkinData skinData { get; protected set; }
        public GraphicInstance hairGraphic { get; protected set; }
        public GraphicInstance headGraphic { get; protected set; }
        public GraphicInstance eyesGraphic { get; protected set; }
        public GraphicInstance bodyGraphic { get; protected set; }
        public GraphicInstance clothesGraphic { get; protected set; }
        public Human human { get; protected set; }
        public Vector2 size { get; protected set; }

        public HumanSkin(Human human, Vector2 size)
        {
            this.human = human;
            this.size = size;
            Randomize();
            UpdateLookingAt(Direction.S);
        }

        public void UpdateLookingAt(Direction direction)
        {
            bodyGraphic = GraphicInstance.GetNew(
                human.def.graphics,
                Defs.colorPallets["human_body"].colors[skinData.bodyColorID],
                Res.textures["Body_" + (skinData.bodyID)],
                1,
                MeshPool.GetHumanPlaneMesh(size, direction)
            );
            clothesGraphic = GraphicInstance.GetNew(
                human.def.graphics,
                Defs.colorPallets["human_clothes"].colors[skinData.clothesColorID],
                Res.textures["Clothes_" + (skinData.clothesID)],
                2,
                MeshPool.GetHumanPlaneMesh(size, direction)
            );
            headGraphic = GraphicInstance.GetNew(
                human.def.graphics,
                Defs.colorPallets["human_body"].colors[skinData.bodyColorID],
                Res.textures["Head_" + (skinData.headID)],
                3,
                MeshPool.GetHumanPlaneMesh(size, direction)
            );
            eyesGraphic = GraphicInstance.GetNew(
                human.def.graphics,
                Color.white,
                Res.textures["Eye_" + (skinData.eyeID)],
                4,
                MeshPool.GetHumanPlaneMesh(size, direction)
            );
            hairGraphic = GraphicInstance.GetNew(
                human.def.graphics,
                Defs.colorPallets["human_hair"].colors[skinData.hairColorID],
                Res.textures["Hair_" + (skinData.hairID)],
                4,
                MeshPool.GetHumanPlaneMesh(size, direction)
            );
        }

        private Vector3 GetVisualPosition(float drawPriority)
        {
            return human.movement.visualPosition + new Vector3(0, 0, drawPriority);
        }

        public void UpdateDraw()
        {
            Graphics.DrawMesh(
                bodyGraphic.mesh,
                GetVisualPosition(bodyGraphic.drawPriority),
                Quaternion.identity,
                bodyGraphic.material,
                0
            );
            Graphics.DrawMesh(
                clothesGraphic.mesh,
                GetVisualPosition(clothesGraphic.drawPriority),
                Quaternion.identity,
                clothesGraphic.material,
                0
            );
            Graphics.DrawMesh(
                headGraphic.mesh,
                GetVisualPosition(headGraphic.drawPriority),
                Quaternion.identity,
                headGraphic.material,
                0
            );
            Graphics.DrawMesh(
                eyesGraphic.mesh,
                GetVisualPosition(eyesGraphic.drawPriority),
                Quaternion.identity,
                eyesGraphic.material,
                0
            );
            Graphics.DrawMesh(
                hairGraphic.mesh,
                GetVisualPosition(hairGraphic.drawPriority),
                Quaternion.identity,
                hairGraphic.material,
                0
            );
        }

        public void Randomize()
        {
            skinData = new HumanSkinData(true);
        }
    }
}