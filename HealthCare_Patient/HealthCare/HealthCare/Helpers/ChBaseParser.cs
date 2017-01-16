using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using HealthCare.Models.ChBaseModel;
using Newtonsoft.Json;

namespace HealthCare.Helpers
{
    public class ChBaseParser
    {
        private string GetThingId(string data)
        {
            try
            {
                XDocument doc = XDocument.Parse(data);
                var temp = doc.Descendants("thing-id");
                return temp.FirstOrDefault().ToString(SaveOptions.DisableFormatting);
            }
            catch (Exception ex)
            {

            }
            return "";
        }

        private string GetData(string data, string node)
        {
            try
            {
                XDocument doc = XDocument.Parse(data);
                var temp = doc.Descendants(node);

                var xElements = temp as XElement[] ?? temp.ToArray();
                XElement first = xElements.FirstOrDefault();
                if (first == null)
                {
                    return "";
                }

                return xElements.FirstOrDefault().Value;
            }
            catch (Exception ex)
            {

            }
            return "";
        }

        private string GetData(string data, string parent, string child)
        {
            try
            {
                XDocument doc = XDocument.Parse(data);
                var parentNode = doc.Descendants(parent).FirstOrDefault();
                var childNode = parentNode.Descendants(child).FirstOrDefault();
                return childNode.Value;

            }
            catch (Exception ex)
            {

            }
            return "";
        }
        private XElement GetXElement(string data, string node)
        {
            XDocument doc = XDocument.Parse(data);
            var temp = doc.Descendants(node);
            return temp.FirstOrDefault();
        }

        private XElement GetXElement(string data, string parent, string child)
        {
            XDocument doc = XDocument.Parse(data);
            var parentNode = doc.Descendants(parent).FirstOrDefault();
            var childNode = parentNode.Descendants(child).FirstOrDefault();
            return childNode;
        }
        private List<XElement> GetDatas(string data, string node)
        {
            try
            {
                XDocument doc = XDocument.Parse(data);
                var temp = doc.Descendants(node);
                return temp.ToList();
            }
            catch (Exception ex)
            {

            }
            return new List<XElement>();
        }
        public string GetAuthToken(string xml)
        {
            ChBaseHelper.SharedSecret = GetSharedScrept(xml);
            return GetData(xml, "token");
        }
        public string GetSharedScrept(string xml)
        {
            return GetData(xml, "shared-secret");
        }

        public PersonModel GetUserInfo(string xml)
        {
            PersonModel p = new PersonModel();
            p.Id = GetData(xml, "person-id");
            p.Name = GetData(xml, "name");
            p.RecordId = GetData(xml, "selected-record-id");
            ChBaseHelper.RecordId = p.RecordId;
            return p;
        }

        public ObservableCollection<HeightModel> ParseHeights(string xml)
        {
            var heights = new ObservableCollection<HeightModel>();
            foreach (var item in GetDatas(xml, "thing"))
            {
                HeightModel h = new HeightModel
                {
                    Id = GetThingId(item.ToString(SaveOptions.DisableFormatting)), 
                    Value = GetData(item.ToString(SaveOptions.DisableFormatting), "value") + " m",
                    When = GetDate(GetXElement(item.ToString(SaveOptions.DisableFormatting), "date"))
                };
                heights.Add(h);
            }
            return heights;
        }

        public ObservableCollection<WeightModel> ParseWeights(string xml)
        {
            var weights = new ObservableCollection<WeightModel>();
            foreach (var item in GetDatas(xml, "thing"))
            {
                WeightModel h = new WeightModel
                {
                    Id = GetThingId(item.ToString(SaveOptions.DisableFormatting)),
                    Value = GetData(item.ToString(SaveOptions.DisableFormatting), "value") + " kg",
                    When = GetDate(GetXElement(item.ToString(SaveOptions.DisableFormatting), "date"))
                };
                weights.Add(h);

            }
            return weights;
        }

