﻿using System;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.World;

namespace StandWorld.Characters.AI.Node
{
    public class CutNode : BrainNodeCondition
    {
        private class CutPlantAtPosition : BrainNode
        {
            public override Task GetTask()
            {
                Tilable tilable = WorldUtilsPlant.Instance.NextToCut(character.position);
                if (tilable != null)
                {
                    return new Task(
                        Defs.tasks["task_cut"],
                        new TargetList(new Target(tilable))
                    );
                }

                return null;
            }
        }


        public CutNode(Func<bool> condition) : base(condition)
        {
            subNodes.Add(new CutPlantAtPosition());
        }
    }
}