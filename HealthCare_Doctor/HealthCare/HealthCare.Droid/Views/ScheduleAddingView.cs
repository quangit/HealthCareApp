using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Widget;
using HealthCare.Core.Models.Enums;
using HealthCare.Core.Resources;
using HealthCare.Core.ViewModels;
using HealthCare.Droid.Controls;
using HealthCare.Droid.Utilities;
using Android.Graphics;
using Java.Lang.Reflect;
using Java.Lang;

namespace HealthCare.Droid.Views
{
    [Activity(Label = "")]
    public class ScheduleAddingView : MvxActionBarActivity, TimePickerDialog.IOnTimeSetListener
    {
        private ScheduleAddingViewModel _vm;
        //private TimePickerFragment dialog = null;
        //private TimePickerFragment dialog2 = null;
        private TextView _startTime;
        private TextView _endTime;
        private TextView _dateText;
        private TextView _weekCountText;
        private TextView _weekCountLabel;

        protected override int LayoutResource
        {
            get { return Resource.Layout.ScheduleAddingView; }
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetIcon(Resource.Drawable.logo_bs);

            SupportActionBar.SetBackgroundDrawable(new ColorDrawable(Resources.GetColor(Resource.Color.ButtonGreenMainColor)));
            SupportActionBar.Title = AppResources.ScheduleAdding_Title;
            var itmMyPakaze = menu.Add("Refresh")
                .SetIcon(Resource.Drawable.refresh);
            itmMyPakaze.SetShowAsAction(ShowAsAction.Always);
            itmMyPakaze.SetOnMenuItemClickListener(new OnMenuItemClickAction((e) =>
            {
                var scheduleAddingViewModel = this.ViewModel as ScheduleAddingViewModel;
                if (scheduleAddingViewModel != null)
                    scheduleAddingViewModel.RefreshCommand.Execute();
            }));
            return base.OnCreateOptionsMenu(menu);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            _vm = ViewModel as ScheduleAddingViewModel;
            _startTime = FindViewById<TextView>(Resource.Id.startTime);
            _endTime = FindViewById<TextView>(Resource.Id.endTime);
            _dateText = FindViewById<TextView>(Resource.Id.dateText);
            _weekCountText = FindViewById<TextView>(Resource.Id.weekCount);
            _weekCountLabel = FindViewById<TextView>(Resource.Id.weekCountLabel);

            _startTime.Click += StarttimeOnClick;
            _endTime.Click += EndtimeOnClick;
            _weekCountText.Click +=
                (s, e) =>
                {
                    PickNumberWeek();
                };
            _weekCountText.FocusChange += (s, e) => { PickNumberWeek(); };
            _startTime.FocusChange += (sender, args) =>
            {
                if (args.HasFocus)
                    ShowStarTimePicker();
            };
            _endTime.FocusChange += (sender, args) =>
            {
                if (args.HasFocus)
                    ShowEndTimePicker();
            };


            if (_vm != null)
            {
                if (_vm.StartTime != null) _startTime.Text = _vm.StartTime.Value.ToString("hh:mm");
                if (_vm.EndTime != null) _endTime.Text = _vm.EndTime.Value.ToString("hh:mm");
                _dateText.Text = _vm.CurrentDate.ToString("d");


                _dateText.Click += delegate
                {
                    MyDatePickerDialog d = new MyDatePickerDialog(date =>
                    {
                        _vm.CurrentDate = date;
                        _dateText.Text = date.ToString("d");
                    });

                    d.Show(this.FragmentManager, null);
                };
                var spinnerSelect = FindViewById<MultiSelectSpinner>(Resource.Id.repeatMultipleWeekDaysSpinner);

                spinnerSelect.OnDismissed += (s, e) =>
                {
                    List<string> selected = spinnerSelect.GetSelectedStrings();
                    List<DoctorDayOfWeek> list = new List<DoctorDayOfWeek>();
                    if (selected.Count > 0)
                    {
                        //dateText.Enabled = false;
                        list.AddRange(selected.Select(item => _vm.DaySource.FirstOrDefault(x => x.ToString().Equals(item))).Select(day => day.Value));
                        _vm.DayOfWeeks = list.ToArray();
                        _weekCountLabel.Visibility = _weekCountText.Visibility = ViewStates.Visible;
                        //dateTextLabel.Visibility = dateText.Visibility = ViewStates.Gone;
                    }
                    else
                    {
                        _weekCountLabel.Visibility = _weekCountText.Visibility = ViewStates.Gone;
                        //dateTextLabel.Visibility = dateText.Visibility = ViewStates.Visible;
                    }
                };

                spinnerSelect.SetItems(_vm.DaySource.Select(x => x.ToString()).ToArray());

                //List<String> selected = spinner_select.GetSelectedStrings();


                var upButton = FindViewById<Button>(Resource.Id.UpButton);
                var downButton = FindViewById<Button>(Resource.Id.DownButton);
                var qualityEditText = FindViewById<EditText>(Resource.Id.qualityEditText);
                _vm.Quality = 1;
                qualityEditText.Clickable = false;
                qualityEditText.Focusable = false;
                upButton.Click += (sender, args) =>
                {
                    int value = int.Parse(qualityEditText.Text);
                    //if (value >= 20)
                    //    return;
                    value += 1;
                    qualityEditText.Text = value.ToString();
                };

                downButton.Click += (sender, args) =>
                {
                    int value = int.Parse(qualityEditText.Text);
                    if (value <= 1)
                        return;
                    value -= 1;
                    qualityEditText.Text = value.ToString();
                };
            }
        }

