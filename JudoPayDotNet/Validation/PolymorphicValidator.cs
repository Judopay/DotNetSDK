using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;

namespace JudoPayDotNet.Validation
{
    public class PolymorphicValidator<TBaseClass> : NoopPropertyValidator where TBaseClass : class 
    {
        readonly Dictionary<Type, IValidator> _derivedValidators = new Dictionary<Type, IValidator>();
        readonly IValidator<TBaseClass> _baseValidator;

        public PolymorphicValidator(IValidator<TBaseClass> baseValidator) 
        {
            _baseValidator = baseValidator;
        }

        private IEnumerable<ValidationFailure> ActualValidation(TBaseClass value)
        {
            // bail out if the property is null or the collection is empty
            if (value == null) return Enumerable.Empty<ValidationFailure>();

            // get the first element out of the collection and check its real type. 
            var actualType = value.GetType();

            IValidator derivedValidator;

            if (_derivedValidators.TryGetValue(actualType, out derivedValidator))
            {
                // we found a validator for the specific subclass. 
                return derivedValidator.Validate(value).Errors;
            }

            // Otherwise fall back to the validator for the base class.
            return _baseValidator.Validate(value).Errors;
        } 


        public PolymorphicValidator<TBaseClass> Add<TDerived>(IValidator<TDerived> derivedValidator) where TDerived : TBaseClass 
        {
            _derivedValidators[typeof(TDerived)] = derivedValidator;
            return this;
        }

// ReSharper disable UnusedMethodReturnValue.Global
        public IEnumerable<ValidationFailure> Validate(TBaseClass value)
// ReSharper restore UnusedMethodReturnValue.Global
        {
            return ActualValidation(value);
        }

        public override IEnumerable<ValidationFailure> Validate(PropertyValidatorContext context) 
        {
            var value = context.PropertyValue as TBaseClass;

            return ActualValidation(value);
        }
    }

}
