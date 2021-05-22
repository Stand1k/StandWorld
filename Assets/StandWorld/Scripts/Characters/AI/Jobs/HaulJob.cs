using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Game;

namespace StandWorld.Characters.AI.Jobs
{
    public struct HaulResult
    {
        public Job get;
        public int qty;

        public HaulResult(Job get, int qty = 0)
        {
            this.get = get;
            this.qty = qty;
        }
    }

    public class HaulJob : JobBase
    {
        public HaulJob(BaseCharacter character, Task task) : base(character, task)
        {
            jobs = Haul(character, task);
            Next(false);
        }

        public static HaulResult Get(BaseCharacter character, Task task, int qty = 1)
        {
            Job get = new Job(
                () => (
                    character.inventory.def == null ||
                    (character.inventory.free > 0 &&
                     character.inventory.def == task.targets.current.tilable.tilableDef)
                )
            );
            get.OnEnd = () =>
            {
                Stackable stack = (Stackable) ToolBox.Instance.map.grids[Layer.Stackable].GetTilableAt(task.targets.current.position);
                if (stack == null || stack.inventory.count == 0)
                {
                    task.state = TaskState.Failed;
                    return;
                }

                stack.inventory.TransfertTo(character.inventory, qty);
            };

            Stackable _stack = (Stackable) ToolBox.Instance.map.grids[Layer.Stackable].GetTilableAt(task.targets.current.position);
            if (_stack != null)
            {
                return new HaulResult(get, _stack.inventory.count);
            }

            return new HaulResult(get);
        }

        public static Queue<Job> Haul(BaseCharacter character, Task task)
        {
            Queue<Job> jobs = new Queue<Job>();
            Job put = new Job(
                () => character.inventory.count > 0
            );
            return jobs;
        }
    }
}