        public ObservableCollection<ProcedureModel> ParseProcedure(string xml)
        {
            var procedures = new ObservableCollection<ProcedureModel>();
            foreach (var item in GetDatas(xml, "thing"))
            {
                ProcedureModel p = new ProcedureModel
                {
                    Id = GetThingId(item.ToString(SaveOptions.DisableFormatting)),
                    Name = GetData(item.ToString(SaveOptions.DisableFormatting), "name"),
                    Location = GetData(item.ToString(SaveOptions.DisableFormatting), "anatomic-location"),
                    PrimaryProvider = new ProviderModel()
                    {
                        Name = GetData(item.ToString(SaveOptions.DisableFormatting), "primary-provider", "name"),
                        Type = GetData(item.ToString(SaveOptions.DisableFormatting), "primary-provider", "type")
                    },
                    SecondaryProvider = new ProviderModel()
                    {
                        Name = GetData(item.ToString(SaveOptions.DisableFormatting), "secondary-provider", "name"),
                        Type = GetData(item.ToString(SaveOptions.DisableFormatting), "secondary-provider", "type")
                    },
                    When = GetDate(GetXElement(item.ToString(SaveOptions.DisableFormatting), "date"))
                };
                procedures.Add(p);
            }
            return procedures;
        }

        public ObservableCollection<MedicationModel> ParseMedication(string xml)
        {
            //List<T> tempArr = new List<T>();
            ////tempArr = Func<business logic> 
            //return new ObservableCollection<MedicationModel>(tempArr);


                var medication = new ObservableCollection<MedicationModel>();
            foreach (var item in GetDatas(xml, "thing"))
            {
                MedicationModel m = new MedicationModel
                {
                    Id = GetThingId(item.ToString(SaveOptions.DisableFormatting)),
                    Name = GetData(item.ToString(SaveOptions.DisableFormatting), "name"),
                    Dose = GetData(item.ToString(SaveOptions.DisableFormatting), "dose"),
                    Strength = GetData(item.ToString(SaveOptions.DisableFormatting), "strength"),
                    Indication = GetData(item.ToString(SaveOptions.DisableFormatting), "indication"),
                    HowOftenTaken = GetData(item.ToString(SaveOptions.DisableFormatting), "frequency"),
                    DateStarted = GetDate(GetXElement(item.ToString(SaveOptions.DisableFormatting), "date-started", "date")),
                    DateDiscontinued = GetDate(GetXElement(item.ToString(SaveOptions.DisableFormatting), "date-discontinued", "date")),
                    GenericName = GetData(item.ToString(SaveOptions.DisableFormatting), "generic-name"),
                    HowTaken = GetData(item.ToString(SaveOptions.DisableFormatting), "route"),
                    Prescribed = GetData(item.ToString(SaveOptions.DisableFormatting), "prescribed"),
                    PrescribedBy = GetData(item.ToString(SaveOptions.DisableFormatting), "prescribed-by"),
                };
                medication.Add(m);
            }
            return medication;
        }

        public string ParseUrlImage(string data)
        {
            try
            {
                var definition = new {Message = ""};
                var obj = JsonConvert.DeserializeAnonymousType(data, definition);
                return obj.Message;
            }
            catch
            {
                return "";
            }
        }

