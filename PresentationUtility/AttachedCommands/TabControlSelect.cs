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
    public class TabControlSelect
    {
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
                typeof(TabControlSelect),
                new PropertyMetadata(OnSetCommandCallback));


        public static object GetCommandParameterProperty(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandParameterProperty);
        }

        public static void SetCommandParameterProperty(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameterProperty",
                typeof(object),
                typeof(TabControlSelect),
                new PropertyMetadata(OnSetCommandParameterCallback));


        private static readonly DependencyProperty SelectCommandBehaviorProperty =
            DependencyProperty.RegisterAttached("SetCommandBehaviorProperty",
                typeof(object),
                typeof(TabControlSelect),
                null);

        private static void OnSetCommandCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TabControl tb = d as TabControl;

            if(tb != null)
            {
                TabControlSelectCommandBehavior behavior =
                    GetOrCreateBehavior(tb);
                behavior.Command = e.NewValue as ICommand;
            }
                
        }

        private static void OnSetCommandParameterCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TabControl tb = d as TabControl;

            if (tb != null)
            {
                TabControlSelectCommandBehavior behavior =
                    GetOrCreateBehavior(tb);
                behavior.CommandParameter = e.NewValue;
            }
        }

        private static TabControlSelectCommandBehavior GetOrCreateBehavior(TabControl tb)
        {
            TabControlSelectCommandBehavior behavior =
                tb.GetValue(SelectCommandBehaviorProperty) as TabControlSelectCommandBehavior;

            if(behavior == null)
            {
                behavior = new TabControlSelectCommandBehavior(tb);
                tb.SetValue(SelectCommandBehaviorProperty, behavior);
            }
            return behavior;
        }
    }
}
