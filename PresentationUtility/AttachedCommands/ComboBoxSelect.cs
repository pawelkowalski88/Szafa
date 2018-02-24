using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PresentationUtility.AttachedCommands
{
    public class ComboBoxSelect
    {
        //Command DependencyProperty
        public static ICommand GetCommandProperty(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommandProperty(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("CommandProperty",
                typeof(ICommand),
                typeof(ComboBoxSelect),
                new PropertyMetadata(OnSetCommandCallback));


        //CommandParameter DependencyProperty
        public static object GetCommandParameterProperty(DependencyObject obj)
        {
            return obj.GetValue(CommandParameterProperty);
        }

        public static void SetCommandParameterProperty(DependencyObject obj, object value)
        {
            obj.SetValue(CommandParameterProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameterProperty",
                typeof(object),
                typeof(ComboBoxSelect),
                new PropertyMetadata(OnSetCommandParameterCallback));


        //SetCommandBehavior DependencyProperty
        private static readonly DependencyProperty SelectCommandBehaviorProperty =
            DependencyProperty.RegisterAttached("SetCommandBehaviorProperty",
                typeof(object),
                typeof(ComboBoxSelect),
                null);


        //EventHandlers
        private static void OnSetCommandCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ComboBox cb = d as ComboBox;

            if (cb != null)
            {
                //Acquire behavior attached to the list view and set the command
                //The behavior class executes the command
                ComboBoxSelectCommandBehavior behavior =
                    GetOrCreateBehavior(cb);
                behavior.Command = e.NewValue as ICommand;
            }
        }

        private static void OnSetCommandParameterCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ComboBox cb = d as ComboBox;

            if (cb != null)
            {
                //Acquire behavior attached to the list view and set the command parameter
                //The behavior class executes the command
                ComboBoxSelectCommandBehavior behavior =
                    GetOrCreateBehavior(cb);
                behavior.CommandParameter = e.NewValue;
            }
        }

        private static ComboBoxSelectCommandBehavior GetOrCreateBehavior(ComboBox cb)
        {
            //First get the behavior, maybe it is already created
            ComboBoxSelectCommandBehavior behavior =
                cb.GetValue(SelectCommandBehaviorProperty) as ComboBoxSelectCommandBehavior;

            if (behavior == null)
            {
                //if the behavior is not created then create one
                behavior = new ComboBoxSelectCommandBehavior(cb);
                cb.SetValue(SelectCommandBehaviorProperty, behavior);
            }
            //return the behavior
            //Caller function sets the command or command parameter properties
            return behavior;
        }
    }
}
