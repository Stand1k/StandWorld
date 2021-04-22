using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Game;

namespace StandWorld.Characters.AI.Jobs
{
    public class HaulRecipeJob : JobBase
    {
        public HaulRecipeJob(BaseCharacter character, Task task) : base(character, task)
        {
            jobs = Haul(character, task);
            OnEnd += this.character.DropOnTheFloor;
            Next(false);
        }

        public static Queue<Job> Haul(BaseCharacter character, Task task)
        {
            Queue<Job> jobs = new Queue<Job>();
            Job job = new Job(
                () => character.inventory.count > 0
            );

            job.OnEnd = () =>
            {
                Building building = (Building) ToolBox.map.GetTilableAt(task.targets.current.position, Layer.Building);

                Recipe recipe = building.recipe;

                if (recipe.needs[character.inventory.def].full == false)
                {
                    character.inventory.TransfertTo(recipe.needs[character.inventory.def],
                        recipe.needs[character.inventory.def].max);
                }
            };

            HaulResult res = HaulJob.Get(character, task, character.inventory.free);
            jobs.Enqueue(res.get);

            List<Target> targetList = task.targets.ToList();
            foreach (Target target in targetList)
            {
                if (target.tilable is Stackable)
                {
                    res = HaulJob.Get(character, task, character.inventory.free);
                    jobs.Enqueue(res.get);
                }
                else
                {
                    jobs.Enqueue(job);
                }
            }

            return jobs;
        }
    }
}