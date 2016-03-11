namespace HW01_CustomerCRUD.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var db = new 客戶資料Entities();

            if (this.Id == 0)
            {
                //Create  同一個客戶下的聯絡人 : c.客戶Id == this.客戶Id
                if (db.客戶聯絡人.Where( c => c.客戶Id == this.客戶Id && c.Email == this.Email).Any())
                {
                    yield return new ValidationResult("Email 已存在", new string[] { "Email" });  //new string[] {"Email"} 讓Email報錯而不是顯示在上方
                }
            }
            else
            {
                //Update  c.Id != this.Id : 更新時要排除自己再去判斷
                if (db.客戶聯絡人.Where(c => c.客戶Id == this.客戶Id && c.Id != this.Id && c.Email == this.Email).Any())
                {
                    yield return new ValidationResult("Email 已存在", new string[] { "Email" });
                }
            }

            yield return ValidationResult.Success;
        }
    }
    
    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(30, ErrorMessage="欄位長度不得大於 30 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(10, ErrorMessage="欄位長度不得大於 10 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [StringLength(15, ErrorMessage="欄位長度不得大於 15 個字元")]
        [RegularExpression(@"\d{4}-\d{6}", ErrorMessage="手機號碼格式必須為：09XX-XXXXXX")]
        public string 手機 { get; set; }
        
        [StringLength(15, ErrorMessage="欄位長度不得大於 15 個字元")]
        public string 電話 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
