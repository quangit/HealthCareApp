﻿#pragma checksum "D:\SVN\HealthcareProject\HealthCare_Doctor\HealthCare\Phase 2\HealthCare.Win\Controls\NumberPicker.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C53FA0E5C3093EC2D176B0E31DE3232F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HealthCare.Win.Controls
{
    partial class NumberPicker : 
        global::Windows.UI.Xaml.Controls.UserControl, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        internal class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_TextBox_Text(global::Windows.UI.Xaml.Controls.TextBox obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
        };

        private class NumberPicker_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            INumberPicker_Bindings
        {
            private global::HealthCare.Win.Controls.NumberPicker dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.TextBox obj3;

            private NumberPicker_obj1_BindingsTracking bindingsTracking;

            public NumberPicker_obj1_Bindings()
            {
                this.bindingsTracking = new NumberPicker_obj1_BindingsTracking(this);
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 3:
                        this.obj3 = (global::Windows.UI.Xaml.Controls.TextBox)target;
                        (this.obj3).LostFocus += (global::System.Object sender, global::Windows.UI.Xaml.RoutedEventArgs e) =>
                            {
                                if (this.initialized)
                                {
                                    // Update Two Way binding
                                    this.dataRoot.Value = (global::System.Int32) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Int32), (this.obj3).Text);
                                }
                            };
                        break;
                    default:
                        break;
                }
            }

            // INumberPicker_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.initialized = false;
            }

            // NumberPicker_obj1_Bindings

            public void SetDataRoot(global::HealthCare.Win.Controls.NumberPicker newDataRoot)
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.dataRoot = newDataRoot;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::HealthCare.Win.Controls.NumberPicker obj, int phase)
            {
                this.bindingsTracking.UpdateChildListeners_(obj);
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_Value(obj.Value, phase);
                    }
                }
            }
            private void Update_Value(global::System.Int32 obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBox_Text(this.obj3, obj.ToString(), null);
                }
            }

            private class NumberPicker_obj1_BindingsTracking
            {
                global::System.WeakReference<NumberPicker_obj1_Bindings> WeakRefToBindingObj; 

                public NumberPicker_obj1_BindingsTracking(NumberPicker_obj1_Bindings obj)
                {
                    WeakRefToBindingObj = new global::System.WeakReference<NumberPicker_obj1_Bindings>(obj);
                }

                public void ReleaseAllListeners()
                {
                    UpdateChildListeners_(null);
                }

                public void DependencyPropertyChanged_Value(global::Windows.UI.Xaml.DependencyObject sender, global::Windows.UI.Xaml.DependencyProperty prop)
                {
                    NumberPicker_obj1_Bindings bindings;
                    if(WeakRefToBindingObj.TryGetTarget(out bindings))
                    {
                        global::HealthCare.Win.Controls.NumberPicker obj = sender as global::HealthCare.Win.Controls.NumberPicker;
        if (obj != null)
        {
            bindings.Update_Value(obj.Value, DATA_CHANGED);
        }
                    }
                }
                private long tokenDPC_Value = 0;
                public void UpdateChildListeners_(global::HealthCare.Win.Controls.NumberPicker obj)
                {
                    NumberPicker_obj1_Bindings bindings;
                    if(WeakRefToBindingObj.TryGetTarget(out bindings))
                    {
                        if (bindings.dataRoot != null)
                        {
                            bindings.dataRoot.UnregisterPropertyChangedCallback(global::HealthCare.Win.Controls.NumberPicker.ValueProperty, tokenDPC_Value);
                        }
                        if (obj != null)
                        {
                            bindings.dataRoot = obj;
                            tokenDPC_Value = obj.RegisterPropertyChangedCallback(global::HealthCare.Win.Controls.NumberPicker.ValueProperty, DependencyPropertyChanged_Value);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2:
                {
                    global::Windows.UI.Xaml.Controls.Border element2 = (global::Windows.UI.Xaml.Controls.Border)(target);
                    #line 25 "..\..\..\Controls\NumberPicker.xaml"
                    ((global::Windows.UI.Xaml.Controls.Border)element2).Tapped += this.Reduce_Tapped;
                    #line default
                }
                break;
            case 4:
                {
                    global::Windows.UI.Xaml.Controls.Border element4 = (global::Windows.UI.Xaml.Controls.Border)(target);
                    #line 45 "..\..\..\Controls\NumberPicker.xaml"
                    ((global::Windows.UI.Xaml.Controls.Border)element4).Tapped += this.Increase_Tapped;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1:
                {
                    global::Windows.UI.Xaml.Controls.UserControl element1 = (global::Windows.UI.Xaml.Controls.UserControl)target;
                    NumberPicker_obj1_Bindings bindings = new NumberPicker_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            }
            return returnValue;
        }
    }
}