        private void PickNumberWeek()
        {
            NumberPickerDialogFragment r = new NumberPickerDialogFragment(this, 1, _vm.Weeks.Max(), _vm.WeekCount,
                i => _vm.WeekCount = i);
            r.Show(FragmentManager, null);
        }


        private void StarttimeOnClick(object sender, EventArgs eventArgs)
        {
            ShowStarTimePicker();
        }

        private void EndtimeOnClick(object sender, EventArgs eventArgs)
        {
            ShowEndTimePicker();
        }



        public int Cur;
        public void ShowStarTimePicker()
        {
            var vm = ViewModel as ScheduleAddingViewModel;


            if (vm != null)
            {
                if (vm.StartTime != null)
                {
                    var dialog = new TimePickerFragment(this, vm.StartTime.Value.Hour, vm.StartTime.Value.Minute, this);


                    Cur = 0;
                    //dialog = new TimePickerDialog(Application.Context, this, DateTime.Now.Hour, DateTime.Now.Minute, true);
                    //dialog = new TimePickerDialog();
                    dialog.Show(FragmentManager, null);
                }
            }
        }

        public void ShowEndTimePicker()
        {
            var vm = ViewModel as ScheduleAddingViewModel;

            if (vm != null)
            {
                if (vm.EndTime != null)
                {
                    var dialog2 = new TimePickerFragment(this, vm.EndTime.Value.Hour, vm.EndTime.Value.Minute, this);

                    Cur = 1;
                    //dialog = new TimePickerDialog(Application.Context, this, DateTime.Now.Hour, DateTime.Now.Minute, true);
                    //dialog = new TimePickerDialog();
                    dialog2.Show(FragmentManager, null);
                }
            }
        }

        public void OnTimeSet(TimePicker view, int hourOfDay, int minute)
        {
            var time = new TimeSpan(hourOfDay, minute, 0);
            if (Cur == 0)
            {
                _startTime.Text = time.ToString(@"hh\:mm");
                _vm.StartTime = new DateTime(1970, 1, 1, hourOfDay, minute, 0);
            }
            else
            {
                _endTime.Text = time.ToString(@"hh\:mm");
                _vm.EndTime = new DateTime(1970, 1, 1, hourOfDay, minute, 0);
            }
        }
    }

    public class TimePickerFragment : DialogFragment
    {
        private readonly Context _context;
        private int _hour;
        private int _minute;
        private readonly TimePickerDialog.IOnTimeSetListener _listener;
        public TimePickerFragment(Context context, int hour, int minute, TimePickerDialog.IOnTimeSetListener listener)
        {
            _context = context;
            _hour = hour;
            _minute = minute;
            _listener = listener;
        }

        public override Dialog OnCreateDialog(Bundle savedState)
        {
            var dialog = new TimePickerDialog(_context, _listener, _hour, _minute, false);
            return dialog;
        }
    }
    public class NumberPickerDialogFragment : DialogFragment, NumberPicker.IOnValueChangeListener
    {
        private readonly Context _context;
        private int _min, _max, _current;
        private Action<int> _selected;
        public NumberPickerDialogFragment(Context context, int min, int max, int current, Action<int> doneAction)
        {
            _context = context;
            _min = min;
            _max = max;
            _current = current;
            _selected = doneAction;
        }

        public override Dialog OnCreateDialog(Bundle savedState)
        {
            var inflater = (LayoutInflater)_context.GetSystemService(Context.LayoutInflaterService);
            var view = inflater.Inflate(Resource.Layout.NumberPickerDialog, null);
            var numberPicker = view.FindViewById<NumberPicker>(Resource.Id.numberPicker);
            numberPicker.MaxValue = _max;
            numberPicker.MinValue = _min;
            numberPicker.Value = _current;
            numberPicker.SetOnValueChangedListener(this);
            SetNumberPickerTextColor(numberPicker);
            var dialog = new AlertDialog.Builder(_context);
            dialog.SetTitle(AppResources.ScheduleAdding_WeekCount);
            dialog.SetView(view);
            dialog.SetNegativeButton(AppResources.Message_Cancel, (s, a) => { });
            dialog.SetPositiveButton(AppResources.Message_OK, (s, a) =>
            {
                if (_selected != null)
                    _selected(_current);
            });
            return dialog.Create();
        }

        public void OnValueChange(NumberPicker picker, int oldVal, int newVal)
        {
            _current = newVal;
        }

        public static bool SetNumberPickerTextColor(NumberPicker numberPicker)
        {
            int count = numberPicker.ChildCount;
            for (int i = 0; i < count; i++)
            {
                View child = numberPicker.GetChildAt(i);
                if (child is EditText)
                {
                    try
                    {
                        Field selectorWheelPaintField = numberPicker.Class.GetDeclaredField("mSelectorWheelPaint");
                        selectorWheelPaintField.Accessible = true;
                        ((Paint)selectorWheelPaintField.Get(numberPicker)).Color = Color.Black;
                        ((EditText)child).SetTextColor(Color.Black);
                        numberPicker.Invalidate();
                        return true;
                    }
                    catch (NoSuchFieldException)
                    {
                        //Debug.("setNumberPickerTextColor", e);
                    }
                    catch (IllegalAccessException)
                    {
                        //Log.w("setNumberPickerTextColor", e);
                    }
                    catch (IllegalArgumentException)
                    {
                        //Log.w("setNumberPickerTextColor", e);
                    }
                }
            }
            return false;
        }


    }


}