        public ObservableCollection<ImmunizationModel> ParseImmunization(string xml)
        {
            var immunization = new ObservableCollection<ImmunizationModel>();
            foreach (var item in GetDatas(xml, "thing"))
            {
                ImmunizationModel i = new ImmunizationModel
                {
                    Id = GetThingId(item.ToString(SaveOptions.DisableFormatting)),
                    Name = GetData(item.ToString(SaveOptions.DisableFormatting), "name"),
                    DateAdministrated = GetDate(GetXElement(item.ToString(SaveOptions.DisableFormatting), "administration-date", "date")),
                    Administrator = GetData(item.ToString(SaveOptions.DisableFormatting), "administrator", "name"),
                    Manufacturer = GetData(item.ToString(SaveOptions.DisableFormatting), "manufacturer"),
                    Lot = GetData(item.ToString(SaveOptions.DisableFormatting), "lot"),
                    Route = GetData(item.ToString(SaveOptions.DisableFormatting), "route"),
                    ExpirationDate = GetDate(GetXElement(item.ToString(SaveOptions.DisableFormatting), "expiration-date")),
                    Sequence = GetData(item.ToString(SaveOptions.DisableFormatting), "sequence"),
                    AnatomicSurface = GetData(item.ToString(SaveOptions.DisableFormatting), "anatomic-surface"),
                    Consent = GetData(item.ToString(SaveOptions.DisableFormatting), "consent"),
                    AdverseEvent = GetData(item.ToString(SaveOptions.DisableFormatting), "adverse-event"),
                };
                immunization.Add(i);
            }
            return immunization;
        }

        public ObservableCollection<BloodGlucoseModel> ParseBloodGlucose(string xml)
        {
            var bloodGlucose = new ObservableCollection<BloodGlucoseModel>();
            foreach (var item in GetDatas(xml, "thing"))
            {
                BloodGlucoseModel b = new BloodGlucoseModel
                {
                    Id = GetThingId(item.ToString(SaveOptions.DisableFormatting)),
                    Value = GetData(item.ToString(SaveOptions.DisableFormatting), "value"),
                    Type = GetData(item.ToString(SaveOptions.DisableFormatting), "glucose-measurement-type"),
                    OutsideOperatingTemp = bool.Parse(GetData(item.ToString(SaveOptions.DisableFormatting), "outside-operating-temp")),
                    IsControlTest = bool.Parse(GetData(item.ToString(SaveOptions.DisableFormatting), "is-control-test")),
                    Normalcy = GetData(item.ToString(SaveOptions.DisableFormatting), "normalcy"),
                    MeasurementContext = GetData(item.ToString(SaveOptions.DisableFormatting), "measurement-context"),
                    When = GetDate(GetXElement(item.ToString(SaveOptions.DisableFormatting), "date"))
                };
                bloodGlucose.Add(b);
            }
            return bloodGlucose;
        }

        public ObservableCollection<BloodPressureModel> ParseBloodPressure(string xml)
        {
            var bloodPressure = new ObservableCollection<BloodPressureModel>();
            foreach (var item in GetDatas(xml, "thing"))
            {
                BloodPressureModel b = new BloodPressureModel
                {
                    Id = GetThingId(item.ToString(SaveOptions.DisableFormatting)),
                    Systolic = GetData(item.ToString(SaveOptions.DisableFormatting), "systolic"),
                    Diastolic = GetData(item.ToString(SaveOptions.DisableFormatting), "diastolic"),
                    IrregularHeartbeat = bool.Parse(GetData(item.ToString(SaveOptions.DisableFormatting), "irregular-heartbeat")),
                    Pulse = GetData(item.ToString(SaveOptions.DisableFormatting), "pulse"),
                    When = GetDate(GetXElement(item.ToString(SaveOptions.DisableFormatting), "date"))
                };
                bloodPressure.Add(b);
            }
            return bloodPressure;
        }
        public bool IsSuccess(string data)
        {
            return GetData(data, "status") == "0" ;
        }
        private DateTime GetDate(XElement element)
        {
            return new DateTime(int.Parse(element.Descendants("y").FirstOrDefault().Value), int.Parse(element.Descendants("m").FirstOrDefault().Value), int.Parse(element.Descendants("d").FirstOrDefault().Value));
            //return element.Descendants("d").FirstOrDefault().Value + "/" +
            //       element.Descendants("m").FirstOrDefault().Value + "/" +
            //       element.Descendants("y").FirstOrDefault().Value;
        }
    }
}
