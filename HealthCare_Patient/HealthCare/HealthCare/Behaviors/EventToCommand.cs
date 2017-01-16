﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace HealthCare.Behaviors
{
    /// <summary>
    ///     Invoked a command when an event raises
    /// </summary>
    public class EventToCommand : Behavior
    {
        public static readonly BindableProperty EventNameProperty =
            BindableProperty.Create<EventToCommand, string>(p => p.EventName, null);

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create<EventToCommand, ICommand>(p => p.Command, null);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create<EventToCommand, object>(p => p.CommandParameter, null);

        public static readonly BindableProperty CommandNameProperty =
            BindableProperty.Create<EventToCommand, string>(p => p.CommandName, null);

        public static readonly BindableProperty CommandNameContextProperty =
            BindableProperty.Create<EventToCommand, object>(p => p.CommandNameContext, null);

        private EventInfo eventInfo;
        private Delegate handler;

        /// <summary>
        ///     Gets or sets the name of the event to subscribe
        /// </summary>
        /// <value>
        ///     The name of the event.
        /// </value>
        public string EventName
        {
            get { return (string) GetValue(EventNameProperty); }
            set { SetValue(EventNameProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the command to invoke when event raised
        /// </summary>
        /// <value>
        ///     The command.
        /// </value>
        public ICommand Command
        {
            get { return (ICommand) GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the optional command parameter.
        /// </summary>
        /// <value>
        ///     The command parameter.
        /// </value>
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the name of the relative command.
        /// </summary>
        /// <value>
        ///     The name of the command.
        /// </value>
        public string CommandName
        {
            get { return (string) GetValue(CommandNameProperty); }
            set { SetValue(CommandNameProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the relative context used with command name.
        /// </summary>
        /// <value>
        ///     The command name context.
        /// </value>
        public object CommandNameContext
        {
            get { return GetValue(CommandNameContextProperty); }
            set { SetValue(CommandNameContextProperty, value); }
        }

        protected override void OnAttach()
        {
            var events = AssociatedObject.GetType().GetRuntimeEvents();
            if (events.Any())
            {
                eventInfo = events.FirstOrDefault(e => e.Name == EventName);
                if (eventInfo == null)
                    throw new ArgumentException(
                        string.Format("EventToCommand: Can't find any event named '{0}' on attached type"));
                AddEventHandler(eventInfo, AssociatedObject, OnFired);
            }
        }

        protected override void OnDetach()
        {
            if (handler != null) eventInfo.RemoveEventHandler(AssociatedObject, handler);
        }

        /// <summary>
        ///     Subscribes the event handler.
        /// </summary>
        /// <param name="eventInfo">The event information.</param>
        /// <param name="item">The item.</param>
        /// <param name="action">The action.</param>
        private void AddEventHandler(EventInfo eventInfo, object item, Action action)
        {
            //Got inspiration from here: http://stackoverflow.com/questions/9753366/subscribing-an-action-to-any-event-type-via-reflection
            //Maybe it is possible to pass Event arguments as CommanParameter

            var mi = eventInfo.EventHandlerType.GetRuntimeMethods().First(rtm => rtm.Name == "Invoke");
            var parameters = mi.GetParameters().Select(p => Expression.Parameter(p.ParameterType)).ToList();
            var actionMethodInfo = action.GetMethodInfo();
            Expression exp = Expression.Call(Expression.Constant(this), actionMethodInfo, null);
            handler = Expression.Lambda(eventInfo.EventHandlerType, exp, parameters).Compile();
            eventInfo.AddEventHandler(item, handler);
        }

        /// <summary>
        ///     Called when subscribed event fires
        /// </summary>
        private void OnFired()
        {
            if (!string.IsNullOrEmpty(CommandName))
            {
                if (Command == null) CreateRelativeBinding();
            }

            if (Command == null)
                throw new InvalidOperationException(
                    string.Format("No command available for Event: {0}, Is Command properly properly set up?", EventName));

            if (Command.CanExecute(CommandParameter))
            {
                Command.Execute(CommandParameter);
            }
        }

        /// <summary>
        ///     Cretes a binding between relative context and provided Command name
        /// </summary>
        private void CreateRelativeBinding()
        {
            if (CommandNameContext == null)
                throw new ArgumentNullException(
                    "CommandNameContext property cannot be null when using CommandName property, consider using CommandNameContext={b:RelativeContext [ElementName]} markup markup extension.");
            if (Command != null)
                throw new InvalidOperationException(
                    "Both Command and CommandName properties specified, only one mode supported.");

            var pi = CommandNameContext.GetType().GetRuntimeProperty(CommandName);
            if (pi == null)
                throw new ArgumentNullException(string.Format("Can't find a command named '{0}'", CommandName));
            Command = pi.GetValue(CommandNameContext) as ICommand;
            if (Command == null)
                throw new ArgumentNullException(string.Format("Can't create binding with CommandName '{0}'", CommandName));
        }
    }
}