using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCare.Conveters.JsonConverters;
using Newtonsoft.Json;

namespace HealthCare.Models
{
   public class SupportAnswerModel:Entity
    {
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }


        [JsonProperty(PropertyName = "firstName")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "question")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public SupportQuestionModel Question { get; set; }


        [JsonProperty(PropertyName = "responses")]
        public ObservableCollection<SupportResponsesModel> Responses { get; set; }
    }

    public class SupportResponsesModel
    {
        [JsonProperty(PropertyName = "id")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string Id { get; set; }


        [JsonProperty(PropertyName = "supportRequestId")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string SupportRequestId { get; set; }



        [JsonProperty(PropertyName = "comment")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string Comment { get; set; }

        [JsonProperty(PropertyName = "status")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public int Status { get; set; }


        [JsonProperty(PropertyName = "whenCreated")]
        [JsonConverter(typeof(DateTimeConverterAttribute))]
        public DateTime WhenCreated { get; set; }

        [JsonProperty(PropertyName = "createdByUserId")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public string CreatedByUserId { get; set; }

        [JsonProperty(PropertyName = "doctor")]
        [JsonConverter(typeof(NullConverterAttribute))]
        public DoctorModel Doctor { get; set; }
    }
}
