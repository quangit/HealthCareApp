using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Microsoft.Phone.Controls;
using HealthCare.Core.Resources;
using HealthCare.Core.ViewModels;

namespace HealthCare.Phone
{
    public class ViewToShow : DependencyObject
    {
        public string Type { get; set; }

        public Type View
        {
            get { return System.Type.GetType(Type); }
        }



        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ViewToShow), new PropertyMetadata(""));


    }

    public enum SideMenuState
    {
        MenuLeftOpened,
        RightMenuOpened,
        Normal,
    }
    public delegate void SideMenuStateChangedEventHandler(
              object sender, SideMenuStateChangedEventArgs e);
    public class SideMenuStateChangedEventArgs : EventArgs
    {
        public SideMenuState State { get; private set; }
        public SideMenuStateChangedEventArgs(SideMenuState state)
        {
            State = state;
        }
    }
    public partial class SideMenuPage
    {
        public event SideMenuStateChangedEventHandler StateChanged;
        private double DragDistanceToOpenLeft => Left / 2.5;

        private double DragDistanceNegativeLeft => Left / 2.5;

        private double DragDistanceToOpenRight => Right / 2.5;

        private double DragDistanceNegativeRight => Right / 2.5;

        public double Hidden => Left * -1;

        public double Total => Center + Left + Right;

        public double Left
        {
            get { return (double)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Left.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register("Left", typeof(double), typeof(SideMenuPage), new PropertyMetadata(420D, PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //(d as SideMenuPage).LeftSidebar.Margin = new Thickness(-(d as SideMenuPage).Left, 0, 0, 0);
            //(d as SideMenuPage).RightSidebar.Margin = new Thickness((d as SideMenuPage).Right + (d as SideMenuPage).Center, 0, 0, 0);
        }



        public double Right
        {
            get { return (double)GetValue(RightProperty); }
            set { SetValue(RightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Right.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightProperty =
            DependencyProperty.Register("Right", typeof(double), typeof(SideMenuPage), new PropertyMetadata(420D, PropertyChangedCallback));



        public double Center
        {
            get { return (double)GetValue(CenterProperty); }
            set { SetValue(CenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Center.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CenterProperty =
            DependencyProperty.Register("Center", typeof(double), typeof(SideMenuPage), new PropertyMetadata(480D));



        public bool IsLock
        {
            get { return (bool)GetValue(IsLockProperty); }
            set { SetValue(IsLockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsLock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLockProperty =
            DependencyProperty.Register("IsLock", typeof(bool), typeof(SideMenuPage), new PropertyMetadata(false));

        public object RightContent
        {
            get { return GetValue(RightContentProperty); }
            set { SetValue(RightContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightContentProperty =
            DependencyProperty.Register("RightContent", typeof(object), typeof(SideMenuPage), new PropertyMetadata(null));



        public object MainContent
        {
            get { return GetValue(MainContentProperty); }
            set { SetValue(MainContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MainContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainContentProperty =
            DependencyProperty.Register("MainContent", typeof(object), typeof(SideMenuPage), new PropertyMetadata(null,
                (o, args) =>
                {
                    var v = ((SideMenuPage)o).MainContent as FrameworkElement;
                    if (v != null)
                    {
                        v.DataContext = ((SideMenuPage)o).DataContext;
                    }
                }));



        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(SideMenuPage), new PropertyMetadata(""));



        private bool _leftOpen;

        public bool LeftOpen
        {
            get { return _leftOpen; }
            set
            {
                _leftOpen = value;
                if (_leftOpen)
                    OpenLeft();
                else
                    Reset();
            }
        }

        private bool _rightOpen;
        public bool RightOpen
        {
            get { return _rightOpen; }
            set
            {
                _rightOpen = value;
                if (_rightOpen)
                    OpenRight();
                else
                    Reset();
            }
        }
        public SideMenuPage()
        {
            InitializeComponent();

        }

        #region private methods, event handler


        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = Parent as FrameworkElement;
            if (element != null)
            {
                Center = element.ActualWidth;
            }
            LeftSidebar.Margin = new Thickness(-Left, 0, 0, 0);
            RightSidebar.Margin = new Thickness(Center, 0, 0, 0);
            LayoutRoot.Margin = new Thickness(0, 0, -Right, 0);
            Radio0.IsChecked = true;
            LeftSidebar.Visibility = Visibility.Visible;
            this.Loaded -= OnLoaded;

        }

        public event EventHandler ItemSelected;
        public int SelectedIndex { get; private set; }
        private void ChangeView(object sender, RoutedEventArgs e)
        {
            var r = sender as RadioButton;
            var v = r?.CommandParameter as ViewToShow;
            if (v != null)
            {
                try
                {
                    MainContent = Activator.CreateInstance(v.View);
                    this.Title = v.Title;
                }
                catch (Exception)
                {
                    //throw;
                }
            }
            if (r != null)
            {
                var c = int.Parse(r.Name.Last().ToString());
                SelectedIndex = c;
                ItemSelected?.Invoke(c, EventArgs.Empty);
            }

            this.Reset();
        }
        private void UnCheck(object sender, RoutedEventArgs e)
        {
            ((RadioButton)sender).IsChecked = false;
        }

        public RadioButton GetNavButton(int index)
        {
            return LeftSidebar.FindName("Radio" + index) as RadioButton;
        }
        #endregion

        public void OpenLeft()
        {

            var trans = LayoutRoot.GetHorizontalOffset().Transform;
            trans.Animate(trans.X, Left, TranslateTransform.XProperty, 300, 0, new CubicEase
            {
                EasingMode = EasingMode.EaseOut
            });

            _leftOpen = true;
            _rightOpen = false;
            //leftOpen = true;

            //MoveViewWindow(0);
            //if (StateChanged != null)
            //    StateChanged(this, new SideMenuStateChangedEventArgs(SideMenuState.MenuLeftOpened));

            //EasyTracker.GetTracker().SendView("Menu");
        }
        public void OpenRight()
        {
            var trans = LayoutRoot.GetHorizontalOffset().Transform;
            trans.Animate(trans.X, -Right, TranslateTransform.XProperty, 300, 0, new CubicEase
            {
                EasingMode = EasingMode.EaseOut
            });


            _rightOpen = true;

            //rightOpen = true;
            _leftOpen = false;
            //MoveViewWindow(-(Left + Right));
            StateChanged?.Invoke(this, new SideMenuStateChangedEventArgs(SideMenuState.RightMenuOpened));

            //EasyTracker.GetTracker().SendView("Form Search");
        }
        public void Reset()
        {
            if (_leftOpen || _rightOpen)
            {
                var trans = LayoutRoot.GetHorizontalOffset().Transform;
                trans.Animate(trans.X, 0, TranslateTransform.XProperty, 300, 0, new CubicEase
                {
                    EasingMode = EasingMode.EaseOut
                });

                _leftOpen = _rightOpen = false;

                //MoveViewWindow(-Left);

                StateChanged?.Invoke(this, new SideMenuStateChangedEventArgs(SideMenuState.Normal));
            }
        }


        private void ResetLayoutRoot()
        {
            if (!_leftOpen || !_rightOpen)
            {
                LayoutRoot.SetHorizontalOffset(0.0);
                _leftOpen = _rightOpen = false;
            }
            else
            {
                if (_leftOpen)
                    LayoutRoot.SetHorizontalOffset(Left);
                if (_rightOpen)
                    LayoutRoot.SetHorizontalOffset(-Right);
            }


        }
        private void GestureListener_OnDragCompleted(object sender, DragCompletedGestureEventArgs e)
        {
            if (IsLock) return;
            if (e.Direction == Orientation.Horizontal && e.HorizontalChange > 0)
                if (!_leftOpen && !_rightOpen)
                {
                    if (e.HorizontalChange < DragDistanceToOpenLeft)
                        ResetLayoutRoot();
                    else
                        OpenLeft();
                }
                else
                    if (_rightOpen)
                {
                    if (e.HorizontalChange > -DragDistanceNegativeRight)
                        ResetLayoutRoot();
                    else
                        Reset();
                }

            if (e.Direction == Orientation.Horizontal && e.HorizontalChange < 0)
                if (_leftOpen)
                {
                    if (e.HorizontalChange > DragDistanceNegativeLeft)
                        ResetLayoutRoot();
                    else
                        Reset();
                }
                else
                    if (!_rightOpen && !_leftOpen)
                {
                    if (e.HorizontalChange < -DragDistanceToOpenRight)
                        ResetLayoutRoot();
                    else
                        OpenRight();
                }
        }

        private void GestureListenerOnDragDelta(object sender, DragDeltaGestureEventArgs e)
        {
            if (IsLock) return;
            if (e.Direction == Orientation.Horizontal && e.HorizontalChange > 0)
                if (!_leftOpen && !_rightOpen)
                {
                    double offset = LayoutRoot.GetHorizontalOffset().Value + e.HorizontalChange;
                    if (offset > DragDistanceToOpenLeft)
                        OpenLeft();
                    else
                    {
                        LayoutRoot.SetHorizontalOffset(offset);
                    }
                }
                else
                    if (_rightOpen)
                {
                    double offsetContainer = LayoutRoot.GetHorizontalOffset().Value + e.HorizontalChange;
                    if (offsetContainer > -(DragDistanceToOpenRight))
                        Reset();
                    else
                    {
                        LayoutRoot.SetHorizontalOffset(offsetContainer);
                    }
                }

            if (e.Direction == Orientation.Horizontal && e.HorizontalChange < 0)
            {
                if (_leftOpen)
                {
                    double offsetContainer = LayoutRoot.GetHorizontalOffset().Value + e.HorizontalChange;
                    if (offsetContainer < DragDistanceNegativeLeft)
                        Reset();
                    else
                    {
                        LayoutRoot.SetHorizontalOffset(offsetContainer);
                    }
                }
                else
                    if (!_rightOpen && !_leftOpen)
                {
                    double offset = LayoutRoot.GetHorizontalOffset().Value + e.HorizontalChange;
                    if (offset < -(DragDistanceToOpenRight))
                        OpenRight();
                    else
                    {
                        LayoutRoot.SetHorizontalOffset(offset);
                    }
                }
            }

        }

        public void Select(int i)
        {
            switch (i)
            {
                case 0:
                    Radio0.IsChecked = true;
                    break;
                case 1:
                    Radio1.IsChecked = true;
                    break;
                case 2:
                    Radio2.IsChecked = true;
                    break;
                case 3:
                    Radio3.IsChecked = true;
                    break;

            }
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mr = MessageBox.Show(AppResources.Logout_contentMessageBox, AppResources.MainPage_logoutAppBar, MessageBoxButton.OKCancel);

            if (mr == MessageBoxResult.OK)
            {
                ((HomeViewModel)DataContext).DoLogoutTap();
                //LoginView.SocialLogout();
            }
        }


    }


    public static class Extensions
    {
        public static void SetHorizontalOffset(this FrameworkElement fe, double offset)
        {
            var translateTransform = fe.RenderTransform as TranslateTransform;
            if (translateTransform == null)
            {
                // create a new transform if one is not alreayd present
                var trans = new TranslateTransform()
                {
                    X = offset
                };
                fe.RenderTransform = trans;
            }
            else
            {
                translateTransform.X = offset;
            }
        }

        public static Offset GetHorizontalOffset(this FrameworkElement fe)
        {
            var trans = fe.RenderTransform as TranslateTransform;
            if (trans == null)
            {
                // create a new transform if one is not alreayd present
                trans = new TranslateTransform()
                {
                    X = 0
                };
                fe.RenderTransform = trans;
            }
            return new Offset()
            {
                Transform = trans,
                Value = trans.X
            };
        }

        public static void Animate(this DependencyObject target, double from, double to, object propertyPath, int duration, int startTime, IEasingFunction easing = null, Action completed = null)
        {
            if (easing == null)
                easing = new SineEase();

            var db = new DoubleAnimation
            {
                To = to,
                From = @from,
                EasingFunction = easing,
                Duration = TimeSpan.FromMilliseconds(duration)
            };

            Storyboard.SetTarget(db, target);
            Storyboard.SetTargetProperty(db, new PropertyPath(propertyPath));

            var sb = new Storyboard { BeginTime = TimeSpan.FromMilliseconds(startTime) };

            if (completed != null)
            {
                sb.Completed += (s, e) => completed();
            }

            sb.Children.Add(db);
            sb.Begin();
        }
    }

    public struct Offset
    {
        public double Value { get; set; }
        public TranslateTransform Transform { get; set; }
    }
}
