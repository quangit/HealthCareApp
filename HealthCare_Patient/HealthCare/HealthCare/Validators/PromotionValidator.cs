using HealthCare.Models;

namespace HealthCare.Validators
{
    public class PromotionValidator : AbstractValidator<PromotionModel>
    {
        public override ValidationResult Validate(PromotionModel data)
        {
            Result.Reset();
            Check(data.Content, ValidNotEmpty, "Nội dung khuyến mãi không được rỗng");
            Check(data.Name, ValidNotEmpty, "Tên khuyến mãi không được rỗng");
            Check(data.Hospital, ValidNotNull, "Bệnh viện không tồn tại");
            return Result;
        }
    }
}