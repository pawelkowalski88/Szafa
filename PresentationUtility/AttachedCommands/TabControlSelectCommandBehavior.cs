using Prism.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PresentationUtility.AttachedCommands
{
    class TabControlSelectCommandBehavior : CommandBehaviorBase<TabControl>
    {
        public TabControlSelectCommandBehavior(TabControl targetObject) : base(targetObject)
        {
            targetObject.SelectionChanged += TargetObject_SelectionChanged;
        }

        private void TargetObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //the command & command parameter are set in the class that creates this behavior
                ExecuteCommand(CommandParameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
