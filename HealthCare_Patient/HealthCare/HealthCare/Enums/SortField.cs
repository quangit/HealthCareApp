namespace HealthCare.Enums
{
    public enum SortField
    {
        /**
        * PATIENT
        */
        [Description("")] None,
        [Description("whenCreated")] PatientCreatedDateTime,
        [Description("firstName")] PatientFirstName,
        [Description("lastName")] PatientLastName,
        [Description("email")] PatientEmail,
        [Description("address")] PatientAddress,
        [Description("phone")] PatientPhone,
        [Description("userName")] PatientUsername,

        /**
        * PROMOTION
        */
        [Description("name")] PromotionName,
        [Description("content")] PromotionContent,
        [Description("discountPercent")] PromotionDiscountPercent,
        [Description("startDate")] PromotionStartDate,
        [Description("endDate")] PromotionEndDatew
    }
}