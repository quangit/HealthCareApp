using System.Reflection;
using HealthCare.Enums;
using HealthCare.Helpers;
using HealthCare.Models;
using Xamarin.Forms;
using System;
using HealthCare.Conveters.JsonConverters;
using HealthCare.ModelApis;
using HealthCare.Resx;
using Newtonsoft.Json;

namespace HealthCare.Objects
{
    public class ProxyCheckupModel : CheckupModel, IApiModel<CheckupModel>
    {
        public ProxyCheckupModel()
        {
        }

        public ProxyCheckupModel(CheckupModel baseObject)
        {
            var fieldsOfBaseClass = baseObject.GetType().GetRuntimeProperties();
            var fieldsOfDerivedClass = GetType().GetRuntimeProperties();
            foreach (var fieldOfBaseClass in fieldsOfBaseClass)
            {
                foreach (var fieldOfDerivedClass in fieldsOfDerivedClass)
                {
                    if (fieldOfDerivedClass.Name.Equals(fieldOfBaseClass.Name) && fieldOfDerivedClass.CanWrite)
                        fieldOfDerivedClass.SetValue(this, fieldOfBaseClass.GetValue(baseObject));
                }
            }
        }

        public string UserAsString => FullName + " - " + UserCode;

        public double TotalFee => SchedulingFee + CheckupFee;

        public Color ViewColor
        {
            get
            {
                switch (Status)
                {
                    case CheckupStatus.Accepted:
                        return IsPaid
                            ? HcStyles.CheckupAcceptedColor
                            : HcStyles.CheckupPaymentPendingColor;
                    case CheckupStatus.Pending:
                    case CheckupStatus.Rejected:
                    case CheckupStatus.Deleted:
                        return HcStyles.CheckupPendingColor;
                    case CheckupStatus.Finished:
                        return HcStyles.CheckupFinishColor;
                    default:
                        return Color.Black;
                }
            }
        }

        /// <summary>
        ///     Visibility for Delete button
        /// </summary>
        public bool IsDeleteable
        {
            get
            {
                if (Status != CheckupStatus.Finished)
                {
                    if (Status == CheckupStatus.Accepted)
                    {
                        //Nếu màu vàng thì luôn hiện nút xoá, khi quá hạn khám -> API tự xoá phiếu vàng đó
                        if (IsPaid)
                            return Schedule.StartDateTime.ToLocalTimeZone().AddHours(-24).
                                CompareTo(DateTime.Now) > 0;
                        else return true;
                    }

                    return true;
                }
                return false;
            }
        }

        //Status == CheckupStatus.Rejected || Status == CheckupStatus.Pending;

        /// <summary>
        ///     Visibility for Edit button
        /// </summary> 
        public bool IsEditable
        {
            get
            {
                if (Status == CheckupStatus.Pending) return true;

                if (Status == CheckupStatus.Accepted && IsPaid)
                {
                    return Schedule.StartDateTime.ToLocalTimeZone().AddHours(-4).CompareTo(DateTime.Now) > 0;
                }

                return IsPayable;
                //return false;
            }
        }


        /// <summary>
        ///     Visibility for Payment button
        /// </summary>
        public bool IsPayable => Status == CheckupStatus.Accepted && !IsPaid;
        public ImageSource TickImageSource
        {
            get
            {
                var imgPath = Status == CheckupStatus.Accepted && IsPaid ? "ic_tick_accepted.png" : null;

                return string.IsNullOrWhiteSpace(imgPath) ? null : string.IsNullOrWhiteSpace(imgPath)
                    ? null
                    : new FileImageSource
                    {
                        File = imgPath
                    };
            }
        }

        public bool IsShowResultBlock => !string.IsNullOrWhiteSpace(Result) && Status == CheckupStatus.Finished;
        public bool IsShowButtonBlock => IsPayable || IsDeleteable || IsEditable;
        public bool IsShowButtonBlockReverse => !IsShowButtonBlock && Common.OS == TargetPlatform.WinPhone;

        /// <summary>
        ///     Visibility for top block (Name, UserCode, Ticket image) on Finished Checkup
        /// </summary>
        public bool IsFinishedCheckup => Status == CheckupStatus.Finished;

        public string ScheduleTimeString
        {
            get
            {
                var startTime = Schedule.StartDateTime.ToLocalTimeZone();
                var endTime = Schedule.EndDateTime.ToLocalTimeZone();
                return $"{startTime.ToString("HH:mm")} - {endTime.ToString("HH:mm")} {endTime.ToString("d")}";
            }
        }

        public bool CanViewHospitalDetail => Schedule.Hospital.Status && !Schedule.Hospital.IsCLASHospital;

        public bool IsRated => Rating > 0;

        public bool IsShowExamPlaceRoom => !string.IsNullOrWhiteSpace(Room?.Name);

        public string DeleteString => Status != CheckupStatus.Accepted ? AppResources.delete : AppResources.cancel;

        public string AppointmentNoString => string.Format(AppResources.appointment_no, AppointmentNo);

        public string StatusString
        {
            get
            {
                switch (Status)
                {
                    case CheckupStatus.Accepted:
                        return IsPaid ? AppResources.checkup_status_accepted : AppResources.checkup_status_payment_pending;
                    case CheckupStatus.Pending:
                        return AppResources.checkup_status_pending;
                    case CheckupStatus.Rejected:
                        return AppResources.checkup_status_cancelled;
                    default:
                        return "";
                }
            }
        }

        public CheckupModel ToBaseModel()
        {
            return this;
        }

        public void RaiseIsRatedPropertyChanged()
        {
            RaisePropertyChanged("IsRated");
        }
    }
}