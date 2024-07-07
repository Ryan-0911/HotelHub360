using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.CustomValidator
{
    public class DateGreaterThanValidationAttribute: ValidationAttribute
    {
        private readonly string _comparisonPropertyName;

        // 建構子，接受一個比較屬性的名稱
        public DateGreaterThanValidationAttribute(string comparisonPropertyName)
        {
            _comparisonPropertyName = comparisonPropertyName;
            // 設定預設的錯誤訊息
            ErrorMessage = "The date must be greater than the comparison date.";
        }

        // 覆寫 IsValid 方法進行自定義的驗證邏輯
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // 將傳入的值轉換為 DateTime? 型別
            var currentDate = value as DateTime?;

            // 取得要比較的屬性資訊 
            var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonPropertyName);

            // 取得要比較的屬性的值並轉換為 DateTime? 型別
            var comparisonDate = comparisonProperty?.GetValue(validationContext.ObjectInstance, null) as DateTime?;

            // 檢查 currentDate 是否大於 comparisonDate
            if (currentDate.HasValue && comparisonDate.HasValue && currentDate.Value <= comparisonDate.Value)
            {
                // 如果 currentDate 不大於 comparisonDate，則返回錯誤訊息
                return new ValidationResult(ErrorMessage);
            }

            // 如果驗證通過，返回成功
            return ValidationResult.Success;
        }
    }
}
