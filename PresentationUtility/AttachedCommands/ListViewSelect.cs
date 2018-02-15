using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PresentationUtility.AttachedCommands
{
    //This class attaches two public and one private dependency property to handle object selection on the ListView class
    public class ListViewSelect
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
                typeof(ListViewSelect), 
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
                typeof(ListViewSelect), 
                new PropertyMetadata(OnSetCommandParameterCallback));


        //SetCommandBehavior DependencyProperty
        private static readonly DependencyProperty SelectCommandBehaviorProperty =
            DependencyProperty.RegisterAttached("SetCommandBehaviorProperty", 
                typeof(object), 
                typeof(ListViewSelect), 
                null);


        //EventHandlers
        private static void OnSetCommandCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ListView lv = d as ListView;

            if(lv != null)
            {
                //Acquire behavior attached to the list view and set the command
                //The behavior class executes the command
                ListViewSelectCommandBehavior behavior = 
                    GetOrCreateBehavior(lv);
                behavior.Command = e.NewValue as ICommand;
            }
        }

        private static void OnSetCommandParameterCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ListView lv = d as ListView;
            
            if(lv != null)
            {
                //Acquire behavior attached to the list view and set the command parameter
                //The behavior class executes the command
                ListViewSelectCommandBehavior behavior =
                    GetOrCreateBehavior(lv);
                behavior.CommandParameter = e.NewValue;
            }
        }

        private static ListViewSelectCommandBehavior GetOrCreateBehavior(ListView lv)
        {
            //First get the behavior, maybe it is already created
            ListViewSelectCommandBehavior behavior =
                lv.GetValue(SelectCommandBehaviorProperty) as ListViewSelectCommandBehavior;

            if(behavior == null)
            {
                //if the behavior is not created then create one
                behavior = new ListViewSelectCommandBehavior(lv);
                lv.SetValue(SelectCommandBehaviorProperty, behavior);
            }
            //return the behavior
            //Caller function sets the command or command parameter properties
            return behavior;
        }
    }
}
