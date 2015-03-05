using System;
using System.ComponentModel.DataAnnotations;

namespace DataModel
{
    [AttributeUsage(AttributeTargets.Property)]
    public class StockLevelValidationAttribute : ValidationAttribute
    {
        private readonly DbModel _context = new DbModel();
        private readonly bool _displayName;
        private readonly string _other;

        public StockLevelValidationAttribute(string other, bool dispayProductName = true)
        {
            _other = other;
            _displayName = dispayProductName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_other);
            if (property == null)
            {
                return new ValidationResult(
                    string.Format("Unknown property: {0}", _other)
                    );
            }

            var productId = property.GetValue(validationContext.ObjectInstance, null);
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                return new ValidationResult("Something went wrong");
            }

            // Prepare message
            var errorMessage = "";
            if (_displayName)
            {
                errorMessage += product.Name + " - ";
            }

            if (!(value is int))
            {
                errorMessage += "Quantity must be numeric";
                return new ValidationResult(errorMessage);
            }

            if (product.StockTotal < (int) value)
            {
                errorMessage += product.StockTotal + " items left in stock";

                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
