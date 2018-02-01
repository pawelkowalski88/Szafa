using Prism.Interactivity;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PresentationUtility.AttachedCommands
{
    class ListViewSelectCommandBehavior : CommandBehaviorBase<ListView>
    {
        public ListViewSelectCommandBehavior(ListView targetObject) : base(targetObject)
        {
            //Adds the event handler that executes the command bound in the view class
            targetObject.SelectionChanged += TargetObject_SelectionChanged;
        }

        private void TargetObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //the command & command parameter are set in the class that creates this behavior
                ExecuteCommand(CommandParameter);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
