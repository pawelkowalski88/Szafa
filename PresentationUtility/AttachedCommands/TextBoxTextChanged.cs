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
    public class TextBoxTextChanged
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
                typeof(TextBoxTextChanged),
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
                typeof(TextBoxTextChanged),
                new PropertyMetadata(OnSetCommandParameterCallback));  
        
        //SetCommandBehavior DependencyProperty
        private static readonly DependencyProperty TextChangedCommandBehaviorProperty =
            DependencyProperty.RegisterAttached("SetCommandBehaviorProperty",
                typeof(object),
                typeof(TextBoxTextChanged),
                null);

        private static void OnSetCommandCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox tb = d as TextBox;

            if(tb != null)
            {
                TextBoxTextChangedCommandBehavior behavior = GetOrCreateBehavior(tb);
                behavior.Command = e.NewValue as ICommand;
            }
        }

        private static void OnSetCommandParameterCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox tb = d as TextBox;

            if (tb != null)
            {
                TextBoxTextChangedCommandBehavior behavior = GetOrCreateBehavior(tb);
                behavior.CommandParameter = e.NewValue;
            }
        }

        private static TextBoxTextChangedCommandBehavior GetOrCreateBehavior(TextBox tb)
        {
            TextBoxTextChangedCommandBehavior behavior =
                tb.GetValue(TextChangedCommandBehaviorProperty) as TextBoxTextChangedCommandBehavior;

            if (behavior == null)
            {
                behavior = new TextBoxTextChangedCommandBehavior(tb);
                tb.SetValue(TextChangedCommandBehaviorProperty, behavior);
            }

            return behavior;
        }
    }
}
