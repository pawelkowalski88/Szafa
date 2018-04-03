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
    public class TextBoxTextChangedCommandBehavior : CommandBehaviorBase<TextBox>
    {
        public TextBoxTextChangedCommandBehavior(TextBox targetObject) : base(targetObject)
        {
            targetObject.TextChanged += TargetObject_TextChanged;
        }

        private void TargetObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ExecuteCommand(CommandParameter);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
