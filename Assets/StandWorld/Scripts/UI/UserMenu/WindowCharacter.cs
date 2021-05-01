using StandWorld.Characters;
using StandWorld.Definitions;

namespace StandWorld.UI
{
    public class WindowCharacter : Window
    {
        private BaseCharacter _character;

        public WindowCharacter(BaseCharacter character)
        {
            _character = character;
            SetTitle(_character.name);
            AddTab("Character");
            AddTab("Infos");
            AddTab("Inventory");
        }

        public override void Content()
        {
            if (activeTab == 0)
            {
                if (_character.brain.currentTask != null)
                {
                    vGrid.Span(_character.brain.currentTask.def.uId);
                }
                else
                {
                    vGrid.Span("Халтурить ...");
                }

                vGrid.H2("Vitals");
                foreach (Vital vital in _character.stats.vitals.Values)
                {
                    WindowComponents.FillableBarWithLabelValue(
                        vGrid.GetNewRect(30f),
                        vital.name,
                        vital,
                        Defs.namedColorPallets["cols_vitals"].colors[vital.name]
                    );
                }

                vGrid.H2("Stats");
                foreach (Stat stat in _character.stats.stats.Values)
                {
                    WindowComponents.SimpleStat(vGrid.GetNewRect(25f), stat.name, stat.value, stat.baseValue);
                }
            }
            else if (activeTab == 1)
            {
                vGrid.H2("Info");
                vGrid.Paragraph(_character.def.shortDescription);
                vGrid.H2("Detailled Stats");
                foreach (Attribute attr in _character.stats.attributes.Values)
                {
                    WindowComponents.SimpleStat(vGrid.GetNewRect(25f), attr.name, attr.value, attr.baseValue);
                }
            }
            else if (activeTab == 2)
            {
                vGrid.H2("Inventory");
                if (_character.inventory.def != null)
                {
                    vGrid.Paragraph(_character.inventory.def.name + " : " + _character.inventory.count + "/" +
                                    _character.inventory.max);
                }
                else
                {
                    vGrid.Paragraph("Empty");
                }
            }
        }
    }
}