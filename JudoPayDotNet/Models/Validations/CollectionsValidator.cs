﻿using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
    public class CollectionsValidator : AbstractValidator<CollectionModel>
    {
        public CollectionsValidator()
        {
            RuleFor(model => model.ReceiptId)
                .NotEmpty().WithMessage("You must supply the receipt id of pre authorization transaction");

            RuleFor(model => model.Amount)
                .NotEmpty().WithMessage("You must supply the amount to collect");

            RuleFor(model => model.YourPaymentReference)
                .NotEmpty().WithMessage("You must supply your payment reference");
        }
    }
}